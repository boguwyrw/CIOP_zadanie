using UnityEngine;

namespace ciop.task
{
    public class BuildingComponent : MonoBehaviour
    {
        private bool hasBeenChecked = false;

        public bool HasBeenChecked { get { return hasBeenChecked; } }

        public void SetHasBeenChecked()
        {
            hasBeenChecked = true;
        }
    }
}