using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public float ballInitialVelocity = 6000f;
    public AudioClip hitSound;


    private Rigidbody rb;
    private bool ballInPlay;
    private AudioSource audioSource;
    [SerializeField] float _timerEndInSeconds;
    [SerializeField] Image _timeBar;
    float _timeconst;
    bool _activeTimer = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        _timeconst = _timerEndInSeconds;
        _timeBar = GameObject.Find("TimeEndLine").GetComponent<Image>();
        _timeBar.color = new Color(255, 255, 255, 0);
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
            EndTimer();
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
            if (!ballInPlay && !_activeTimer)
                StartTimer();

        }
        if (_activeTimer)
        {
            _timerEndInSeconds -= Time.deltaTime;
            _timeBar.fillAmount = _timerEndInSeconds / _timeconst;
            if (_timerEndInSeconds < 0)
            {
                EndGame();
            }
        }
    }
    void EndTimer()
    {
        if (_activeTimer)
        {
            _activeTimer = false;
            _timerEndInSeconds = _timeconst;
            _timeBar.color = new Color(255, 255, 255, 0);
        }
    }
    void StartTimer()
    {
        _activeTimer = true;
        _timeBar.color = new Color(255, 255, 255, 255);
    }

    void EndGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    private void OnCollisionEnter(Collision other)
    {
        audioSource.PlayOneShot(hitSound);
    }
}
