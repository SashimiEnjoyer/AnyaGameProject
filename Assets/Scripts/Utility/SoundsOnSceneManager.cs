using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnSceneManager : MonoBehaviour
{
    public static SoundsOnSceneManager instance;
    public static Action<float> OnVolumeChange;

    public List<AudioUtility> listOfSounds = new List<AudioUtility>();

    [SerializeField]
    private float globalVolume;
    public float GlobalVolume
    {
        set 
        {
            globalVolume = value;
            OnVolumeChange.Invoke(value);
        }
        get { return globalVolume; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AddSound(AudioUtility _audioUtility)
    {
        listOfSounds.Add(_audioUtility);
        _audioUtility.source.volume = _audioUtility.currentVolume = globalVolume;
    }

    public void RemoveSound(AudioUtility _audioUtility)
    {
        listOfSounds.Remove(_audioUtility);
    }

    public void AllAudioFadeIn()
    {
        foreach (AudioUtility s in listOfSounds)
        {
            s.FadeInSound(globalVolume);
        }
    }

    public void AllAudioFadeOut()
    {
        foreach(AudioUtility s in listOfSounds)
        {
            s.FadeOutSound();
        }
    }

    public void SetGlobalVolume(float volume)
    {
        GlobalVolume = volume;
    }
}
