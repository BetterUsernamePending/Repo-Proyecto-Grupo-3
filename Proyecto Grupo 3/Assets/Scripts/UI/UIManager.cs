using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameObject MenuDePausa;
    public GameObject PausePanel;

    [SerializeField] public GameObject battleUI;
    [SerializeField] public GameObject p1WinUI;
    [SerializeField] public GameObject p2WinUI;
    [SerializeField] private TurnController turnController;


    public bool isPaused = false;

    [Header("ActionUI")]
    [SerializeField] public GameObject FirstButton;
    [SerializeField] public GameObject SecondButton;
    [SerializeField] public GameObject ThirdButton;

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


    public void RevealButtons()
    {
        var testingValue = TurnController.currentCharacter.origStats["totalAbilities"];

        switch (testingValue)
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

    public void DeactivateBattleUI()
    {
        battleUI.SetActive(false);
    }
    public void ActivateBattleUI()
    {
        battleUI.SetActive(true);
    }
    private void OnExitButtonPressed()
    {

    }
    private void OnResumeButtonPressed()
    {

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


    public void OnButtonPressed()
    {

    }
    public void HealthBarManagement()
    {

    }
    /* UI DINAMICA */
}
