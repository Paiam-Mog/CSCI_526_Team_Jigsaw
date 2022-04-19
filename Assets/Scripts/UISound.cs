using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{

    public void UIPopUp()
    {
        SoundEffectController.instance.PlayUIPopUpSound();
    }
    public void UIClose()
    {
        SoundEffectController.instance.PlayUICloseSound();
    }
}
