using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class MasterList
{

    public static List<Task> list;
    public static string saveFile = Application.persistentDataPath + "/popListData_0.4.0.json";
    public static Vector2 GLOBAL_NEWBUBBLEPOS;

    // #### LOADING AND SAVING #### //

    // Loads the list up into the MasterList var, or creates a temp empty MasterList
    public static void LoadMasterList(){
        if(File.Exists(saveFile)){
            string json = File.ReadAllText(saveFile);
            list = JsonUtility.FromJson<Serialization<Task>>(json).ToList();
            Debug.Log("Loaded MasterList from disk!");
        }else{
            list = new List<Task>();
            Debug.Log("Created new Master list in Temp");
        }
    }

    // Saves the current MasterList to disk
    public static void SaveMasterList(){
        string json = JsonUtility.ToJson(new Serialization<Task>(list));
        System.IO.File.WriteAllText(saveFile, json);
        Debug.Log("Saved MasterList to disk!");
    }

}



[System.Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}