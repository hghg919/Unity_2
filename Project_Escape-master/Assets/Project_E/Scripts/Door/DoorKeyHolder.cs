using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyHolder : MonoBehaviour, IInteract
{
    public DoorKey doorkey;
 
    public void Interact()
    {
        DoorManager.Instance.UnLockDoor(doorkey.doorId);
        DoorManager.Instance.SetDoorState(true, doorkey.doorId);
    }
}
