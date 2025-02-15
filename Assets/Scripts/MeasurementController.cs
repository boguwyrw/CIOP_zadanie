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


    private Door door = null;

    private Transform playerCameraHead;

    public int CurrentDoorValue { get { return currentDoorValue; } }

    public Door GetDoor { get { return door; } }

    private void Update()
    {
        if (!GameManager.Instance.IsGameEnded)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                AssignDoor();
                currentDoorValue = door.DoorValue;
                Debug.Log("Door value: " + currentDoorValue);
                door.SetIsDoubleCheck(currentDoorValue);
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
            Debug.Log("Brak drzwi");
        }
    }

    public void RoomCheck(float ringValue)
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(playerCameraHead.position, playerCameraHead.forward, out raycastHit, deviceRange, roomLayerMask))
        { 
            if ((int)ringValue == allowedValue)
            {
                Debug.Log(raycastHit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Bledna wartosc");
            }
        }
    }
}
