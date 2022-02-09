using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioUtility : MonoBehaviour
{
    AudioSource source;
    bool isStarting;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SoundsOnSceneManager.instance.AddSound(this);
    }

    [ContextMenu("Fade Out Sound Test")]
    public void FadeOutSound()
    {
        if (!isStarting)
            isStarting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarting)
            return;

        if(source.clip == null)
        {
            isStarting = false;
            return;
        }

        if (source.volume > 0)
            source.volume = Mathf.Lerp(source.volume, 0, Time.deltaTime * 1.7f);
        else
        {
            source.volume = 0;
            isStarting = false;
        }

    }

    private void OnDestroy()
    {
        SoundsOnSceneManager.instance.RemoveSound(this);
    }
}
