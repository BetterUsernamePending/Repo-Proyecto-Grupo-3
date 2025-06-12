using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
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
    public Block currentBlock; //bloque en el que está parado el personaje
    public Block targetBlock;
    [SerializeField] private LayerMask Triggers;

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
        
    }
    private void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name);
        currentBlock = GetComponent<Block>();
    }

    public void CharacterMove(List<Block> blockPath)
    {
        currentBlock.characterOnBlock = null;
        targetBlock = blockPath.Last();
        Vector3[] blockPositions = new Vector3[blockPath.Count];
        for (int i = 0; i < blockPath.Count; i++) 
        {
            float ypos = blockPath[i].height + 1.5f;
            blockPositions[i] = new Vector3(blockPath[i].transform.position.x,ypos, blockPath[i].transform.position.z);
        }
        transform.DOPath(blockPositions, blockPath.Count)
            .OnComplete(()=>
            { 
                Reposition();
               
            });

    }

    public void Reposition()
    {

        currentBlock = targetBlock;
        currentBlock.characterOnBlock = this;
        targetBlock = null;
        Debug.Log(name);
        
    }
}