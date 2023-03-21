using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] List<string> answers;
    [SerializeField] Sprite sprite;
    public Sprite GetSprite { get { return sprite; } }

    /// <summary>
    /// Returns the answer that is stored in 
    /// the given index of the heads answers list
    /// </summary>
    /// <param name="questionIndex">index of the question being asked</param>
    /// <returns>answer to the question in the form of a string</returns>
    public string AnswerQuestion(int questionIndex)
    {
        return answers[questionIndex];
    }
}
