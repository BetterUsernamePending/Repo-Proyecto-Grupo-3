using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject blackScreen;

    public void Play()
    {

        blackScreen.GetComponent<Image>().DOFade(0,0.1f).OnComplete(() =>
            {
                blackScreen.SetActive(true);
                blackScreen.GetComponent<Image>().DOFade(1,0.75f).OnComplete(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                });
            });
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
