using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class InspectManager : SingleTon<InspectManager>
{
    // Object Name, Detail 화면에 출력한다.
    // UI - Image - BG, TMP - text

    [Header("Object Name UI")]
    public TextMeshProUGUI objectNameText;
    public GameObject objectNameBG;

    [Header("Object Detail UI")]
    public TextMeshProUGUI objectDetailText;
    public GameObject objectDetailBG;

    [Header("Timer")]
    public float onScreenTimer = 5f;
    public float fadeDuration = 1f;

    private CanvasGroup objectDetailCanvasGroup;
    private bool startTimer;
    private float timer;

    protected override void Awake()
    {
        base.Awake();
        objectDetailCanvasGroup = objectDetailBG.GetComponent<CanvasGroup>();
        objectNameBG.SetActive(false);
        objectDetailCanvasGroup.alpha = 0;
    }

    private void Update()
    {
        if(startTimer)
        {
            timer -= Time.deltaTime;
            if(timer <=0)
            {
                timer = 0;
                startTimer = false;
                StartCoroutine(FadeUI(false, fadeDuration));
            }
        }
    }

    public void ShowName(string objectName, bool show)
    {
        if(show)
        {
            objectNameBG.SetActive(true);
            objectNameText.text = objectName;
        }
        else
        {
            objectNameBG.SetActive(false);
            objectNameText.text = "";
        }
    }

    public void ShowObjectDetail(string info)
    {
        objectDetailText.text = info;
        // BG 활성화, 비활성화
        // Fade In, Out
        // 코루틴
        StartCoroutine(FadeUI(true, fadeDuration));
        timer = onScreenTimer;
        startTimer = true;
    }

    IEnumerator FadeUI(bool fadeIn, float duration)
    {
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = 1f - startAlpha;
        float elapsedTime = 0f;

        objectDetailCanvasGroup.alpha = startAlpha;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / duration;
            objectDetailCanvasGroup.alpha =
                Mathf.Lerp(startAlpha, endAlpha, progress);
            yield return null;
        }
        objectDetailCanvasGroup.alpha = endAlpha;
    }

}
