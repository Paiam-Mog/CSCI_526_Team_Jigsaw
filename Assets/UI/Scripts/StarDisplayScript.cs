using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StarDisplayScript : MonoBehaviour
{
    [SerializeField] public GameObject[] levelBtnObjects;
    [SerializeField] int numberOfLevels = 8;

    [SerializeField] public GameObject levelBtnGrid1;
    [SerializeField] public GameObject levelBtnGrid2;

    [SerializeField] public GameObject arrow_Right;
    [SerializeField] public GameObject arrow_Left;

    [SerializeField] public Image bubbleGrid1;
    [SerializeField] public Image bubbleGrid2;

    int totalStarCount;

    public TextMeshProUGUI totalStarsText;

    void Awake()
    {
        levelBtnGrid1.SetActive(true);         //specifically for UI level selection arrows and bubbles
        levelBtnGrid2.SetActive(false);
        arrow_Right.SetActive(true);
        arrow_Left.SetActive(false);

        for (int i = 1; i <= numberOfLevels; i++) {
            GameManagerScript.LevelStars levelStars;
            levelStars.levelNumber = i;
            levelStars.starsEarned = PlayerPrefs.GetInt("levelStars_" + i);

            GameManagerScript.starList.Add(levelStars);
		}

        UpdateLevelSelectionScores();
    }

    public void UpdateLevelSelectionScores() //level selection screen stars
    {
        totalStarsText.text = "0";
        totalStarCount = int.Parse(totalStarsText.text);

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

            totalStarCount += levelStarCount;

        }

        totalStarsText.text = totalStarCount.ToString();
    }

    public void SwitchPage()
    {
        if(levelBtnGrid1.activeSelf)
        {
            levelBtnGrid2.SetActive(true);
            levelBtnGrid1.SetActive(false);

            arrow_Left.SetActive(true);
            arrow_Right.SetActive(false);

            bubbleGrid1.color = Color.grey;
            bubbleGrid2.color = Color.white;
        }
        else
        {
            levelBtnGrid2.SetActive(false);
            levelBtnGrid1.SetActive(true);

            arrow_Right.SetActive(true);
            arrow_Left.SetActive(false);

            bubbleGrid1.color = Color.white;
            bubbleGrid2.color = Color.grey;
        }
    }
}
