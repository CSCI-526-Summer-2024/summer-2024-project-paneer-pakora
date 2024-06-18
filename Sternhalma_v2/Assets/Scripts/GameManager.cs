using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
        GameManager.Instance.rotateButton.SetActive(false);
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log($"Changing state from {GameState} to {newState}");

        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateHexGrid();
                break;
            case GameState.SpawnObjects:
                UnitManager.Instance.SpawnObjects();
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
        float timeRemaining = Timer.Instance.timeRemaining;
        float totalTime = Timer.Instance.initialTime;
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

public enum GameState
{
    GenerateGrid = 0,
    SpawnObjects = 1,
    PlayerTurn = 2,
    WinState = 3,
    LoseState = 4
}
