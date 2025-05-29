using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private GameObject activeTargetObject;

    private Healthbar healthbar;

    void Start()
    {
        healthbar = GetComponentInChildren<Healthbar>();
        ActiveTarget(false);
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
}
