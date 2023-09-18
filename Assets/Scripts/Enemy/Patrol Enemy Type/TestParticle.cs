using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    public ParticleSystem particle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            particle.Emit(1);
            Debug.Log("Pressed A!");
        }

    }

    private void OnParticleTrigger()
    {
        Debug.Log("Hit!");
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject.name);
    }
}
