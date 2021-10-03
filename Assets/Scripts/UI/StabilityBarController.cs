using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StabilityBarController : MonoBehaviour
{
    public Image innerBar;
    public RectTransform marker;

    public void SetThreshold(float threshold)
    {
        var finalX = -100 + (threshold * 200);
        marker.localPosition = new Vector2(finalX, marker.localPosition.y);
    }

    public void SetStability(float stability)
    {
        innerBar.fillAmount = stability;
    }
}
