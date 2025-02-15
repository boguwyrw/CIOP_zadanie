using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementController : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayerMask;
    [SerializeField] private LayerMask roomLayerMask;

    [SerializeField] private Transform measuringDevice;

    private float deviceRange = 5.0f;

    private int currentDoorValue = -1;
    private int allowedValue = 0;

    private bool canMeasureRoom = false;

    private Door door = null;

    private Transform playerCameraHead = null;

    public int CurrentDoorValue { get { return currentDoorValue; } }

    public bool CanMeasureRoom { get { return canMeasureRoom; } }

    public Door GetDoor { get { return door; } }

    private void Update()
    {
        if (!GameManager.Instance.IsGameEnded)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                AssignDoor();
                if (door != null)
                {
                    currentDoorValue = door.DoorValue;
                    GameManager.Instance.ShowMeasurementResults(currentDoorValue.ToString());
                    door.SetIsDoubleCheck(currentDoorValue);
                    canMeasureRoom = door.IsDoubleCheck;
                    door = null;
                }
                else
                {
                    GameManager.Instance.ShowMeasurementResults("Point to Door");
                }
            }
        }   
    }

    public void GetPlayerHead(Transform cameraHead)
    {
        playerCameraHead = cameraHead;
    }

    public void AssignDoor()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(playerCameraHead.position, playerCameraHead.forward, out raycastHit, deviceRange, doorLayerMask))
        {
            door = raycastHit.collider.GetComponent<Door>();
        }
    }

    public void ReleaseDoor()
    {
        door = null;
    }

    public void RoomCheck(float ringValue)
    {
        if (playerCameraHead != null && canMeasureRoom)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(playerCameraHead.position, playerCameraHead.forward, out raycastHit, deviceRange, roomLayerMask))
            {
                if ((int)ringValue == allowedValue)
                {
                    BuildingComponent buildingComponent = raycastHit.collider.GetComponent<BuildingComponent>();
                    if (!buildingComponent.HasBeenChecked)
                    {
                        GameManager.Instance.ShowMeasurementResults("Ok");
                        buildingComponent.SetHasBeenChecked();
                    }
                    else
                    {
                        GameManager.Instance.ShowMeasurementResults("Checked");
                    }
                }
                else
                {
                    GameManager.Instance.ShowMeasurementResults("Error");
                }
            }
        }
    }
}
