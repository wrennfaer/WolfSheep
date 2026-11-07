using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class SceneManagement : MonoBehaviour
{
    public GameObject MainObject;
    public GameObject HTPObject;
    public GameObject CreditsObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainObject.SetActive(true);   
        HTPObject.SetActive(false);
        CreditsObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
    // public void Pause()
    // {
    //     PauseObject.SetActive(true);
    //     Time.timeScale = 0f;
    //     GameIsPaused = true;
    // }
    // public void ReturnToGame() 
    // {
    //     // Resume
    //     PauseObject.SetActive(false);
    //     Time.timeScale = 1f;
    //     GameIsPaused = false;
        
    // }
    public void CreditsScene()
    {
        CreditsObject.SetActive(true);
        MainObject.SetActive(false);
        HTPObject.SetActive(false);
    }
    public void HowToPlayScene() 
    {
        HTPObject.SetActive(true);
        MainObject.SetActive(false);
        CreditsObject.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
