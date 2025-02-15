using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorFire;
    [SerializeField] private float openDoorAngle = -90.0f;

    private int minDoorValue = 1;
    private int maxDoorValue = 9;
    private int doorValue = 0;
    private int doorSafeValue = 0;

    private float openDoorSpeed = 50.0f;

    private bool isDoubleCheck = false;
    private bool canOpenDoor = false;

    private Quaternion openDoor;

    public int DoorValue { get { return doorValue; } }

    private void Start()
    {
        doorFire.SetActive(false);

        doorValue = UnityEngine.Random.Range(minDoorValue, maxDoorValue + 1);

        openDoor = Quaternion.Euler(new Vector3(transform.eulerAngles.x, openDoorAngle, transform.eulerAngles.z));
    }

    private void Update()
    {
        if (canOpenDoor)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, openDoor, openDoorSpeed * Time.deltaTime);
        }
    }

    public void NeutralizedDoor()
    {
        doorValue = doorSafeValue;
        Debug.Log("Zneutralizowano drzwi");
    }

    public void SetIsDoubleCheck(int currentValue)
    {
        if (currentValue == doorSafeValue)
        {
            isDoubleCheck = true;
        }
    }

    public void OpenDoor()
    {
        bool canOpen = (doorValue == doorSafeValue) && isDoubleCheck;
        if (isDoubleCheck)
        {
            canOpenDoor = true;
            Debug.Log("Otwarto drzwi");
        }
        else
        {
            doorFire.SetActive(true);
            GameManager.Instance.PlayerLoseChance();
            Debug.Log("Drzwi plona");
        }
    }
}
