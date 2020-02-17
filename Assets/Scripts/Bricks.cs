using UnityEngine;

public class Bricks : MonoBehaviour
{
    public GameObject brickParticle;
    public AudioClip destroySound;
    void OnCollisionEnter(Collision other)
    {
        GM.instance.DestroyBrick();
        var particles = Instantiate(brickParticle, transform.position, Quaternion.identity);
        particles.AddComponent<AudioSource>().PlayOneShot(destroySound);
        Destroy(gameObject, 0.04f);
    }
}