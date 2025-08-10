using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectionManager : MonoBehaviour
{

    //public static SelectionManager Instance { get; set; }

    public bool onTarget;

    public GameObject interaction_Info_UI;
    TMP_Text interaction_text;

    private void Start()
    {
        onTarget = false;

        interaction_text = interaction_Info_UI.GetComponent<TMP_Text>();
    }

    /*
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    */

    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var selectionTransform = hit.transform;
            Debug.Log("Raycast hit an object.");

            if (selectionTransform.GetComponent<InteractableObject>() && selectionTransform.GetComponent<InteractableObject>().playerInRange)
            {
                onTarget = true;
                
                
                Debug.Log("Hit object: " + selectionTransform.name);
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                onTarget = false;

                interaction_Info_UI.SetActive(false);
            }

        }
        else
        {

            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}

