using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace RPG.Core
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource BGM, Ambience;
        public AudioSource[] PlayerSFXSources;
        public AudioSource[] EnemySFXSources;
        public AudioSource ItemSource;
        public AudioSource UISource;

        public AudioSource ChangeAudioClip(AudioSource mySource, AudioClip myClip)
        {
            mySource.clip = myClip;
            return mySource;
        }

        public void PlayBGM()
        {
            BGM.Play();
        }

        public void StopBGM()
        {
            BGM.Stop();
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

}