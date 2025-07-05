using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingUIScript : MonoBehaviour
{
    private CharacterController currentCharacter;
    public List<CharacterController> fakeCharacterOrder = new List<CharacterController>();

    [Header("Truck")]
    [SerializeField] public GameObject FirstButton;
    [SerializeField] public GameObject SecondButton;
    [SerializeField] public GameObject ThirdButton;

    void Start()
    {
        fakeCharacterOrder.AddRange(FindObjectsByType<CharacterController>(FindObjectsSortMode.None));
        currentCharacter = fakeCharacterOrder[0];
    }


    void Update()
    {
        
    }

    public void SkipButtonPressed()
    {
        int i = 0;
        CharacterController storedCharacterController = fakeCharacterOrder[0];
        for (i = 0; i < fakeCharacterOrder.Count - 1; i++)
        {
            fakeCharacterOrder[i] = fakeCharacterOrder[i + 1];
        }
        fakeCharacterOrder[fakeCharacterOrder.Count - 1] = storedCharacterController;
        currentCharacter = fakeCharacterOrder[0];

        RevealButtons();
    }

    void RevealButtons()
    {
        var testingValue = currentCharacter.origStats["totalAbilities"];

        switch(testingValue)
        {
            case 1:
                FirstButton.SetActive(true);
                SecondButton.SetActive(false);
                ThirdButton.SetActive(false);
                break;
            case 2:
                FirstButton.SetActive(true);
                SecondButton.SetActive(true);
                ThirdButton.SetActive(false);
                break;
            case 3:
                FirstButton.SetActive(true);
                SecondButton.SetActive(true);
                ThirdButton.SetActive(true);
                break;
        }
    }
}
