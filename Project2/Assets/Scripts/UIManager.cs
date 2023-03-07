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
        DisplayDialogue();
        DisplayQuestions();
    }

    private void DisplayDialogue()
    {
        dialogue.GetComponent<DialogueManager>().ReadDialogue("test dialogue box yass (insert index here)", dialogueBox, 0.1f);
    }

    private void DisplayCharacter()
    {

    }

    private void DisplayQuestions()
    {
        q1Text.text = "test1";
        q2Text.text = "test2";
        q3Text.text = "test3";

        
    }

    private void ChangeHeart()
    {

    }
}
