using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClipList;
    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void WeaponShoot()
    {
        Instantiate(projectilePrefab, muzzle.transform.position, transform.rotation);

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();
        muzzleFlash.Play();
    }

    public void WeaponIdle()
    {
        muzzleFlash.Stop();
    }
}
