using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private float startTime;
    private int starCount;

    Color star1Color;
    Color star2Color;
    Color star3Color;

    string currentSceneName;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;

    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;

    private Scene scene;

    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        currentSceneName = scene.name;

        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();

        startTime = 0f;
        starCount = 0;
        levelText.text = "Level " + scene.buildIndex;

        Time.timeScale = 1;
    }

    void Update()
    {
        startTime += Time.deltaTime;

        DisplayTime();
        DisplayStars();

        if (Input.GetKey(KeyCode.X))
        {
            GetTime();
        }
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(startTime / 60);
        float seconds = Mathf.FloorToInt(startTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    void DisplayStars()
    {
        if (startTime <= 10.0f)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;
            starCount = 3;
        }
        else if (10.0f <= startTime && startTime < 20.0f)
        {
            star1.enabled = false;
            starCount = 2;

        }
        else if (20.0f <= startTime && startTime < 30.0f)
        {
            star2.enabled = false;
            starCount = 1;
        }
        else
        {
            star3.enabled = false;
            starCount = 0;
        }
    }

    public int GetStarCount()
    {
        return starCount;
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

    public float GetTime()
    {
        float tempTime = startTime;

        Debug.Log(tempTime);

        return tempTime;
    }

    public Scene GetScene()
    {
        Debug.Log("Scene " + scene.buildIndex);

        return SceneManager.GetActiveScene();
    }

    public int GetLevelNumber()
    {
        return scene.buildIndex;
    }

}
