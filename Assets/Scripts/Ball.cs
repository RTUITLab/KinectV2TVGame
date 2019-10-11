using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballInitialVelocity = 600f;

    private Rigidbody rb;
    private bool ballInPlay;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            if (rb.velocity.magnitude > 23)
            {
                rb.velocity = rb.velocity.normalized * 23;
            }
            else if (rb.velocity.magnitude < 19)
            {
                rb.velocity = rb.velocity.normalized * 19;
            }
            //KinectManager.instance.IsFire = false;
        }
    }
}