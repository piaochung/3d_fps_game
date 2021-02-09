using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    public Transform gunPivot;

    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();    
    }

    void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerController.fire)
        {
            gun.Fire();
        }
        else if (playerController.reload)
        {
            if (gun.Reload())
            {
                //재장전 애니메이션
            }
        }
    }

    void UpdateUIInfo()
    {
        if (gun != null && UIManager.instance != null)
        {
            UIManager.instance.UpdataAmmoText(gun.magAmmo, gun.remainAmmo);
        }
    }
}
