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

    [SerializeField] TextMeshProUGUI testDialogue;

    private void Start()
    {
        dialogue.GetComponent<DialogueManager>().ReadDialogue("test dialogue box yass (insert index here)", testDialogue, 0.1f);
    }

    private void DisplayCharacter()
    {

    }

    private void DisplayQuestions()
    {

    }

    private void ChangeHeart()
    {

    }
}
