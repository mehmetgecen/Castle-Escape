using System;
using UnityEngine;

namespace CastleEscape.Control
{
    public class DoorController : MonoBehaviour
    {
        public bool playerHasKey = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && playerHasKey)
            {
                OpenDoor();
            }
        }

        void OpenDoor()
        {
            // todo: animation will be added
            // Implement code to open the door here
            Debug.Log("The door is now open. You can proceed.");
        }
    }
}
