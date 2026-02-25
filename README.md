# 🏨 StayBright Hotels — Demo

An end-to-end hotel booking demo showcasing a functional website with **OpenAPI-driven APIs**, **MCP (Model Context Protocol)** support, and an **integrated agentic chat experience** powered by the **Microsoft Agent Framework**.

## Architecture

| Component | Technology | Port |
|-----------|-----------|------|
| **Backend API** | ASP.NET Core 8, OpenAPI, MCP Server, AG-UI | `http://localhost:5000` |
| **Frontend** | React + Vite + Tailwind CSS | `http://localhost:5173` |
| **Console Agent** | .NET Console, MAF + MCP Client | CLI |

## Features

- 🔍 **Hotel Search** — Browse and filter 10 curated hotels worldwide
- 🏨 **Hotel Details** — View rooms, amenities, pricing, and photos
- 📝 **Booking Flow** — Book rooms with real-time confirmation
- 👤 **Customer Zone** — View and cancel bookings by email
- 🤖 **Integrated Chat** — AI assistant in the website (AG-UI protocol, feature-toggled)
- 🔧 **MCP Server** — Exposes hotel tools via SSE for external agents
- 🖥️ **Console Agent** — Standalone CLI agent using MCP tools
- 📄 **OpenAPI** — Full Swagger docs at `/openapi/v1.json`

## Quick Start

### Prerequisites
- .NET 8+ SDK
- Node.js 18+
- Azure OpenAI endpoint (for AI chat features)

### 1. Start the Backend

```bash
cd backend/HotelBooking.Api

# Optional: set Azure OpenAI for AI chat
export AZURE_OPENAI_ENDPOINT="https://your-resource.openai.azure.com/"
export AZURE_OPENAI_DEPLOYMENT_NAME="gpt-4o-mini"

dotnet run
```

The API runs at `http://localhost:5000`. Browse the OpenAPI spec at `http://localhost:5000/openapi/v1.json`.

### 2. Start the Frontend

```bash
cd frontend
npm install
npm run dev
```

Open `http://localhost:5173` to use the hotel booking website.

### 3. Run the Console Agent

```bash
cd console-agent/ConsoleAgent

# Required for the console agent
export AZURE_OPENAI_ENDPOINT="https://your-resource.openai.azure.com/"
export AZURE_OPENAI_DEPLOYMENT_NAME="gpt-4o-mini"

dotnet run
```

The console agent connects to the MCP server at `http://localhost:5000/mcp` and provides an interactive chat experience.

## Feature Toggle

The chat widget is controlled by a backend feature toggle:

```bash
# Check current state
curl http://localhost:5000/api/config/features

# Disable chat
curl -X PUT "http://localhost:5000/api/config/features/chat?enabled=false"

# Enable chat
curl -X PUT "http://localhost:5000/api/config/features/chat?enabled=true"
```

## MCP Tools

The MCP server at `/mcp` exposes these tools:

| Tool | Description |
|------|-------------|
| `SearchHotels` | Search hotels by city, country, stars, price, guests |
| `GetHotelDetails` | Get full hotel details including rooms |
| `GetAvailableRooms` | List available rooms for a hotel |
| `CreateBooking` | Book a room |
| `GetBooking` | Look up a booking by ID |
| `ListBookings` | List bookings by email |
| `CancelBooking` | Cancel a booking |

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/hotels` | Search hotels (query params: city, country, minStars, maxPrice, guests) |
| GET | `/api/hotels/{id}` | Get hotel details |
| GET | `/api/hotels/{id}/rooms` | Get available rooms |
| POST | `/api/bookings` | Create a booking |
| GET | `/api/bookings/{id}` | Get booking by ID |
| GET | `/api/bookings/by-email/{email}` | List bookings by email |
| DELETE | `/api/bookings/{id}` | Cancel a booking |
| GET | `/api/config/features` | Get feature toggles |
| PUT | `/api/config/features/chat?enabled=bool` | Toggle chat |

## Key Technologies

- **Microsoft Agent Framework** — Powers the AI agents (both web chat and console)
- **AG-UI Protocol** — Streams agent responses to the web chat widget via SSE
- **Model Context Protocol (MCP)** — Exposes hotel tools for external agent consumption
- **OpenAPI** — Full API documentation and specification

## Deploy to Azure

The app deploys as a **single Azure App Service** (backend API + frontend SPA).

### Prerequisites
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli) installed and logged in (`az login`)
- An Azure OpenAI resource with a `gpt-4o` deployment

### Option 1: PowerShell Script

```powershell
./deploy.ps1 `
    -ResourceGroup "rg-staybright" `
    -Location "westeurope" `
    -AppName "staybright-demo" `
    -AzureOpenAiEndpoint "https://your-resource.openai.azure.com/" `
    -AzureOpenAiApiKey "your-api-key"
```

For code-only updates (skip infrastructure):
```powershell
./deploy.ps1 -ResourceGroup "rg-staybright" -Location "westeurope" -AppName "staybright-demo" -SkipInfra
```

### Option 2: Manual Bicep + Deploy

```bash
# 1. Create resource group
az group create --name rg-staybright --location westeurope

# 2. Deploy infrastructure
az deployment group create \
    --resource-group rg-staybright \
    --template-file infra/main.bicep \
    --parameters appName=staybright-demo \
                 azureOpenAiEndpoint="https://..." \
                 azureOpenAiApiKey="your-key"

# 3. Build & deploy code
cd frontend && npm ci && npx vite build && cd ..
cp -r frontend/dist backend/HotelBooking.Api/wwwroot
dotnet publish backend/HotelBooking.Api -c Release -o ./publish
cd publish && zip -r ../app.zip . && cd ..
az webapp deploy --resource-group rg-staybright --name staybright-demo-app --src-path app.zip --type zip
```

### Option 3: GitHub Actions

Configure these repository settings:

**Secrets:**
| Secret | Description |
|--------|-------------|
| `AZURE_CLIENT_ID` | Service principal application (client) ID |
| `AZURE_CLIENT_SECRET` | Service principal client secret |
| `AZURE_TENANT_ID` | Azure AD tenant ID |
| `AZURE_SUBSCRIPTION_ID` | Azure subscription ID |
| `AZURE_OPENAI_ENDPOINT` | Azure OpenAI endpoint URL |
| `AZURE_OPENAI_API_KEY` | Azure OpenAI API key |

**Variables:**
| Variable | Description |
|----------|-------------|
| `AZURE_RESOURCE_GROUP` | Target resource group name |
| `APP_NAME` | Base name for resources (must be globally unique) |
| `AZURE_LOCATION` | Azure region (default: `westeurope`) |
| `AZURE_OPENAI_DEPLOYMENT_NAME` | Model deployment name (default: `gpt-4o`) |

Push to `main` or trigger the workflow manually to deploy.
