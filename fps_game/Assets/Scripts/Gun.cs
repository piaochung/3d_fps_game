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
        }
        hitPosition = hit.point;
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
        return true;
    }

    IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        yield return waitTime200;

        state = State.Ready;
    }
}
