using UnityEngine;


public class UIManager : MonoBehaviour
{
    public GameObject MenuDePausa;
    public GameObject PausePanel;

    [SerializeField] public GameObject battleUI;
    [SerializeField] public GameObject p1WinUI;
    [SerializeField] public GameObject p2WinUI;
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
}
