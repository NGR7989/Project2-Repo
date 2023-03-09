using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] float clearPauseTime;
    [SerializeField] float clearPauseBeforeContinue;

    /*[Header("Temproary testing paramters")]
    [TextArea(5, 10)]
    [SerializeField] string testText;
    [SerializeField] TextMeshProUGUI testTextMesh;
    [SerializeField] float testTextSpeed;*/

    private Dictionary<TextMeshProUGUI, Coroutine> dialogueCoroutines;
    private Dictionary<TextMeshProUGUI, Coroutine> clearCoroutines;

    // Start is called before the first frame update
    void Start()
    {
        dialogueCoroutines = new Dictionary<TextMeshProUGUI, Coroutine>();
        clearCoroutines = new Dictionary<TextMeshProUGUI, Coroutine>();

        // Purely for testing 
        // ReadDialogue(testText, testTextMesh, testTextSpeed);
    }

    public void ReadDialogue(string text, TextMeshProUGUI textMesh, float pauseTime)
    {

        if (clearCoroutines.ContainsKey(textMesh))
        {
            // Don't continue if something is working on that textMesh
            if (dialogueCoroutines[textMesh] != null)
            {
                return;
            }

            print(dialogueCoroutines[textMesh] != null);
        }

        // Check first if must be cleared
        if (textMesh.text.Length == 0)
        {
            // There is no clearing needed to be considered 
            SafeAddTyping(text, textMesh, pauseTime);
        }
        else
        {
            // Clearing must be considered 
            RunReadDialogueAfterClearing(text, textMesh, pauseTime);
        }
    }

    private void SafeAddTyping(string text, TextMeshProUGUI textMesh, float pauseTime)
    {
        // Checks index for active coroutines
        if (dialogueCoroutines.ContainsKey(textMesh))
        {
            // Make sure multiple coroutines are not typing at the same time 
            if (dialogueCoroutines[textMesh] == null)
            {
                // No text to clear and free to set index to a
                // new coroutine 
                dialogueCoroutines[textMesh] = StartCoroutine(ReadDialogueCo(text, textMesh, pauseTime));
            }
            else
            {
                // Possibly stops current typing co
                // then clears out text and being new typing 
            }
        }
        else
        {
            // No text to clear and free to add a new textmesh
            // coroutine pair to the dictionary 
            dialogueCoroutines.Add(textMesh, StartCoroutine(ReadDialogueCo(text, textMesh, pauseTime)));
        }
    }

    /// <summary>
    /// Calls the coroutine that clears away text in the mesh
    /// before starting to type the new text 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textMesh"></param>
    /// <param name="pauseTime"></param>
    private void RunReadDialogueAfterClearing(string text, TextMeshProUGUI textMesh, float pauseTime)
    {
        StartCoroutine(RunReadDialogueAfterClearingCo(text, textMesh, pauseTime));
    }

    /// <summary>
    /// This coroutine clears away all text on a specified 
    /// textmesh 
    /// </summary>
    /// <param name="textMesh"></param>
    /// <param name="clearSpeed"></param>
    /// <returns></returns>
    private IEnumerator ClearText(TextMeshProUGUI textMesh, float clearSpeed)
    {
        // Clear out other typing if happening at the same time 
        if (dialogueCoroutines.ContainsKey(textMesh))
        {
            if (dialogueCoroutines[textMesh] != null)
            {
                StopCoroutine(dialogueCoroutines[textMesh]);
            }
        }

        // Continue to cleare until nothing is left 
        while (textMesh.text.Length > 0)
        {
            textMesh.text = textMesh.text.Remove(textMesh.text.Length - 1, 1);
            yield return new WaitForSeconds(clearSpeed);
        }

        // Cleanup
        clearCoroutines[textMesh] = null;
    }

    /// <summary>
    /// This coroutine will continue to append the next character
    /// within its passed string until it is fully written out 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textMesh"></param>
    /// <param name="pauseTime"></param>
    /// <returns></returns>
    private IEnumerator ReadDialogueCo(string text, TextMeshProUGUI textMesh, float pauseTime)
    {
        for (int i = 0; i < text.Length; i++)
        {
            // No pausing for spaces >:)
            if (text[i] == ' ')
            {
                textMesh.text += text[i];
                continue;
            }

            // Ads text to mesh 
            textMesh.text += text[i];

            yield return new WaitForSeconds(pauseTime);
        }

        dialogueCoroutines[textMesh] = null;
    }

    /// <summary>
    /// A coroutine to call that clears away all text on a mesh
    /// first and then calls then begins the dialgoue typing coroutine
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textMesh"></param>
    /// <param name="pauseTime"></param>
    /// <returns></returns>
    private IEnumerator RunReadDialogueAfterClearingCo(string text, TextMeshProUGUI textMesh, float pauseTime)
    {
        Coroutine clearCo = StartCoroutine(ClearText(textMesh, clearPauseTime));

        // Considering to implement multiple clear cos 
        /*if(clearCoroutines.ContainsKey(textMesh))
        {
            if(clearCoroutines[textMesh] != null)
            {
                //StopCoroutine(clearCoroutines[textMesh]);
            }
        }

        clearCoroutines[textMesh] = clearCo;*/

        yield return clearCo;
        yield return new WaitForSeconds(clearPauseBeforeContinue);
        SafeAddTyping(text, textMesh, pauseTime);
    }
}