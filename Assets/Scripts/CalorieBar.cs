using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalorieBar : MonoBehaviour
{

    private Slider slider;
    public TMP_Text caloriesCounter;

    public GameObject playerState;

    private float currentCalories, maxCalories;


    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentCalories = playerState.GetComponent<PlayerState>().currentHealth;
        maxCalories = playerState.GetComponent<PlayerState>().maxHealth;

        float fillValue = currentCalories / maxCalories; // 0 - 1
        slider.value = fillValue;

        caloriesCounter.text = currentCalories + "/" + maxCalories;



    }
}
