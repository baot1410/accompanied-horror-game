using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieCollision : MonoBehaviour
{
    string losingScene = "LoseMenu";
    private Rigidbody playerRb; // You declared this in Start but didn't define it

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Monster"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(losingScene);
    }

}