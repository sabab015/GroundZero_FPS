using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgm;

    public AudioSource[] soundEffects;
    // Start is called before the first frame update


    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopbgm()
    {
        bgm.Stop();
    }

    public void playSFX( int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
        soundEffects[sfxNumber].Play();
        
    }

    public void stopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }
}
