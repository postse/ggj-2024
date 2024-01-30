using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{

    private static BackgroundMusicController instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void LowerVolume() {
        audioSource.volume = 0.1f;
    }

    public void RaiseVolume() {
        audioSource.volume = 1f;
    }

    
}
