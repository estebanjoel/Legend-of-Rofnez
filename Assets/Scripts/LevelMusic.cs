using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] AudioClip bgmClip;
    [SerializeField] AudioClip ambienceClip;

    public AudioClip GetBGMClip()
    {
        return bgmClip;
    }
    public AudioClip GetAmbienceClip()
    {
        return ambienceClip;
    }
}
