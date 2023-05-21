using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //загрузка сцени наступною по індексу
    }
    public void ExitGame()
    {
        Debug.Log("гра закрилась");
        Application.Quit();
    }
}
