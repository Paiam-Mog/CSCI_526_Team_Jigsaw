using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelResult : MonoBehaviour
{
    GameManagerScript gm;
    [SerializeField] Image levelStar1;
    [SerializeField] Image levelStar2;
    [SerializeField] Image levelStar3;

    void Awake()
    {
        gm = GetComponent<GameManagerScript>();
    }
    
    void Update()
    {
        UpdateLevelSelectionScores();
    }

    void UpdateLevelSelectionScores()
    {
        for (int i = 0; i <= gm.starScores.Count; i++)
        {
            if (gm.starScores[i] == 0)
            {
                levelStar1.enabled = false;
                levelStar2.enabled = false;
                levelStar3.enabled = false;
            }
            else if (gm.starScores[i] == 1)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = false;
                levelStar3.enabled = false;
            }
            else if (gm.starScores[i] == 2)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = true;
                levelStar3.enabled = false;
            }
            else if (gm.starScores[i] == 3)
            {
                levelStar1.enabled = true;
                levelStar2.enabled = true;
                levelStar3.enabled = true;
            }
        }
    }
}
