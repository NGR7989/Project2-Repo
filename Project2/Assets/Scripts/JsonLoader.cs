using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    [SerializeField] public TextAsset[] textJSON;
    public GameObject heartsObj;
    public GameObject headObj;
    private Head head;
    private List<Heart> hearts;
    private int numHearts;

    [System.Serializable]
    public class TempHead
    {
        public List<string> answers;
    }

    [System.Serializable]
    public class TempHeart
    {
        public List<Emotion> answers;
        public int correct;
    }

    [System.Serializable]
    public class InformationArray
    {
        public TempHead head;
        public List<TempHeart> hearts;
    }

    public InformationArray infoArray = new InformationArray();

    void Start()
    {
        // Get the head's and hearts' scripts
        head = headObj.GetComponent<Head>();
        hearts = new List<Heart>();
        numHearts = heartsObj.transform.childCount;

        for (int i = 0; i < numHearts; i++)
        {
            hearts.Add(heartsObj.transform.GetChild(i).gameObject.GetComponent<Heart>());
        }

        // Load the first level
        LoadLevel(1);
    }

    void LoadLevel(int levelNum)
    {
        // parse the json
        infoArray = JsonUtility.FromJson<InformationArray>(textJSON[levelNum - 1].text);

        for (int i = 0; i < numHearts; i++)
        {
            // check if json has run out of hearts
            if (i >= infoArray.hearts.Count)
            {
                // tell which json is lacking information break the loop
                print("Not enough hearts in json for level " + levelNum);
                break;
            }

            // call the hearts' and head's load level function
            hearts[i].LoadHeart(infoArray.hearts[i].answers, infoArray.hearts[i].correct == 1);
            head.LoadHead(infoArray.head.answers);
        }
    }
}
