using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Heart : MonoBehaviour
{
    [SerializeField] public List<Emotion> answers;

    /// <summary>
    /// Test code disregard
    /// </summary>
    private void Start()
    {
        CorrectMatch(); // test code
    }

    /// <summary>
    /// Returns the emotion that is stored in 
    /// the given index of the hearts answers list
    /// </summary>
    /// <param name="questionIndex"></param>
    /// <returns>answer to the question as an Emotion enum</returns>
    public Emotion AnswerQuestion(int questionIndex)
    {
        return answers[questionIndex];
    }

    /// <summary>
    /// Checks the tag of the parent object
    /// to see if it is the correct heart
    /// </summary>
    /// <returns>bool of wether it is the correct heart or not</returns>
    public bool CorrectMatch()
    {
        // Check if the parent object is a head through its tag
        if (this.gameObject.CompareTag("Correct"))
        {
            print("Correct heart - " + this.gameObject.name); // Test code
            return true;
        }

        print("Incorrect heart - " + this.gameObject.name); // Test code
        return false;
    }
}
