@echo off
echo Installing prerequisites via winget...
winget import -i "%~dp0prerequisites.json" --accept-package-agreements --accept-source-agreements

echo.
echo Installing GitHub Copilot CLI extension...
gh extension install github/gh-copilot 2>nul
gh extension upgrade github/gh-copilot 2>nul

echo.
echo Installing GitHub Copilot CLI plugins...
gh copilot plugin install workiq@copilot-plugins 
gh copilot plugin install microsoftdocs/mcp

echo.
echo Done. You may need to restart your terminal for PATH changes to take effect.
