using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public Rigidbody2D rb;
    [HideInInspector]
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Vector2 direction)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Debug.Log("Dead");
            Die(direction);
        }
        else
        {
            Debug.Log($"Health: {currentHealth}");
            Hit(direction);
        }
    }

    private void Die(Vector2 direction)
    {

    }

    private void Hit(Vector2 direction)
    {
        // Debug.Log("hit");
        // rb.AddForce(new Vector2 (direction.x > transform.position.x ? -350 : 350, 100), ForceMode2D.Force);
    }
}
