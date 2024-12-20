using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : SingleTon<TriggerManager>
{
    public AudioTrigger[] triggers;
    private int currentTriggerIndex = 0;

    public bool canMove = true;
    

    public void StopPlayerMove()
    {
        canMove = false;
    }

    public void EnablePlayerMove()
    {
        canMove = true;
    }



    protected override void Awake()
    {
        base.Awake();
        foreach(var trigger in triggers)
        {
            trigger.gameObject.SetActive(false);
        }

        triggers[0].gameObject.SetActive(true);
    }

    public void ExecuteTrigger()
    {
        currentTriggerIndex++;
        if(currentTriggerIndex >= triggers.Length - 1) currentTriggerIndex = triggers.Length - 1;

        triggers[currentTriggerIndex].gameObject.SetActive(true);   
    }


}
