using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] List<Emotion> answers;
    [SerializeField] Sprite sprite;

    public Sprite GetSprite { get { return sprite; } }

    /// <summary>
    /// Returns the emotion that is stored in 
    /// the given index of the hearts answers list
    /// </summary>
    /// <param name="questionIndex"></param>
    /// <returns>answer to the question as an Emotion enum</returns>
    public Emotion AnswerQuestion(int questionIndex)
    {
        if (questionIndex < 0 || questionIndex >= answers.Count)
        {
            return Emotion.Indifference;
        }

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
        if (transform.gameObject.CompareTag("Head"))
        {
            // print("Correct heart"); // Test code
            return true;
        }


        return false;
    }
}
