using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    [SerializeField] public TextAsset[] textJSON;
    public GameObject hearts;

    [System.Serializable]
    public class TempHead
    {
        public List<string> answers;
    }

    [System.Serializable]
    public class TempHeart
    {
        public Emotion[] answers;
        public string tag;
    }

    [System.Serializable]
    public class InformationArray
    {
        public TempHead head;
        public TempHeart[] hearts;
    }

    public InformationArray myHeartsArray = new InformationArray();

    void Start()
    {
        LoadLevel(1);

        //// parse the json
        //myHeartsArray = JsonUtility.FromJson<InformationArray>(textJSON.text);

        //// setup the hearts
        //Heart[] changeHearts = new Heart[3];
        //for (int i = 0; i < changeHearts.Length; i++)
        //{
        //    changeHearts[i] = hearts.transform.GetChild(i).gameObject.GetComponent<Heart>();
        //    // changeHearts[i].GetComponent<Emotion[]>() = myHeartsArray.hearts[i];
        //}
    }

    bool LoadLevel(int levelNum)
    {
        // parse the json
        myHeartsArray = JsonUtility.FromJson<InformationArray>(textJSON[levelNum - 1].text);

        return false;
    }
}
