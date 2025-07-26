using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject MenuDePausa;
    public GameObject PausePanel;

    [SerializeField] public GameObject battleUI;
    [SerializeField] public GameObject p1WinUI;
    [SerializeField] public GameObject p2WinUI;
    [SerializeField] private TurnController turnController;

    [SerializeField] public Button SkipButton;
    [SerializeField] public Button ActButton;
    [SerializeField] public Button MoveButton;


    public bool isPaused = false;

    [Header("ActionUI")]
    [SerializeField] public GameObject FirstButton;
    [SerializeField] public GameObject SecondButton;
    [SerializeField] public GameObject ThirdButton;
    public static Action<int> abilityIndexPressed;
    private List<GameObject> buttonList;

    [Header("TurnAnnouncer")]
    [SerializeField] private TextMeshProUGUI AnnounceCurrentPlayer;

    [Header("Bio")]
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider MPBar;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private Image CharacterPortrait;

    private void Start()
    {
        MenuDePausa.SetActive(false);

        turnController.OnTurnFinished += SetValues;
        SetValues();
        buttonList = new List<GameObject>() { FirstButton, SecondButton, ThirdButton };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                OnResume();
            }
            else
            {
                OnPause();
            }
        }
    }
    public void DarkenButtons()
    {
        SkipButton.interactable = false;
    }
    public void RevealButtons()
    {
        foreach (GameObject button in buttonList)
        {
            button.SetActive(false);
        }
        var abilityCount = TurnController.currentCharacter.abilityList.Count;
        for (int i = 0; i < abilityCount; i++)
        {
            buttonList[i].GetComponentInChildren<TextMeshProUGUI>().text = TurnController.currentCharacter.abilityList[i]._name;
            buttonList[i].SetActive(true);
        }
    }
    public void DeactivateBattleUI()
    {
        battleUI.SetActive(false);
    }
    public void ActivateBattleUI()
    {
        battleUI.SetActive(true);
    }
    public void GameOverP1Win()
    {
        p1WinUI.SetActive(true);
    }
    public void GameOverP2Win()
    {
        p2WinUI.SetActive(true);
    }
    public void OnPause()
    {
        MenuDePausa.SetActive(true);
        PausePanel.SetActive(true);
        isPaused = true;
    }
    public void OnResume()
    {
        MenuDePausa.SetActive(false);
        PausePanel.SetActive(false);
        isPaused = false;
    }

    /* UI DINAMICA */
    public void SetValues()
    {
        var CurrentCharacter = TurnController.currentCharacter;

        HealthBar.maxValue = CurrentCharacter.origStats["hp"];
        MPBar.maxValue = CurrentCharacter.origStats["mp"];

        HealthBar.value = CurrentCharacter.currentStats["hp"];
        MPBar.value = CurrentCharacter.currentStats["mp"];

        CharacterPortrait.sprite = CurrentCharacter.Portrait;
        CharacterName.text = CurrentCharacter.PortraitName;
    }
    public void refreshValues()
    {

    }


    public void OnAbilityPressed(int i)
    {
        abilityIndexPressed?.Invoke(i);
    }
    public void HealthBarManagement()
    {

    }
    /* UI DINAMICA */
}
