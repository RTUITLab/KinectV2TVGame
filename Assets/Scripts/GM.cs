using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GM : MonoBehaviour
{
    public int lives = 3;
    public int bricks = 60;
    public float resetDelay = 1f;
    public Text livesText;

    public GameObject gameOver;
    public GameObject youWon;
    public GameObject bricksPrefab;
    public GameObject paddle;
    public GameObject deathParticles;

    public static GM instance = null;

    private GameObject clonePaddle;

    public AudioClip winSound;
    public AudioClip lostLifeSound;
    public AudioClip loseSound;
    private AudioSource audioSource;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Setup();
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Setup()
    {
        clonePaddle = Instantiate(paddle, paddle.transform.position, paddle.transform.rotation);
        Instantiate(bricksPrefab, bricksPrefab.transform.position, bricksPrefab.transform.rotation);
    }

    void CheckGameOver(bool lostlife = false)
    {

        if (bricks < 1)
        {
            youWon.SetActive(true);
            audioSource.PlayOneShot(winSound);
            Time.timeScale = .1f;
            Invoke("Reset", resetDelay);
            return;
        }

        if (lostlife)
        {
            if (lives < 1)
            {
                gameOver.SetActive(true);
                audioSource.PlayOneShot(loseSound);
                Time.timeScale = .1f;
                Invoke("Reset", resetDelay);
            }
            else
            {
                audioSource.PlayOneShot(lostLifeSound, 0.6f);
            }
        }
    }

    void Reset()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = string.Format("Жизни: {0}", lives);
        Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
        Destroy(clonePaddle);

        Invoke("SetupPaddle", resetDelay);

        CheckGameOver(true);
    }

    void SetupPaddle()
    {
        KinectManager.instance.IsFire = false;
        clonePaddle = Instantiate(paddle, paddle.transform.position, paddle.transform.rotation);
    }

    public void DestroyBrick()
    {
        bricks -= 1;
        CheckGameOver();
    }
}