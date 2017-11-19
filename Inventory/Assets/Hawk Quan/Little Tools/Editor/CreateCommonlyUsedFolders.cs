using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR    
using UnityEditor;
#endif

public class CreateCommonlyUsedFolders{

#if UNITY_EDITOR
    [MenuItem("Tools/Hawk Quan/Little Tools/Create Commonly Used Folders")]
    public static void Create()
    {
        CreateFolders();
    }

    static void CreateFolders()
    {
        string m_dataPath = Application.dataPath;

        if (m_dataPath.StartsWith(Application.dataPath))
        {
            m_dataPath = m_dataPath.Substring(Application.dataPath.Length - "Assets".Length);
        }

        string folderName;

        folderName = "Scripts";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName +" Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        folderName = "Prefabs";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName + " Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        folderName = "Sprites";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName + " Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        folderName = "Scenes";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName + " Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        folderName = "Materials";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName + " Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        folderName = "Resources";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName + " Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        folderName = "Audio";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName + " Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        folderName = "Animation";
        if (!Directory.Exists(m_dataPath + "/" + folderName))
        {
            Debug.Log("Success Create " + folderName + " Folder");
            AssetDatabase.CreateFolder(m_dataPath, folderName);
        }
        else
            Debug.Log(folderName + " Folder Already Exists");

        //folderName = "Scripts";
        //if (!Directory.Exists(m_dataPath + "/" + folderName))
        //{
        //    Debug.Log("Success Create " + folderName + " Folder");
        //    AssetDatabase.CreateFolder(m_dataPath, folderName);
        //}
        //else
        //    Debug.Log(folderName + " Folder Already Exists");

        //folderName = "Scripts";
        //if (!Directory.Exists(m_dataPath + "/" + folderName))
        //{
        //    Debug.Log("Success Create " + folderName + " Folder");
        //    AssetDatabase.CreateFolder(m_dataPath, folderName);
        //}
        //else
        //    Debug.Log(folderName + " Folder Already Exists");

        AssetDatabase.Refresh();
    }
#endif
}
