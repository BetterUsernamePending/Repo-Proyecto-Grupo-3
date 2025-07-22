using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITooltipSys : MonoBehaviour
{
    private static UITooltipSys actual;
    public UITooltip tooltip;

    void Awake()
    {
        actual = this;
    }

    public static void Show()
    {
        actual.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        actual.tooltip.gameObject.SetActive(false);
    }
}
