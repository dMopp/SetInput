# SetInput

SetInput is a small, lightweight command-line tool to control monitor input sources via DDC/CI VCP commands on Windows.

The project is aimed at users who want to automatically switch monitor inputs – for example, when using a USB switch or KVM setup.

---

## Features

- Sends VCP codes to monitors (e.g., switch between DisplayPort and HDMI)
- Supports Windows 10 and Windows 11
- Extremely small file (~100 KB for Single-File, ~3 MB for Self-Contained)
- Two build variants provided:  
  - **SetInput_Single.exe** (requires .NET 8 Runtime)  
  - **SetInput_SelfContained.exe** (runs without any runtime)

---

## Usage

Command line example:

```bash
SetInput.exe 60 15
```

- **60** = VCP code for “Input Source Select”
- **15** = DisplayPort input
- **17** = HDMI 1 input
- **18** = HDMI 2 input

---

## Typical Input Values

| Input | Value |
|:--|:--|
| DisplayPort 1 | 15 |
| HDMI 1 | 17 |
| HDMI 2 | 18 |

---

## Requirements

- Windows 10 or Windows 11
- .NET 8 Runtime installed (only for **SetInput_Single.exe**)

---

## Build Instructions

Requirements:

- .NET 8 SDK
- PowerShell or Terminal

Build manually:

```bash
dotnet build -c Release
```

Or use the automated script (builds both variants):

```bash
.\publish.ps1
```

---

## Project Structure

| File/Folder | Description |
|:--|:--|
| `SetInput.csproj` | .NET project file |
| `Program.cs` | Main program (source code) |
| `publish.ps1` | Build script for both EXE variants |
| `publish/` | Output folder for built files |
| `README.md`, `README.en.md` | Documentation |
| `LICENSE` | License file (MIT)

---

## License

This project is released under the **MIT License**.  
See [LICENSE](LICENSE) for details.

---

## Credits

- Idea & implementation: [Your Name or Organization]
- DDC/CI low-level access via Windows dxva2.dll