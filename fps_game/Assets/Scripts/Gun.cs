using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }
    public Transform fireTransform;
    public ParticleSystem muzzleFlashEffect;
    public ParticleSystem shellEjectEffect;

    LineRenderer bulletLineRenderer;

    AudioSource gunAudioSource;
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 5f;
    float fireDistance = 20f;

    public int remainAmmo = 100;
    public int magCapacity = 25;
    public int magAmmo;

    public float timeBetFire = 0.12f;
    float lastFireTime;

    WaitForSeconds waitTime30 = new WaitForSeconds(0.3f);
    WaitForSeconds waitTime200 = new WaitForSeconds(2f);

    void Awake()
    {
        gunAudioSource = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    void OnEnable()
    {
        magAmmo = magCapacity;
        state = State.Ready;
        lastFireTime = 0;
    }

    public  void Fire()
    {
        if (state.Equals(State.Ready) && Time.time >= lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    void Shot()
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if(Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            IDamagealbe target = hit.collider.GetComponent<IDamagealbe>();

            if(target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        StartCoroutine(ShotEffect(hitPosition));

        magAmmo -= 1;
        if (magAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        gunAudioSource.PlayOneShot(shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        bulletLineRenderer.enabled = true;

        yield return waitTime30;

        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        if(state == State.Reloading || remainAmmo <= 0 || magAmmo >= magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;
    }

    IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        gunAudioSource.PlayOneShot(reloadClip);

        yield return waitTime200;

        int ammoToFill = magCapacity - magAmmo;

        if(remainAmmo < ammoToFill)
        {
            ammoToFill = remainAmmo;
        }

        magAmmo += ammoToFill;
        remainAmmo -= ammoToFill;

        state = State.Ready;
    }
}
