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
        actual.tooltip.titulo.text = TurnController.currentCharacter.abilityList[0]._name;
        actual.tooltip.contenido.text = TurnController.currentCharacter.abilityList[0]._description;
        actual.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        actual.tooltip.gameObject.SetActive(false);
    }
}
