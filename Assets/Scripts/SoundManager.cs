using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource HittSound;
    public AudioSource gameoverSound;
    public AudioSource finishSound;

    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void HittSoundStatus(bool status)
    {
        if (status)
        {
            HittSound.Play();
        }
        else
        {
            HittSound.Stop();
        }
    }

    public void PlayGameOverSound()
    {
        gameoverSound.Play();
    }
    public void StopGameOverSound()
    {
        gameoverSound.Stop();
    }


    public void PlayFinishSound()
    {
        finishSound.Play();
    }
}
