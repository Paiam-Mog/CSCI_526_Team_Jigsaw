using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarDisplayScript : MonoBehaviour
{
    [SerializeField] public GameObject[] levelBtnObjects;
    [SerializeField] int numberOfLevels = 8;

    //int levelStarCount;

    void Awake()
    {
        for(int i = 1; i <= numberOfLevels; i++) {
            GameManagerScript.LevelStars levelStars;
            levelStars.levelNumber = i;
            levelStars.starsEarned = PlayerPrefs.GetInt("levelStars_" + i);

            GameManagerScript.starList.Add(levelStars);
		}

        UpdateLevelSelectionScores();
    }


    public void UpdateLevelSelectionScores() //level selection screen stars
    {
        for (int i = 0; i < levelBtnObjects.Length; i++)
        {
            Image levelStar1 = levelBtnObjects[i].transform.Find("Star_Slot1/EarnedStar1").GetComponent<Image>();
            Image levelStar2 = levelBtnObjects[i].transform.Find("Star_Slot2/EarnedStar2").GetComponent<Image>();
            Image levelStar3 = levelBtnObjects[i].transform.Find("Star_Slot3/EarnedStar3").GetComponent<Image>();

            int levelStarCount = 0;
            for(int j = 0; j < GameManagerScript.starList.Count; j++) {
                if (GameManagerScript.starList[j].levelNumber == i + 1) {
                    levelStarCount = GameManagerScript.starList[j].starsEarned;
				}
			}

            if (levelStarCount == 1)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = false;
                levelStar3.enabled = false;
            }
            else if (levelStarCount == 2)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = true;
                levelStar3.enabled = false;
            }
            else if (levelStarCount == 3)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = true;
                levelStar3.enabled = true;
            }
            else
            {
                levelStar1.enabled = false;
                levelStar2.enabled = false;
                levelStar3.enabled = false;
            }
        }
        /*
        foreach (GameObject levelGO in levelBtnObjects)
        {
            Image levelStar1 = GameObject.Find("Star_Slot1/EarnedStar1").GetComponent<Image>();
            Image levelStar2 = GameObject.Find("Star_Slot2/EarnedStar2").GetComponent<Image>();
            Image levelStar3 = GameObject.Find("Star_Slot3/EarnedStar3").GetComponent<Image>();

            if (levelStarCount == 1)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = false;
                levelStar3.enabled = false;


            }
            else if (levelStarCount == 2)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = true;
                levelStar3.enabled = false;
            }
            else if (levelStarCount == 3)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = true;
                levelStar3.enabled = true;
            }
            else
            {
                levelStar1.enabled = false;
                levelStar2.enabled = false;
                levelStar3.enabled = false;
            }
        }
        */
    }
}
