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
        public AudioSource PlayerRunSource;
        public AudioSource[] EnemySFXSources;
        public AudioSource ItemSource;
        public AudioSource UISource;
        public AudioSource[] trapSources;
        public AudioSource[] obstacleSources;

        private void Start()
        {
            PlayerRunSource = PlayerSFXSources[0];
        }

        public AudioSource ChangeAudioClip(AudioSource mySource, AudioClip myClip)
        {
            mySource.clip = myClip;
            return mySource;
        }

        public void PlayClip(AudioSource source, AudioClip clip)
        {
            if(!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
            }
        }

        public void TryToPlayClip(AudioSource[] sources, AudioClip clip)
        {
            for(int i = 0; i < sources.Length; i++)
            {
                if(!sources[i].isPlaying)
                {
                    sources[i].clip = clip;
                    sources[i].Play();
                    break;
                }
            }
        }

        public void StopClipFromSource(AudioSource source, AudioClip clip)
        {
            if(source.isPlaying && source.clip == clip) source.Stop();
        }

        public void StopClipFromSources(AudioSource[] sources, AudioClip clip)
        {
            for(int i = 0; i < sources.Length; i++)
            {
                if(sources[i].isPlaying)
                {
                    if(sources[i].clip == clip) sources[i].Stop();
                }
            }
        }

        public void StopSoruce(AudioSource source)
        {
            source.Stop();
        }
    }

}