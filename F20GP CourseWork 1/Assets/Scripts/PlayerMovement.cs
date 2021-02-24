using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for player movement as well as collisions into powers ups
// Main player movement code is adapted from Brackeys Youtube tutorial https://www.youtube.com/watch?v=_QajrabyTJc
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public int Health;

    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- Preventing player health dropping below zero, always display 0
        while (UI_Manager.instance.Health < 0)
        {
            UI_Manager.instance.Health = 0;
        }
        UI_Manager.instance.UpdateHealthDisplay();
    }

    // --- If hit by enemy, remove 5 hp
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            UI_Manager.instance.Health -= 5;
            
        }
    }

    // --- If player collides with a certain power up, they acquire their respective changes temporarily
    // --- speed boost, weapon boost and health gain
    private void OnCollisionEnter(Collision collision)
    {
        // --- Here UI_Manager checks which boost is active and displays respective pop ups on the screen

        if (collision.gameObject.tag == "SpeedOrb")
        { 
            StartCoroutine("speedBoost");
            UI_Manager.instance.speedBoostActive = true;
        }

        if (collision.gameObject.tag == "HealthOrb")
        {
            UI_Manager.instance.Health += 20;
        }

        if(collision.gameObject.tag == "WeaponBoostOrb")
        {
            UI_Manager.instance.WeaponBoostActive = true;
        }

    }

    // --- For the coroutine of giving the player temporary x2 movement speed (10 seconds)
    // --- Set back to false when boost when 10 seconds is up
    IEnumerator speedBoost()
    {
        speed = 25f;
        yield return new WaitForSeconds(10);
        speed = 12f;
        UI_Manager.instance.speedBoostActive = false;
    }
}
