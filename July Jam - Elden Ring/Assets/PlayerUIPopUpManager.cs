using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIPopUpManager : MonoBehaviour
{
    [Header("YOU DIED Pop-Up")]
    [SerializeField] GameObject youDiedPopUpGameObject;
    [SerializeField] TextMeshProUGUI youDiedPopUpBackgroundText;
    [SerializeField] TextMeshProUGUI youDiedPopUpText;
    [SerializeField] CanvasGroup youDiedPopUpTextCanvasGroup;

    public void SendYouDiedPopUp(){
        //ACITIVATE POST PROCESSING EFFECTS

        youDiedPopUpGameObject.SetActive(true);
        youDiedPopUpBackgroundText.characterSpacing = 0;
        //STRETCH OUT THE POP UP
        StartCoroutine(StretchPopUpOverTime(youDiedPopUpBackgroundText, 10, 20));
        //FADE IN THE POP UP
        StartCoroutine(FadeInPopUpOverTime(youDiedPopUpTextCanvasGroup, 10));
        //WAIT, THEN FADE OUT THE POP UP
        StartCoroutine(WaitThenFadeOutPopUpOverTime(youDiedPopUpTextCanvasGroup, 4, 10));
    }

    private IEnumerator StretchPopUpOverTime(TextMeshProUGUI text, float duration, float stretchAmount){
        if(duration > 0f){
            text.characterSpacing = 0;
            float timer = 0;

            yield return null;

            while (timer < duration){
                timer = timer + Time.deltaTime;
                text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20));
                yield return null;
            }
        }
    }

    private IEnumerator FadeInPopUpOverTime(CanvasGroup canvas, float duration){
        if(duration > 0){
            canvas.alpha = 0;
            float timer = 0;

            yield return null;

            while (timer < duration){
                timer = timer + Time.deltaTime;
                canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                yield return null;

            }
        }

        canvas.alpha = 1;
        yield return null;
    }

    private IEnumerator WaitThenFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay){
        if(duration > 0){

            while(delay > 0){
                delay = delay - Time.deltaTime;
                yield return null;
            }

            canvas.alpha = 1;
            float timer = 0;

            yield return null;

            while (timer < duration){
                timer = timer + Time.deltaTime;
                canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                yield return null;

            }
        }

        canvas.alpha = 0;
        yield return null;
    }
}
