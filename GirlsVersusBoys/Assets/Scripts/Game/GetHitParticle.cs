using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitParticle : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClipList;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();

        Destroy(gameObject, 5);
    }
}
