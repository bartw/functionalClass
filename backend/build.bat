@echo off
cls

if not exist .paket\paket.exe (
    @echo "Installing Paket"

    .paket\paket.bootstrapper.exe
    if errorlevel 1 (
        exit /b %errorlevel%
    )
)

if not exist paket.lock (
    @echo "Installing dependencies"
    .paket\paket.exe install
) else (
    @echo "Restoring dependencies"
    .paket\paket.exe update
)

@echo "Build app"
packages\FAKE\tools\FAKE.exe build.fsx