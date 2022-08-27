using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUI : MonoBehaviour
{
    public Slider healthBarSlider;
    public void SetHealth(int value){
        healthBarSlider.value = value;
        Debug.Log("current health" + value);
    }

    public void SetMaxHealth(int max){
        healthBarSlider.maxValue = max;
        healthBarSlider.value = max;
    }
}
