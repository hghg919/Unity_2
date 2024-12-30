using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "GameData/ItemData")]
public class ItemData : ScriptableObject
{
    // 아이템 이름, 아이템 설명, 아이템 아이디 번호, 아이템 아이콘, 아이템 타입
    public enum ItemType { A, B, C, D}
    
    public int id = -1;
    public ItemType type = ItemType.A;
    public string itemName = "이름";
    public string itemDesc = "아이템 설명";
    public Sprite itemIcon = null;

}
