using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] explosions;
    public AudioClip hurt;

    AudioSource source;

    #region Singleton
    
    static public AudioManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound) {
        if (sound != null)
        {
            source.PlayOneShot(sound);
        }
    }

    public void PlayRandomSound(AudioClip[] sounds) {
        if (sounds != null)
        {
            source.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        }
    }

}
