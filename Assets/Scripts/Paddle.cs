using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float paddleSpeed = 1f;

    private Vector3 playerPos = new Vector3(0, -9.5f, 0);
    
    //added
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() //switched to fixed. it physics, cmon!
    {
        float xPos = transform.position.x;

        if (KinectManager.instance.IsAvailable)
            xPos = KinectManager.instance.PaddlePosition;
        else
            xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);

        playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -9.5f, 0f);

        rb.MovePosition(playerPos); // move physics obj only this way!!!
    }
}



