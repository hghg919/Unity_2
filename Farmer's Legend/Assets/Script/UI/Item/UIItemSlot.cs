using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] UIItemElementView[] itemSlotViews;

    private int currentSlotNumber = 0;

    private void Start()
    {
        foreach (var item in itemSlotViews)
        {
            // 슬롯 효과를 실행하는 함수
            item.onEndSpin += PlaySlot;
        }

        PlaySlot(itemSlotViews[0]);
    }

    public void PlaySlot(UIItemElementView itemView)
    {
        if (currentSlotNumber >= itemSlotViews.Length) return;

        itemView = itemSlotViews[currentSlotNumber];
        currentSlotNumber++;
        StartCoroutine(itemView.SpinItem());
    }
}
