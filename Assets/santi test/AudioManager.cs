using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource PlatformBGM, BattleBGM, Ambience;
    public AudioSource[] PlatformSFX, BattleSFX;
    public AudioClip normalBattleBGM, bossBattleBGM;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource ChangeAudioClip(AudioSource mySource, AudioClip myClip)
    {
        mySource.clip = myClip;
        return mySource;
    }

    public void PlayPlatformBGM()
    {
        PlatformBGM.Play();
    }

    public void StopPlatformBGM()
    {
        PlatformBGM.Stop();
    }

    public void PlayBattleBGM()
    {
        BattleBGM.Play();
    }

    public void StopBattleBGM()
    {
        BattleBGM.Stop();
    }

    public void PlayAmbience()
    {
        Ambience.Play();
    }

    public void StopAmbience()
    {
        Ambience.Stop();
    }
}
