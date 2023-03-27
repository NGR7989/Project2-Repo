using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFading : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] Color colorA;
    [SerializeField] Color colorB;

    [SerializeField] float fadeSpeed;
    [SerializeField] AnimationCurve fadeToBCurve;
    [SerializeField] AnimationCurve fadeToACurve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeLoopCo());
    }

    private IEnumerator FadeLoopCo()
    {
        bool fadeToA = false;
        while(true)
        {
            if(fadeToA)
            {
                yield return FadeCo(colorB, colorA, fadeSpeed, fadeToACurve);
            }
            else
            {
                yield return FadeCo(colorA, colorB, fadeSpeed, fadeToBCurve);
            }

            fadeToA = !fadeToA;
            yield return null;
        }
    }

    private IEnumerator FadeCo(Color start, Color target, float speed, AnimationCurve curve)
    {
        float lerp = 0; 

        while (lerp <= 1)
        {
            image.color = Color.Lerp(start, target, curve.Evaluate(lerp));

            lerp += Time.deltaTime * speed;
            yield return null;
        }
    }
}
