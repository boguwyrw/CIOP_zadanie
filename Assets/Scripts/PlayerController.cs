using UnityEngine;

namespace ciop.task
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerHead;

        [SerializeField] private MeasurementController measurementController;

        private float mouseSensivity = 100f;
        private float xRotation = 0f;
        private float lookUp = 80f;
        private float lookDown = 50f;
        private float moveSpeed = 8f;

        private void Update()
        {
            if (!GameManager.Instance.IsGameEnded)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

                CameraHeadRotation(mouseY);
                PlayerBodyRotation(mouseX);

                PlayerMove();

                PlayerOpenDoor();
            }
        }

        private void CameraHeadRotation(float mouseY)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -lookUp, lookDown);

            playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        private void PlayerBodyRotation(float mouseX)
        {
            transform.Rotate(Vector3.up * mouseX);
        }

        private void PlayerMove()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, 0f, moveZ);
            transform.Translate(move * moveSpeed * Time.deltaTime);
        }

        private void PlayerOpenDoor()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                measurementController.AssignDoor();
                if (measurementController.GetDoor != null)
                {
                    measurementController.GetDoor.OpenDoor();
                }
            }
        }
    }
}