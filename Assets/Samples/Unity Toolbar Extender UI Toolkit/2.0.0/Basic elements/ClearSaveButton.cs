using System.IO;
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "FirstLeftButton", ToolbarAlign.Left, order: 1)]
public class ClearSaveButton : Button
{
    public void InitializeElement()
    {
        text = "Clear";
        clicked += ClearSaveData;
        
    }

    private void ClearSaveData()
    {
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "SaveData")))
            Directory.Delete(Path.Combine(Application.persistentDataPath, "SaveData"), true);
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear Player Prefs");
    }
}