using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public float initialTime = 300;
    public float timeRemaining = 180;  
    private bool timeIsRunning = true;
    public TMP_Text timeText;
    public TMP_Text gameEndText;

    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

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
                if(GridManager.Instance.selectedLevel == 1)  // TUt 3
                {
                    Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                else if (GridManager.Instance.selectedLevel == 2) //Tutorial 2
                {
                    GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.Instance.selectedLevel == 0) //Tutorial level 1
                {
                    Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                }
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
        int nonNullCount = UnitManager.Instance.currentStatus.Values.Count(value => value != null);
        int visitedTileCount = UnitManager.Instance.isVisited.Count;
        if (nonNullCount == 1 || visitedTileCount == 19)
        {
            timeIsRunning = false;

            if (GridManager.Instance.selectedLevel == 1)  // TUt 3
            {
                Tut2_GameManager.Instance.ChangeState(GameState.WinState);
            }
            else if (GridManager.Instance.selectedLevel == 2) //Tutorial 2
            {
                GameManager.Instance.ChangeState(GameState.WinState);
            }

            else if (GridManager.Instance.selectedLevel == 0) //Tutorial level 1
            {
                Tut1_GameManager.Instance.ChangeState(GameState.WinState);
            }
            DisplayEndGameText("You Win!");
            return;
        }

        else
        {

            Dictionary<Vector3, BaseUnit> dict = UnitManager.Instance.currentStatus;
            bool onlyLoneIsland = CheckIsOnlyLoneIsland(dict);
            bool onlyPerimeter = CheckIsOnlyPerimeter(dict);
            bool onlyOnePieceType = CheckIsOnlyOnePiece(dict);
            //bool onlyLoneIslandInterior = CheckLoneIslandInterior(dict);
        
            if (onlyOnePieceType)
            {
                timeIsRunning = false;
                Debug.Log("In only one piece type lose condition");
                if (GridManager.Instance.selectedLevel == 1)  // TUt 3
                {
                    Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                else if (GridManager.Instance.selectedLevel == 2) //Tutorial 2
                {
                    GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.Instance.selectedLevel == 0) //Tutorial level 1
                {
                    Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                DisplayEndGameText("You Lose!");
                return;
            }

            if (onlyLoneIsland)
            {
                timeIsRunning = false;
                Debug.Log("In Lone Island lose condition");
                if (GridManager.Instance.selectedLevel == 1)  // TUt 3
                {
                    Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                else if (GridManager.Instance.selectedLevel == 2) //Tutorial 2
                {
                    GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.Instance.selectedLevel == 0) //Tutorial level 1
                {
                    Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                DisplayEndGameText("You Lose!");
                return;
            }

            if (onlyPerimeter)
            {
                if (CheckValidMoveOnPerimeter(dict))
                {
                    return;
                }
                else
                {
                    timeIsRunning = false;
                    Debug.Log("In no valid perimeter move condition");
                    if (GridManager.Instance.selectedLevel == 1)  // TUt 3
                    {
                        Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                    }
                    else if (GridManager.Instance.selectedLevel == 2) //Tutorial 2
                    {
                        GameManager.Instance.ChangeState(GameState.LoseState);
                    }

                    else if (GridManager.Instance.selectedLevel == 0) //Tutorial level 1
                    {
                        Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                    }
                    DisplayEndGameText("You Lose!");
                    return;
                }
            }

            return;
        }
    }

    public void DisplayEndGameText(string message)
    {
        gameEndText.text = message;
        gameEndText.gameObject.SetActive(true);
    }

    private bool CheckIsOnlyLoneIsland(Dictionary<Vector3, BaseUnit> dict)
    {
        List<Vector3> keys = new List<Vector3>(dict.Keys);

        foreach (Vector3 pos in keys)
        {
            if (dict[pos] != null)
            {
                float posX = pos.x;
                float posY = pos.y;

                Vector3 topPos = new Vector3(posX, posY + 1.0f);
                Vector3 topRightPos = new Vector3(posX + 1.5f, posY + 0.5f);
                Vector3 bottomRightPos = new Vector3(posX + 1.5f, posY - 0.5f);
                Vector3 bottomPos = new Vector3(posX, posY - 1.0f);
                Vector3 bottomLeftPos = new Vector3(posX - 1.5f, posY - 0.5f);
                Vector3 topLeftPos = new Vector3(posX - 1.5f, posY + 0.5f);

                if ((keys.Contains(topPos) && dict[topPos] != null) ||
                    (keys.Contains(topRightPos) && dict[topRightPos] != null) ||
                    (keys.Contains(bottomRightPos) && dict[bottomRightPos] != null) ||
                    (keys.Contains(bottomPos) && dict[bottomPos] != null) ||
                    (keys.Contains(bottomLeftPos) && dict[bottomLeftPos] != null) ||
                    (keys.Contains(topLeftPos) && dict[topLeftPos] != null))
            {
                // Debug.Log($"Unit at {pos} ({unit.Faction}) has a valid move.");
                return false;
            }
        }
        }

        return true;
    }

    private bool CheckIsOnlyPerimeter(Dictionary<Vector3, BaseUnit> dict)
    {
        List<Vector3> internalPos = null;
        if (GridManager.Instance.selectedLevel == 2)
        {
            internalPos = new List<Vector3> { new Vector3(0, 1.0f), new Vector3(1.5f, 0.5f), new Vector3(1.5f, -0.5f),
                                                        new Vector3(0, -1.0f), new Vector3(-1.5f, -0.5f), new Vector3(-1.5f, 0.5f),
                                                        new Vector3(0, 0)};
        }
        else if (GridManager.Instance.selectedLevel == 1)
        {
            internalPos = new List<Vector3> { new Vector3(-3.0f, -0.5f), new Vector3(0.0f, -0.5f), new Vector3(1.5f, 0.0f),
                                                        new Vector3(3.0f, 0.5f)};
        }
        else if(GridManager.Instance.selectedLevel == 0)
        {
            internalPos = new List<Vector3> {};
        }
        

        for (int i = 0; i < internalPos.Count; i++)
        {
            if (dict[internalPos[i]] != null)
            {
                if (!IsLonePiece(dict, internalPos[i]))
                {
                    return false;
                }
            }
        }

                return true;
            }

    private bool CheckIsOnlyOnePiece(Dictionary<Vector3, BaseUnit> dict)
    {

        int paperCount = 0;
        int scissorCount = 0;
        int rockCount = 0;

        foreach (Vector3 key in dict.Keys)
        {

            if (dict[key] != null && dict[key].Faction == Faction.Paper)
            {
                paperCount += 1;
        }

            else if (dict[key] != null && dict[key].Faction == Faction.Rock)
            {
                rockCount += 1;
            }
            else if (dict[key] != null && dict[key].Faction == Faction.Scissor)
            {
                scissorCount += 1;
            }
        }

        if (rockCount == 0 && paperCount == 0 && scissorCount != 0)
        {
            if (GridManager.Instance.selectedLevel == 1)
            {
                Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.Instance.selectedLevel == 2)
            {
                GameManager.Instance.ChangeState(GameState.LoseState);
            }

            else if (GridManager.Instance.selectedLevel == 0)
            {
                Tut1_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            return true;
        }
        else if (rockCount == 0 && paperCount != 0 && scissorCount == 0)
        {
            if (GridManager.Instance.selectedLevel == 1)
            {
                Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.Instance.selectedLevel == 2)
            {
                GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.Instance.selectedLevel == 0)
            {
                Tut1_GameManager.Instance.ChangeState(GameState.LoseState);

            }
            return true;
        }
        else if (rockCount != 0 && paperCount == 0 && scissorCount == 0)
        {
            if (GridManager.Instance.selectedLevel == 1)
            {
                Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.Instance.selectedLevel == 2)
            {
                GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.Instance.selectedLevel == 0)
            {
                Tut1_GameManager.Instance.ChangeState(GameState.LoseState);

            }
            return true;
        }
        else
        {
        return false;
    }
    }

    private bool CheckValidMoveOnPerimeter(Dictionary<Vector3, BaseUnit> dict)
    {
        List<Vector3> perimeterPos = null;
        if (GridManager.Instance.selectedLevel == 2)
        {
            perimeterPos = new List<Vector3> { new Vector3(0f, 2.0f), new Vector3(1.5f, 1.5f),
                                                         new Vector3(3.0f, 1.0f), new Vector3(3.0f, 0f),
                                                         new Vector3(3.0f, -1.0f), new Vector3(1.5f, -1.5f),
                                                         new Vector3(0f, -2.0f), new Vector3(-1.5f, -1.5f),
                                                         new Vector3(-3.0f, -1.0f), new Vector3(-3.0f, 0f),
                                                         new Vector3(-3.0f, 1.0f), new Vector3(-1.5f, 1.5f)
                                                        };
        }

        else if ( GridManager.Instance.selectedLevel == 1)
        {
            perimeterPos = new List<Vector3> { new Vector3(-4.5f, -1.0f), new Vector3(-4.5f, 0.0f),
                                                         new Vector3(-3.0f, -1.5f), new Vector3(-3.0f, 0.5f),
                                                         new Vector3(-1.5f, -1.0f), new Vector3(-1.5f, 0.0f),
                                                         new Vector3(0f, -1.5f), new Vector3(0f, 0.5f),
                                                         new Vector3(1.5f, -1.0f), new Vector3(1.5f, 1.0f),
                                                         new Vector3(3.0f, -0.5f), new Vector3(3.0f, 1.5f),
                                                         new Vector3(4.5f, 0.0f), new Vector3(4.5f, 1.0f)
                                                        };
        }

        else if (GridManager.Instance.selectedLevel == 0)
        {
            perimeterPos = new List<Vector3> {          new Vector3(0.0f, 0.0f),
                                                        new Vector3(0.0f, 1.0f),
                                                         new Vector3(0.0f,-1.0f),
                                                          new Vector3(1.5f,-1.5f),
                                                         

                                                        new Vector3(-1.5f, 0.5f),
                                                         new Vector3( -1.5f,-0.5f),
               
                                                         new Vector3(-3.0f, -1.5f)
                                                        };
        }




            for (int i = 0; i < perimeterPos.Count - 1; i = i + 2)
        {
            if (i < 10)
            {
                if (dict[perimeterPos[i]] != null && dict[perimeterPos[i + 1]] != null && dict[perimeterPos[i + 2]] == null)
                {
                    if (isMovePossible(dict, perimeterPos[i], perimeterPos[i + 1]))
                    {
                        return true;
                    }
                }
            }

            else
            {
                if (dict[perimeterPos[i]] != null && dict[perimeterPos[i + 1]] != null && dict[perimeterPos[0]] == null)
                {
                    if (isMovePossible(dict, perimeterPos[i], perimeterPos[i + 1]))
                    {
                        return true;
                    }
                }
            }
        }

        for (int i = perimeterPos.Count - 2; i >= 0; i = i - 2)
                {
            if (i > 1)
            {
                if (dict[perimeterPos[i]] != null && dict[perimeterPos[i - 1]] != null && dict[perimeterPos[i - 2]] == null)
                {
                    if (isMovePossible(dict, perimeterPos[i], perimeterPos[i - 1]))
                    {
                        return true;
                    }
                }
            }

            else
            {
                if (dict[perimeterPos[i]] != null && dict[perimeterPos[11]] != null && dict[perimeterPos[10]] == null)
                {
                    if (isMovePossible(dict, perimeterPos[i], perimeterPos[perimeterPos.Count - 1]))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool isMovePossible(Dictionary<Vector3, BaseUnit> dict, Vector3 pos1, Vector3 pos2)
    {
        if (dict[pos1].Faction == Faction.Rock)
        {
            if (dict[pos2].Faction == Faction.Scissor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        else if (dict[pos1].Faction == Faction.Scissor)
        {
            if (dict[pos2].Faction == Faction.Paper)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        else
        {
            if (dict[pos2].Faction == Faction.Rock)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private bool CheckLoneIslandInterior(Dictionary<Vector3, BaseUnit> dict)
    {
        List<Vector3> internalPos = null;
        if (GridManager.Instance.selectedLevel == 2)
        {
            internalPos = new List<Vector3> { new Vector3(0, 1.0f), new Vector3(1.5f, 0.5f), new Vector3(1.5f, -0.5f),
                                                        new Vector3(0, -1.0f), new Vector3(-1.5f, -0.5f), new Vector3(-1.5f, 0.5f),
                                                        new Vector3(0, 0)};
        }
        else if (GridManager.Instance.selectedLevel == 1)
        {
            internalPos = new List<Vector3> { new Vector3(-3.0f, -0.5f), new Vector3(0.0f, -0.5f), new Vector3(1.5f, 0.0f),
                                                        new Vector3(3.0f, 0.5f)};
        }
        else if (GridManager.Instance.selectedLevel == 0)
        {

            internalPos = new List<Vector3> { };
        }

            for (int i = 0; i < internalPos.Count; i++)
        {

            Vector3 pos = internalPos[i];

            if (dict[pos] != null)
            {
                float posX = pos.x;
                float posY = pos.y;

                Vector3 topPos = new Vector3(posX, posY + 1.0f);
                Vector3 topRightPos = new Vector3(posX + 1.5f, posY + 0.5f);
                Vector3 bottomRightPos = new Vector3(posX + 1.5f, posY - 0.5f);
                Vector3 bottomPos = new Vector3(posX, posY - 1.0f);
                Vector3 bottomLeftPos = new Vector3(posX - 1.5f, posY - 0.5f);
                Vector3 topLeftPos = new Vector3(posX - 1.5f, posY + 0.5f);

                if ((dict[topPos] != null) ||
                    (dict[topRightPos] != null) ||
                    (dict[bottomRightPos] != null) ||
                    (dict[bottomPos] != null) ||
                    (dict[bottomLeftPos] != null) ||
                    (dict[topLeftPos] != null))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsLonePiece(Dictionary<Vector3, BaseUnit> dict, Vector3 pos)
    {
        float posX = pos.x;
        float posY = pos.y;

        Vector3 topPos = new Vector3(posX, posY + 1.0f);
        Vector3 topRightPos = new Vector3(posX + 1.5f, posY + 0.5f);
        Vector3 bottomRightPos = new Vector3(posX + 1.5f, posY - 0.5f);
        Vector3 bottomPos = new Vector3(posX, posY - 1.0f);
        Vector3 bottomLeftPos = new Vector3(posX - 1.5f, posY - 0.5f);
        Vector3 topLeftPos = new Vector3(posX - 1.5f, posY + 0.5f);

        if ((dict[topPos] != null) ||
            (dict[topRightPos] != null) ||
            (dict[bottomRightPos] != null) ||
            (dict[bottomPos] != null) ||
            (dict[bottomLeftPos] != null) ||
            (dict[topLeftPos] != null))
        {
            return false;
        }

        return true;
    }
}