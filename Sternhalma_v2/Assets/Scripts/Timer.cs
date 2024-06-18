using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public float timeRemaining = 180;  
    public float initialTime = 180;    
    private bool timeIsRunning = true;
    public TMP_Text timeText;
    public TMP_Text gameEndText;

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

    void Start()
    {
        DisplayTime(timeRemaining);
        gameEndText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timeIsRunning = false;
                GameManager.Instance.ChangeState(GameState.LoseState);
                DisplayEndGameText("You Lose!");
            }

            CheckGameStatus();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckGameStatus()
    {
        int rockCount = GameObject.FindGameObjectsWithTag("Rock").Length;
        int paperCount = GameObject.FindGameObjectsWithTag("Paper").Length;
        int scissorCount = GameObject.FindGameObjectsWithTag("Scissors").Length;

        bool oneTypeLeft = (rockCount == 0 && paperCount == 0 && scissorCount == 1) ||
                           (rockCount == 0 && paperCount == 1 && scissorCount == 0) ||
                           (rockCount == 1 && paperCount == 0 && scissorCount == 0);

        bool allTilesVisited = UnitManager.Instance.isVisited.Count == UnitManager.Instance.currentStatus.Count;

        if (oneTypeLeft || allTilesVisited )
        {
            Debug.Log("One type of unit left and all tiles visited.");
            timeIsRunning = false;
            GameManager.Instance.ChangeState(GameState.WinState);
            DisplayEndGameText("You Win!");
        }
    }

    public void DisplayEndGameText(string message)
    {
        gameEndText.text = message;
        gameEndText.gameObject.SetActive(true);
    }

    bool AllUnitsHaveNoValidMoves()
    {
        foreach (var kvp in UnitManager.Instance.currentStatus)
        {
            Vector3 pos = kvp.Key;
            BaseUnit unit = kvp.Value;

            if (unit != null && HasValidMoveForUnit(pos, unit))
            {
                Debug.Log($"Unit at {pos} ({unit.Faction}) has a valid move.");
                return false;
            }
        }
        Debug.Log("No units have valid moves.");
        return true;
    }

    bool HasValidMoveForUnit(Vector3 pos, BaseUnit unit)
    {
        Vector3[] directions = {
            new Vector3(1.5f, 0.5f), new Vector3(1.5f, -0.5f),
            new Vector3(-1.5f, 0.5f), new Vector3(-1.5f, -0.5f),
            new Vector3(3.0f, 1.0f), new Vector3(3.0f, -1.0f),
            new Vector3(-3.0f, 1.0f), new Vector3(-3.0f, -1.0f)
        };

        foreach (var dir in directions)
        {
            Vector3 targetPos = pos + dir;
            Debug.Log($"Checking move from {pos} to {targetPos} for {unit.Faction}.");
            if (IsValidMove(pos, targetPos, unit.Faction))
            {
                Debug.Log($"Valid move found for {unit.Faction} at {pos} to {targetPos}.");
                return true;
            }
        }
        Debug.Log($"No valid moves found for {unit.Faction} at {pos}.");
        return false;
    }

    bool IsValidMove(Vector3 fromPos, Vector3 toPos, Faction faction)
    {
        Debug.Log($"Checking move from {fromPos} to {toPos} for {faction}.");

        if (UnitManager.Instance.currentStatus.ContainsKey(toPos))
        {
            if (UnitManager.Instance.currentStatus[toPos] == null)
            {
                Debug.Log($"Target tile at {toPos} is empty.");

                Vector3 midPos = (fromPos + toPos) / 2;
                Debug.Log($"Calculated mid position: {midPos}");

                if (UnitManager.Instance.currentStatus.ContainsKey(midPos))
                {
                    BaseUnit midUnit = UnitManager.Instance.currentStatus[midPos];
                    if (midUnit != null)
                    {
                        Debug.Log($"Mid unit at {midPos} is a {midUnit.Faction}.");
                        if ((faction == Faction.Rock && midUnit.Faction == Faction.Scissor) ||
                            (faction == Faction.Paper && midUnit.Faction == Faction.Rock) ||
                            (faction == Faction.Scissor && midUnit.Faction == Faction.Paper))
                        {
                            Debug.Log($"Valid capture move for {faction} from {fromPos} to {toPos} over {midUnit.Faction}.");
                            return true;
                        }
                        else
                        {
                            Debug.Log($"Mid unit at {midPos} is not capturable by {faction}. It's a {midUnit.Faction}.");
                        }
                    }
                    else
                    {
                        Debug.Log($"No mid unit at {midPos} to capture.");
                    }
                }
                else
                {
                    Debug.Log($"Mid position {midPos} not within bounds.");
                }
            }
            else
            {
                Debug.Log($"Target tile at {toPos} is not empty.");
            }
        }
        else
        {
            Debug.Log($"Target position {toPos} not within bounds.");
        }

        Debug.Log($"Invalid move for {faction} from {fromPos} to {toPos}.");
        return false;
    }
}
