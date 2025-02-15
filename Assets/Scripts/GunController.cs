using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if (!GameManager.Instance.IsGameEnded)
        {
            AdjustRingValue();
            if (Input.GetMouseButtonDown(0))
            {
                if (adjustingRingSlider.value == measurementController.CurrentDoorValue)
                {
                    measurementController.GetDoor.NeutralizedDoor();
                }
            }
        }    
    }

    private void AdjustRingValue()
    {
        ringValueText.text = adjustingRingSlider.value.ToString();
    }
}
