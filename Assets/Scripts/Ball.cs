using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public float ballInitialVelocity = 6000f;
    public AudioClip hitSound;
    

    private Rigidbody rb;
    private bool ballInPlay;
    private AudioSource audioSource;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetButtonDown("Fire1") || KinectManager.instance.IsFire) && ballInPlay == false)
        {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
            KinectManager.instance.IsFire = false;
        }
        else
        {
            if (rb.velocity.magnitude > 30)
            {
                rb.velocity = rb.velocity.normalized * 30;
            }
            else if (rb.velocity.magnitude < 11 && rb.velocity.magnitude > 0.5f)
            {
                rb.velocity = rb.velocity.normalized * 11;
            }
            else if (rb.velocity.magnitude < 0.5f)
            {
                rb.velocity = Random.insideUnitSphere;
            }
            //KinectManager.instance.IsFire = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        audioSource.PlayOneShot(hitSound);
    }
}