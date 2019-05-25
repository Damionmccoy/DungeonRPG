using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

    public List<ScriptableObject> objects = new List<ScriptableObject>();

    public void SaveScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            //create a new file to save and put it in the correct file location for the system
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dqg", i));
            //Create a binary formatter for one step encryption. 
            BinaryFormatter binary = new BinaryFormatter();
            //serialize the object in a json file. 
            var json = JsonUtility.ToJson(objects[i]);
            //serialize the json to a binary file
            binary.Serialize(file, json);
            //Close the file
            file.Close();
        }
    }


    private void OnEnable()
    {
        LoadScriptables();
    }


    private void OnDisable()
    {
        SaveScriptables();
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            //First make sure the file exists 
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dqg", i)))
            {
                //If it does open the file 
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dqg", i),FileMode.Open);
                //create a new binary formater to deserialize the file
                BinaryFormatter binary = new BinaryFormatter();
                //deserialize the file to json and finaly load the objects values with the info
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                //And last close the file
                file.Close();
            }
        }
    }

    public void ResetScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dqg", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dqg", i));
            }
        }
    }

}
