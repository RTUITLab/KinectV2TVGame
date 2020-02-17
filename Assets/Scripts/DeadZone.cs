using UnityEngine;

public class DeadZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
        GM.instance.LoseLife();
    }
}