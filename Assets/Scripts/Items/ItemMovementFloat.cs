using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovementFloat : MonoBehaviour
{
    public Rigidbody2D rb;
    public float commontime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    private void Update()
    {
        commontime += Time.deltaTime;

        if(commontime <= 2f)
        {
            rb.gravityScale = -0.1f;
        }

        if(commontime >= 2f)
        {
            rb.gravityScale = 0.1f;
        }

        if(commontime >= 4f)
        {
            commontime = 0;
        }
        
    }
}
