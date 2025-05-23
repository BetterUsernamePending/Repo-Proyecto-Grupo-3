using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    public int atk;
    public int def;
    public int spd;
    public int hp;
    public int mp;
    public int jump;
    public int dist;
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

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity,Triggers))
        {
            currentBlock = hit.collider.gameObject.GetComponent<Block>();
        }
    }
    private void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name);
        currentBlock = GetComponent<Block>();
    }
}