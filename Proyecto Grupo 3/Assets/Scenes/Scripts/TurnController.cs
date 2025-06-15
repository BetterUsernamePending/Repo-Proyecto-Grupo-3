using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public bool currentp1 = true;
    public static CharacterController currentCharacter; //Personaje de quien es el turno actual.
    private CharacterController currentCharacterType; //Qué personaje es el que está seleccionado
    private List<CharacterController> characterOrder = new List<CharacterController>(); //Lista de personajes. Se utiliza para definir el orden
    private CharacterController storedCharacterController;

    private void Awake()
    {
        characterOrder.AddRange(FindObjectsByType<CharacterController>(FindObjectsSortMode.None));
        currentCharacter = characterOrder[0];   
    }

    private void TurnBegin()
    {
        ActivateBattleUI(); 

    }
    private void ActivateBattleUI()
    {
        //acá tiene que estar el codigo que active la UI en pantalla
    }

    private void ActivateActionUI()
    {
        //hacer un switch con un case para cada tipo de personaje. Cada case debe activar la actionUI del personaje correspondiente (en "currentCharacterType")
        //acá tiene que estar el codigo que active la UI en pantalla ESPECIFICA de las acciones cada personaje
    }
    private void DeactivateBattleUI()
    {
        //acá tiene que estar el codigo que DESACTIVE la UI en pantalla
    }
    public void passTurn()
    {
        storedCharacterController = characterOrder[0];

        for(int i = 0; i < characterOrder.Count - 1; i++)
        {
            characterOrder[i] = characterOrder[i+1];
        }
        characterOrder[characterOrder.Count - 1] = storedCharacterController;
        currentCharacter = characterOrder[0];
        Debug.Log(currentCharacter.name);
    }

    /*
    private void startTurn(bool currentp1)
    {
        //comienza el turno y carga la UI correspondiente
    }

    private bool turnOrder(List order)
    {
        //ordena en una lista a los personajes segun su velocidad, para determinar el orden de turnos
    }
    IEnumerator turnTimer()
    {
        //timer del turno. Al terminarse el tiempo, el turno se saltea.
    }*/
}
