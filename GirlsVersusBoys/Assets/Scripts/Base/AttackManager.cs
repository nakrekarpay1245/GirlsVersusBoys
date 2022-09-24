using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private AttackBox attackBox;

    [SerializeField]
    private GameObject swordTrail;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClipList;
    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void ActivateHitBox()
    {
        attackBox.gameObject.SetActive(true);
        swordTrail.SetActive(true);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();
    }

    public void DeactivateHitBox()
    {
        attackBox.gameObject.SetActive(false);
        swordTrail.SetActive(false);
    }
}
