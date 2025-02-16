using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ciop.task
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private TMP_Text ringValueText;

        [SerializeField] private Slider adjustingRingSlider;

        [SerializeField] private MeasurementController measurementController;

        private void Start()
        {
            AdjustRingValue();
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameEnded) return;

            AdjustRingValue();
            if (Input.GetMouseButtonDown(0) && !GameManager.Instance.IsPointerOverUIObject())
            {
                measurementController.RoomCheck(adjustingRingSlider.value);

                bool isDoorNeutralized = !measurementController.CanMeasureRoom && adjustingRingSlider.value == measurementController.CurrentDoorValue;
                if (!isDoorNeutralized) return;

                measurementController.AssignDoor();

                if (measurementController.GetDoor == null)
                {
                    GameManager.Instance.ShowMeasurementResults("Point to Door");
                    return;
                }

                measurementController.GetDoor.NeutralizedDoor();
                measurementController.ReleaseDoor();
            }
        }

        private void AdjustRingValue()
        {
            ringValueText.text = adjustingRingSlider.value.ToString();
        }
    }
}