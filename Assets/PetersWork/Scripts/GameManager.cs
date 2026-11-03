using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{   
    public GameObject PauseObject;
    public GameObject WinObject;
    public GameObject LoseObject;
    public static bool GameIsPaused = false;
    private bool GameEnded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseObject.SetActive(false);
        WinObject.SetActive(false);
        LoseObject.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        GameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameEnded)
        {
            return;
        }
                //if(Input.GetKeyDown(KeyCode.Escape)) old
        if(Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(GameIsPaused)
            {
                ReturnToGame();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        if(GameEnded)
        {
            return;
        }
        PauseObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void ReturnToGame() 
    {
        if(GameEnded)
        {
            return;
        }
        // Resume
        PauseObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }
    public void WinScene()
    {
        if (GameEnded){
            return;
        }
        GameEnded = true;
        WinObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoseScene() //gameOver
    {
        if(GameEnded)
        {
            return;
        }
        GameEnded = true;
        LoseObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PlayGame()
    {
        // main game scene. replace with real name of the game scene
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
