using UnityEngine;


public class UIManager : MonoBehaviour
{
    public GameObject MenuDePausa;
    public GameObject PausePanel;

    public GameObject darkenMove;
    public GameObject darkenAct;
    public GameObject darkenSkip;

    public GameObject cancelMove;
    public GameObject moveButton;
    [SerializeField] public GameObject battleUI;
    [SerializeField] private TurnController turnController;
    public bool isPaused = false;
    private CharacterController CurrentCharacter;

    private void Start ()
    {
        MenuDePausa.SetActive(false);
        CurrentCharacter = TurnController.currentCharacter; 
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveButton.SetActive(true);
            cancelMove.SetActive(false);

            darkenMove.SetActive(true);
            darkenAct.SetActive(false);
            darkenSkip.SetActive(false);
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
}
