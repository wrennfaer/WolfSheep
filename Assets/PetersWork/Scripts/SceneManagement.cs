using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class SceneManagement : MonoBehaviour
{
    public GameObject PauseObject;
    public GameObject HTPObject;
    public GameObject CreditsObject;
    public GameObject MainObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseObject.SetActive(false);
        HTPObject.SetActive(false);
        CreditsObject.SetActive(false);
        MainObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape)) old
        if(Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseObject.SetActive(true);
            HTPObject.SetActive(false);
            CreditsObject.SetActive(false);
            MainObject.SetActive(false);
        }
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
    public void Pause()
    {
        PauseObject.SetActive(true);
    }
    public void ReturnToGame() 
    {
        //Pause Screen
        PauseObject.SetActive(false);
        
    }
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
