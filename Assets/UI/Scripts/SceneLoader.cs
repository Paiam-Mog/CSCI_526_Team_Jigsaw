using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Scene scene;
    string currentSceneName;

    public LaserInteractionCount laserInteractionCount;

    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        currentSceneName = scene.name;
    }

    public void LoadNextScene() //assign this play / complete level
    {
        if(scene.buildIndex!=0) {
            laserInteractionCount.setLaserTouchCount(0);
        }
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public void QuitToMainMenu() //assign this play / complete level
    {
        laserInteractionCount.setLaserTouchCount(0);
        SceneManager.LoadScene(0);
    }

    public void ResetLevel() //assign this play / complete level
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void Quit() //assign this to quit button
    {
        if(scene.buildIndex!=0) {
            laserInteractionCount.setLaserTouchCount(0);
        }
        Application.Quit();
    }
}
