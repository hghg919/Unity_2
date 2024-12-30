using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "GameData/ItemData")]
public class ItemData : ScriptableObject
{
    // ������ �̸�, ������ ����, ������ ���̵� ��ȣ, ������ ������, ������ Ÿ��
    public enum ItemType { A, B, C, D}
    
    public int id = -1;
    public ItemType type = ItemType.A;
    public string itemName = "�̸�";
    public string itemDesc = "������ ����";
    public Sprite itemIcon = null;

}
