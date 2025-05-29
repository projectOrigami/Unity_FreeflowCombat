using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float destroyAfterSeconds = 2f;

    private void Start()
    {
        Destroy(gameObject, destroyAfterSeconds); // 2 second baad destroy
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // sirf Player tag se collide hote hi destroy
        }
    }
}
