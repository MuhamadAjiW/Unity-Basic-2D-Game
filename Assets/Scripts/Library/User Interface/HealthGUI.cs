using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
    [SerializeField] DamageableObject entity;
    private Text textComp;
    private Slider sliderComp;

    void Awake(){
        textComp = GetComponentInChildren<Text>();
        sliderComp = GetComponentInChildren<Slider>();

        updateView();

        entity.OnDamaged += OnUpdate;
        entity.OnHeal += OnUpdate;
    }

    private void OnUpdate(){
        updateView();
    }

    private void updateView(){
        textComp.text = entity.Health.ToString() + "/" + entity.MaxHealth.ToString();
        sliderComp.maxValue = entity.MaxHealth;
        sliderComp.value = entity.Health;
    }
}
