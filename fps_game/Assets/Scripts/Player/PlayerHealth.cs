using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;

    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemPickupClip;

    AudioSource playerAudioSource;

    PlayerController playerController;
    PlayerShooter playerShooter;

    void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = currentHealth;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!isDead)
        {
            playerAudioSource.PlayOneShot(hitClip);
        }

        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = currentHealth;
    }

    public override void Die()
    {
        base.Die();

        healthSlider.gameObject.SetActive(false);

        playerAudioSource.PlayOneShot(deathClip);

        playerController.enabled = false;
        playerShooter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            IItem item = other.GetComponent<IItem>();
            if(item != null)
            {
                item.Use(gameObject);
                playerAudioSource.PlayOneShot(itemPickupClip);
            }
        }
    }
}
