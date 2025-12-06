# Log2Json.csx

## Overview

`Log2Json.csx` is a simple C# script that converts all `.log` and `.txt` files in a specified folder into **JSON files**. Each input file produces a separate JSON file containing:

- `Filename`: name of the original file  
- `Timestamp`: UTC timestamp when the line was processed  
- `Message`: the text of the line  

> **Note:** If the format of your log files is different (e.g., columns or structure), you can easily update the parsing logic directly in the `.csx` script to handle the new format.


---

## Configuration

At the top of the script, you can configure:

```csharp
const string inputFolder = @".\testdata\logfiles\";
const string outputFolder = @".\testdata\logfiles\jsons\";
List<string> allowedExtensions = new List<string> { ".log", ".txt" };
```

- `inputFolder` – folder where log/text files are located.  
- `outputFolder` – folder where JSON files will be written. The script will create it automatically if it does not exist.  
- `allowedExtensions` – which file types should be processed.

---

## How it works

1. The script scans the `inputFolder` for files with allowed extensions (`.log` and `.txt`).  
2. For each file, it reads all lines and converts each line into a JSON object with `Filename`, `Timestamp`, and `Message`.  
3. Writes each file as `filename.json` in the `outputFolder`.  
4. Outputs messages to the console during processing.  
5. Waits for a keypress at the end to keep the console window open when double-clicked.

