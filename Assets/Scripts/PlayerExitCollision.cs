using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerExitCollision : MonoBehaviour
{
    string winningScene = "WinMenu";
    private Rigidbody playerRb; // You declared this in Start but didn't define it

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("EndWall")) {
            GameOver();
        }
    }
    
    void GameOver()
    {
        //Debug.Log("won");
       SceneManager.LoadScene(winningScene);
    }
    
}