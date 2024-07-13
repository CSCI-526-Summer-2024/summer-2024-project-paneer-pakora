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
    public float timeRemaining = 300;
    private bool timeIsRunning = true;
    public TMP_Text timeText;
    public TMP_Text gameEndText;  //flashes game has ended 
    public GameObject levelClearMenu;
    public GameObject levelFailMenu;
    private FirebaseHandler firebaseHandler;

    void Start()
    {
        DisplayTime(timeRemaining);
        gameEndText.gameObject.SetActive(false);
        levelClearMenu.SetActive(false);
        firebaseHandler = FindObjectOfType<FirebaseHandler>();
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

                if(GridManager.selectedLevel == 1)  // TUt 3
                {
                    Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                else if (GridManager.selectedLevel == 2) //Tutorial 2
                {
                    GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 0) //Tutorial level 1
                {
                    Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 3) //Tutorial level 3
                {
                    Tut3_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 4) // Level 0 (Technically level 1)
                {
                    Level0_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 5) // Level 1 (Technically level 2)
                {
                    Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                //DisplayEndGameText("You Lose!");
                DisplayLevelFailPanel();
                HandleLoseState();
            }

            CheckGameStatus();  //This is called to see if the game should end early based on other conditions
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        //Formatting time for display
        timeToDisplay = Mathf.Max(timeToDisplay, 0);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  //textTime UI element updated for display
    }

    void CheckGameStatus()
    {
        int nonNullCount = UnitManager.Instance.currentStatus.Values.Count(value => value != null);
        int visitedTileCount = UnitManager.Instance.isVisited.Count;
        int totalTileCount = UnitManager.Instance.currentStatus.Count;

        if (nonNullCount == 1 || visitedTileCount == totalTileCount)   //If only one unit is left OR all 19 tiles are visited, the player wins and the level clear message is shown
        {
            timeIsRunning = false;

            if (GridManager.selectedLevel == 1)  // TUt 3
            {
                Tut2_GameManager.Instance.ChangeState(GameState.WinState);
            }
            else if (GridManager.selectedLevel == 2) //Tutorial 2
            {
                GameManager.Instance.ChangeState(GameState.WinState);
            }

            else if (GridManager.selectedLevel == 0) //Tutorial level 1
            {
                Tut1_GameManager.Instance.ChangeState(GameState.WinState);
            }

            else if (GridManager.selectedLevel == 3) //Tutorial level 1
            {
                Tut3_GameManager.Instance.ChangeState(GameState.WinState);
            }

            else if (GridManager.selectedLevel == 4) // Level 0 (Technically level 1)
            {
                Level0_GameManager.Instance.ChangeState(GameState.WinState);
            }
            else if (GridManager.selectedLevel == 5) 
            {
                Level0_5_GameManager.Instance.ChangeState(GameState.WinState);
            }

            //DisplayEndGameText("You Win!");
            HandleWinState();
            DisplayLevelClearPanel();
            return;
        }
        else
        {
            //Checks for various lose conditions and display level failed

            Dictionary<Vector3, BaseUnit> dict = UnitManager.Instance.currentStatus;
            bool onlyLoneIsland = CheckIsOnlyLoneIsland(dict);
            bool onlyPerimeter = CheckIsOnlyPerimeter(dict);  //no valid moves on the perimeter
            bool onlyOnePieceType = CheckIsOnlyOnePiece(dict);
            //bool onlyLoneIslandInterior = CheckLoneIslandInterior(dict);

            if (onlyOnePieceType)
            {
                timeIsRunning = false;
                Debug.Log("In only one piece type lose condition");
                if (GridManager.selectedLevel == 1)  // TUt 3
                {
                    Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                else if (GridManager.selectedLevel == 2) //Tutorial 2
                {
                    GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 0) //Tutorial level 1
                {
                    Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 3) //Tutorial level 3
                {
                    Tut3_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 4) // Level 0 (Technically level 1)
                {
                    Level0_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 5) 
                {
                    Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                //DisplayEndGameText("You Lose!");
                //DisplayLevelFailPanel();
                HandleLoseConditions();
                return;
            }

            if (onlyLoneIsland)
            {
                timeIsRunning = false;
                Debug.Log("In Lone Island lose condition");
                if (GridManager.selectedLevel == 1)  // TUt 3
                {
                    Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                else if (GridManager.selectedLevel == 2) //Tutorial 2
                {
                    GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 0) //Tutorial level 1
                {
                    Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                }
                else if (GridManager.selectedLevel == 3) //Tutorial level 3
                {
                    Tut3_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 4) // Level 0 (Technically level 1)
                {
                    Level0_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                else if (GridManager.selectedLevel == 5) // 
                {
                    Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);
                }

                //DisplayEndGameText("You Lose!");
                //DisplayLevelFailPanel();
                HandleLoseConditions();
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
                    if (GridManager.selectedLevel == 1)  // TUt 3
                    {
                        Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
                    }
                    else if (GridManager.selectedLevel == 2) //Tutorial 2
                    {
                        GameManager.Instance.ChangeState(GameState.LoseState);
                    }

                    else if (GridManager.selectedLevel == 0) //Tutorial level 1
                    {
                        Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
                    }
                    else if (GridManager.selectedLevel == 3) //Tutorial level 1
                    {
                        Tut3_GameManager.Instance.ChangeState(GameState.LoseState);
                    }

                    else if (GridManager.selectedLevel == 4) // Level 0 (Technically level 1)
                    {
                        Level0_GameManager.Instance.ChangeState(GameState.LoseState);
                    }

                    else if (GridManager.selectedLevel == 5) 
                    {
                        Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);
                    }

                    //DisplayEndGameText("You Lose!");
                    //DisplayLevelFailPanel();
                    HandleLoseConditions();
                    return;
                }
            }
            return;
        }
    }

    private void HandleWinState()
    {
        float timeTaken = initialTime - timeRemaining;
        Debug.Log("Level Won in " + timeTaken + " seconds");

        if (firebaseHandler != null)
        {
            firebaseHandler.UpdateSessionStatus("Win", timeTaken);
        }

        if (GridManager.selectedLevel == 1)  // Tutorial level 2
        {
            Tut2_GameManager.Instance.ChangeState(GameState.WinState);
        }
        else if (GridManager.selectedLevel == 2) // Level 1
        {
            GameManager.Instance.ChangeState(GameState.WinState);
        }
        else if (GridManager.selectedLevel == 0) // Tutorial level 1
        {
            Tut1_GameManager.Instance.ChangeState(GameState.WinState);
        }
        else if (GridManager.selectedLevel == 3) // Tutorial level 3
        {
            Tut3_GameManager.Instance.ChangeState(GameState.WinState);
        }
        else if (GridManager.selectedLevel == 4) // Level 0 (Technically level 1)
        {
            Level0_GameManager.Instance.ChangeState(GameState.WinState);
        }
        else if (GridManager.selectedLevel == 5)
        {
            Level0_5_GameManager.Instance.ChangeState(GameState.WinState);
        }
    }

        private void HandleLoseState()
    {
        float timeTaken = initialTime - timeRemaining;
        Debug.Log("Level Lost in " + timeTaken + " seconds");

        if (firebaseHandler != null)
        {
            firebaseHandler.UpdateSessionStatus("Lose", timeTaken);
        }

        if (GridManager.selectedLevel == 1)  // Tutorial level 2
        {
            Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
        }
        else if (GridManager.selectedLevel == 2) // Level 1
        {
            GameManager.Instance.ChangeState(GameState.LoseState);
        }
        else if (GridManager.selectedLevel == 0) // Tutorial level 1
        {
            Tut1_GameManager.Instance.ChangeState(GameState.LoseState);
        }
        else if (GridManager.selectedLevel == 3) // Tutorial level 3
        {
            Tut3_GameManager.Instance.ChangeState(GameState.LoseState);
        }
        else if (GridManager.selectedLevel == 4) // Level 0 (Technically level 1)
        {
            Level0_GameManager.Instance.ChangeState(GameState.LoseState);
        }
        else if (GridManager.selectedLevel == 5)
        {
            Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);
        }

        DisplayLevelFailPanel();
    }

    private void HandleLoseConditions()
    {
        Dictionary<Vector3, BaseUnit> dict = UnitManager.Instance.currentStatus;
        bool onlyLoneIsland = CheckIsOnlyLoneIsland(dict);
        bool onlyPerimeter = CheckIsOnlyPerimeter(dict);  //no valid moves on the perimeter
        bool onlyOnePieceType = CheckIsOnlyOnePiece(dict);

        //if (onlyOnePieceType || onlyLoneIsland || (!onlyPerimeter && !CheckValidMoveOnPerimeter(dict)))
        //{
            timeIsRunning = false;
            HandleLoseState();
        //}
    }

    public void DisplayLevelClearPanel()
    {
        levelClearMenu.SetActive(true);
    }

    public void DisplayLevelFailPanel()
    {
        levelFailMenu.SetActive(true);
    }

    public void DisplayEndGameText(string message)
    {
        gameEndText.text = message;
        gameEndText.gameObject.SetActive(true);
    }



    private bool CheckIsOnlyLoneIsland(Dictionary<Vector3, BaseUnit> dict)   //checks if all the units on the game board are isolated- each unit has no neighbour units
    {
        //If every unit is isolated, return true else return false
        List<Vector3> keys = new List<Vector3>(dict.Keys);  //extract all keys and store em in a lsit

        foreach (Vector3 pos in keys)  //iterarte over each position
        {
            if (dict[pos] != null)  //if position has a unit
            {
                float posX = pos.x;
                float posY = pos.y;
                //Calculate all 6 neighbors
                Vector3 topPos = new Vector3(posX, posY + 1.0f);
                Vector3 topRightPos = new Vector3(posX + 1.5f, posY + 0.5f);
                Vector3 bottomRightPos = new Vector3(posX + 1.5f, posY - 0.5f);
                Vector3 bottomPos = new Vector3(posX, posY - 1.0f);
                Vector3 bottomLeftPos = new Vector3(posX - 1.5f, posY - 0.5f);
                Vector3 topLeftPos = new Vector3(posX - 1.5f, posY + 0.5f);


                //Check if any neighboring position has a unit
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

    private bool CheckIsOnlyPerimeter(Dictionary<Vector3, BaseUnit> dict)  //checks if there are any units on the internal, non-perimeter tiles of the game board
    {

        //We first set the internal tiles here as per the level
        List<Vector3> internalPos = null;
        if (GridManager.selectedLevel == 2)
        { 
            internalPos = new List<Vector3> { new Vector3(0, 1.0f), new Vector3(1.5f, 0.5f), new Vector3(1.5f, -0.5f),
                                                        new Vector3(0, -1.0f), new Vector3(-1.5f, -0.5f), new Vector3(-1.5f, 0.5f),
                                                        new Vector3(0, 0)};
        }
        else if (GridManager.selectedLevel == 1)
        {
            internalPos = new List<Vector3> { new Vector3(-3.0f, -0.5f), new Vector3(0.0f, -0.5f), new Vector3(1.5f, 0.0f),
                                                        new Vector3(3.0f, 0.5f)};
        }
        else if(GridManager.selectedLevel == 0)
        {
            internalPos = new List<Vector3> {};
        }

        else if (GridManager.selectedLevel == 3)
        {
            internalPos = new List<Vector3> { new Vector3(0.0f, 0.0f) };
        }

        else if (GridManager.selectedLevel == 4)
        {
            internalPos = new List<Vector3> { new Vector3(0.0f, 0.0f) };
        }

        else if (GridManager.selectedLevel == 5)
        {
            internalPos = new List<Vector3> { new Vector3(0.0f, 0.0f) , new Vector3(0.0f, 1.0f) };
        }

        //iterate over internal positions
        for (int i = 0; i < internalPos.Count; i++)
        {
            if (dict[internalPos[i]] != null)   //Checks if there is a unit at the current internal position
            {
                if (!IsLonePiece(dict, internalPos[i]))   //Check if the Unit is isolated
                {
                    return false;
                }
            }
        }

        return true;  //return true if all units are isolated
    }



    private bool CheckIsOnlyOnePiece(Dictionary<Vector3, BaseUnit> dict)  //This method is checking if all the units remaining on the board belong to the same typr of faction
    {
        //Input to this fn is a dictionary where keys are positions and values are units at these positions
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


        //Check if only One Faction remains
        if (rockCount == 0 && paperCount == 0 && scissorCount != 0)
        {
            if (GridManager.selectedLevel == 1)
            {
                Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.selectedLevel == 2)
            {
                GameManager.Instance.ChangeState(GameState.LoseState);
            }

            else if (GridManager.selectedLevel == 0)
            {
                Tut1_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 3)
            {
                Tut3_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 4)
            {
                Level0_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 5)
            {
                Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            return true;
        }


        else if (rockCount == 0 && paperCount != 0 && scissorCount == 0)
        {
            if (GridManager.selectedLevel == 1)
            {
                Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.selectedLevel == 2)
            {
                GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.selectedLevel == 0)
            {
                Tut1_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 3)
            {
                Tut3_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 4)
            {
                Level0_GameManager.Instance.ChangeState(GameState.LoseState);

            }
            else if (GridManager.selectedLevel == 5)
            {
                Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            return true;
        }


        else if (rockCount != 0 && paperCount == 0 && scissorCount == 0)
        {
            if (GridManager.selectedLevel == 1)
            {
                Tut2_GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.selectedLevel == 2)
            {
                GameManager.Instance.ChangeState(GameState.LoseState);
            }
            else if (GridManager.selectedLevel == 0)
            {
                Tut1_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 3)
            {
                Tut3_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 4)
            {
                Level0_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            else if (GridManager.selectedLevel == 5)
            {
                Level0_5_GameManager.Instance.ChangeState(GameState.LoseState);

            }

            return true;
        }
        else
        {
        return false;
    }
    }




    private bool CheckValidMoveOnPerimeter(Dictionary<Vector3, BaseUnit> dict)  //This method checks if her is a valid move available for any unit on "perimeter" of the board. There should also be a empty unit being able to jump over another unit to an empty position on the opposite side
    {

        //Define  perimeter tiles  based on the Level

        List<Vector3> perimeterPos = null;
        if (GridManager.selectedLevel == 2)
        {
            perimeterPos = new List<Vector3> { new Vector3(0f, 2.0f), new Vector3(1.5f, 1.5f),
                                                         new Vector3(3.0f, 1.0f), new Vector3(3.0f, 0f),
                                                         new Vector3(3.0f, -1.0f), new Vector3(1.5f, -1.5f),
                                                         new Vector3(0f, -2.0f), new Vector3(-1.5f, -1.5f),
                                                         new Vector3(-3.0f, -1.0f), new Vector3(-3.0f, 0f),
                                                         new Vector3(-3.0f, 1.0f), new Vector3(-1.5f, 1.5f)
                                                        };
        }

        else if ( GridManager.selectedLevel == 1)
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

        else if (GridManager.selectedLevel == 0)
        {
            //perimeterPos = new List<Vector3> { new Vector3(-3.0f, -1.0f), new Vector3(-1.5f, -0.5f),
            //                                   new Vector3( -1.5f, 0.5f), new Vector3(0.0f, -1.0f),
            //                                   new Vector3(0.0f, 0.0f), new Vector3(0.0f, 1.0f),
            //                                   new Vector3(1.5f,-1.5f) };
            //perimeterPos = new List<Vector3> {          new Vector3(0.0f, 0.0f),
            //                                            new Vector3(0.0f, 1.0f),
            //                                             new Vector3(0.0f,-1.0f),
            //                                              new Vector3(1.5f,-1.5f),


            //                                            new Vector3(-1.5f, 0.5f),
            //                                             new Vector3( -1.5f,-0.5f),

            //                                             new Vector3(-3.0f, -1.5f)
            //                                            };
            perimeterPos = new List<Vector3> { new Vector3(0.0f, 1.0f), new Vector3(0.0f, 0.0f),
                                                new Vector3(0.0f, -1.0f), new Vector3(1.5f,-1.5f),
                                                new Vector3(-1.5f,-0.5f), new Vector3(-3.0f,-1.0f),
                                                new Vector3(-1.5f,0.5f)
            };

        }



        else if (GridManager.selectedLevel == 3)
        {
            perimeterPos = new List<Vector3> {           new Vector3(0.0f, -1.0f), new Vector3(0.0f, -2.0f),
                                                         new Vector3(0.0f, 1.0f), new Vector3(1.5f, 0.5f),
                                                         new Vector3(1.5f,-0.5f),new Vector3(1.5f, -1.5f),

                                                         new Vector3(-1.5f,0.5f), new Vector3(-1.5f,1.5f),
                                                         new Vector3(-1.5f,-0.5f), new Vector3(3.0f,-2.0f)

                                                        };
        }

        else if (GridManager.selectedLevel == 4)
        {
            perimeterPos = new List<Vector3> {           new Vector3(-3.0f,0.0f),

                                                         new Vector3(-1.5f, -1.5f), new Vector3(-1.5f, -0.5f), new Vector3(-1.5f, 0.5f),

                                                         new Vector3(0.0f,-2.0f),new Vector3(0.0f, -1.0f),new Vector3(0.0f,1.0f), new Vector3(0.0f,2.0f),


                                                         new Vector3(1.5f,-0.5f), new Vector3(1.5f,0.5f),new Vector3(1.5f,1.5f),

                                                         new Vector3(3.0f,0.0f)

                                                        };
        }


        else if (GridManager.selectedLevel == 5)
        {
            perimeterPos = new List<Vector3> {           

                                                         new Vector3(-1.5f, -1.5f), new Vector3(-1.5f, -0.5f), new Vector3(-1.5f, 0.5f), new Vector3(-1.5f,0.5f),

                                                         new Vector3(0.0f,-1.0f),  new Vector3(0.0f,2.0f),


                                                         new Vector3(1.5f,-0.5f), new Vector3(1.5f,0.5f), new Vector3(1.5f,1.5f), new Vector3(1.5f,-1.5f),

                                                         new Vector3(3.0f,0.0f)

                                                        };
        }

        //Iterate over perimeter tiles


        for (int i=0; i< perimeterPos.Count; i++)
        {
            if (dict[perimeterPos[i]]!= null)
            {
                float posX = perimeterPos[i].x;
                float posY = perimeterPos[i].y;


                //iterate over neighbors, calculate the position on opposite side of the neighbour to check if it is empty
                Vector3 topPos = new Vector3(posX, posY + 1.0f);
                Vector3 topRightPos = new Vector3(posX + 1.5f, posY + 0.5f);
                Vector3 bottomRightPos = new Vector3(posX + 1.5f, posY - 0.5f);
                Vector3 bottomPos = new Vector3(posX, posY - 1.0f);
                Vector3 bottomLeftPos = new Vector3(posX - 1.5f, posY - 0.5f);
                Vector3 topLeftPos = new Vector3(posX - 1.5f, posY + 0.5f);

                List<Vector3> potentialPos = new List<Vector3>{ topPos, topRightPos, bottomRightPos,
                    bottomPos, bottomLeftPos, topLeftPos };

                for (int j=0; j<potentialPos.Count; j++)   
                {
                    Vector3 emptyPos = new Vector3(2 * potentialPos[j].x - perimeterPos[i].x,
                                                    2*potentialPos[j].y - perimeterPos[i].y);

                    if(dict.ContainsKey(potentialPos[j]) && dict[potentialPos[j]] != null && dict.ContainsKey(emptyPos) && dict[emptyPos] == null)
                    {
                        if (isMovePossible(dict, perimeterPos[i], potentialPos[j]))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;

            //for (int i = 0; i < perimeterPos.Count - 1; i = i + 2)
            //{
            //    if (i < 10)
            //    {
            //        if (dict[perimeterPos[i]] != null && dict[perimeterPos[i + 1]] != null && dict[perimeterPos[i + 2]] == null)
            //        {
            //            if (isMovePossible(dict, perimeterPos[i], perimeterPos[i + 1]))
            //            {
            //                return true;
            //            }
            //        }
            //    }

            //    else
            //    {
            //        if (dict[perimeterPos[i]] != null && dict[perimeterPos[i + 1]] != null && dict[perimeterPos[0]] == null)
            //        {
            //            if (isMovePossible(dict, perimeterPos[i], perimeterPos[i + 1]))
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //}

        //for (int i = perimeterPos.Count - 2; i >= 0; i = i - 2)
        //        {
        //    if (i > 1)
        //    {
        //        if (dict[perimeterPos[i]] != null && dict[perimeterPos[i - 1]] != null && dict[perimeterPos[i - 2]] == null)
        //        {
        //            if (isMovePossible(dict, perimeterPos[i], perimeterPos[i - 1]))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    else
        //    {
        //        if (dict[perimeterPos[i]] != null && dict[perimeterPos[11]] != null && dict[perimeterPos[10]] == null)
        //        {
        //            if (isMovePossible(dict, perimeterPos[i], perimeterPos[perimeterPos.Count - 1]))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //}
        //return false; 
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



    private bool CheckLoneIslandInterior(Dictionary<Vector3, BaseUnit> dict)  //checks if all units at specific internal (non peim) positions on the game board are isolated
    {

        //We are defining internal poisitons based on level here:
        List<Vector3> internalPos = null;
        if (GridManager.selectedLevel == 2)
        {
            internalPos = new List<Vector3> { new Vector3(0, 1.0f), new Vector3(1.5f, 0.5f), new Vector3(1.5f, -0.5f),
                                                        new Vector3(0, -1.0f), new Vector3(-1.5f, -0.5f), new Vector3(-1.5f, 0.5f),
                                                        new Vector3(0, 0)};
        }
        else if (GridManager.selectedLevel == 1)
        {
            internalPos = new List<Vector3> { new Vector3(-3.0f, -0.5f), new Vector3(0.0f, -0.5f), new Vector3(1.5f, 0.0f),
                                                        new Vector3(3.0f, 0.5f)};
        }
        else if (GridManager.selectedLevel == 0)
        {

            internalPos = new List<Vector3> { };
        }

        else if (GridManager.selectedLevel == 3)
        {

            internalPos = new List<Vector3> { new Vector3(0.0f, 0.0f) };
        }



        else if (GridManager.selectedLevel == 5)
        {

            internalPos = new List<Vector3> { new Vector3(0.0f, 0.0f), new Vector3(0.0f, 1.0f) };
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





    private bool IsLonePiece(Dictionary<Vector3, BaseUnit> dict, Vector3 pos)   //method wghich checks if a unit at a given position is isolated
    {
        float posX = pos.x;
        float posY = pos.y;

        //Calculate 6 neighbors
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