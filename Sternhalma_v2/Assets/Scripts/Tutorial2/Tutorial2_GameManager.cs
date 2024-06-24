using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class Tutorial2_GameManager : MonoBehaviour
{
    public static Tutorial2_GameManager Instance;
    public GameObject rotateButton;
    public GameState GameState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
        //ChangeState(GameState.MainMenu);
        //ChangeState(GameState.Tutorial2);

    }

    public void ChangeState(GameState newState)
    {
        Debug.Log($"Changing state from {GameState} to {newState}");

        GameState = newState;
        switch (newState)
        {
            //case GameState.MainMenu:
            //    break;
            case GameState.GenerateGrid:
                //Debug.Log("GenerateGrid.Instance is: ");
                //Debug.Log(Tutorial2_GridManager.Instance);
                Tutorial2_GridManager.Instance.GenerateHexGrid();
                Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
                break;
            case GameState.SpawnObjects:
                Tutorial2_UnitManager.Instance.SpawnObjects();
                //Tutorial2_UnitManager.Instance.SpawnTutorial2Objects();
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.WinState:
                Debug.Log("Player Wins!");
                SendAnalyticsEvent("win");
                break;
            case GameState.LoseState:
                Debug.Log("Player Loses!");
                SendAnalyticsEvent("lose");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void SendAnalyticsEvent(string result)
    {
        float timeRemaining = Tutorial2_Timer.Instance.timeRemaining;
        float totalTime = Tutorial2_Timer.Instance.initialTime;
        float timeTaken = totalTime - timeRemaining;

        var parameters = new Dictionary<string, object>
        {
            { "result", result },
            { "timeRemaining", timeRemaining },
            { "timeTaken", timeTaken }
        };

        AnalyticsService.Instance.CustomData("level_complete", parameters);
        AnalyticsService.Instance.Flush();
    }
}

//public enum GameState
//{
//    GenerateGrid = 0,
//    SpawnObjects = 1,
//    PlayerTurn = 2,
//    WinState = 3,
//    LoseState = 4,
//    Tutorial2 = 5
//    //    ,
//    //MainMenu = 6
//}
