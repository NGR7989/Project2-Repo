using System.IO;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    public TextAsset textJSON;
    public GameObject hearts;

    [System.Serializable]
    public class Hearts
    {
        public Emotion[] answers;
    }

    [System.Serializable]
    public class HeartsArray
    {
        public Hearts[] hearts;
    }

    public HeartsArray myHeartsArray = new HeartsArray();

    void Start()
    {
        // parse the json
        myHeartsArray = JsonUtility.FromJson<HeartsArray>(textJSON.text);

        // setup the hearts
        Heart[] changeHearts = new Heart[3];
        for (int i = 0; i < changeHearts.Length; i++)
        {
            changeHearts[i] = hearts.transform.GetChild(i).gameObject.GetComponent<Heart>();
            // changeHearts[i].GetComponent<Emotion[]>() = myHeartsArray.hearts[i];
        }
    }
}
