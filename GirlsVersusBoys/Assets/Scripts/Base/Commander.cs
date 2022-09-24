using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Commander : MonoBehaviour
{
    [Header("CAMERA")]
    [SerializeField]
    private Camera mainCamera;

    [Header("ATTACK")]
    [SerializeField]
    private Soldier deployingSoldier;

    [SerializeField]
    private Soldier selectedSoldier;

    private Vector3 mousePosition = Vector3.zero;

    public List<GridBox> gridBoxes;
    public GridBox selectedGridBox;

    public GameObject grid;

    public LayerMask commanderRaycastLayer;

    void Start()
    {
        mainCamera = CameraMovement.instance.camera;
    }

    void Update()
    {
        #region ATTACK
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, commanderRaycastLayer))
        {
            mousePosition = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);

            // Debug.Log("RAYCAST OBJECT: " + raycastHit.collider.gameObject.name);

            if (deployingSoldier)
            {
                CameraMovement.instance.Command();
                grid.transform.DOScale(Vector3.one, 0.15f);

                if (raycastHit.collider.CompareTag("GridBox"))
                {
                    // Debug.Log("GridBox");
                    if (selectedGridBox != raycastHit.collider.gameObject.GetComponent<GridBox>())
                    {
                        if (selectedGridBox)
                        {
                            // Debug.Log("GB: " + selectedGridBox.name);
                            if (!selectedGridBox.isDeployed)
                            {
                                selectedGridBox.UnDeployed();
                            }
                        }

                        selectedGridBox = raycastHit.collider.gameObject.GetComponent<GridBox>();
                    }

                    if (!selectedGridBox.isDeployed)
                    {
                        selectedGridBox.Deploying();

                        deployingSoldier.transform.position =
                            new Vector3(selectedGridBox.transform.position.x,
                            1, selectedGridBox.transform.position.z);


                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            grid.transform.DOScale(Vector3.zero, 0.05f);

                            CameraMovement.instance.Move();

                            deployingSoldier.Deploye();
                            selectedGridBox.Deployed();
                            deployingSoldier.SetGridBox(selectedGridBox);
                            deployingSoldier = null;
                            selectedGridBox = null;
                        }
                    }
                }
                else
                {
                    // currentSoldier.transform.position = mousePosition;
                }
            }
            else
            {
                grid.transform.DOScale(Vector3.zero, 0.05f);

                CameraMovement.instance.Move();

                if (raycastHit.collider.CompareTag("Soldier"))
                {
                    selectedSoldier = raycastHit.collider.gameObject.GetComponent<Soldier>();
                }
            }
        }
        #endregion
    }

    public void SetDeployingSoldier(Soldier _soldier)
    {
        if (!deployingSoldier)
        {
            if (Manager.instance.GetMoney() >= _soldier.price)
            {
                Manager.instance.DecreaseMoney(_soldier.price);
                deployingSoldier = Instantiate(_soldier, mousePosition,
                    Quaternion.LookRotation(Vector3.forward));
            }
        }
    }

}
