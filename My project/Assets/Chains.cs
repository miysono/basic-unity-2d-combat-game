using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : MonoBehaviour
{
    public float climbSpeed = 2f; // speed at which the player climbs the ladder
    private bool isClimbing; // whether the player is currently climbing the ladder
    private float inputVertical; // vertical input from the player
    public float gravityScale;
    bool canClimb = false;
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chains"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Chains"))
        {
            canClimb = false;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if(canClimb && Input.GetKey(KeyCode.W)){
            isClimbing = true;
            rb.gravityScale = 0;
        }else{
            isClimbing = false;
            rb.gravityScale = 5;
        }



        if (isClimbing)
        {
            // move the player up or down the ladder based on their input
            transform.Translate(Vector2.up * climbSpeed * Time.deltaTime);
        }
    }
}
