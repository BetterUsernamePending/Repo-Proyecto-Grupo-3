using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject MenuDePausa;
    public GameObject battleUI;
    public GameObject winUI;
    public GameObject p1WinUI;
    public GameObject p2WinUI;
    private static UIManager _instance;
    public static UIManager instance => _instance;
    [SerializeField] private TurnController turnController;

    [Header("Basic Actions")]
    public Button Move;
    public Button Act;
    public Button Skip;
    public List<Button> MoveActSkip;
    public GameObject ongoingMovementPanel;

    public bool isPaused = false;

    [Header("ActionUI")]
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirdButton;
    public static Action<int> abilityIndexPressed;
    private List<GameObject> buttonList;
    public GameObject actionPanel;
    public GameObject actCancelButton;
    public GameObject atkConfirmPanel;
    public GameObject ongoingAbilityConfirmPanel;

    [Header("TurnAnnouncer")]
    [SerializeField] private TextMeshProUGUI AnnounceCurrentPlayer;

    [Header("Bio")]
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider MPBar;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private Image CharacterPortrait;
    [SerializeField] private Image CharacterPortraitBorder;
    [SerializeField] private GameObject Player1Flag;
    [SerializeField] private GameObject Player2Flag;

    [SerializeField] private GameObject selectorPanel;
    [SerializeField] private TextMeshProUGUI playerSelectorPanel;
    [SerializeField] private TextMeshProUGUI playerTurnText;
    [SerializeField] private GameObject selectorPanelDoneButton;
    [SerializeField] public GameObject blackScreen;

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        MenuDePausa.SetActive(false);

        AnnouncePlayerTurn();

        MoveActSkip = new List<Button>() { Move, Act, Skip };

        turnController.OnTurnFinished += SetValues;
        buttonList = new List<GameObject>() { FirstButton, SecondButton, ThirdButton };
        crossfadeTransition();
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

    public void ActivateMovementPanel()
    {
        ongoingMovementPanel.SetActive(true);
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
    public void ReactivateCertainButtons()
    {
        Act.interactable = !TurnController.instance.alreadyAttacked;
        Move.interactable = !TurnController.instance.alreadyMoved;
        Skip.interactable = true;
    }
    public void DeactivateActButtons()
    {
        foreach(GameObject buttons in buttonList)
        {
            buttons.GetComponent<Button>().interactable = false;
        }
    }
    public void ReactivateActButtons()
    {
        foreach (GameObject buttons in buttonList)
        {
            buttons.GetComponent<Button>().interactable = true;
        }
    }
    public void WhenExecutingAbility()
    {
        ongoingAbilityConfirmPanel.SetActive(false);
        Act.interactable = false;
        actCancelButton.SetActive(false);
    }
    public void AnnouncePlayerTurn()
    {

    }
    public void HideActionUI()
    {
        actionPanel.SetActive(false);
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
        winUI.SetActive(true);
        p1WinUI.SetActive(true);
    }
    public void GameOverP2Win()
    {
        winUI.SetActive(true);
        p2WinUI.SetActive(true);
    }
    public void OnPause()
    {
        if (!winUI.activeSelf) // si todavia no termino el juego
        {
            MenuDePausa.SetActive(true);
            isPaused = true;
        }
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
        CharacterPortraitBorder.sprite = CurrentCharacter.PortraitBorder;
        CharacterName.text = CurrentCharacter.PortraitName;
        if (CurrentCharacter.belongsToPlayer == 1)
        {
            Player1Flag.SetActive(true);
            Player2Flag.SetActive(false);
            playerTurnText.text = "Jugador 1";
            playerTurnText.color = CurrentCharacter.teamColor;
        }
        else
        {
            Player1Flag.SetActive(false);
            Player2Flag.SetActive(true);
            playerTurnText.text = "Jugador 2";
            playerTurnText.color = CurrentCharacter.teamColor;
        }
    }

    public void OnAbilityPressed(int i)
    {
        abilityIndexPressed?.Invoke(i);
    }
    public void OnAbilityCanceled()
    {
        CancelInvoke();
    }
    public void DeactivateSelectorPanel()
    {
        selectorPanel.SetActive(false);
        selectorPanelDoneButton.SetActive(true);
        turnController.FirstTurnSetup();
    }
    public void ChangeSelectingPlayer()
    {
        playerSelectorPanel.text = "Jugador 2";
        playerSelectorPanel.color = new Color(0.71f, 0.16f, 0.15f);//Color.red;
    }

    public void crossfadeTransition()
    {
        blackScreen.SetActive(true);
        blackScreen.GetComponent<Image>().DOFade(1, 0.5f).OnComplete(() =>
                {
                    blackScreen.GetComponent<Image>().DOFade(0, 0.75f).OnComplete(() =>
                    {
                        blackScreen.SetActive(false);
                    });
                });
    }

}
