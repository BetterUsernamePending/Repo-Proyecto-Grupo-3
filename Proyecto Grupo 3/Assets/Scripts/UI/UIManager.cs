using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject MenuDePausa;

    [SerializeField] public GameObject battleUI;
    [SerializeField] public GameObject p1WinUI;
    [SerializeField] public GameObject p2WinUI;

    [SerializeField] private TurnController turnController;

    [Header("Basic Actions")]
    [SerializeField] public Button Move;
    [SerializeField] public Button Act;
    [SerializeField] public Button Skip;
    public List<Button> MoveActSkip;

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

        AnnouncePlayerTurn();

        MoveActSkip = new List<Button>() { Move, Act, Skip };

        turnController.OnTurnFinished += SetValues;
        SetValues();
        buttonList = new List<GameObject>() { FirstButton, SecondButton, ThirdButton };
    }

    private void Update()
    {
#if PLATFORM_WEBGL
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPause();
        }
#else
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
#endif
    }


    // Funcion para el menu de pausa, devuelve al menu principal al jugador.
    public void MainMenuLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Cuando se apreta Move, se bloquean todos los botones basicos, y viceversa cuando se cancela o se confirma un movimiento.
    public void DeactivateButtons()
    {
        foreach (Button buttons in MoveActSkip)
        {
            buttons.interactable = false;
        }
    }
    public void ReactivateButtons()
    {
        foreach (Button buttons in MoveActSkip)
        {
            buttons.interactable = true;
        }
    }

    public void AnnouncePlayerTurn()
    {
    }

    // Botones de accion. Adicionalmente, se define sus titulos aqui, basandose en el nombre de las habilidades dentro de sus scripts respectivos.
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

    public void SkipButtonPressed()
    {
    }

    // Utilizado en funcion "CharacterMove(List<Block> blockPath)" de CharacterController
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
        isPaused = true;
    }

    // Barras de vida y de mana
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



    public void OnAbilityPressed(int i)
    {
        abilityIndexPressed?.Invoke(i);
    }
    public void OnAbilityCanceled()
    {
        CancelInvoke();
    }

}
