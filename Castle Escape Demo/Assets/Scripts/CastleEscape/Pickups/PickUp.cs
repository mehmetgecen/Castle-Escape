using System;
using System.Collections;
using CastleEscape.Attributes;
using CastleEscape.Stats;
using UnityEngine;

namespace CastleEscape.Pickups
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] float spawnTime = 5f;

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
            
            // todo: add key to inventory
            /*
            if (gameObject.CompareTag("Key"))
            {
                subject.GetComponent<DoorKey>().HasKey = true;
            }*/
            
            
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
