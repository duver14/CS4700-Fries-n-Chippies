using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    

    public string ItemName;
    public bool playerInRange;

    public string GetItemName()
    {
        return ItemName;
    }
    
    void Update()
    {
        SelectionManager selectionManager = SelectionManager.Instance;
        
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && selectionManager.onTarget)
        {
            // If inventory is not full
            if (!InventorySystem.Instance.CheckIfFull())
            {
                //InventorySystem.Instance.AddToIventory(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is Full");
            }
            
            
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}