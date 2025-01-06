using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private bool isStopped = false;  // 활동 정지 여부

    void Update()
    {
        if (isStopped)
        {
            return; // 활동 정지 시 아무 것도 하지 않음
        }
    }

    public void StopActivities()
    {
        isStopped = true; // 활동 정지
        Debug.Log($"{gameObject.name} has stopped.");
    }
}