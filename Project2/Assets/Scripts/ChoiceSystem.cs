using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UIManager ui;

    [Header("Settings")]
    [Tooltip("Index of main list relates to the current level")]
    [SerializeField] List<LevelData> levels;


    private int currentLevel;
    private int currentHeartIndex;
    private Head currentHead;
    private Heart currentHeart { get { return levels[currentLevel].hearts[currentHeartIndex]; } }

    private void Start()
    {
        InitializeData();
    }

    private void Update()
    {
        // Check if player every tries to burn current pair 
        if(Input.GetKeyDown(KeyCode.Return))
        {
            print(currentHeart.CorrectMatch());
        }
    }

    /// <summary>
    /// This function sets up the UI with the proper heart and head data 
    /// </summary>
    private void InitializeData()
    {
        // Get all the head and heart data
        // Pass into UI manager 

        currentLevel = 0;
        currentHeartIndex = 0;

        currentHead = levels[currentLevel].head;

        ui.SetUpLevel(
            levels[currentLevel].questions[0],
            levels[currentLevel].questions[1],
            levels[currentLevel].questions[2]
            );
    }

    public void ChangeHeart(bool isPositive)
    {
        int nextIndex = currentHeartIndex;

        if(isPositive)
        {
            // Moves up 
            nextIndex++;

            if(nextIndex >= levels[currentLevel].hearts.Count)
            {
                nextIndex = 0;
            }
        }
        else
        {
            // Moves down 
            nextIndex--;

            if (nextIndex < 0)
            {
                nextIndex = levels[currentLevel].hearts.Count - 1;
            }
        }

        // Sets to new index after processing 
        currentHeartIndex = nextIndex;
        print("New heart is " + currentHeart.name);
    }

    public Sprite GetHeartTexutre()
    {
        return currentHeart.GetSprite;
    }

    /// <summary>
    /// Call when the player makes a choice from the pool of questions 
    /// </summary>
    /// <param name="questionIndex"></param>
    public void RunPlayerQuestion(int questionIndex)
    {
        // Make sure int is within range of head and hearts 
        // Default is "..." and "indifference" 

        // Runs index through head question function and hold string 
        // Runs index through heart question function and hold enum
        string headResponse = currentHead.AnswerQuestion(questionIndex);
        Emotion heartResponse = currentHeart.AnswerQuestion(questionIndex);

        // Runs response of head through dialogue 
        // Runs emotion of heart through heart display in UI

        ui.DisplayDialogue(headResponse, heartResponse);
    }

    [System.Serializable]
    public class LevelData
    {
        [SerializeField] public Head head;
        [SerializeField] public List<string> questions;
        [SerializeField] public List<Heart> hearts;
    }
}
