using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "GameData/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { A, B, C, D }

    public int id = -1;
    public ItemType type = ItemType.A;
    public string itemName = "이름";
    public string itemDesc = "아이템 설명";
    public Sprite itemIcon = null;

    // 아이템 효과를 위한 속성 추가
    public enum ItemEffectType { None, 공속, 이속, Damage }
    public ItemEffectType effectType = ItemEffectType.None;
    public float effectAmount = 0f;  // 효과의 수치 (예: 체력 회복량, 증가하는 스탯 등)
}
