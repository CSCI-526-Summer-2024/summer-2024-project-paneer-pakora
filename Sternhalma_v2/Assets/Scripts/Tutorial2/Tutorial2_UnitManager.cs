using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tutorial2_UnitManager : MonoBehaviour
{
    public static Tutorial2_UnitManager Instance;

    private List<Tutorial2_ScriptableUnit> units;
    public Tutorial2_HexTile selectedTile;

    public Dictionary<Tutorial2_HexTile, Tutorial2_BaseUnit> tileToUnit = new Dictionary<Tutorial2_HexTile, Tutorial2_BaseUnit>();
    public Dictionary<Vector3, Tutorial2_BaseUnit> currentStatus = new Dictionary<Vector3, Tutorial2_BaseUnit>();

    public HashSet<Tutorial2_HexTile> isVisited = new HashSet<Tutorial2_HexTile>();

    public int pieceCount, piecesRemoved;
    public ProgressMeter tileCoverageMeter;
    public ProgressMeter piecesRemovedMeter;

    private void Awake()
    {
        Instance = this;
        units = Resources.LoadAll<Tutorial2_ScriptableUnit>("Units").ToList();
    }

    private T GetUnit<T>(Faction faction) where T : Tutorial2_BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction).First().UnitPrefab;
    }

    public void SpawnObjects()
    {
        tileToUnit = new Dictionary<Tutorial2_HexTile, Tutorial2_BaseUnit>();
        currentStatus = new Dictionary<Vector3, Tutorial2_BaseUnit>();

        // Initialize the currentStatus dictionary with null values
        for (float x = -4.5f; x <= 4.5f; x += 1.5f)
        {
            if (x == -4.5f)
            {
                for (int y = -1; y <= 1; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == -3.0f)
            {
                for (float y = -1.5f; y <= 0.5f; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == -1.5f)
            {
                for (int y = -1; y <= 0; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == 0f)
            {
                for (float y = -1.5f; y <= 0.5f; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == 1.5f)
            {
                for (int y = -1; y <= 1; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == 3.0f)
            {
                for (float y = -0.5f; y <= 1.5f; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
            else if (x == 4.5f)
            {
                for (int y = 0; y <= 1; y++)
                {
                    currentStatus[new Vector3(x, y)] = null;
                }
            }
        }

        // Spawn scissor units
        //List<Vector3> scissorList = new List<Vector3> { new Vector3(0, 0) };
        //List<Vector3> scissorList = new List<Vector3> { new Vector3(-3.0f, 1.0f), new Vector3(0, -1.0f) };
        List<Vector3> scissorList = new List<Vector3> { new Vector3(-3.0f, -0.5f), new Vector3(4.5f, 1.0f) };
        var scissorCount = scissorList.Count;

        for (int i = 0; i < scissorCount; i++)
        {
            var scissorPrefab = GetUnit<Tutorial2_Scissor>(Faction.Scissor);
            Tutorial2_BaseUnit spawnedScissor = Instantiate(scissorPrefab);
            Tutorial2_HexTile scissorTile = Tutorial2_GridManager.Instance.GetTileAtPos(Tutorial2_GridManager.Instance.GetTranslatedPos(scissorList[i]));

            scissorTile.SetUnit(spawnedScissor);
            tileToUnit[scissorTile] = spawnedScissor;
            currentStatus[scissorList[i]] = spawnedScissor;
            isVisited.Add(scissorTile);
            scissorTile.SetColorToGreen();

        }

        // Spawn rock units
        //List<Vector3> rockList = new List<Vector3> { new Vector3(1.5f, -0.5f), new Vector3(-1.5f, -0.5f) };
        //List<Vector3> rockList = new List<Vector3> { new Vector3(-1.5f, -1.5f), new Vector3(-1.5f, 0.5f), new Vector3(0.0f, 1.0f), new Vector3(1.5f, -0.5f), new Vector3(3.0f, 0) };
        List<Vector3> rockList = new List<Vector3> { new Vector3(3.0f, 0.5f) };
        var rockCount = rockList.Count;

        for (int i = 0; i < rockCount; i++)
        {
            var rockPrefab = GetUnit<Tutorial2_Rock>(Faction.Rock);
            Tutorial2_BaseUnit spawnedRock = Instantiate(rockPrefab);
            Tutorial2_HexTile rockTile = Tutorial2_GridManager.Instance.GetTileAtPos(Tutorial2_GridManager.Instance.GetTranslatedPos(rockList[i]));
            rockTile.SetUnit(spawnedRock);
            tileToUnit[rockTile] = spawnedRock;
            currentStatus[rockList[i]] = spawnedRock;
            isVisited.Add(rockTile);
            rockTile.SetColorToGreen();

        }

        // Spawn paper units
        //List<Vector3> paperList = new List<Vector3> { new Vector3(1.5f, 0.5f), new Vector3(0, -1), new Vector3(-1.5f, 0.5f) };
        //List<Vector3> paperList = new List<Vector3> { new Vector3(-3.0f, 0f), new Vector3(-1.5f, 1.5f), new Vector3(1.5f, 1.5f), new Vector3(1.5f, -1.5f), new Vector3(3.0f, -1.0f) };
        List<Vector3> paperList = new List<Vector3> { new Vector3(-4.5f, -1.0f) };
        var paperCount = paperList.Count;

        for (int i = 0; i < paperCount; i++)
        {
            var paperPrefab = GetUnit<Tutorial2_Paper>(Faction.Paper);

            Tutorial2_BaseUnit spawnedPaper = Instantiate(paperPrefab);
            Tutorial2_HexTile paperTile = Tutorial2_GridManager.Instance.GetTileAtPos(Tutorial2_GridManager.Instance.GetTranslatedPos(paperList[i]));

            paperTile.SetUnit(spawnedPaper);
            tileToUnit[paperTile] = spawnedPaper;
            currentStatus[paperList[i]] = spawnedPaper;
            isVisited.Add(paperTile);
            paperTile.SetColorToGreen();

        }

        // set tile coverage progress meter
        var tileCount = currentStatus.Count;
        var coveredTileCount = isVisited.Count;
        tileCoverageMeter.SetMaxProgress(tileCount);
        tileCoverageMeter.SetProgress(coveredTileCount);

        // set # pieces removed progress meter
        pieceCount = scissorCount + rockCount + paperCount;
        piecesRemoved = 0;                                      // start with all pieces on board; none removed
        piecesRemovedMeter.SetMaxProgress(pieceCount - 1);      // win condition requires 1 piece remaining
        piecesRemovedMeter.SetProgress(piecesRemoved);                      

        // Change the game state to PlayerTurn after spawning objects
        Tutorial2_GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }

    public void SetSelectedTile(Tutorial2_HexTile tile)
    {
        selectedTile = tile;
    }

    public void UpdateCurrentStatus(Vector3 rem1Pos, Vector3 rem2Pos, Vector3 addPos)
    {
        Tutorial2_BaseUnit unit = currentStatus[rem1Pos];
        currentStatus[rem1Pos] = null;
        currentStatus[rem2Pos] = null;
        currentStatus[addPos] = unit;

        Tutorial2_HexTile newTile = Tutorial2_GridManager.Instance.GetTileAtPos(Tutorial2_GridManager.Instance.GetTranslatedPos(addPos));
        if (newTile != null)
        {
            isVisited.Add(newTile);  
            // increment % tiles covered (?)
        }
    }

    public void UpdateCurrentStatusRotation(Vector3 pos, Tutorial2_BaseUnit unit)
    {
        currentStatus[pos] = unit;
    }
}
