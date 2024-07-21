using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public static string currentLevel = "MainMenu";
   


    //public static GameManager Instance;
    //public string sceneName;

    //public void PlayGame()
    //{
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    SceneManager.LoadScene("Level1");
    //    //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    //}

    public void NextLevel()
    {
        currentLevel =  SceneManager.GetSceneByBuildIndex( SceneManager.GetActiveScene().buildIndex + 1).name;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void MainMenu()
    {
        currentLevel = "MainMenu";
        SceneManager.LoadScene("MainMenu");
        UnityEngine.Debug.Log("Current Level: " + currentLevel);

    }

    public void LevelSelect()
    {
        currentLevel = "LevelSelect";
        SceneManager.LoadScene("LevelSelect");
        UnityEngine.Debug.Log("Current Level: " + currentLevel);

    }

    public void Tutorial1()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Tutorial1");
        currentLevel = "Tutorial1";
        UnityEngine.Debug.Log("Tutorial 1: " + currentLevel);
        SceneManager.LoadScene("Tutorial1");
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void Tutorial2()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        currentLevel = "Tutorial2";
        UnityEngine.Debug.Log("Tutorial 2 Method Called");
        UnityEngine.Debug.Log("Tutorial 2: " + currentLevel);
        SceneManager.LoadScene("Tutorial2");
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void Tutorial3()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        currentLevel = "Tutorial3";
        SceneManager.LoadScene("Tutorial3");
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void Level1()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        currentLevel = "Level1";
        SceneManager.LoadScene("Level0");
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void Level2()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        currentLevel = "Level2";
        SceneManager.LoadScene("Level0_5");
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void Level3()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        currentLevel = "Level3";
        SceneManager.LoadScene("Level1");
    
        //GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void Level0_25()
    {
        currentLevel = "Level0_25";
        SceneManager.LoadScene("Level0_25");
    }

    public void Level8()
    {
        currentLevel = "Level8";
        SceneManager.LoadScene("Level8");
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

}
