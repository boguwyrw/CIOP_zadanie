using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementController : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    [SerializeField] private Transform measuringDevice;

    private float deviceRange = 10.0f;

    private int currentDoorValue = -1;

    private Door door = null;

    public int CurrentDoorValue { get { return currentDoorValue; } }

    public Door GetDoor { get { return door; } }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AssignDoor();
            currentDoorValue = door.DoorValue;
            Debug.Log("Door value: " + currentDoorValue);
            door.SetIsDoubleCheck(currentDoorValue);
        }
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
}
