using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject rotateButton;

    //public static GameManager Instance;
    //public string sceneName;

    //public void PlayGame()
    //{
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    SceneManager.LoadScene("Level1");
    //    //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    //}

    public void Tutorial2()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Tutorial2");
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void Level1()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Level1");
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //public void PlayGame()
    //{
    //    UnityEngine.Debug.Log("PlayGame pushed");
    //    StartCoroutine(LoadSceneAndGenerateGrid());
    //}

    //private IEnumerator LoadSceneAndGenerateGrid()
    //{
    //    UnityEngine.Debug.Log("Async Load started");
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1");

    //    while (!asyncLoad.isDone)
    //    {
    //        UnityEngine.Debug.Log("asyncLoad is NOT Done");
    //        yield return null;
    //    }

    //    UnityEngine.Debug.Log("asyncLoad is Done");
    //    //if (GridManager.Instance != null)
    //    //{
    //    //    UnityEngine.Debug.Log("GridManager.Instance exists!");
    //    //    GridManager.Instance.GenerateHexGrid();
    //    //    UnityEngine.Debug.Log("After GridManager.Instance.GenerateHexGrid()");
    //    //}
    //    //else
    //    //{
    //    //    UnityEngine.Debug.Log("GridManager instance not found!");
    //    //}
    //    if (GameManager.Instance != null)
    //    {
    //        GameManager.Instance.ChangeState(GameState.GenerateGrid);
    //    }
    //    else
    //    {
    //        UnityEngine.Debug.Log("GameManager instance not found!");
    //    }
    //}

    public void QuitGame()
    {
        UnityEngine.Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
