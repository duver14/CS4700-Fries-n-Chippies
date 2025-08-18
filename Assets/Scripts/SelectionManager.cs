using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectionManager : MonoBehaviour
{
    
    public static SelectionManager Instance { get; set; }

    public bool onTarget;

    public GameObject selectedObject;

    public GameObject interaction_Info_UI;
    TMP_Text interaction_text;

    private void Start()
    {
        onTarget = false;
        interaction_Info_UI.SetActive(false);
        interaction_text = interaction_Info_UI.GetComponent<TMP_Text>();

        if (interaction_text == null)
            Debug.LogError("TMP_Text component not found in children of interaction_Info_UI.");

        if (Camera.main == null)
            Debug.LogError("Main camera not found! Make sure your camera is tagged 'MainCamera'.");
    }

    
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
    

    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            Debug.Log("Raycast hit an object.");

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                interaction_text.text = interactable.GetItemName();
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


