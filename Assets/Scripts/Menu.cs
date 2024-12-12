using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void replay(string sceneName)
    {
        Debug.Log("replay");
        SceneManager.LoadScene(sceneName);

    }

    public void MainMenu(string sceneName)
    {
        Debug.Log("start");
        SceneManager.LoadScene(sceneName);

    }

    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Enable(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void Disable(GameObject obj)
    {
        obj.SetActive(false);
    }
}
