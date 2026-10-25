
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepDie : MonoBehaviour
{
    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Sheep died!");

        Destroy(gameObject);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
