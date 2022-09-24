using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox : MonoBehaviour
{
    public bool isDeployed;
    public bool isDeploying;

    public Color deployingColor;
    public Color unDeployedColor;
    public Color deployedColor;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Deploying()
    {
        if (!isDeployed)
        {
            spriteRenderer.color = deployingColor;
            isDeploying = true;
        }
    }

    public void Deployed()
    {
        spriteRenderer.color = deployedColor;
        isDeployed = true;
        isDeploying = false;
    }

    public void UnDeployed()
    {
        isDeployed = false;
        isDeploying = false;
        spriteRenderer.color = unDeployedColor;
    }
}
