using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitEffect", menuName = "ScriptableObject/HitEffect")]
public class HitEffect : ScriptableObject
{
    public GameObject effectPrefab;
    public AudioClip effectSound;
}
