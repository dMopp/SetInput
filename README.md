# SetInput

SetInput ist ein kleines, leichtes Kommandozeilen-Tool, um Eingangsquellen (Input Source) von Monitoren über DDC/CI VCP-Kommandos unter Windows zu steuern.

Das Projekt richtet sich an Nutzer, die Eingänge automatisch wechseln möchten – zum Beispiel in Kombination mit einem USB-Switch oder KVM-Setup.

---

## Funktionen

- Setzt VCP-Codes auf Monitore (z. B. Eingangsumschaltung zwischen DisplayPort und HDMI)
- Unterstützung für Windows 10 und Windows 11
- Extrem kleine Datei (~100 KB für Single-File, ~3 MB für Self-Contained)
- Zwei bereitgestellte Versionen:  
  - **SetInput_Single.exe** (benötigt .NET 8 Runtime)  
  - **SetInput_SelfContained.exe** (läuft ohne Runtime)

---

## Anwendung

Kommandozeile Beispiel:

```bash
SetInput.exe 60 15
```

- **60** = VCP-Code für „Input Source Select“
- **15** = DisplayPort Eingang
- **17** = HDMI 1 Eingang
- **18** = HDMI 2 Eingang

---

## Typische Eingabewerte

| Eingang | Wert |
|:--|:--|
| DisplayPort 1 | 15 |
| HDMI 1 | 17 |
| HDMI 2 | 18 |

---

## Voraussetzungen

- Windows 10 oder Windows 11
- .NET 8 Runtime installiert (nur für **SetInput_Single.exe**)

---

## Build-Anleitung

Voraussetzungen:

- .NET 8 SDK
- PowerShell oder Terminal

Kommando:

```bash
dotnet build -c Release
```

Alternativ vollautomatisiert (Single und SelfContained):

```bash
.\publish.ps1
```

---

## Projektstruktur

| Datei/Ordner | Beschreibung |
|:--|:--|
| `SetInput.csproj` | .NET-Projektdatei |
| `Program.cs` | Hauptprogramm (Quellcode) |
| `publish.ps1` | Build-Skript für beide EXE-Varianten |
| `publish/` | Ausgabeordner für fertige Builds |
| `README.md`, `README.en.md` | Dokumentation |
| `LICENSE` | Lizenzdatei (MIT)

---

## Lizenz

Dieses Projekt ist unter der **MIT-Lizenz** veröffentlicht.  
Details siehe [LICENSE](LICENSE).

---

## Credits

- Idee & Umsetzung: [Dein Name oder Organisation]
- DDC/CI Low-Level Zugriff via Windows dxva2.dll

