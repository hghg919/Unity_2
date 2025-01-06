using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemElementView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameTextCompoent;
    [SerializeField] private TextMeshProUGUI descTextCompoent;

    private ItemData currentItemData;

    [Header("Spin Item Data")]
    public float spinDuration = 2f;
    public float revealDelay = 1f;
    public float chnageInterval = 0.1f;

    public Action<UIItemElementView> onEndSpin;

    private void SetItemElement()
    {
        iconImage.sprite = currentItemData.itemIcon;
        nameTextCompoent.text = currentItemData.name;
        descTextCompoent.text = currentItemData.itemDesc;
    }

    public IEnumerator SpinItem()
    {
        iconImage.transform.DOScale(Vector3.one * 1.2f, spinDuration / 2).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(spinDuration / 2);

        // image 랜덤하게 변경 

        float elapsedTime = 0f;
        while(elapsedTime < spinDuration)
        {
            ChangeRandomSprite();
            elapsedTime += chnageInterval;
            yield return new WaitForSeconds(chnageInterval);
        }

        iconImage.transform.DOScale(Vector3.one, spinDuration / 2).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(revealDelay);
        onEndSpin?.Invoke(this);
        SetItemElement();
    }

    private void ChangeRandomSprite()
    {
        int spriteLength = GameManager.Instance.Data.GetItemLength();
        int randomIndex = UnityEngine.Random.Range(0, spriteLength);

        currentItemData = GameManager.Instance.Data.GetItemData(randomIndex);
        iconImage.sprite = currentItemData.itemIcon;
    }
}
