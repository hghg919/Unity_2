using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleST : SingleTon<ExampleST>
{
    

    public void ShowDebug()
    {
        Debug.Log("문과 상호작용하였습니다.");
    }
}
