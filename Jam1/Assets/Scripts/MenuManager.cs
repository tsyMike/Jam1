using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{   
    public int gameStartScene;
    public int mainMenuScene;
    public void gameStart(){
        SceneManager.LoadScene(gameStartScene);

    }
    public void QuitGame(){
        Application.Quit();
    }
    public void MainMenu(){
        SceneManager.LoadScene(mainMenuScene);

    }

   
}
