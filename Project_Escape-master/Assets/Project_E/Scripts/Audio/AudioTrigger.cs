using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Execute();
        }
    }

    private void Execute()
    {
        if (audioClip != null)
            AudioManager.Instance.PlaySFX(audioClip);

        TriggerManager.Instance.ExecuteTrigger();
        gameObject.SetActive(false);
    }
}
