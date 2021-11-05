using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float jumpForce = 60f;
    public float moveForce = 12f;
    public Vector3 spawnPoint;
    public float rotationSpeed = 60f;
    public bool jumpedOnce = false;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        spawnPoint = GetComponent<Rigidbody>().position;
    }
    // Update is called once per frame
    private void Update()
    {
        float moveVertical = Input.GetAxis("Vertical"); //Gets player input exclusively for W/S (forward/backward) keys

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical); //Gathers user input in real-time

        player.AddRelativeForce(movement * moveForce); //Moves the player cube on input


        if (Input.GetKey(KeyCode.A)) //Rotates the player left when A is pressed
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D)) //Rotates the player right when D is pressed
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F)) //Shoots a projectile when F is pressed
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !jumpedOnce) //Allows the player to jump when Spacebar is pressed
        {
            jumpedOnce = true; //Limits the player to jump only once before landing
            player.AddRelativeForce(transform.up * jumpForce, ForceMode.Impulse); //Pushes the player up
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpedOnce = false; //Resets the player's jump ability
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.transform.position = spawnPoint;
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            //DestroyImmediate(other);
        }
    }
}