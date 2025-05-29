using DG.Tweening;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private GameObject activeTargetObject;

    private Healthbar healthbar;

    private PlayerControl player;

    private void Start()
    {
        healthbar = GetComponentInChildren<Healthbar>();
        player = FindFirstObjectByType<PlayerControl>();

        ActiveTarget(false);
    }
    private void Update()
    {
        FaceThis(player.transform.position);

        if (Input.GetKeyDown(KeyCode.F))
            Fire();
    }

    public void SpawnHitVfx(Vector3 Pos_)
    {
        Instantiate(hitVfx, Pos_, Quaternion.identity);

        healthbar.UpdateHealth(-5, (healthEmpty) =>
        {
            if (healthEmpty)
                gameObject.SetActive(false);
        }); // temp
    }

    public void ActiveTarget(bool bool_)
    {
        activeTargetObject.SetActive(bool_);
    }

    public void FaceThis(Vector3 target)
    {
        Vector3 target_ = new Vector3(target.x, target.y, target.z);
        Quaternion lookAtRotation = Quaternion.LookRotation(target_ - transform.position);
        lookAtRotation.x = 0;
        lookAtRotation.z = 0;
        transform.rotation = lookAtRotation;
    }

    // fire logic
    [SerializeField] private GameObject toFire;
    [SerializeField] private Transform firePoint;

    public void Fire()
    {
        Transform hitObject = Instantiate(toFire, firePoint).transform;
        hitObject.SetParent(null);
        hitObject.DOMove(player.transform.position, 0.5f).SetEase(Ease.Linear);
    }
}
