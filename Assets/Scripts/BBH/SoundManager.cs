using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonManager<SoundManager>
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

}
