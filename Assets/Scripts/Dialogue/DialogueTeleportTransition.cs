using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTeleportTransition : MonoBehaviour
{
    public DialogueExecuteAction dialogueExecuteAction;
    public GameObject blackOutSquare;
    private float commontime;
    public float unfadetime;
    public float fadeSpeedEnd;

    void Update()
    {
        if (dialogueExecuteAction.teleportTransition == true)
        {
            StartCoroutine(FadeBlackOutSquare());
            commontime += Time.deltaTime;

            if (commontime >= unfadetime)
            {
                StartCoroutine(FadeBlackOutSquare(false));
            }
        }
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 200)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack == true)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a >0)
            {
                fadeAmount = objectColor.a - (fadeSpeedEnd * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
