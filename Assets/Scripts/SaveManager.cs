using UnityEngine;
using System.IO;

public static class SaveManager 
{
    // Not going to get attached to any game object.
    // make it static so that we can't make another instance of this object.

    public static string directory = "/SaveData/";
    public static string fileName = "MyData.txt";

    public static void Save(RoomObject ro)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(ro);
        File.WriteAllText(dir + fileName, json);

        Debug.Log(dir);

    }

    public static RoomObject Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        RoomObject ro = new RoomObject();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            ro = JsonUtility.FromJson<RoomObject>(json);
        } else
        {
            Debug.Log("Saved file does not exists");
        }

        Debug.Log("Loaded from txt");
        return ro;
    }
}
