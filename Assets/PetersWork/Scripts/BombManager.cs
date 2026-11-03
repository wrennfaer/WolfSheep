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
        //Debug.Log("CollectBomb called!");
        if(GameManager.GameIsPaused)
        {
            return;
        }
        bombCount++;
        //Debug.Log("Bombs collected: " + bombCount);
        UpdateBombText();
        if(bombCount >= bombsNeeded)
        {
            //Debug.Log("WIN CONDITION REACHED!");
            FindAnyObjectByType<GameManager>().WinScene();
        }
    }

    void UpdateBombText()
    {
        bombText.text = ": "+ bombCount.ToString();
    }

}
