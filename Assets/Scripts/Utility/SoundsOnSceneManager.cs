using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnSceneManager : MonoBehaviour
{

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
         if(listOfSounds.Count <= 0)
            return;

        foreach (AudioUtility s in listOfSounds)
        {
            if(s != null)
                s.FadeInSound(globalVolume);
        }
    }

    public void AllAudioFadeOut()
    {
        if(listOfSounds.Count <= 0)
            return;

        foreach(AudioUtility s in listOfSounds)
        {
            if(s != null)
                s.FadeOutSound();
        }
    }

    public void SetGlobalVolume(float volume)
    {
        GlobalVolume = volume;
    }
}
