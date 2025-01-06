using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "GameData/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { A, B, C, D }

    public int id = -1;
    public ItemType type = ItemType.A;
    public string itemName = "�̸�";
    public string itemDesc = "������ ����";
    public Sprite itemIcon = null;

    // ������ ȿ���� ���� �Ӽ� �߰�
    public enum ItemEffectType { None, ����, �̼�, Damage }
    public ItemEffectType effectType = ItemEffectType.None;
    public float effectAmount = 0f;  // ȿ���� ��ġ (��: ü�� ȸ����, �����ϴ� ���� ��)
}
