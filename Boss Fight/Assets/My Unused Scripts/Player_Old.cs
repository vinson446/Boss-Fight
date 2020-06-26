using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Old : MonoBehaviour {

    public float moveSpeedMultiplier = 5f;
    public float sprintSpeed = 2.5f;
    public float jumpForce = 1000f;
    int jumpTimer = 0;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // jump
		if (Input.GetKeyDown(KeyCode.Space) && jumpTimer == 0)
        {
            jumpTimer += 1;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // WASD controls
    void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0.0f;

        transform.position += Vector3.Normalize(direction) * moveSpeedMultiplier;
    }

    // for jump flag
    private void OnCollisionEnter(Collision collision)
    {
        jumpTimer = 0;
    }
}
