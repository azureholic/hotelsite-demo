<#
.SYNOPSIS
    Deploy StayBright Hotels to Azure.

.DESCRIPTION
    Builds the frontend and backend, then deploys infrastructure (Bicep)
    and application code to Azure App Service.

.PARAMETER ResourceGroup
    Name of the Azure resource group (will be created if it doesn't exist).

.PARAMETER Location
    Azure region (e.g., westeurope, eastus).

.PARAMETER AppName
    Base name for Azure resources. Must be globally unique.

.PARAMETER AzureOpenAiEndpoint
    Azure OpenAI service endpoint URL.

.PARAMETER AzureOpenAiApiKey
    Azure OpenAI API key.

.PARAMETER AzureOpenAiDeploymentName
    Model deployment name (default: gpt-4o).

.PARAMETER SkipInfra
    Skip Bicep deployment (useful for code-only updates).

.EXAMPLE
    .\deploy.ps1 -ResourceGroup rg-staybright -Location westeurope -AppName staybright-demo `
        -AzureOpenAiEndpoint "https://my-oai.openai.azure.com/" -AzureOpenAiApiKey "sk-..."
#>

param(
    [Parameter(Mandatory)] [string] $ResourceGroup,
    [Parameter(Mandatory)] [string] $Location,
    [Parameter(Mandatory)] [string] $AppName,
    [string] $AzureOpenAiEndpoint = "",
    [string] $AzureOpenAiApiKey = "",
    [string] $AzureOpenAiDeploymentName = "gpt-4o",
    [string] $AppServicePlanSku = "B1",
    [switch] $SkipInfra
)

$ErrorActionPreference = "Stop"
$root = $PSScriptRoot

Write-Host "`n🏨 StayBright Hotels — Azure Deployment" -ForegroundColor Cyan
Write-Host "========================================`n"

# ── Step 1: Build Frontend ──
Write-Host "📦 Building frontend..." -ForegroundColor Yellow
Push-Location "$root/frontend"
npm ci --silent
npx vite build
Pop-Location

# ── Step 2: Copy frontend build to backend wwwroot ──
Write-Host "📂 Copying frontend to backend wwwroot..." -ForegroundColor Yellow
$wwwroot = "$root/backend/HotelBooking.Api/wwwroot"
if (Test-Path $wwwroot) { Remove-Item $wwwroot -Recurse -Force }
Copy-Item "$root/frontend/dist" $wwwroot -Recurse

# ── Step 3: Publish backend ──
Write-Host "🔨 Publishing backend..." -ForegroundColor Yellow
$publishDir = "$root/.publish"
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
dotnet publish "$root/backend/HotelBooking.Api/HotelBooking.Api.csproj" `
    -c Release -o $publishDir --nologo -v quiet

# ── Step 4: Deploy Infrastructure (Bicep) ──
if (-not $SkipInfra) {
    Write-Host "🏗️  Deploying infrastructure..." -ForegroundColor Yellow

    # Create resource group if needed
    $rgExists = az group exists --name $ResourceGroup 2>$null
    if ($rgExists -eq "false") {
        Write-Host "   Creating resource group '$ResourceGroup' in '$Location'..."
        az group create --name $ResourceGroup --location $Location --output none
    }

    # Deploy Bicep
    $deployResult = az deployment group create `
        --resource-group $ResourceGroup `
        --template-file "$root/infra/main.bicep" `
        --parameters appName=$AppName `
                     appServicePlanSku=$AppServicePlanSku `
                     azureOpenAiEndpoint=$AzureOpenAiEndpoint `
                     azureOpenAiApiKey=$AzureOpenAiApiKey `
                     azureOpenAiDeploymentName=$AzureOpenAiDeploymentName `
        --output json | ConvertFrom-Json

    $appUrl = $deployResult.properties.outputs.appServiceUrl.value
    Write-Host "   Infrastructure deployed!" -ForegroundColor Green
}

# ── Step 5: Deploy Application Code ──
Write-Host "🚀 Deploying application code..." -ForegroundColor Yellow
$zipPath = "$root/.publish.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath }
Compress-Archive -Path "$publishDir/*" -DestinationPath $zipPath

az webapp deploy `
    --resource-group $ResourceGroup `
    --name "$AppName-app" `
    --src-path $zipPath `
    --type zip `
    --output none

# ── Step 6: Clean up ──
Remove-Item $publishDir -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item $zipPath -Force -ErrorAction SilentlyContinue

# ── Done ──
$appUrl = "https://$AppName-app.azurewebsites.net"
Write-Host "`n✅ Deployment complete!" -ForegroundColor Green
Write-Host "   🌐 Website:    $appUrl" -ForegroundColor Cyan
Write-Host "   📄 OpenAPI:    $appUrl/openapi/v1.json" -ForegroundColor Cyan
Write-Host "   🔧 MCP Server: $appUrl/mcp" -ForegroundColor Cyan
Write-Host "   🤖 AG-UI Chat: $appUrl/agent-chat" -ForegroundColor Cyan
Write-Host ""
