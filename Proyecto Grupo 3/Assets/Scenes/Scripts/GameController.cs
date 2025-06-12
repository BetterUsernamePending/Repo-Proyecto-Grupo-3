using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] public Block initBlock;
    State currentState;
    void Start()
    {
       //StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Pathfinding.showPossible(TurnController.currentCharacter.currentBlock, 5, 4);
        //Corutina en pausa
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Por ahora moverse se activa tocando espacio
            currentState.ExecuteAction();
    }

    public void LoadState()
    {
        currentState = new BattleState();
        currentState.OnStateEnter();
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
