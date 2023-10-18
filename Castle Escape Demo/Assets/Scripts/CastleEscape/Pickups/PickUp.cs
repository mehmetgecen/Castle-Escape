using System;
using System.Collections;
using CastleEscape.Attributes;
using CastleEscape.Control;
using CastleEscape.Stats;
using UnityEngine;

namespace CastleEscape.Pickups
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] float spawnTime = 5f;
        [SerializeField] private DoorController door;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PickUpItem(other.gameObject);
            }
        }

        private void PickUpItem(GameObject subject)
        {
            if (gameObject.CompareTag("Upgrader"))
            {
                subject.GetComponent<Experience>().IncrementLevel();
                
            }
            
            if (gameObject.CompareTag("Key"))
            {
                door.playerHasKey = true;
            }
            
            
            StartCoroutine(SpawnPickup(spawnTime));
        }
        
        IEnumerator SpawnPickup(float seconds)
        {
            HidePickup();
            yield return new WaitForSeconds(seconds);
            ShowPickup();

        }
        
        private void ShowPickup()
        {
            GetComponent<SphereCollider>().enabled = true;
            Transform pickUpTransform = transform;
            
            for (int i = 0; i < pickUpTransform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        
        private void HidePickup()
        {
            GetComponent<SphereCollider>().enabled = false;

            Transform pickUpTransform = transform;
            
            for (int i = 0; i < pickUpTransform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }

    
}
