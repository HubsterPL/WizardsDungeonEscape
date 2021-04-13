using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileUtils
{
    public static void CreateDirectory(string path)
    {
        if (path.Contains(Application.persistentDataPath))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            throw new Exception("Path does not fit into Application.persistentDataPath");
        }
    }

    public static void CreateFile(string path)
    {
        if (path.Contains(Application.persistentDataPath))
        {
            File.Create(path).Close();
        }
        else
        {
            throw new Exception("Path does not fit into Application.persistentDataPath");
        }

    }
}
