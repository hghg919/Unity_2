using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleST : SingleTon<ExampleST>
{
    

    public void ShowDebug()
    {
        Debug.Log("���� ��ȣ�ۿ��Ͽ����ϴ�.");
    }
}
