using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Rigidbody2D initialized.");
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxis("Walk");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 14f);
            Debug.Log("Jump, Larenzo, jump!");
        }
    }
}
