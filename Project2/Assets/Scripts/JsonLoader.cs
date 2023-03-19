using System.IO;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    public TextAsset textJSON;
    [SerializeField] public GameObject heartsObject;
    public Heart[] heartsArray;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < heartsArray.Length; i++)
        //{
        //    heartsArray[i] = heartsObject.transform.GetChild(i).gameObject.GetComponent<Heart>();
        //    print(heartsArray[i]);
        //}

        heartsArray = JsonUtility.FromJson<Heart[]>(textJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
