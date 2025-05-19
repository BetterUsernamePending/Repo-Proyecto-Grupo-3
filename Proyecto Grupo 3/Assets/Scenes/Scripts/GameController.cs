using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] public Block initBlock;

    void Start()
    {
        StartCoroutine(Delay()); 
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Pathfinding.showPossible(initBlock, 5, 4);
    }
}
