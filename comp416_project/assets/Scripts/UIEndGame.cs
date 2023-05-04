using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndGame : MonoBehaviour
{
    public GameObject endGameScreen;
    public GameObject UI;
    public GameObject UIPause;
    public GameObject victoryText;
    public GameObject victoryImage;
    public GameObject defeatText;
    public GameObject defeatImage;

    public static UIEndGame instance;

    private string sceneName;

    void Awake ()
    {
        instance = this;
    }

    public void EndGame(bool victory)
    {   
        UI.SetActive(false);
        UIPause.SetActive(false);
        endGameScreen.SetActive(true);

        victoryText.SetActive(victory);
        victoryImage.SetActive(victory);

        defeatText.SetActive(!victory);
        defeatImage.SetActive(!victory);
    }

    public void Restart()
    {   
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        
        if (sceneName == "GameScene")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");
        }
        if (sceneName == "Demo1")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Demo1");
        }
        if (sceneName == "Demo2")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Demo2");
        }
        if (sceneName == "Demo3")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Demo3");
        }
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
