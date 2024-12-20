using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItem : MonoBehaviour
{
    public InspectData inspectData;

    public void ShowObjectName(bool enable)
    {
        if(inspectData.showObjectName)
        {
            InspectManager.Instance.ShowName(inspectData.objectName, enable);
        }
    }

    public void ShowDetails()
    {
        if(inspectData.showObjectDetails)
        {
            // InspectManager
            InspectManager.Instance.ShowObjectDetail(inspectData.objectDetails);
        }
    }
}
