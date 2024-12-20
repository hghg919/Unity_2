using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "textData", menuName = "ItemData/textData")]
public class InspectData : ItemData
{
    [Header("Information Selection")]
    public bool showObjectName = true;
    public bool showObjectDetails = false;

    [Header("Text Parameter")]
    public string objectName = "Generic Object";

    [Space(10)]
    [TextArea]
    public string objectDetails = "This is a description";
}
