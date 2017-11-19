using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateBluePrint{

    //[MenuItem("Assets/Create/Q Inventory/BluePrint")]
    public static CraftingBluePrint Create()
    {
        int num = 1;
        while (System.IO.File.Exists(EditorPrefs.GetString("DatabasePath") + "/BluePrints/New BluePrint" + "(" + num.ToString() + ")" + ".asset"))
            num++;

        CraftingBluePrint asset = ScriptableObject.CreateInstance<CraftingBluePrint>();
        AssetDatabase.CreateAsset(asset, EditorPrefs.GetString("DatabasePath") + "/BluePrints/New BluePrint(" + num.ToString() + ").asset");
        asset.bluePrintName = asset.name;
        AssetDatabase.SaveAssets();
        return asset;
    }
}
