using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatScaling : MonoBehaviour
{
    [SerializeField] float beatOutSpeed;
    [SerializeField] float beatInSpeed;
    [SerializeField] float pauseBetweenBeats;

    [SerializeField] AnimationCurve beatOutCurve;
    [SerializeField] AnimationCurve beatInCurve;

    [SerializeField] Vector3 beatOutScale;

    private Vector3 holdScale;
    private void Start()
    {
        holdScale = this.transform.localScale;
        StartCoroutine(BeatHeart());
    }

    private IEnumerator BeatHeart()
    {
        while(true)
        {
            yield return BeatOutThenIn();
            yield return BeatOutThenIn();
            yield return new WaitForSeconds(pauseBetweenBeats);
        }
    }

    private IEnumerator BeatOutThenIn()
    {
        float lerp = 0;

        // Beat out 
        while (lerp <= 1)
        {

            this.transform.localScale = Vector3.LerpUnclamped(holdScale, beatOutScale, beatOutCurve.Evaluate(lerp));

            lerp += Time.deltaTime * beatOutSpeed;
            yield return null;
        }

        // Reset
        lerp = 0;

        // Beat in 
        while (lerp <= 1)
        {

            this.transform.localScale = Vector3.LerpUnclamped(beatOutScale, holdScale, beatInCurve.Evaluate(lerp));

            lerp += Time.deltaTime * beatInSpeed;
            yield return null;
        }
    }
}
