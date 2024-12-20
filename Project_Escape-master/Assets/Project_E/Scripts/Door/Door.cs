using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    Animator anim;

    [Header("Door Information")]
    public int id;
    public bool currentState = false;
    public bool canAccess = true;

    int openHashCode = Animator.StringToHash("Open");
    int closeHashCode = Animator.StringToHash("Close");
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        anim.Play(openHashCode);
    }
    public void CloseDoor()
    {
        anim.Play(closeHashCode);
    }

    public void Interact()
    {
        DoorManager.Instance.CheckDoorId(id);
    }
}
