using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarDisplayScript : MonoBehaviour
{
    [SerializeField] public GameObject[] levelBtnObjects;

    int levelStarCount;

    void Awake()
    {
        levelStarCount = PlayerPrefs.GetInt("stars");
    }

    public void Update()
    {
        UpdateLevelSelectionScores();
    }

    public void UpdateLevelSelectionScores() //level selection screen stars
    {
        foreach (GameObject levelGO in levelBtnObjects)
        {
            GameObject levelStar1 = GameObject.Find("EarnedStar1");
            GameObject levelStar2 = GameObject.Find("EarnedStar2");
            GameObject levelStar3 = GameObject.Find("EarnedStar3");

            if (levelStarCount == 1)
            {
                levelStar1.SetActive(true);
                levelStar2.SetActive(false);
                levelStar3.SetActive(false);
            }
            else if (levelStarCount == 2)
            {
                levelStar1.SetActive(true);
                levelStar2.SetActive(true);
                levelStar3.SetActive(false);
            }
            else if (levelStarCount == 3)
            {
                levelStar1.SetActive(true);
                levelStar2.SetActive(true);
                levelStar3.SetActive(true);
            }
            else
            {
                levelStar1.SetActive(false);
                levelStar2.SetActive(false);
                levelStar3.SetActive(false);
            }
        }
    }
}
