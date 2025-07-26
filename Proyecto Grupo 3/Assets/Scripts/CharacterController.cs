using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public int belongsToPlayer;
    public Block currentBlock; //bloque en el que est� parado el personaje
    public Block targetBlock;
    private LayerMask layerToFind;
    private UIManager uiManager;
    public bool isMoving = false;
    public bool isAlive = true;
    public List<AbilityClass> abilityList = new List<AbilityClass>();
    public Dictionary<string,int> origStats = new Dictionary<string,int>();
    public Dictionary<string, int> currentStats = new Dictionary<string, int>();
    public Sprite Portrait;
    public string PortraitName;
    public int atk=1;
    public int def = 1;
    public int hp = 1;
    public int mp = 1;
    public int movedist = 1;
    public int atkrange = 1;
    public int jump = 1;
    public int spd = 1;
    public int atkheight = 1;


    public Animator animator; // animaciones

    private void Start()
    {
        layerToFind= LayerMask.GetMask("BottomLayer");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerToFind))
        {
            currentBlock = hit.collider.gameObject.GetComponent<Block>();
            Debug.Log("character" + " " + this.name + " " + "standing in " + currentBlock.name);
        }
        uiManager = FindAnyObjectByType<UIManager>();
        switch (belongsToPlayer)
        {
            case 1:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.blue);
                break;
            case 2:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.red);
                break;
            case 3:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.green);
                break;
            case 4:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.yellow);
                break;
        }
        animator = GetComponentInChildren<Animator>(); // animaciones
    }
    public void CharacterMove(List<Block> blockPath)
    {
        uiManager.DeactivateBattleUI();
        currentBlock.characterOnBlock = null;
        targetBlock = blockPath.Last();
        Vector3[] blockPositions = new Vector3[blockPath.Count];
        for (int i = 0; i < blockPath.Count; i++)
        {
            float ypos = blockPath[i].height + 1.5f;
            blockPositions[i] = new Vector3(blockPath[i].transform.position.x, ypos, blockPath[i].transform.position.z);
        }
        animator.SetBool("isMoving", true);
        transform.DOPath(blockPositions, blockPath.Count/2)
            .OnComplete(() =>
            {
                Reposition();
                animator.SetBool("isMoving", false); // animaciones
                uiManager.ActivateBattleUI();
            });
    }
    public void IsDead()
    {
        this.isAlive = false;
        this.gameObject.SetActive(false);
    }
    public void Reposition()
    {
        currentBlock.containsCharacter = false;
        currentBlock = targetBlock;
        currentBlock.characterOnBlock = this;
        targetBlock = null;
        Debug.Log(this.name + "new position is " + currentBlock.name);
    }

    public void LockBlock()
    {
        currentBlock.containsCharacter = true;
    }
}