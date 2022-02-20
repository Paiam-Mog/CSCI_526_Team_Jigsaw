using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplayScript : MonoBehaviour
{
    [SerializeField] private GameManagerScript gm;

    [SerializeField] Image earnedStar1;
    [SerializeField] Image earnedStar2;
    [SerializeField] Image earnedStar3;

    void Awake()
    {
        earnedStar1.enabled = false;
        earnedStar2.enabled = false;
        earnedStar3.enabled = false;
    }

    void Update()
    {
        UpdateStarCount();
    }

    void UpdateStarCount()
    {
        if (gm.GetStarCount() == 1)
        {
            earnedStar1.enabled = true;
            earnedStar2.enabled = false;
            earnedStar3.enabled = false;
        }
        else if (gm.GetStarCount() == 2)
        {
            earnedStar1.enabled = true;
            earnedStar2.enabled = false;
            earnedStar3.enabled = false;
        }
        else if (gm.GetStarCount() == 3)
        {
            earnedStar1.enabled = true;
            earnedStar2.enabled = false;
            earnedStar3.enabled = false;
        }
        else
        {
            earnedStar1.enabled = false;
            earnedStar2.enabled = false;
            earnedStar3.enabled = false;
        }
    }
}
