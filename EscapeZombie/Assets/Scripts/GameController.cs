using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public Text textTG;
    private float ThoiGian = 500f;
    public Text textTT;
    public Text Again;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update() {
        if (ThoiGian > 0)
            textTG.text = "Time: " + (ThoiGian -= Time.deltaTime).ToString();
        if (ThoiGian <= 0)
            Time.timeScale = 0;
        if (Time.timeScale == 0) {
            if (ThoiGian <= 0) {
                textTT.text = "YOU WIN!";
                Again.text = "Continue";
            }
            else {
                textTT.text = "YOU LOST!";
                Again.text = "Try Again";
            }
                
        } 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}