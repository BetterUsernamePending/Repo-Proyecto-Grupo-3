using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class UITooltip : MonoBehaviour
{

    // Este es el script que define las propiedades reactivas del Tooltip y sus elementos

    public TextMeshProUGUI titulo;
    public TextMeshProUGUI contenido;
    public LayoutElement layout;

    public RectTransform rectTransform;

    public int limiteReactivo;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        int tamañoDelTitulo = titulo.text.Length;
        int tamañoDelContenido = contenido.text.Length;

        // Aqui se define si el layout se activa, dependiendo si el numero de caracteres es mayor al limiteReactivo o no
        // El limite reactivo NO esta definido en codigo, esta definido en el GameObject CajaTooltip, bien al fondo en la propiedad "UI Tooltip (script)"
        layout.enabled = (tamañoDelTitulo > limiteReactivo || tamañoDelContenido > limiteReactivo) ? true : false;

        Vector2 position = Input.mousePosition;

        transform.position = position;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
    }

    // Coming Soon
    //public void EstablecerTexto()
    //{
    //}
}
