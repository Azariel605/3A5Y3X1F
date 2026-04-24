# Release APK Manager - Organize your Android builds
# Usage: .\Release-APK.ps1 -Version v1.1

param([string]$Version = "v1.0")

$projectRoot = "c:\Users\mikol\Documents\fonz\Git\3a5y3x1f"
$apkSource = Join-Path $projectRoot "e45y3x1f.mobile\bin\Release\net8.0-android\com.easyexif.mobile-Signed.apk"
$releasesDir = Join-Path $projectRoot "Releases"

Write-Host ""
Write-Host "Easy Exif Mobile - Release Manager"
Write-Host ""

if (-not (Test-Path $apkSource)) {
    Write-Host "ERROR: APK not found at $apkSource"
    Write-Host "Run: cd e45y3x1f.mobile && dotnet build -c Release -f net8.0-android"
    exit 1
}

$apkFile = Get-Item $apkSource
$timestamp = $apkFile.LastWriteTime.ToString("yyyy-MM-dd_HHmmss")
$sizeInMB = [math]::Round($apkFile.Length / 1MB, 2)

Write-Host "Packaging APK - Version: $Version | Size: $sizeInMB MB"
Write-Host ""

if (-not (Test-Path $releasesDir)) {
    New-Item -ItemType Directory -Path $releasesDir | Out-Null
}

$versionedName = "com.easyexif.mobile-$Version`_$timestamp.apk"
$versionedPath = Join-Path $releasesDir $versionedName
Copy-Item $apkSource $versionedPath -Force

$latestPath = Join-Path $releasesDir "com.easyexif.mobile-latest.apk"
Copy-Item $apkSource $latestPath -Force

Write-Host "Releases:"
Get-ChildItem -Path $releasesDir -Filter "*.apk" | ForEach-Object {
    $sz = [math]::Round($_.Length / 1MB, 2)
    Write-Host "  * $($_.Name) ($sz MB)"
}

Write-Host ""
Write-Host "Done! Use com.easyexif.mobile-latest.apk for distribution"
Write-Host ""
