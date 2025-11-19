using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class AudioUtility : MonoBehaviour
{
    public AudioSource source;

    [SerializeField, Range(0,1)]
    private float maxVolume = 1;
    public float MaxVolume
    {
        get { return maxVolume; }
        private set { maxVolume = value; }
    }

    [HideInInspector]
    public float currentVolume;

    private void Awake()
    {
        if(source == null)
            source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.instance.SoundsOnSceneManager.AddSound(this);
        SoundsOnSceneManager.OnVolumeChange += SetVolumeSound;
    }

    public void FadeInSound(float destVal)
    {
        if (source.volume > 0)
            source.volume = 0;

        if(!source.isPlaying)
            source.Play();

        DOTween.To(() => source.volume, x => source.volume = x, GetVolume(destVal), 5f);
    }

    public void FadeOutSound()
    {
        if(source.volume > 0)
            DOTween.To(() => source.volume, x => source.volume = x, 0, 1.7f);
    }

    void SetVolumeSound(float volume)
    {
        source.volume = GetVolume(volume);
    }

    float GetVolume(float volume)
    {
        return volume * maxVolume;
    }

    private void OnDestroy()
    {
        SoundsOnSceneManager.OnVolumeChange -= SetVolumeSound;
        GameManager.instance.SoundsOnSceneManager.RemoveSound(this);

    }
}
