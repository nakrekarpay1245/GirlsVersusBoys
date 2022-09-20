using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox : MonoBehaviour
{
    public bool isOccupied;

    public Color greenColor;
    public Color redColor;

    public MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (isOccupied)
        {
            meshRenderer.material.color = redColor;
        }

        else
        {
            meshRenderer.material.color = greenColor;
        }
    }
}
