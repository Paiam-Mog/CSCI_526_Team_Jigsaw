using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TopBarScript : MonoBehaviour
{
    Scene scene;

    private float startTime;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;

    public float GetTimer()
    {
        return startTime;
    }

    public int GetLevelNumber()
    {
        return scene.buildIndex;
    }

    void Start()
    {
        scene = SceneManager.GetActiveScene();

        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        startTime = 0f;
        levelText.text = "Level " + scene.buildIndex;

        Time.timeScale = 1;
    }

    void Update()
    {
        startTime += Time.deltaTime;
        DisplayTime();
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(startTime / 60);
        float seconds = Mathf.FloorToInt(startTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
