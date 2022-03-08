using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public List<int> starList = new List<int>();

    private float startTime;
    private int starCount;

    int totalLevels;

    Color star1Color;
    Color star2Color;
    Color star3Color;

    string currentSceneName;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;

    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;

    [SerializeField] Image earnedStar1;
    [SerializeField] Image earnedStar2;
    [SerializeField] Image earnedStar3;

    [SerializeField] float maxTimeFor3Stars;
    [SerializeField] float maxTimeFor2Stars;

    private Scene scene;

    void Awake()
    {

        maxTimeFor3Stars = 30.0f;
        maxTimeFor2Stars = 60.0f;

        totalLevels = SceneManager.sceneCountInBuildSettings;
        
        scene = SceneManager.GetActiveScene();
        currentSceneName = scene.name;

        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();

        startTime = 0f;

        starCount = 0;

        if (levelText) {
            levelText.text = "Level " + (scene.buildIndex - 1);
        }


        Time.timeScale = 1;
    }

    void Update()
    {
        startTime += Time.deltaTime;

        DisplayTime();
        DisplayStars();
        UpdateStarCount();


        if (Input.GetKey(KeyCode.X))
        {
            GetTime();
        }

    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(startTime / 60);
        float seconds = Mathf.FloorToInt(startTime % 60);

        if (timerText) {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    void UpdateStarCount() //TopBar and Menu stars
    {

        if (GetStarCount() == 1)
        {
            earnedStar1.enabled = true;
            earnedStar2.enabled = false;
            earnedStar3.enabled = false;
        }
        else if (GetStarCount() == 2)
        {
            earnedStar1.enabled = true;
            earnedStar2.enabled = true;
            earnedStar3.enabled = false;
        }
        else if (GetStarCount() == 3)
        {
            earnedStar1.enabled = true;
            earnedStar2.enabled = true;
            earnedStar3.enabled = true;
        }

    }

    void DisplayStars()
    {
        if (star1 == null || star2 == null || star3 == null) {
            return;
		}

        if (startTime <= maxTimeFor3Stars && starCount == 0)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;
            starCount = 3;
        }
        else if (maxTimeFor3Stars < startTime && startTime <= maxTimeFor2Stars && starCount == 3)
        {
            star1.enabled = false;
            star2.enabled = true;
            star3.enabled = true;
            starCount = 2;

        }
        else if (maxTimeFor2Stars < startTime && starCount == 2)
        {
            star1.enabled = false;
            star2.enabled = false;
            star3.enabled = true;
            starCount = 1;
        }

    }

    public int GetStarCount()
    {
        return starCount;
    }

    public void LoadNextScene() //assign this play / complete level
    {

        SceneManager.LoadScene(scene.buildIndex + 1);
        if(scene.buildIndex+1>0)
        {
            CustomAnalytics customAnalytics = new CustomAnalytics();
            customAnalytics.levelStartedVsFinished(scene.buildIndex+1, 1, 0);
        }
    }

    public void LoadScene(int sceneIndex) 
    {

        if (sceneIndex>0)
        {
            CustomAnalytics customAnalytics = new CustomAnalytics();
            customAnalytics.levelStartedVsFinished(sceneIndex, 1, 0);
        }
        SceneManager.LoadScene(sceneIndex);
	}

    public void QuitToMainMenu() //assign this play / complete level
    {

        SaveGame();
        SceneManager.LoadScene(0);
    }

    public void ResetLevel() //assign this play / complete level
    {
        SceneManager.LoadScene(currentSceneName);
        if(scene.buildIndex>0)
        {
            CustomAnalytics customAnalytics = new CustomAnalytics();
            customAnalytics.levelStartedVsFinished(scene.buildIndex, 1, 0);
        }
    }

    public void Quit() //assign this to quit button
    {
        SaveGame();
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

    public void SaveGame()
    {
        starList.Add(starCount);
        PlayerPrefs.SetInt("stars", GetStarCount());
        PlayerPrefs.SetInt("furthestLevel", GetLevelNumber());
        PlayerPrefs.SetFloat("time", GetTime());
    }


}
