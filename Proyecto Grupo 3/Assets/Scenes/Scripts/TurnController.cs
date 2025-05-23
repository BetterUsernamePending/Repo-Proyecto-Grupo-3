using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public bool currentp1 = true;
    public static CharacterController currentCharacter; //Personaje de quien es el turno actual.
    private List<CharacterController> playerCharacterController = new List<CharacterController>(); //Lista de personajes. Se utiliza para definir el orden

    private void Start()
    {
        playerCharacterController.AddRange(FindObjectsByType<CharacterController>(FindObjectsSortMode.None));
        currentCharacter = playerCharacterController[0];
    }
}
 
    /*
    private void startTurn(bool currentp1)
    {
        //comienza el turno y carga la UI correspondiente
    }
    private void passTurn(bool currentp1)
    {
        //modificar el bool para cambiar de turno
    }

    private bool turnOrder(List order)
    {
        //ordena en una lista a los personajes segun su velocidad, para determinar el orden de turnos
    }
    IEnumerator turnTimer()
    {
        //timer del turno. Al terminarse el tiempo, el turno se saltea.
    }
   */
