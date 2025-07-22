using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UITooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Si el pointer del mouse esta encima de las UI seleccionadas, la Toolkit aparece, y viceversa
    // IMPORTANTE: Para hacer que el Tooltip se active en X pieza de UI, añade este script como propiedad al GameObject de la UI en cuestion!
    public void OnPointerEnter(PointerEventData eventData)
    {
        UITooltipSys.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UITooltipSys.Hide();
    }
}
