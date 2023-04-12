using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource efxmusic;
    public AudioSource musicSource;
    public static AudioManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    public void playSingle(AudioClip clip)
    {
        efxmusic.clip = clip;
        efxmusic.Play();
    }
}
