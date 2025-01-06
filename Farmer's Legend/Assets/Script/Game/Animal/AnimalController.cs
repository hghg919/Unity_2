using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private bool isStopped = false;  // Ȱ�� ���� ����

    void Update()
    {
        if (isStopped)
        {
            return; // Ȱ�� ���� �� �ƹ� �͵� ���� ����
        }
    }

    public void StopActivities()
    {
        isStopped = true; // Ȱ�� ����
        Debug.Log($"{gameObject.name} has stopped.");
    }
}