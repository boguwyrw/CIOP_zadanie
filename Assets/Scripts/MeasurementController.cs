using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementController : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayerMask;
    [SerializeField] private LayerMask roomLayerMask;

    [SerializeField] private Transform measuringDevice;

    private float deviceRange = 10.0f;

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
                currentDoorValue = door.DoorValue;
                GameManager.Instance.ShowMeasurementResults(currentDoorValue.ToString());
                door.SetIsDoubleCheck(currentDoorValue);
                canMeasureRoom = door.IsDoubleCheck;
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
        if (Physics.Raycast(measuringDevice.position, measuringDevice.forward, out raycastHit, deviceRange, doorLayerMask))
        {
            door = raycastHit.collider.gameObject.GetComponent<Door>();
        }
        else
        {
            GameManager.Instance.ShowMeasurementResults("No Door");
        }
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
                    GameManager.Instance.ShowMeasurementResults("Ok");
                }
                else
                {
                    GameManager.Instance.ShowMeasurementResults("Error");
                }
            }
        }
    }
}
