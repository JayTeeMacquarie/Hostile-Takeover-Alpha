using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static private UIManager instance;
    static public UIManager Instance 
    {
        get 
        {
            if (instance == null) 
            {
                Debug.LogError("There is no UIManager in the scene.");
            }            
            return instance;
        }
    }

    public Text timerText;

    public Text healthtext;

    private PlayerHealth playerhp;
    private float startTime;
    public float totalTime;
    private bool finished = false;
    private GameManager GameManagerScript;
    void Start()
    {
        startTime = Time.time;
        playerhp = FindObjectOfType<PlayerHealth>();
        GameManagerScript = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(finished == false)
        {
            totalTime = Time.time - startTime;
            string minutes = ((int) totalTime / 60).ToString();
            string seconds = (totalTime % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;
        }
        healthtext.text = playerhp.hp.ToString("f0");
        if (playerhp.hp <= 0)
        {
            healthtext.text = "0";
            finished = true;
        }
    }

        void Awake() 
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
