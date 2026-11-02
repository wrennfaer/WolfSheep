using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombManager : MonoBehaviour
{
    public int bombCount;
    public TextMeshProUGUI bombText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bombText.text = "Bombs: " + bombCount.ToString();
    }

}
