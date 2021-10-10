using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public static void EndGame()
    {
        SceneManager.LoadScene("EndScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
