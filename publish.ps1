# publish.ps1
# Build script to produce both Single-File and Self-Contained EXEs

Write-Host "🔵 Building SetInput.exe (Release, .NET 8)..."
Write-Host ""

# Projektpfad bestimmen
$projectDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $projectDir

# Ausgabeordner vorbereiten
$outputDir = "$projectDir\publish"

if (Test-Path $outputDir) {
    Remove-Item -Recurse -Force -Path $outputDir
}
New-Item -ItemType Directory -Path $outputDir | Out-Null

# Single-File bauen (benötigt .NET 8)
Write-Host "🟢 Building Single-File executable (needs installed .NET runtime)..."
dotnet publish -c Release -r win-x64 --self-contained false /p:PublishSingleFile=true -o "$outputDir\Single"
Rename-Item -Path "$outputDir\Single\SetInput.exe" -NewName "SetInput_Single.exe"

# Self-Contained bauen (läuft überall)
Write-Host "🟠 Building Self-Contained executable (no runtime needed)..."
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true -o "$outputDir\SelfContained"
Rename-Item -Path "$outputDir\SelfContained\SetInput.exe" -NewName "SetInput_SelfContained.exe"

Write-Host ""
Write-Host "✅ Build completed!"
Write-Host "📦 Output Files:"
Write-Host "   ➡️  publish/Single/SetInput_Single.exe"
Write-Host "   ➡️  publish/SelfContained/SetInput_SelfContained.exe"
