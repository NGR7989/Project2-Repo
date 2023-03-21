using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [Header("Animation settings")]
    [Header("Focus")]
    [SerializeField] RectTransform focus;
    [SerializeField] float transitionSpeed;
    [SerializeField] AnimationCurve transitionCurvePos;
    [SerializeField] AnimationCurve transitionCurveRadius;

    [SerializeField] List<TutPart> tutorialParts;

    [Header("Text Box")]
    [SerializeField] RectTransform textBox;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextBoxAnimDetails textBoxAnimDetails;

    [Space]
    [Header("References")]
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject tutCanvas;
    [SerializeField] DialogueManager dialogueManager;

    private string heldText;

    // What is currently being applied to the text box 
    private Coroutine currentCo; 

    private TextBoxAnimationTypes heldAnim;
    private enum TextBoxAnimationTypes
    {
        summon,
        banish,
        none
    }

    // Start is called before the first frame update
    void Start()
    {
        // Disable game scripts 
        gameManager.SetActive(false);
        heldAnim = TextBoxAnimationTypes.none;

        StartCoroutine(TutorialCo());
        StartCoroutine(TextBoxCoManger());
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

    private void TrySummonTextBox()
    {
        // Overrides current held anim to be summon
        heldAnim = TextBoxAnimationTypes.summon;
    }

    private void TryBanishTextBox()
    {
        // Overrides current held anim to be banish
        heldAnim = TextBoxAnimationTypes.banish;
    }

    private IEnumerator TextBoxCoManger()
    {
        while(true)
        {
            if(currentCo == null)
            {
                // Checks if there is animation waiting to be played 
                switch (heldAnim)
                {
                    case TextBoxAnimationTypes.summon:
                        currentCo = StartCoroutine(SummonTextBoxCo());
                        heldAnim = TextBoxAnimationTypes.none;
                        break;
                    case TextBoxAnimationTypes.banish:
                        currentCo = StartCoroutine(BanishTextBoxCo());
                        heldAnim = TextBoxAnimationTypes.none;
                        break;
                    case TextBoxAnimationTypes.none:
                    default:
                        break;
                }
            }

            yield return null;
        }
    }

    private IEnumerator TutorialCo()
    {
        for (int i = 0; i < tutorialParts.Count; i++)
        {
            // Animates changing from one section to the next 
            yield return StartCoroutine(FocusCo(tutorialParts[i]));

            // Read Dialogue after arriving
            heldText = tutorialParts[i].desc;
            TrySummonTextBox();

            // Loops until continue is pressed 
            // Will not loop if final animation was just played 
            while (!Input.GetMouseButtonDown(0) && i < tutorialParts.Count - 1)
            {
                yield return null;
            }
            
            // When moving to next spot textbox dissapears 
            TryBanishTextBox();
        }

        // Re enable everything 
        gameManager.SetActive(true);

        // No longer needed
        Destroy(tutCanvas);
        Destroy(this.gameObject); 
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

    private IEnumerator SummonTextBoxCo()
    {
        float lerp = 0;

        while (lerp <= 1)
        {
            Vector2 next = new Vector2(
                Mathf.Lerp(0, textBoxAnimDetails.targetWH.x, textBoxAnimDetails.appearCurveX.Evaluate(lerp)),
                Mathf.Lerp(0, textBoxAnimDetails.targetWH.y, textBoxAnimDetails.appearCurveY.Evaluate(lerp))
                );

            textBox.sizeDelta = next;
            
            lerp += Time.deltaTime * textBoxAnimDetails.appearSpeed;
            yield return null;
        }

        dialogueManager.ReadDialogue(heldText, textMesh, textBoxAnimDetails.writeWaitTime);
        currentCo = null;
    }

    private IEnumerator BanishTextBoxCo()
    {
        // Waits for text to clear first 
        yield return dialogueManager.ClearText(textMesh, textBoxAnimDetails.clearWaitTime);

        float lerp = 0;
        while (lerp <= 1)
        {
            Vector2 next = new Vector2(
                Mathf.Lerp(textBoxAnimDetails.targetWH.x, 0, textBoxAnimDetails.appearCurveX.Evaluate(lerp)),
                Mathf.Lerp(textBoxAnimDetails.targetWH.y, 0, textBoxAnimDetails.appearCurveY.Evaluate(lerp))
                );

            textBox.sizeDelta = next;

            lerp += Time.deltaTime * textBoxAnimDetails.disappearSpeed;
            yield return null;
        }

        currentCo = null;
    }

    [System.Serializable]
    private class TutPart
    {
        [SerializeField] public string desc;
        [SerializeField] public Vector3 focusPoint;
        [SerializeField] public float radius;
    }

    [System.Serializable]
    private class TextBoxAnimDetails
    {
        [SerializeField] public Vector2 targetWH; // Width and Height 
        [SerializeField] public float appearSpeed;
        [SerializeField] public AnimationCurve appearCurveX;
        [SerializeField] public AnimationCurve appearCurveY;
        [SerializeField] public float disappearSpeed;
        [SerializeField] public AnimationCurve disappearCurveX;
        [SerializeField] public AnimationCurve disappearCurveY;
        [Space]
        [SerializeField] public float writeWaitTime;
        [SerializeField] public float clearWaitTime;

        // Offset pool to choose from 
        [SerializeField] public List<Vector3> offsets;
    }
}
