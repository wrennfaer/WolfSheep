using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombManager : MonoBehaviour
{
    public int bombCount; 
    public int bombsNeeded = 5;
    public TextMeshProUGUI bombText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    //"Bombs: "
    void Update()
    {
        UpdateBombText();
    } 
    public void CollectBomb()
    {
        if(GameManager.GameIsPaused)
        {
            return;
        }
        bombCount++;
        UpdateBombText();
        if(bombCount >= bombNeeded)
        {

            FindObjectOfType<GameManager>().WinScene();
        }
    }

    void UpdateBombText()
    {
        bombText.text = ": "+ bombCount.ToString();
    }

}
