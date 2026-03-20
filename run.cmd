@echo off

if not exist "%~dp0frontend\node_modules" (
    echo Installing frontend dependencies...
    pushd "%~dp0frontend"
    call npm install
    popd
)

dotnet run --project "%~dp0aspire\HotelSite.AppHost" --launch-profile https
