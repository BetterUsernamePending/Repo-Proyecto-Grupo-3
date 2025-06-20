using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] public Block initBlock;
    public GameObject OngoingMovement;

    MovementController movementController;
    BattleController battleController;
    void Start()
    {
       //StartCoroutine(Delay());
       battleController = FindAnyObjectByType<BattleController>();
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Pathfinding.showPossible(TurnController.currentCharacter.currentBlock, 5, 4);
        //Corutina en pausa
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            GetClickedBlock();
    }

    public void GetClickedBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Block.onBlockClicked?.Invoke(hit.transform.GetComponent<Block>());
            Debug.Log(hit.transform.name);
        }
    }

    //Movement Section
    public void LoadMovementState()
    {
        movementController = new MovementController();
        movementController.OnStateEnter();
    }
    public void ActivateMovement()
    {
        movementController.MoveToClicked();
    }
    public void CancelMovementState()
    {
        movementController.OnStateCancel();
    }
    //End of Movement Section

    //Battle Section
    public void LoadBattleState()
    {
        battleController = new BattleController();
        battleController.OnStateEnter();
    }
    public void CancelBattleState()
    {
        battleController.OnStateCancel();
    }
    public void ExecuteAttack()
    {
        battleController.ExecuteAttack();
    }
    //End of Battle Section

    private void cameraReposition(bool currentp1)
    {
        //posiciona la camara sobre el personaje del jugador al iniciar el turno
    }

    private void loadScene()
    {
        //carga la escena correspondiente
    }
}
