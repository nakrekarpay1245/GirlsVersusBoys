using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    [Header("ECONOMY")]
    public float price;

    [HideInInspector]
    public GridBox gridBox;
    public virtual void Deploye()
    {

    }
    public virtual void NotDeployed()
    {

    }
    public void SetGridBox(GridBox _gridBox)
    {
        gridBox = _gridBox;
    }
}
