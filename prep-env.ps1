Write-Host "Installing prerequisites via winget..."
winget import -i "$PSScriptRoot\prerequisites.json" --accept-package-agreements --accept-source-agreements

Write-Host ""
Write-Host "Installing GitHub Copilot CLI extension..."
gh extension install github/gh-copilot 2>$null
gh extension upgrade github/gh-copilot 2>$null

Write-Host ""
Write-Host "Installing GitHub Copilot CLI plugins..."
gh copilot plugin install workiq@copilot-plugins 
gh copilot plugin install microsoftdocs/mcp

Write-Host ""
Write-Host "Done. You may need to restart your terminal for PATH changes to take effect."
