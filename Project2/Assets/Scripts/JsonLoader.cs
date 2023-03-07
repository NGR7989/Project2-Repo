using System.IO;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    private string file = "./Assets/Scripts/json-files/test.json";

    // Start is called before the first frame update
    void Start()
    {
        StreamReader streamIn = new StreamReader(file);

        string line;
        string jsonStr = "";

        while((line = streamIn.ReadLine()) != null)
        {
            print(line);
            jsonStr += line;
        }

        print(jsonStr);
        // object json = JsonUtility.ToJson(jsonStr);
        // print(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
