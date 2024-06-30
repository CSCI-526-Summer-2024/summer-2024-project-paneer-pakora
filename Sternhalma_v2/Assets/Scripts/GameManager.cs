using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

        //DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        //GridManager.Instance.selectedLevel = 2;
        GridManager.Instance.setSelectedLevel(2);
        Debug.Log("Selected level set to: ");
        GridManager.Instance.printSelectedLevel();
        ChangeState(GameState.GenerateGrid);
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
                GenerateHexGrid(GridManager.Instance.hexSize, GridManager.Instance.posTile, GridManager.Instance.posTranslator, GridManager.Instance.hexPrefab);
                //GridManager.Instance.rotateButton.SetActive(false);
                break;
            case GameState.SpawnObjects:
                SpawnObjects(UnitManager.Instance.currentStatus, UnitManager.Instance.tileToUnit, UnitManager.Instance.isVisited);
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.WinState:
                Debug.Log("Player Wins!");
                //SendAnalyticsEvent("win");
                break;
            case GameState.LoseState:
                Debug.Log("Player Loses!");
                //SendAnalyticsEvent("lose");
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

    public void GenerateHexGrid(float hexSize, Dictionary<Vector3, HexTile>  posTile, Dictionary<Vector3, Vector3> posTranslator, HexTile hexPrefab)
    {
        float hexWidth = hexSize + 0.1f;
        float hexHeight = hexSize * Mathf.Sqrt(3) + 0.1f;

        //posTile = new Dictionary<Vector3, HexTile>();
        //posTranslator = new Dictionary<Vector3, Vector3>();

        for (float x = -3; x <= 3; x += 1.5f)
        {
            Debug.Log("at x = " + x);
            if (x == -3.0f || x == 3.0f)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Debug.Log("at y = " + y);
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator.Add(new Vector3(x, y, 0), new Vector3(xPos, yPos, 0));
                    Debug.Log("PosTranslator length: " + posTranslator.Count);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    hex.isRotatable = false;
                }
            }

            else if (x == -1.5f || x == 1.5f)
            {
                for (float y = -1.5f; y <= 1.5f; y++)
                {
                    Debug.Log("at y = " + y);
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator.Add(new Vector3(x, y, 0), new Vector3(xPos, yPos, 0));
                    Debug.Log("PosTranslator length inside loop: " + posTranslator.Count);
                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    if (y == -1.5f || y == 1.5f)
                    {
                        hex.isRotatable = false;
                    }
                    else
                    {
                        hex.isRotatable = true;
                    }
                }
            }

            else
            {
                for (int y = -2; y <= 2; y++)
                {
                    Debug.Log("at y = " + y);
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator.Add(new Vector3(x, y, 0), new Vector3(xPos, yPos, 0));
                    Debug.Log("PosTranslator length: " + posTranslator.Count);
                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    if (y == -2f || y == 2)
                    {
                        hex.isRotatable = false;
                    }
                    else
                    {
                        hex.isRotatable = true;
                    }
                }
            }
        }
        Debug.Log("PosTranslator length: " + posTranslator.Count);
        GridManager.Instance.posTranslator = posTranslator;
        GridManager.Instance.posTile = posTile;
        GameManager.Instance.ChangeState(GameState.SpawnObjects);
    }


    public void SpawnObjects(Dictionary<Vector3, BaseUnit> currentStatus, Dictionary<HexTile, BaseUnit> tileToUnit, HashSet<HexTile> isVisited)
    {
        Debug.Log("PosTranslator length in spawnObjects(): " + GridManager.Instance.posTranslator.Count);
        Debug.Log("in spawn objects");
        // Initialize the currentStatus dictionary with null values
        for (float x = -3; x <= 3; x += 1.5f)
        {
            if (x == -3.0f || x == 3.0f)
            {
                for (int y = -1; y <= 1; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == -1.5f || x == 1.5f)
            {
                for (float y = -1.5f; y <= 1.5f; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else
            {
                for (int y = -2; y <= 2; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
        }

        // Spawn scissor units
        //List<Vector3> scissorList = new List<Vector3> { new Vector3(0, 0) };
        List<Vector3> scissorList = new List<Vector3> { new Vector3(-3.0f, 1.0f), new Vector3(0, -1.0f) };
        var scissorCount = scissorList.Count;

        for (int i = 0; i < scissorCount; i++)
        {
            var scissorPrefab = UnitManager.Instance.GetUnit<Scissor>(Faction.Scissor);
            BaseUnit spawnedScissor = Instantiate(scissorPrefab);
            Vector3 translatedPos = GridManager.Instance.GetTranslatedPos(scissorList[i]);
            //Debug.Log(GridManager.Instance.posTranslator.Count);
            foreach (var item in GridManager.Instance.posTranslator)
            {
                Debug.Log("key: " + item.Key + "," + " Value: " + item.Value);
            }
            Debug.Log(scissorList[i]);
            Debug.Log(translatedPos);
            HexTile scissorTile = GridManager.Instance.GetTileAtPos(translatedPos);
            Debug.Log(scissorTile);
            scissorTile.SetUnit(spawnedScissor);
            tileToUnit[scissorTile] = spawnedScissor;
            currentStatus[scissorList[i]] = spawnedScissor;
            isVisited.Add(scissorTile);
            scissorTile.SetColorToGreen();

        }

        // Spawn rock units
        //List<Vector3> rockList = new List<Vector3> { new Vector3(1.5f, -0.5f), new Vector3(-1.5f, -0.5f) };
        List<Vector3> rockList = new List<Vector3> { new Vector3(-1.5f, -1.5f), new Vector3(-1.5f, 0.5f), new Vector3(0.0f, 1.0f), new Vector3(1.5f, -0.5f), new Vector3(3.0f, 0) };
        var rockCount = rockList.Count;

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
        List<Vector3> paperList = new List<Vector3> { new Vector3(-3.0f, 0f), new Vector3(-1.5f, 1.5f), new Vector3(1.5f, 1.5f), new Vector3(1.5f, -1.5f), new Vector3(3.0f, -1.0f) };
        var paperCount = paperList.Count;

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

        // Change the game state to PlayerTurn after spawning objects
        GameManager.Instance.ChangeState(GameState.PlayerTurn);
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
