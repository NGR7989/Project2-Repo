using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] DialogueManager dialogue;

    [Header("Settings")]
    [Tooltip("Index of main list relates to the current level")]
    [SerializeField] List<LevelData> levels;

    //private Head currentHead;
    //private List<Heart> currentHeart;

    /// <summary>
    /// This function sets up the UI with the proper heart and head data 
    /// </summary>
    private void InitializeData()
    {
        // Get all the head and heart data
        // Pass into UI manager 
    }

    /// <summary>
    /// Call when the player makes a choice from the pool of questions 
    /// </summary>
    /// <param name="question"></param>
    public void RunPlayerQuestion(int question)
    {
        // Make sure int is within range of head and hearts 
        // Default is "..." and "indifference" 

        // Get the current head 
        // Get the current heart 

        // Runs index through head question function and hold string 
        // Runs index through heart question function and hold enum

        // Runs response of head through dialogue 
        // Runs emotion of heart through heart display in UI
    }

    [System.Serializable]
    public class LevelData
    {
        [SerializeField] public List<string> questions;
        //[SerializeField] List<Head> heads;
        //[SerializeField] List<List<Heart>> hearts;
    }
}
