using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour, IProgressBar
{
    [SerializeField]
    Image fill;

    float maxValue;

    public void MaxValue (float maxValue)
    {
        this.maxValue = maxValue;
    }

    public void SetProgress(float health)
    {
        fill.fillAmount = health / maxValue;
    }
}
