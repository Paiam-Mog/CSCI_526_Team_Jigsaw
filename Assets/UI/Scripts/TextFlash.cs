using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFlash : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI titleText;
    bool fullWhite;

    void Update()
    {
        FlashEffect();
    }

    public void FlashEffect()
    {
        float x = Mathf.PingPong(Time.time / 3, 1.0f);

        titleText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, x);
    }
}
