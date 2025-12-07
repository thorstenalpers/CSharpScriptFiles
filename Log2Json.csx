#!/usr/bin/env dotnet-script
#r "System.IO"
#r "System.Runtime"
#r "System.Text.Json"

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

// -----------------------------------------------
// Configuration
// -----------------------------------------------
const string inputFolder = @".\Testdata\logfiles\";
const string outputFolder = @".\Testdata\logfiles\json\";
List<string> allowedExtensions = new List<string> { ".log", ".txt" };

Console.WriteLine("Starting Log2Json.csx");

try
{

    List<string> files = new();

    if (!Directory.Exists(inputFolder))
    {
        Console.WriteLine($"ERROR: Folder does not exist: {inputFolder}");
        return;
    }
    if (!Directory.Exists(outputFolder))
    {
        Directory.CreateDirectory(outputFolder);
    }

    foreach (var file in Directory.GetFiles(inputFolder))
    {
        string ext = Path.GetExtension(file).ToLowerInvariant();

        if (allowedExtensions.Contains(ext))
        {
            files.Add(file);
        }
    }

    if (files.Count == 0)
    {
        Console.WriteLine("No matching files found!");
        return;
    }

    foreach (string file in files)
    {
        Console.WriteLine($"Reading: {file}");

        var jsonObjects = new List<object>();

        foreach (var line in File.ReadAllLines(file))
        {
            jsonObjects.Add(new
            {
                Filename = Path.GetFileName(file),
                Timestamp = DateTime.UtcNow,
                Message = line
            });
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string outputFileForThis = Path.Combine(
            outputFolder,
            Path.GetFileNameWithoutExtension(file) + ".json"
        );

        File.WriteAllText(outputFileForThis, JsonSerializer.Serialize(jsonObjects, options));
        Console.WriteLine($"JSON saved to {outputFileForThis}");
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;

    Console.WriteLine(ex);
    Console.ResetColor();
}
Console.WriteLine($"DONE. \n\nPress a key to continue ...");
ConsoleKeyInfo key = Console.ReadKey();
