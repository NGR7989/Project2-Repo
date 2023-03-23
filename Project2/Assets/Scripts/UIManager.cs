using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ChoiceSystem choices;
    [SerializeField] DialogueManager dialogue;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI dialogueBox;
    [SerializeField] TextMeshProUGUI q1Text, q2Text, q3Text;
    [Space]
    [SerializeField] Image headRenderer;
    [SerializeField] Image heartRenderer;
    [SerializeField] Image emotionRenderer;

    [Header("Texture References")]
    [SerializeField] EmotionTextures emotions;

    private string currentResponse;

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
    }

    /// <summary>
    /// Displays the reponse from both the head and heart 
    /// </summary>
    /// <param name="headResponse"></param>
    /// <param name="heartResponse"></param>
    public void DisplayDialogue(string headResponse, Emotion heartResponse)
    {
        if(dialogue.IsRunning(dialogueBox) || currentResponse == headResponse)
        {
            return;
        }

        dialogue.ReadDialogue(headResponse, dialogueBox, 0.05f);
        currentResponse = headResponse;

        DisplayEmotion(heartResponse);
     }

    public void DisplayEmotion(Emotion heartResponse)
    {
        // Sets the new sprite based on emotion
        emotionRenderer.sprite = emotions.GetSprite(heartResponse);

    }

    private void DisplayCharacter()
    {
        headRenderer.sprite = choices.GetHeadSprite();
    }

    private void DisplayQuestions(string q1, string q2, string q3)
    {
        q1Text.text = q1;
        q2Text.text = q2;
        q3Text.text = q3;
    }

    /// <summary>
    /// The button press that changes the current heart 
    /// </summary>
    public void ChangeHeart(bool isPositive)
    {
        choices.ChangeHeart(isPositive);
        heartRenderer.sprite = choices.GetHeartTexutre();
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



    [System.Serializable]
    private struct EmotionTextures
    {
        [SerializeField] public Sprite anger;
        [SerializeField] public Sprite surprise;
        [SerializeField] public Sprite disgust;
        [SerializeField] public Sprite fear;
        [SerializeField] public Sprite happy;
        [SerializeField] public Sprite sad;
        [SerializeField] public Sprite indifference;

        public Sprite GetSprite(Emotion emotion)
        {
            switch (emotion)
            {
                case Emotion.Sadness:
                    return sad;
                case Emotion.Happiness:
                    return happy;
                case Emotion.Fear:
                    return fear;
                case Emotion.Anger:
                    return anger;
                case Emotion.Surprise:
                    return surprise;
                case Emotion.Disgust:
                    return disgust;
                case Emotion.Indifference:
                default:
                    return indifference;
            }
        }
    }
}
