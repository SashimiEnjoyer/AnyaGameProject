using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class AudioUtility : MonoBehaviour
{
    AudioSource source;

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
        DOTweenModuleAudio.DOFade(source, 0, 1.7f);
    }

    private void OnDestroy()
    {
        SoundsOnSceneManager.instance.RemoveSound(this);
    }
}
