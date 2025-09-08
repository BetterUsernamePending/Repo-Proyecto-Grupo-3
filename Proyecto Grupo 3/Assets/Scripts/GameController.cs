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
    Block block;
    List<Block> blocksInScene;
 MovementController movementController;
    BattleController battleController;
    TurnController turnController;
    CameraBrainController cameraBrainController;
    AbilityController abilityController;
    void Start()
    {
        battleController = FindAnyObjectByType<BattleController>();
        turnController = FindAnyObjectByType<TurnController>();
        cameraBrainController = FindAnyObjectByType<CameraBrainController>();
        blocksInScene = FindObjectsByType<Block>(FindObjectsSortMode.InstanceID).ToList();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            GetClickedBlock();
        if (turnController.matchStarted && Input.GetKeyDown(KeyCode.Tab))
        {
            CameraBrainController.cameraAngle++;
            cameraBrainController.LookAtCurrent();
        }
    }
    public void GetClickedBlock()
    {
        LayerMask blockLayer = LayerMask.GetMask("BottomLayer");
        bool overUI = EventSystem.current.IsPointerOverGameObject();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, blockLayer) && !overUI)
        {
            Block.onBlockClicked?.Invoke(hit.transform.GetComponent<Block>());
            Debug.Log(hit.transform.name);
        }
    }
    public void RefreshBlockInfo()
    {
        foreach(Block block in blocksInScene)
        {
            blocksInScene = FindObjectsByType<Block>(FindObjectsSortMode.InstanceID).ToList();
            block.DetectCharacter();
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
    public void LoadBattleState() //Attack button pressed
    {
        battleController = new BattleController();
        battleController.OnStateEnter();
    }
    public void CancelBattleState() //ACT cancelled
    {
        battleController.OnStateCancel();
    }
    public void ExecuteAttack() //ACT confirmed
    {
        battleController.ExecuteAttack();
    }
    public void CancelAbility()
    {
        abilityController.OnStateCancel();
    }
    public void ExecuteAbility(int i) //ACT button pressed
    {
        abilityController = new AbilityController();
        abilityController.OnStateEnter(i);
    }
    public void LaunchAbility()
    {
        abilityController.ExecuteAction();
        UIManager.instance.HideActionUI();

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
                UIManager.instance.GameOverP1Win();
                Debug.Log("Ganó el jugador 1");
            }
            else
            {
                UIManager.instance.GameOverP2Win();
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
