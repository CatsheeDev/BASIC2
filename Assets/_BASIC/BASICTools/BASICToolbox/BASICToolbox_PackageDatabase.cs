using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.PackageManager;

public class BASICPackage
{
    public string packageName { get; set; }
    public string verCreated { get; set; }
    public string packageType { get; set; }
}


public class BASICToolbox_PackageDatabase : BASICSingleton<BASICToolbox_PackageDatabase>
{
    public List<dynamic> packageInfos = new();

    public void downloadAllPackages()
    {
        StartCoroutine(DownloadPackages("https://raw.githubusercontent.com/CatsheeDev/BASIC2-Packages/main/packageList.json"));
    }

    private IEnumerator DownloadPackages(string url)
    {
        using UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            extractPackages(www.downloadHandler.text);
            AssetDatabase.Refresh();
        }
    }

    private void extractPackages(string packages)
    {
        ExtractInfoJsonFiles(packages);
    }

    private void ExtractInfoJsonFiles(string packages)
    {
        packageInfos.Clear();
        List<BASICPackage> dPkg = JsonConvert.DeserializeObject<List<BASICPackage>>(packages);
        foreach (var packageInfo in dPkg)
        {
            packageInfos.Add(packageInfo);
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
