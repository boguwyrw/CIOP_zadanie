using UnityEngine;

namespace ciop.task
{
    public class MeasurementController : MonoBehaviour
    {
        [SerializeField] private LayerMask doorLayerMask;
        [SerializeField] private LayerMask roomLayerMask;

        [SerializeField] private Transform playerCameraHead;

        private float deviceRange = 5.0f;

        private int currentDoorValue = -1;
        private int allowedValue = 0;

        private bool canMeasureRoom = false;

        private Door door = null;


        public int CurrentDoorValue { get { return currentDoorValue; } }

        public bool CanMeasureRoom { get { return canMeasureRoom; } }

        public Door GetDoor { get { return door; } }

        private void Start()
        {
            if (playerCameraHead == null)
            {
                playerCameraHead = Camera.main.transform;
            }
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameEnded) return;

            if (Input.GetKeyDown(KeyCode.M))
            {
                AssignDoor();

                if (door == null)
                {
                    GameManager.Instance.ShowMeasurementResults("Point to Door");
                    return;
                }

                currentDoorValue = door.DoorValue;
                GameManager.Instance.ShowMeasurementResults(currentDoorValue.ToString());
                door.SetIsDoubleCheck(currentDoorValue);
                canMeasureRoom = door.IsDoubleCheck;
                door = null;
            }
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
}