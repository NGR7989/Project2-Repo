using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] RectTransform focus;
    [SerializeField] float transitionSpeed;
    [SerializeField] AnimationCurve transitionCurvePos;
    [SerializeField] AnimationCurve transitionCurveRadius;

    [SerializeField] List<TutPart> tutorialParts;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TutorialCo());
    }

    /// <summary>
    /// Set the focus at a certain point and give it a specified radius 
    /// </summary>
    /// <param name="point"></param>
    /// <param name="radius"></param>
    public void Focus(Vector3 point, float radius)
    {
        focus.localPosition = point;
        focus.localScale = Vector3.one * radius;
    }

    private IEnumerator TutorialCo()
    {
        for (int i = 0; i < tutorialParts.Count; i++)
        {
            // Animates changing from one section to the next 
            yield return StartCoroutine(FocusCo(tutorialParts[i]));

            // Loops until continue is pressed 
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }
        }
    }

    private IEnumerator FocusCo(TutPart partDetails)
    {
        float lerp = 0;
        Vector3 startPos = focus.localPosition;
        float startRadius = focus.localScale.x; // All share same value 

        while(lerp <= 1)
        {
            Vector3 point = Vector3.LerpUnclamped(startPos, partDetails.focusPoint, transitionCurvePos.Evaluate(lerp));
            float radius = Mathf.LerpUnclamped(startRadius, partDetails.radius, transitionCurveRadius.Evaluate(lerp));

            Focus(point, radius);

            lerp += Time.deltaTime * transitionSpeed;
            yield return null;
        }
    }

    [System.Serializable]
    private class TutPart
    {
        [SerializeField] public string desc;
        [SerializeField] public Vector3 focusPoint;
        [SerializeField] public float radius;
    }
}
