using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnSceneManager : MonoBehaviour
{
    public static SoundsOnSceneManager instance;
    public List<AudioUtility> listOfSounds = new List<AudioUtility>();
    [SerializeField] [Range(0, 1)] float volumeAudio = 0.5f;

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
        _audioUtility.GetComponent<AudioSource>().volume = volumeAudio;
    }

    public void RemoveSound(AudioUtility _audioUtility)
    {
        listOfSounds.Remove(_audioUtility);
    }

    public void AllAudioFadeOut()
    {
        foreach(AudioUtility s in listOfSounds)
        {
            s.FadeOutSound();
        }
    }
}
