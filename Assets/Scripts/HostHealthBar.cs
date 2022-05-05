using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = 100;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

