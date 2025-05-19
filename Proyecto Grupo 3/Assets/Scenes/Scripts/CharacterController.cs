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
    public Block currentBlock;//bloque en el que está parado el personaje

    //Pathfinding.showPossible(currentBlock,dist,jump); Funcion que usa los valores del personaje para mostrar las casillas posibles. Desactivada actualmente por testeo (los personajes no están implementados)
    void Update()
    {
        
    }
}
