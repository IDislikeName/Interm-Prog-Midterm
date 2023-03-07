using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public AudioSource BGM;
    public AudioSource SFX;

    private void Start()
    {
        BGM.Play();
    }
    public void Playclip(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
