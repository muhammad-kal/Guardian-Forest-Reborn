using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.json";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Data disimpan ke: " + path);
    }

    public static SaveData Load()
    {
        if (!File.Exists(path))
        {
            Debug.Log("Tidak ada file save. Menggunakan data default.");
            SaveData data = new SaveData();
            data.ResetToDefault();
            Save(data);
            return data;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static void Reset()
    {
        SaveData data = new SaveData();
        data.ResetToDefault();
        Save(data);
    }
}
