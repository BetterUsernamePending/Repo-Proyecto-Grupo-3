using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [SerializeField] public Block initBlock;
    public GameObject OngoingMovement;

    MovementController movementController;
    BattleController battleController;
    TurnController turnController;
    UIManager uiManager;
    void Start()
    {
       //StartCoroutine(Delay());
       battleController = FindAnyObjectByType<BattleController>();
       turnController = FindAnyObjectByType<TurnController>();
       uiManager = FindAnyObjectByType<UIManager>();
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
        LayerMask blockLayer = LayerMask.GetMask("BottomLayer");
        bool overUI = EventSystem.current.IsPointerOverGameObject();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,blockLayer) && !overUI)
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

    public void CheckIfGameOver()
    {
        int checkerp1 = 0;
        int checkerp2 = 0;
        foreach (CharacterController character in turnController.characterOrder)
        {
            if (character.isAlive)
            {
                switch (character.belongsToPlayer)
                {
                    case 1:
                        checkerp1++;
                        break;
                    case 2:
                        checkerp2++;
                        break;
                }
            }
        }
        if (checkerp1 == 0 || checkerp2 == 0)
        {
            if (checkerp1 > 0)
            {
                uiManager.GameOverP1Win();
                Debug.Log("Ganó el jugador 1");
            }
            else
            {
                uiManager.GameOverP2Win();
                Debug.Log("Ganó el jugador 2");
            }
        }
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
