using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingleTon<AudioManager>
{
    public AudioSource BGM;
    public AudioSource SFX;

    public void PlayBGM(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
