using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Interaction : MonoBehaviour

{
    string game_Scene = "Map_Hosp1";
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        //Debug.Log("adad");
        SceneManager.LoadScene(game_Scene);
    }
    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void RestartGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(game_Scene);
    }
}
