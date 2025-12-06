# LogUpload.csx

## Overview

`LogUpload.csx` is a **C# script** that uploads all log files from a local folder to **OpenObserve** using the **JSON array ingestion endpoint**.  

---

## Configuration

At the top of the script, you can configure:

string folder = @".\testdata\logfiles\json";   // Folder containing log files  
string orgId = "default";                       // OpenObserve organization ID  
string streamName = "default";                  // Stream name  
string baseUrl = "https://openobserve.myserver"; // OpenObserve URL  
string username = "root@example.com";           // Basic Auth username  
string password = "YOUR_PASSWORD_HERE";        // Basic Auth password  
const int batchSize = 500;                      // Number of log lines per upload batch  

---

## How it works

1. The script scans the folder for all files.  
2. For each file, it reads all lines and filters out empty lines.  
3. Each line is converted into a JSON object:  
3. Lines are uploaded in batches via:  
POST https://<baseUrl>/api/<orgId>/<streamName>/_json  

