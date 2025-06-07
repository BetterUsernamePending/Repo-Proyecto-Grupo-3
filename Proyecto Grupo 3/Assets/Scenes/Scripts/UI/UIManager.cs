using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MenuDePausa;
    public GameObject PausePanel;
    public bool isPaused = false;

    private void Start ()
    {
        MenuDePausa.SetActive(false);
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
