using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Game DataBase", menuName = "GameData/Database")]
public class GameData : ScriptableObject
{
    // ItemData Scriptable
    public ItemData[] itemDatas;

    public ItemData GetItemData(int id)
    {
        return Array.Find(itemDatas, x => x.id == id);
    }

    public int GetItemLength()
    {
        return itemDatas.Length;
    }

    public void ReOrder()
    {
        Array.Sort(itemDatas, (a, b) => a.id.CompareTo(b.id));
    }

    private void OnValidate()
    {
        ReOrder();
    }
}
