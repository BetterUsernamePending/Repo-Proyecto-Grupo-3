using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public int atk;
    public int def;
    public int spd;
    public int hp;
    public int mp;
    public int jump;
    public int dist;
    public int range;
    public int attackHeight;
    public Block currentBlock; //bloque en el que está parado el personaje
    public Block targetBlock;
    [SerializeField] private LayerMask LayerToFind;
    [SerializeField] public UIManager uiManager;
    public bool isMoving = false;
    public bool isAlive = true;

    //Pathfinding.showPossible(currentBlock,dist,jump); Funcion que usa los valores del personaje para mostrar las casillas posibles. Desactivada actualmente por testeo (los personajes no están implementados)
    /* List<Block> Pathfinding.findPath(Block currentBlock, Block targetBlock,int jump)
     {
         //ignorar esto
     }
     private void moveCharacter(Block targetBlock)
     {
         return null; //placeholder. Mueve fisicamente la posición del personaje
     }*/

    private void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerToFind))
        {
            currentBlock = hit.collider.gameObject.GetComponent<Block>();
            Debug.Log("character" + " " + this.name + " " + "standing in " + currentBlock.name);
        }
        uiManager = FindAnyObjectByType<UIManager>();
    }
    /*private void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name);
        currentBlock = GetComponent<Block>();
    }*/

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
        transform.DOPath(blockPositions, blockPath.Count)
            .OnComplete(() =>
            {
                Reposition();
                uiManager.ActivateBattleUI();
            });
    }
    public void IsDead()
    {
        isAlive = false;
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