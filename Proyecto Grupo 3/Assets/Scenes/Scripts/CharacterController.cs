using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int atk;
    public int def;
    public int spd;
    public int hp;
    public int mp;
    public int jump;
    public int dist;
    public Block currentBlock;
    public Block targetBlock;//bloque en el que está parado el personaje

    //Pathfinding.showPossible(currentBlock,dist,jump); Funcion que usa los valores del personaje para mostrar las casillas posibles. Desactivada actualmente por testeo (los personajes no están implementados)
   /* List<Block> Pathfinding.findPath(Block currentBlock, Block targetBlock,int jump)
    {
        //ignorar esto
    }
    private void moveCharacter(Block targetBlock)
    {
        return null; //placeholder. Mueve fisicamente la posición del personaje
    }*/
}
