# Vibe-Coding Hackathon

Welcome! This is a **vibe-coding hackathon**. Use **VS Code with GitHub Copilot** or **Copilot on the command line** (`copilot`, or `copilot --banner` for the awesome splash screen) to add features to this hotel booking app. Let the AI do the heavy lifting — describe what you want, iterate, and ship.

## 1. Install Prerequisites

Run the setup script in a terminal to install everything you need:

```powershell
.\prep-env.ps1
```

This uses `winget` to install:
- .NET 10 SDK
- Node.js LTS
- Git
- GitHub CLI + Copilot CLI extension
- Azure CLI
- VS Code

> Restart your terminal after installation for PATH changes to take effect.

## 2. Authenticate with GitHub Copilot

### VS Code

1. Open VS Code
2. Click the **Accounts** icon in the bottom-left sidebar
3. Select **Sign in with GitHub to use GitHub Copilot**
4. Follow the browser flow to authorize your GitHub account
5. Once signed in, the Copilot icon appears in the status bar — you're ready to go

### Command Line

1. First authenticate with the GitHub CLI:
   ```powershell
   gh auth login
   ```
2. Follow the prompts (select GitHub.com, HTTPS, and authenticate via browser)
3. Then just run `copilot` (or `copilot --banner`) — it uses your `gh` auth automatically

## 3. Configure Azure OpenAI

You'll receive an **endpoint** and **API key** from the organizers. Add them to:

```
aspire\HotelSite.AppHost\appsettings.json
```

Fill in the `AzureOpenAI` section:

```json
{
  "AzureOpenAI": {
    "Endpoint": "https://<your-endpoint>.openai.azure.com",
    "ApiKey": "<your-api-key>",
    "DeploymentName": "gpt-4o"
  }
}
```

> This config is shared by the backend API and the console agent automatically.

## 4. Run the App

From the repo root, run:

```powershell
.\run.ps1
```

(or `.\run.cmd` if you prefer cmd)

This installs frontend dependencies (first run only) and starts the full app via .NET Aspire.

### Access the app

Once running, you'll see a line like this in the terminal:

```
Login to the dashboard at https://localhost:17171/login?t=...
```

Open that URL to view the **Aspire dashboard**. From there you can see all running services and their endpoints. Click the frontend endpoint to open the hotel booking website at **http://localhost:5173**.

## 5. Run the Console Agent

While the app is running (keep that terminal open), open a **new terminal** and run:

```powershell
cd console-agent\ConsoleAgent
dotnet run
```

This starts an interactive chat agent that connects to the backend MCP server. You can search hotels, make bookings, and more — all through natural language.

## 6. Build Features

The `feature-ideas/` folder has ready-made feature specs you can pick up:

| File | Feature |
|------|---------|
| `01-guest-reviews-and-ratings.md` | Let guests leave reviews and ratings after their stay |
| `02-booking-modification-and-upgrades.md` | Modify bookings and offer room upgrades |
| `03-favorites-and-trip-planning.md` | Save hotels to favorites and build trip plans |

Each file describes the backend, frontend, and data changes needed.

### How to use them

1. Open a feature idea file
2. Ask Copilot to implement it (e.g. *"implement the feature described in this file"*)
3. Iterate and refine

### Want something different?

Ask Copilot for new ideas! For example:

- *"Suggest a feature that adds a loyalty points system"*
- *"What would a hotel comparison page look like?"*
- *"Add email confirmation when a booking is made"*
- *"Add a map view showing all hotels"*

The whole point is to vibe — describe it, let Copilot build it, and see what happens. Have fun!
