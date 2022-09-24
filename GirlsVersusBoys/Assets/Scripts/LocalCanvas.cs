using UnityEngine;
using DG.Tweening;

public class LocalCanvas : MonoBehaviour
{
    public GameObject cameraHolder;

    private void Start()
    {
        cameraHolder = CameraMovement.instance.gameObject;
    }

    private void Update()
    {
        transform.DORotateQuaternion(cameraHolder.transform.rotation, 0.15f);
    }
}
