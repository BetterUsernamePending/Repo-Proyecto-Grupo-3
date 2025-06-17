using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] public Block initBlock;
    MovementController movementController;
    BattleController battleController;
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
        if (Input.GetKeyDown(KeyCode.Space) && movementController.alreadyMoved == false) //Por ahora moverse se activa tocando espacio
            movementController.MoveToClicked();
        if (Input.GetKeyDown(KeyCode.A) && battleController.alreadyAttacked == false)
            battleController.ExecuteAttack();
    }

    public void LoadMovementState()
    {
        movementController = new MovementController();
        movementController.OnStateEnter();
    }
    public void LoadBattleState()
    {
        battleController = new BattleController();
        battleController.OnStateEnter();
    }
    public void CancelBattleState()
    {
        battleController.OnStateCancel();
    }
    public void CancelMovementState()
    {
        movementController.OnStateCancel();
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
