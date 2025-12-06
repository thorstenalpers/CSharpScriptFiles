#!/usr/bin/env dotnet-script
#r "System.Net.Http"
#r "System.IO"
#r "System.Runtime"

using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

// -------------------------------------------------------
// Configuration
// -------------------------------------------------------
string folder = @".\testdata\logfiles\json";  
string orgId = "default";
string streamName = "default";
string baseUrl = "https://openobserve.myserver";
string endpoint = $"{baseUrl}/api/{orgId}/{streamName}/_json";
string username = "root@example.com";
string password = "04Xf9qcDLwtElcnn";
const int batchSize = 500;

Console.WriteLine("Starting LogUpload.csx");

try
{
    var httpClient = new HttpClient();

    var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

    async Task SendLogsAsync(List<string> lines)
    {
        var objects = lines.Select(line =>
            $"{{\"message\":\"{line.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"}}"
        );

        string json = "[" + string.Join(",", objects) + "]";

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error on send: {response.StatusCode}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
        else
        {
            Console.WriteLine($"Successfully send");
        }
    }

    Console.WriteLine("load files from " + folder);
    var files = Directory.GetFiles(folder);

    foreach (var file in files)
    {
        Console.WriteLine($"File: {file}");

        var lines = File.ReadAllLines(file)
                        .Where(l => !string.IsNullOrWhiteSpace(l))
                        .ToList();

        if (lines.Count == 0)
            continue;

        for (int i = 0; i < lines.Count; i += batchSize)
        {
            var batch = lines.Skip(i).Take(batchSize).ToList();
            await SendLogsAsync(batch);
        }
    }
}
catch(Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;

    Console.WriteLine(ex);
    Console.ResetColor();
}
Console.WriteLine($"DONE. \n\nPress a key to continue ...");
ConsoleKeyInfo key = Console.ReadKey();

