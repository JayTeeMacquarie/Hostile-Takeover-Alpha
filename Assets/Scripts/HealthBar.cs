using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    private Image box;

    void Start()
    {
        box = slider.fillRect.GetComponent<Image>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health, bool enemy)
    {
        slider.value = health;
        if(health <= 60 && enemy){
            box.color = new Color(0, 1, 0, 1);
        }
    }
}
