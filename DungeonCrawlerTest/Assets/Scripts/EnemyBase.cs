using DG.Tweening;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private GameObject activeTargetObject;
    [SerializeField] private GameObject toFire;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRadius = 10f; // <-- Radius for firing

    private Healthbar healthbar;
    private PlayerControl player;

    private void Start()
    {
        healthbar = GetComponentInChildren<Healthbar>();
        player = FindFirstObjectByType<PlayerControl>();

        ActiveTarget(false);
        InvokeRepeating(nameof(Fire), 1f, 1f);
    }

    private void Update()
    {
        if (player != null)
            FaceThis(player.transform.position);
    }

    public void SpawnHitVfx(Vector3 pos)
    {
        Instantiate(hitVfx, pos, Quaternion.identity);

        healthbar.UpdateHealth(-5, (healthEmpty) =>
        {
            if (healthEmpty)
                gameObject.SetActive(false);
        });
    }

    public void ActiveTarget(bool state)
    {
        activeTargetObject.SetActive(state);
    }

    public void FaceThis(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f; // keep y-rotation flat
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Fire()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= fireRadius)
        {
            Transform hitObject = Instantiate(toFire, firePoint.position, Quaternion.identity).transform;
            hitObject.DOMove(player.transform.position, 0.5f).SetEase(Ease.Linear);
        }
    }

    // Optional: show radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }
}
