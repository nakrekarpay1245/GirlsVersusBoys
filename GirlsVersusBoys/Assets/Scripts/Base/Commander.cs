using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    [Header("ATTACK")]
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject currentSoldier;

    private Vector3 mousePosition = Vector3.zero;

    public List<GridBox> gridBoxes;
    public GridBox selectedGridBox;

    public LayerMask gridBoxLayer;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        #region ATTACK
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mousePosition = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);

            if (currentSoldier)
            {
                currentSoldier.layer = 2;

                if (raycastHit.collider.CompareTag("GridBox"))
                {
                    // Debug.Log("Naber");

                    selectedGridBox = raycastHit.collider.gameObject.GetComponent<GridBox>();

                    currentSoldier.transform.position =
                        new Vector3(selectedGridBox.transform.position.x,
                        1, selectedGridBox.transform.position.z);


                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        selectedGridBox.isOccupied = true;
                        currentSoldier.layer = 11;
                        currentSoldier = null;
                        selectedGridBox = null;
                    }
                }
                else
                {
                    // currentSoldier.transform.position = mousePosition;
                }
            }
        }
        #endregion
    }
    public void SetCurrentSoldier(GameObject _soldier)
    {

        currentSoldier = Instantiate(_soldier, mousePosition, Quaternion.LookRotation(Vector3.back));
    }

}
