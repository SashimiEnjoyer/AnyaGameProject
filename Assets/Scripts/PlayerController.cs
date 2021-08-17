using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    
    Rigidbody2D rb;
    Vector3 playerDirection;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + playerDirection * speed * Time.deltaTime);
    }
}
