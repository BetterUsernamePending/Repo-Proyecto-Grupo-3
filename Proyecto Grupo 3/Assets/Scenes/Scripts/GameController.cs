using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] public Block initBlock;
    MovementState movementState;
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
        if (Input.GetKeyDown(KeyCode.Space) && movementState.alreadyMoved == false) //Por ahora moverse se activa tocando espacio
            movementState.MoveToClicked();
    }

    public void LoadMovementState()
    {
        movementState = new MovementState();
        movementState.OnStateEnter();
    }
    public void CancelMovementState()
    {
        movementState.OnStateCancel();
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
