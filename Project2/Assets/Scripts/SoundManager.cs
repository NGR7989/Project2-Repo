using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource menu, background, burn, question;
    private bool gamePlaying;

    // Start is called before the first frame update
    void Start()
    {
        gamePlaying = false;
    }

    void UpdatePlayState(bool playing)
    {
        gamePlaying = playing;

        if (gamePlaying)
        {
            menu.Stop();
            background.Play();
        }
        else
        {
            background.Stop();
            menu.Play();
        }
    }

    void BurnSound()
    {
        burn.Play();
    }

    void QuestionSound()
    {
        question.Play();
    }
}
