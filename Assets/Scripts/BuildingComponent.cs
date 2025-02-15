using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingComponent : MonoBehaviour
{
    private bool hasBeenChecked = false;

    public bool HasBeenChecked { get { return hasBeenChecked; } }

    public void SetHasBeenChecked()
    {
        hasBeenChecked = true;
    }
}
