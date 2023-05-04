using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Demo1()
    {
        SceneManager.LoadScene("Demo1");
    }

    public void Demo2()
    {
        SceneManager.LoadScene("Demo2");
    }

    public void Demo3()
    {
        SceneManager.LoadScene("Demo3");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
}
