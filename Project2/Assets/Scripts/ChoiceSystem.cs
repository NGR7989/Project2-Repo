using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChoiceSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UIManager ui;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GameObject burnScreen;
    [SerializeField] GameObject failScreen;
    [SerializeField] GameObject endcreen;
    [SerializeField] TextMeshProUGUI scoreMesh;
    [SerializeField] JsonLoader levelLoader;

    [Header("Settings")]
    [Tooltip("Index of main list relates to the current level")]
    [SerializeField] List<LevelData> levels;

    [Header("Animation")]
    [SerializeField] EndScreenAnimDetails burnCorrectAnimDetails;
    [SerializeField] EndScreenAnimDetails burnFailAnimDetails;

    [Header("Sound")]
    [SerializeField] GameObject soundManager;
    private SoundManager soundScript;

    private int correctBurnCount;
    private int currentLevel;
    private int currentHeartIndex;
    private int currentQuestion;
    private Head currentHead;
    private Heart currentHeart { get { return levels[currentLevel].hearts[currentHeartIndex]; } }

    public bool canBurn { get; set; }

    private bool gameOver;


    private void Awake()
    {
        canBurn = false;
        gameOver = false;
    }

    private void Start()
    {
        InitializeData();

    }

    private void Update()
    {
        if(gameOver)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void LoadNextLevel()
    {
        if (currentLevel + 1 < levels.Count)
        {
            // Move to next level 
            currentLevel++;

            // load the next levels head and heart data
            levelLoader.LoadLevel(currentLevel + 1);

            // Reset to default values 
            currentQuestion = -1;
            currentHead = levels[currentLevel].head;

            
            RunPlayerQuestion(-1); // Default show 
            //dialogueManager.StopDialogue(ui.dialogueBox);

            // Since 3 queations is our hard amount we manually put them in 
            ui.SetUpLevel(
                levels[currentLevel].questions[0],
                levels[currentLevel].questions[1],
                levels[currentLevel].questions[2]
            );
        }
        else
        {
            // End game
            endcreen.SetActive(true);
            canBurn = false;

            scoreMesh.text = "You got " + correctBurnCount + " correct burns... ";

            // Apply the score 
            switch (correctBurnCount)
            {
                case 0:
                    scoreMesh.text += "No souls rest on your watch.";
                    break;
                case 1:
                    scoreMesh.text += "Helping one person is better then no one";
                    break;
                case 4:
                    scoreMesh.text += "Almost all matches are correct!";
                    break;
                case 5:
                    scoreMesh.text += "You were able to help everyone move on. Amazing job!";
                    break;
                default:
                    scoreMesh.text += "You were able to help some souls move on";
                    break;
            }

            gameOver = true;
        }
    }

    /// <summary>
    /// This function sets up the UI with the proper heart and head data 
    /// </summary>
    private void InitializeData()
    {
        // Get all the head and heart data
        // Pass into UI manager 

        correctBurnCount = 0;
        currentLevel = 0;
        currentHeartIndex = 0;

        currentHead = levels[currentLevel].head;

        ui.SetUpLevel(
            levels[currentLevel].questions[0],
            levels[currentLevel].questions[1],
            levels[currentLevel].questions[2]
            );

        // Get the sound script
        soundScript = soundManager.GetComponent<SoundManager>();
    }

    public void ChangeHeart(bool isPositive)
    {
        if (!canBurn)
        {
            return;
        }

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
        ui.DisplayEmotion(currentHeart.AnswerQuestion(currentQuestion));
    }

    public Sprite GetHeartTexutre()
    {
        return currentHeart.GetSprite;
    }

    public Sprite GetHeadSprite()
    {
        return levels[currentLevel].head.GetSprite;
    }

    /// <summary>
    /// Call when the player makes a choice from the pool of questions 
    /// </summary>
    /// <param name="questionIndex"></param>
    public void RunPlayerQuestion(int questionIndex)
    {
        if(!canBurn)
        {
            return;
        }

        // Make sure int is within range of head and hearts 
        // Default is "..." and "indifference" 
        currentQuestion = questionIndex;

        // Runs index through head question function and hold string 
        // Runs index through heart question function and hold enum
        string headResponse = currentHead.AnswerQuestion(questionIndex);
        Emotion heartResponse = currentHeart.AnswerQuestion(questionIndex);

        // Play the question sound effect
        soundScript.QuestionSound();

        // Runs response of head through dialogue 
        // Runs emotion of heart through heart display in UI
        
        ui.DisplayDialogue(headResponse, heartResponse);
    }

    public void TryBurn()
    {
        print(canBurn);
        if (!canBurn)
        {
            return;
        }

        // Play burn sound effect
        soundScript.BurnSound();

        // Check if the correct match 
        if (currentHeart.CorrectMatch())
        {
            // Pass level 
            correctBurnCount++;
            burnScreen.SetActive(true);
            LoadNextLevel();

            // Fades out to show new scene 
            StartCoroutine(FadeBurnScreen(burnCorrectAnimDetails));

        }
        else
        {
            // Fail Level 
            failScreen.SetActive(true);
            LoadNextLevel();

            // Fades out to show new scene 
            StartCoroutine(FadeBurnScreen(burnFailAnimDetails));
        }
    }

    private IEnumerator FadeBurnScreen(EndScreenAnimDetails details) // TODO: SHOULD BE DONE IN UI MANAGER 
    {
        float lerp = 0;
        canBurn = false;

        while (lerp <= 1)
        {
            details.image.color = Color.Lerp(Color.black, Color.clear, lerp);
            details.image.GetComponentInChildren<TextMeshProUGUI>().color = Color.Lerp(Color.white, Color.clear, lerp);
            lerp += Time.deltaTime * details.speed;
            yield return null;
        }

        details.image.gameObject.SetActive(false);
        canBurn = true;

        //dialogueManager.ClearText(ui.dialogueBox, 0.01f);
    }

    [System.Serializable]
    public class LevelData
    {
        [SerializeField] public Head head;
        [SerializeField] public List<string> questions;
        [SerializeField] public List<Heart> hearts;
    }

    [System.Serializable]
    public class EndScreenAnimDetails
    {
        [SerializeField] public Image image;
        [SerializeField] public float speed;
    }
}
