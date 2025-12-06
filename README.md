# Project Name

This repository contains several C# scripts for log processing and other tasks.

## Scripts

| Script | Description | Documentation |
|--------|-------------|---------------|
| `Log2Json.csx` | Converts log files to JSON | [README](./README_Log2Json.md) |
| `LogUpload.csx` | Uploads log files openobserve | [README](./README_LogUpload.md) |

> Each script has its own README with usage instructions, configuration, and examples.

## Requirements

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) or higher  
- `dotnet-script` tool to run `.csx` files

---

## Installing dotnet-script

`dotnet-script` is a global tool for running C# scripts (`.csx`) without compiling a full project.

1. Open a terminal/command prompt.  
2. Install the tool:

```bash
dotnet tool install -g dotnet-script
```

## Running the script

### Option 1: From command line

```bash
dotnet-script Log2Json.csx
```

### Option 2: Double-click in Windows

1. Associate `.csx` files with `dotnet-script.exe`:

   - Right-click any `.csx` file → **Open with** → **Choose another app** → **More apps** → **Look for another app on this PC**  
   - Navigate to your `dotnet-script` executable (usually in `C:\Users\<YourUser>\.dotnet\tools\dotnet-script.exe`)  
   - Check **Always use this app to open .csx files**  

2. Now you can double-click any `.csx` script to run it. The console window will stay open until a key is pressed.
