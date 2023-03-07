using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] ChoiceSystem choices;
    [SerializeField] DialogueManager dialogue;

    [SerializeField] Button nextHeart, prevHeart, question1, question2, question3;
    [SerializeField] TextMeshProUGUI q1Text, q2Text, q3Text;
    [SerializeField] TextMeshProUGUI dialogueBox;

    private void Start()
    {
        /*DisplayDialogue("test dialogue box yass (insert index here)");
        DisplayQuestions("Test 1", "Test 2", "Test 3");*/
    }

    /// <summary>
    /// Called at the beginning of each level switch in order
    /// to swap out questions and set up proper textures 
    /// </summary>
    /// <param name="q1"></param>
    /// <param name="q2"></param>
    /// <param name="q3"></param>
    public void SetUpLevel(string q1, string q2, string q3)
    {
        DisplayQuestions(q1, q2, q3);
        DisplayCharacter(); // Take in a texture? 
        ChangeHeart(); // Sets up the first heart 
    }

    public void DisplayDialogue(string headResponse, Emotion heartResponse)
    {
        dialogue.ReadDialogue(headResponse, dialogueBox, 0.1f);
        print(heartResponse);
    }

    private void DisplayCharacter()
    {

    }

    private void DisplayQuestions(string q1, string q2, string q3)
    {
        q1Text.text = q1;
        q2Text.text = q2;
        q3Text.text = q3;
    }

    private void ChangeHeart()
    {

    }


    /// <summary>
    /// This function is used by buttons on the canvas to 
    /// send information to the ChoiceSystem 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public void GetQuestionIndexFromButton(int index)
    {
        // Index is displayed and can be changed in editor 

        choices.RunPlayerQuestion(index);
    }
}
