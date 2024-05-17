using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEditor;

public class BASICToolbox_PackageDatabase : BASICSingleton<BASICToolbox_PackageDatabase>
{
    public dynamic[] packageInfos = new dynamic[0];

    public void downloadAllPackages()
    {
        StartCoroutine(DownloadZIP("https://github.com/CatsheeDev/BASIC2-Packages/archive/main.zip"));
    }

    private IEnumerator DownloadZIP(string url)
    {
        string tempPath = Path.Combine(Application.temporaryCachePath, "temp_zip.zip");
        string finalPath = getBASICPath();

        using UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            byte[] bytes = www.downloadHandler.data;
            File.WriteAllBytes(tempPath, bytes);

            if (File.Exists(finalPath))
            {
                File.Delete(finalPath);
            }

            string finalDirectory = Path.GetDirectoryName(finalPath);
            if (!Directory.Exists(finalDirectory))
            {
                Directory.CreateDirectory(finalDirectory);
            }

            File.Move(tempPath, finalPath);
            extractPackages();
            AssetDatabase.Refresh();
        }
    }

    private void extractPackages()
    {
        string extractedPath = Path.Combine(Application.dataPath, "_BASIC/Temp/Packages/Extracted");
        try
        {
            Directory.Delete(extractedPath, true); 
        }
        catch (IOException ex)
        {
            Console.WriteLine($"error deleting directory: {ex.Message}");
        }
        System.IO.Compression.ZipFile.ExtractToDirectory(getBASICPath(), extractedPath);
        ExtractInfoJsonFiles(extractedPath);
    }

    private void ExtractInfoJsonFiles(string rootFolderPath)
    {
        Array.Resize(ref packageInfos, 0);

        foreach (var directory in Directory.GetDirectories(rootFolderPath))
        {
            var jsonFiles = new List<string>();
            SearchForJsonFiles(directory, "*.json", jsonFiles);

            foreach (var filePath in jsonFiles)
            {
                string jsonContent = File.ReadAllText(filePath);
                dynamic packageInfo = JsonConvert.DeserializeObject(jsonContent);

                Array.Resize(ref packageInfos, packageInfos.Length + 1);
                packageInfos[^1] = packageInfo;
            }
        }
    }

    private void SearchForJsonFiles(string directoryPath, string searchPattern, List<string> result)
    {
        if (Path.GetFileName(directoryPath) == "info.json")
        {
            result.Add(directoryPath);
        }

        foreach (var subdirectory in Directory.GetDirectories(directoryPath))
        {
            SearchForJsonFiles(subdirectory, searchPattern, result);
        }

        foreach (var fileName in Directory.GetFiles(directoryPath, searchPattern))
        {
            result.Add(fileName);
        }
    }


    private string getBASICPath()
    {
        return Path.Combine(Application.dataPath, "_BASIC/Temp/Packages", "packages.zip");
    }
}
