using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StabilityBarController : MonoBehaviour
{
    public Image innerBar;
    public RectTransform marker;

    [Range(0, 1)]
    public float stability = 0;
    [Range(0,1)]
    public float threshold = 0;

    private void Start()
    {
        SetThreshold(threshold);
        SetStability(stability);
    }

    public void SetThreshold(float threshold)
    {
        RectTransform selfTransorm = (RectTransform)transform;
        float markerPosition = threshold * selfTransorm.rect.width;
        float translation = markerPosition - (selfTransorm.rect.width / 2);
        marker.Translate(new Vector2(translation,0));
        this.threshold = threshold;
    }

    public void SetStability(float stability)
    {
        innerBar.fillAmount = stability;
        this.stability = stability;
    }

    public void IncreaseStability(float amount)
    {
        SetStability(Mathf.Clamp01(stability + amount));
    }

    public void DecreaseStability(float amount)
    {
        SetStability(Mathf.Clamp01(stability - amount));
    }
}
