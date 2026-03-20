Push-Location "$PSScriptRoot\frontend"
if (-not (Test-Path "node_modules")) {
    Write-Host "Installing frontend dependencies..."
    npm install
}
Pop-Location

dotnet run --project "$PSScriptRoot\aspire\HotelSite.AppHost" --launch-profile https
