using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    private Image image;
    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
    }

    public void SetPercent(float percent)
    {
        image.fillAmount = percent;
    }
}