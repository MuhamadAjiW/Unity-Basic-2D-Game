using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour{
    [SerializeField] Player player;
    private Text textComp;
    private Slider sliderComp;

    void Awake(){
        textComp = GetComponentInChildren<Text>();
        sliderComp = GetComponentInChildren<Slider>();

        updateView();

        player.OnStaminaUpdate += OnUpdate;
    }

    private void OnUpdate(){
        updateView();
    }

    private void updateView(){
        textComp.text = player.Stamina.ToString() + "/" + player.MaxStamina.ToString();
        sliderComp.maxValue = player.MaxStamina;
        sliderComp.value = player.Stamina;
    }
}
