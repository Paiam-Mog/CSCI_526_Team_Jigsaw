using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    private float startTime;
    
    string currentSceneName;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    
    private Scene scene;

    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        currentSceneName = scene.name;

        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        startTime = 0f;
        levelText.text = "Level " + scene.buildIndex;

        Time.timeScale = 1;
    }

    void Update()
    {
        startTime += Time.deltaTime;
        DisplayTime();

        if(Input.GetKey(KeyCode.X))
        {
            GetTime();
        }
    }

    public void LoadNextScene() //assign this play / complete level
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public void QuitToMainMenu() //assign this play / complete level
    {
        SceneManager.LoadScene(0);
    }

    public void ResetLevel() //assign this play / complete level
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void Quit() //assign this to quit button
    {
        Application.Quit();
    }

    public Scene GetScene()
    {
        Debug.Log("Scene " + scene.buildIndex);

        return SceneManager.GetActiveScene();
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(startTime / 60);
        float seconds = Mathf.FloorToInt(startTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public string GetTime()
    {

        float minutes = Mathf.FloorToInt(startTime / 60);
        float seconds = Mathf.FloorToInt(startTime % 60);

        string tempTime = minutes + " : " + seconds;

        Debug.Log(tempTime);
        return tempTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public float GetTimer()
    {
        return startTime;
    }

    public int GetLevelNumber()
    {
        return scene.buildIndex;
    }

}
