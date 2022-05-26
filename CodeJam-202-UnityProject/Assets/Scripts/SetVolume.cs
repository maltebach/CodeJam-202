using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    // magic number incoming
    public AudioMixer mixer;

    public void SetLevel (float sliderValue)

    {
        mixer.SetFloat ("MusicVolume", Mathf.Log10 (sliderValue) * 20);
    }
}
