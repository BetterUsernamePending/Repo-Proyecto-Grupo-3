using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] public Block initBlock;

    void Start()
    {
        StartCoroutine(Delay()); 
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Pathfinding.showPossible(initBlock, 5, 4);
    }
    
    private void cameraReposition(bool currentp1)
    {
        //posiciona la camara sobre el personaje del jugador al iniciar el turno
    }

    private void loadScene()
    {
        //carga la escena correspondiente
    }
}
