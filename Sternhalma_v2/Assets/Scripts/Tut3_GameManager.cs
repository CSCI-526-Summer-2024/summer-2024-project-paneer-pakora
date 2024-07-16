using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class Tut3_GameManager : MonoBehaviour
{
    public static int retryCount = 0;
    public static string userId = "user_unique_id";
    public static Tut3_GameManager Instance;

    public GameState GameState;
    FirebaseHandler firebaseHandler;
    Timer timer;

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
        firebaseHandler = FindObjectOfType<FirebaseHandler>();

        //DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        GridManager.selectedLevel = 3;
        ChangeState(GameState.GenerateGrid);
        //ChangeState(GameState.MainMenu);
        //ChangeState(GameState.Tutorial2);
        firebaseHandler = FindObjectOfType<FirebaseHandler>();
        timer = FindObjectOfType<Timer>();
        UnityEngine.Debug.Log("FirebaseHandler found: " + (firebaseHandler != null));
        UnityEngine.Debug.Log("Timer found: " + (timer != null));
        UnityEngine.Debug.Log("Current Level at Start: " + MenuManager.currentLevel);

    }

    public void ChangeState(GameState newState)
    {
        //Debug.Log($"Changing state from {GameState} to {newState}");

        GameState = newState;
        switch (newState)
        {
            //case GameState.MainMenu:
            //    break;
            case GameState.GenerateGrid:
                //Debug.Log("Before SetActive : " + GridManager.Instance.rotateButton.activeSelf);
                //GridManager.Instance.rotateButton.SetActive(true);
                //Debug.Log("After SetActive : " + GridManager.Instance.rotateButton.activeSelf);

                // Check if the object is active in the hierarchy
                //Debug.Log("Active In Hierarchy: " + GridManager.Instance.rotateButton.activeInHierarchy);
                GenerateHexGrid(GridManager.Instance.hexSize, GridManager.Instance.posTile, GridManager.Instance.posTranslator, GridManager.Instance.hexPrefab);
                //GridManager.Instance.rotateButton.SetActive(false);
                break;
            //case GameState.Tutorial2:
            //    GridManager.Instance.GenerateTutorial2_Grid();
            //    break;
            case GameState.SpawnObjects:
                SpawnObjects(UnitManager.Instance.currentStatus, UnitManager.Instance.tileToUnit, UnitManager.Instance.isVisited);
                //UnitManager.Instance.SpawnObjects();
                //UnitManager.Instance.SpawnTutorial2Objects();
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.WinState:
                HandleWinState();
                Debug.Log("Player Wins!");
                //SendAnalyticsEvent("win");
                break;
            case GameState.LoseState:
                HandleLoseState();
                Debug.Log("Player Loses!");
                //SendAnalyticsEvent("lose");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleWinState()
    {
        UnityEngine.Debug.Log("Player Wins!");
        Debug.Log("timeTaken " + timer.initialTime + " " + timer.timeRemaining);
        float timeTaken = timer.initialTime - timer.timeRemaining;
        firebaseHandler.UpdateSessionStatus("Win", timeTaken);
    }


    private void HandleLoseState()
    {
        UnityEngine.Debug.Log("Player Loses!");
        float timeTaken = timer.initialTime - timer.timeRemaining;
        firebaseHandler.UpdateSessionStatus("Lose", timeTaken);
    }

    private void LogTime(string result)
    {
        if (timer != null)
        {
            float timeTaken = timer.initialTime - timer.timeRemaining;
            UnityEngine.Debug.Log("Level: " + MenuManager.currentLevel + " Result: " + result + " Time Taken: " + timeTaken + " seconds");
        }
        else
        {
            UnityEngine.Debug.Log("Timer not found!");
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

    public void GenerateHexGrid(float hexSize, Dictionary<Vector3, HexTile> posTile, Dictionary<Vector3, Vector3> posTranslator, HexTile hexPrefab)
    {
        float hexWidth = hexSize + 0.1f;
        float hexHeight = hexSize * Mathf.Sqrt(3) + 0.1f;

        for (float x = -1.5f; x <= 3.0f; x += 1.5f) //overall range of x
        {
            if (x == 0)
            {
                for (int y = -1; y <= 1; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    if (y == 0)
                    {
                        hex.isRotatable = true;
                    }
                    else
                    {
                        hex.isRotatable = false;
                    }
                }
            }

            else if (x == 1.5f)
            {
                for (float y = -1.5f; y <= 0.5f; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);
                    hex.isRotatable = false;

                }
            }

            else  if(x==-1.5f)
            {
                for (float y = -0.5f; y <= 1.5f; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    hex.isRotatable = false;
                }
            }

            else     //when x=3
            {
                int y = -2;
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    hex.isRotatable = false;
                }
            }


        } //for ends here
        GridManager.Instance.posTranslator = posTranslator;
        GridManager.Instance.posTile = posTile;
        Tut3_GameManager.Instance.ChangeState(GameState.SpawnObjects);
    }


    public void SpawnObjects(Dictionary<Vector3, BaseUnit> currentStatus, Dictionary<HexTile, BaseUnit> tileToUnit, HashSet<HexTile> isVisited)
    {
        // Initialize the currentStatus dictionary with null values
        for (float x = -1.5f; x <=3.0f; x += 1.5f)  //range of x
        {
            if (x == 0)
            {
                for (int y = -1; y <= 1; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == 1.5f)
            {
                for (float y = -1.5f; y <= 0.5f; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == -1.5f)
            {
                for (float y = -0.5f; y <= 1.5f; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }


            else
            {

                int y = -2;
                    currentStatus[new Vector3(x, y)] = null;
                
            }




        } //for 

        // Spawn scissor units
        //List<Vector3> scissorList = new List<Vector3> { new Vector3(0, 0) };
        //List<Vector3> scissorList = new List<Vector3> { new Vector3(-3.0f, 1.0f), new Vector3(0, -1.0f) };

        List<Vector3> scissorList = new List<Vector3> { new Vector3(-1.5f, -0.5f), new Vector3(1.5f,-1.5f) };
        var scissorCount = scissorList.Count;
        UnitManager.Instance.currentScissorCount = scissorCount;

        for (int i = 0; i < scissorCount; i++)
        {
            var scissorPrefab = UnitManager.Instance.GetUnit<Scissor>(Faction.Scissor);
            BaseUnit spawnedScissor = Instantiate(scissorPrefab);
            HexTile scissorTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(scissorList[i]));

            scissorTile.SetUnit(spawnedScissor);
            tileToUnit[scissorTile] = spawnedScissor;
            currentStatus[scissorList[i]] = spawnedScissor;
            isVisited.Add(scissorTile);
            scissorTile.SetColorToGreen();

        }

        // Spawn rock units
        //List<Vector3> rockList = new List<Vector3> { new Vector3(1.5f, -0.5f), new Vector3(-1.5f, -0.5f) };
        //List<Vector3> rockList = new List<Vector3> { new Vector3(-1.5f, -1.5f), new Vector3(-1.5f, 0.5f), new Vector3(0.0f, 1.0f), new Vector3(1.5f, -0.5f), new Vector3(3.0f, 0) };

        List<Vector3> rockList = new List<Vector3> { new Vector3(0.0f, 0.0f), new Vector3(1.5f, -0.5f) };
        var rockCount = rockList.Count;
        UnitManager.Instance.currentRockCount = rockCount;

        for (int i = 0; i < rockCount; i++)
        {
            var rockPrefab = UnitManager.Instance.GetUnit<Rock>(Faction.Rock);
            BaseUnit spawnedRock = Instantiate(rockPrefab);
            HexTile rockTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(rockList[i]));
            rockTile.SetUnit(spawnedRock);
            tileToUnit[rockTile] = spawnedRock;
            currentStatus[rockList[i]] = spawnedRock;
            isVisited.Add(rockTile);
            rockTile.SetColorToGreen();

        }

        // Spawn paper units
        //List<Vector3> paperList = new List<Vector3> { new Vector3(1.5f, 0.5f), new Vector3(0, -1), new Vector3(-1.5f, 0.5f) };
        //List<Vector3> paperList = new List<Vector3> { new Vector3(-3.0f, 0f), new Vector3(-1.5f, 1.5f), new Vector3(1.5f, 1.5f), new Vector3(1.5f, -1.5f), new Vector3(3.0f, -1.0f) };
        List<Vector3> paperList = new List<Vector3> { new Vector3(0.0f,1.0f), new Vector3(0.0f, -1.0f), new Vector3(-1.5f, 0.5f) };
        var paperCount = paperList.Count;
        UnitManager.Instance.currentPaperCount = paperCount;

        for (int i = 0; i < paperCount; i++)
        {
            var paperPrefab = UnitManager.Instance.GetUnit<Paper>(Faction.Paper);

            BaseUnit spawnedPaper = Instantiate(paperPrefab);
            HexTile paperTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(paperList[i]));

            paperTile.SetUnit(spawnedPaper);
            tileToUnit[paperTile] = spawnedPaper;
            currentStatus[paperList[i]] = spawnedPaper;
            isVisited.Add(paperTile);
            paperTile.SetColorToGreen();

        }

        // set tile coverage progress meter
        var tileCount = currentStatus.Count;
        var coveredTileCount = isVisited.Count;
        UnitManager.Instance.tileCoverageMeter.SetMaxProgress(tileCount);
        UnitManager.Instance.tileCoverageMeter.SetProgress(coveredTileCount);

        // set # pieces removed progress meter
        UnitManager.Instance.pieceCount = scissorCount + rockCount + paperCount;
        UnitManager.Instance.piecesRemoved = 0;                                      // start with all pieces on board; none removed
        UnitManager.Instance.piecesRemovedMeter.SetMaxProgress(UnitManager.Instance.pieceCount - 1);      // win condition requires 1 piece remaining
        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);

        UnitManager.Instance.rocksLeft.text = UnitManager.Instance.currentRockCount.ToString();
        UnitManager.Instance.papersLeft.text = UnitManager.Instance.currentPaperCount.ToString();
        UnitManager.Instance.scissorsLeft.text = UnitManager.Instance.currentScissorCount.ToString();

        // Change the game state to PlayerTurn after spawning objects
        Tut3_GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }
}


