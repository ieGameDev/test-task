using UnityEngine;

namespace Assets.Scripts.CameraLogic
{
    public class DisableCursor : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

