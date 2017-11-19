using UnityEngine;
using UnityEditor;

public class CreateItemDatabase
{
    //[MenuItem("Assets/Create/Q Inventory/ItemDataBase")]
    public static ItemList Create()
    {
        int num = 1;
        while (System.IO.File.Exists(EditorPrefs.GetString("DatabasePath") + "/ItemDataBase" + "(" + num.ToString() + ")" + ".asset"))
            num++;
        ItemList asset = ScriptableObject.CreateInstance<ItemList>();

        AssetDatabase.CreateAsset(asset, EditorPrefs.GetString("DatabasePath") + "/ItemDataBase(" + num.ToString() + ").asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}