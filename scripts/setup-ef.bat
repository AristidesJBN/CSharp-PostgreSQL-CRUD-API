@echo off
setlocal

echo Running EF Core setup for EscolaAPI...

:: Ensure running from project folder (script should be executed inside the project root)
:: Usage: open cmd in project folder and run scripts\setup-ef.bat

echo Removing unused SQL Server package (if present)...
dotnet remove package Microsoft.EntityFrameworkCore.SqlServer

echo Adding/updating EF Core and Npgsql packages (10.0.8)...
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.8
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 10.0.8
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 10.0.8
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 10.0.8
dotnet add package Microsoft.AspNetCore.OpenApi --version 10.0.8

echo Installing/updating dotnet-ef global tool (10.0.8)...
dotnet tool uninstall --global dotnet-ef >nul 2>&1 || echo dotnet-ef not previously installed
dotnet tool install --global dotnet-ef --version 10.0.8

echo Clearing NuGet caches...
dotnet nuget locals all --clear

echo Restoring project...
dotnet restore

rem Use provided migration name or default to InitialCreate
set MIGRATION_NAME=InitialCreate
if not "%~1"=="" set MIGRATION_NAME=%~1

echo Adding migration: %MIGRATION_NAME% ...
dotnet ef migrations add %MIGRATION_NAME%
if errorlevel 1 (
  echo Migration command failed. See output above.
  pause
  exit /b 1
)

echo Updating database...
dotnet ef database update
if errorlevel 1 (
  echo Database update failed. See output above.
  pause
  exit /b 1
)

echo All done.
pause
endlocal
