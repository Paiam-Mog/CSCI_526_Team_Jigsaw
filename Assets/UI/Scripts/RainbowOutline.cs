using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashEffect : MonoBehaviour
{
    TextMeshPro titleText;

    void Awake()
    {
        titleText = gameObject.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        
    }

    public void FlashText()
    {
        titleText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.5f);
    }
}
