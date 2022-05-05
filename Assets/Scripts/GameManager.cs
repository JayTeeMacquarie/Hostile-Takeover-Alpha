using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public int playerhp;
    public int lives;
    private UIManager UIManagerScript;
    void Start()
    {
        gameOverPanel.SetActive(false);
       // UIManagerScript = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        playerhp = GameObject.Find("Player").GetComponent<Player>().health;
        lives = LiveCounter.livesCount;
        if(playerhp <=0 && lives <=0)
        {
                Debug.Log("Game Over MATE");
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
        }
    }
    public void Restart() // Used to restart when you press play again on the buttons
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}