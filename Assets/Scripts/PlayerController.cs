using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("walking_speed")] public float walkingSpeed;
    [FormerlySerializedAs("jump_speed")] public float jumpSpeed;
    public float gravity;

    private float _direction;
    [FormerlySerializedAs("in_air")] public bool inAir = true;
    private Vector2 _velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _direction = Input.GetAxis("Horizontal");

        //rigid_body.velocity = new Vector2(walking_speed * direction, rigid_body.velocity.y);

        

        if (Input.GetButtonDown("Jump") && !inAir)
        {
            _velocity.y = jumpSpeed;
            Debug.Log("Jump");
        }

        if (!inAir)
        {
            //Debug.Log("Collision");
            _velocity.y = 0; // Reset vertical velocity when on the ground
        }

        // Apply gravity
        _velocity.y -= gravity * Time.deltaTime;

        transform.Translate(_velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            inAir = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            inAir = true;
        }
    }
}
