using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : SingleTon<DoorManager>
{
    public Door[] doorEntities;

    public void SetDoorState(bool enable, int id)
    {
        if (!doorEntities[id].canAccess) return;

        bool isOpen = enable ? SetDoorOpen(id) : SetDoorClose(id);  
    }

    private bool SetDoorOpen(int id)
    {
        doorEntities[id].OpenDoor();
        return false;
    }

    private bool SetDoorClose(int id)
    {
        doorEntities[id].CloseDoor();
        return true;
    }

    public void LockDoor(int id)
    {
        doorEntities[id].canAccess = false;
    }

    public void UnLockDoor(int id)
    {
        doorEntities[id].canAccess = true;
    }

    public void CheckDoorId(int id)
    {
        foreach(var door in doorEntities)
        {
            if (door.id != id)
                continue;

            if (door.canAccess)
            {
                SetDoorState(!door.currentState, id);
                door.currentState = !door.currentState;
            }          
        }
    }
}
