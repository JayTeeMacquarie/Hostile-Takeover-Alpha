using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LiveCounter : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int livesCount = 3;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = livesCount.ToString();
    }
}
