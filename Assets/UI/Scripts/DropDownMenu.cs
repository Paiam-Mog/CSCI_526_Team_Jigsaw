using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu: MonoBehaviour
{
    public Button dropdownBtn;
    public GameObject Menu;
    public bool isToggled;
    TopBarScript topBarScript;

    void Awake()
    {
        topBarScript = GetComponent<TopBarScript>();
        isToggled = false;
        gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        if(!isToggled)
        {
            gameObject.SetActive(true);
            isToggled = true;
        }
        else if (isToggled)
        {
            gameObject.SetActive(false);
            isToggled = false;

            topBarScript.ResumeGame();
        }

    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        isToggled = false;
    }
}
