using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hitBox;

    [SerializeField]
    private GameObject swordTrail;

    public void ActivateHitBox()
    {
        hitBox.SetActive(true);
        swordTrail.SetActive(true);
    }

    public void DeactivateHitBox()
    {
        hitBox.SetActive(false);
        swordTrail.SetActive(false);
    }
}
