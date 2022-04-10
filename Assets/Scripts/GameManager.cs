using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject player;
    private PlayerHealth playerhp;
    private UIManager UIManagerScript;
    void Start()
    {
        gameOverPanel.SetActive(false);
        playerhp = FindObjectOfType<PlayerHealth>();
        UIManagerScript = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        // if(playerhp.hp <=0)
        // {
        //         gameOverPanel.SetActive(true);
        //         Time.timeScale = 0;
        // }
    }
    public void Restart() // Used to restart when you press play again on the buttons
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}