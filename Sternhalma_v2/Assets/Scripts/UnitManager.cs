
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class UnitManager : MonoBehaviour
//{
//    public static UnitManager Instance;

//    private List<ScriptableUnit> units;
//    public HexTile selectedTile;

//    public Dictionary<HexTile, BaseUnit> tileToUnit;
//    public Dictionary<Vector3, BaseUnit> currentStatus;

//    public HashSet<HexTile> isVisited = new HashSet<HexTile>();

//    private void Awake()
//    {
//        Instance = this;
//        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
//    }

//    private T GetUnit<T>(Faction faction) where T : BaseUnit
//    {
//        return (T)units.Where(u => u.Faction == faction).First().UnitPrefab;
//    }

//    public void SpawnObjects()
//    {
//        tileToUnit = new Dictionary<HexTile, BaseUnit>();
//        currentStatus = new Dictionary<Vector3, BaseUnit>();

//        // Initialize the currentStatus dictionary with null values
//        for (float x = -3; x <= 3; x += 1.5f)
//        {
//            if (x == -3.0f || x == 3.0f)
//            {
//                for (int y = -1; y <= 1; y++)
//                {
//                    currentStatus[new Vector3(x, y)] = null;
//                }
//            }
//            else if (x == -1.5f || x == 1.5f)
//            {
//                for (float y = -1.5f; y <= 1.5f; y++)
//                {
//                    currentStatus[new Vector3(x, y)] = null;
//                }
//            }
//            else
//            {
//                for (int y = -2; y <= 2; y++)
//                {
//                    currentStatus[new Vector3(x, y)] = null;
//                }
//            }
//        }

//        // Spawn scissor units
//        //List<Vector3> scissorList = new List<Vector3> { new Vector3(0, 0) };
//        List<Vector3> scissorList = new List<Vector3> { new Vector3(-3.0f, -1.0f) };
//        var scissorCount = scissorList.Count;

//        for (int i = 0; i < scissorCount; i++)
//        {
//            var scissorPrefab = GetUnit<Scissor>(Faction.Scissor);
//            BaseUnit spawnedScissor = Instantiate(scissorPrefab);
//            HexTile scissorTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(scissorList[i]));

//            scissorTile.SetUnit(spawnedScissor);
//            tileToUnit[scissorTile] = spawnedScissor;
//            currentStatus[scissorList[i]] = spawnedScissor;
//            isVisited.Add(scissorTile);
//            scissorTile.SetColorToGreen();
//        }

//        // Spawn rock units
//        //List<Vector3> rockList = new List<Vector3> { new Vector3(1.5f, -0.5f), new Vector3(-1.5f, -0.5f) };
//        List<Vector3> rockList = new List<Vector3> { new Vector3(0.0f, 1.0f) };
//        var rockCount = rockList.Count;

//        for (int i = 0; i < rockCount; i++)
//        {
//            var rockPrefab = GetUnit<Rock>(Faction.Rock);
//            BaseUnit spawnedRock = Instantiate(rockPrefab);
//            HexTile rockTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(rockList[i]));
//            rockTile.SetUnit(spawnedRock);
//            tileToUnit[rockTile] = spawnedRock;
//            currentStatus[rockList[i]] = spawnedRock;
//            isVisited.Add(rockTile);
//            rockTile.SetColorToGreen();
//        }

//        // Spawn paper units
//        //List<Vector3> paperList = new List<Vector3> { new Vector3(1.5f, 0.5f), new Vector3(0, -1), new Vector3(-1.5f, 0.5f) };
//        List<Vector3> paperList = new List<Vector3> { new Vector3(-1.5f, -0.5f), new Vector3(1.5f, -1.5f) };
//        var paperCount = paperList.Count;

//        for (int i = 0; i < paperCount; i++)
//        {
//            var paperPrefab = GetUnit<Paper>(Faction.Paper);

//            BaseUnit spawnedPaper = Instantiate(paperPrefab);
//            HexTile paperTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(paperList[i]));

//            paperTile.SetUnit(spawnedPaper);
//            tileToUnit[paperTile] = spawnedPaper;
//            currentStatus[paperList[i]] = spawnedPaper;
//            isVisited.Add(paperTile);
//            paperTile.SetColorToGreen();
//        }

//        // Change the game state to PlayerTurn after spawning objects
//        GameManager.Instance.ChangeState(GameState.PlayerTurn);
//    }

//    public void SetSelectedTile(HexTile tile)
//    {
//        selectedTile = tile;
//    }

//    public void UpdateCurrentStatus(Vector3 rem1Pos, Vector3 rem2Pos, Vector3 addPos)
//    {
//        BaseUnit unit = currentStatus[rem1Pos];
//        currentStatus[rem1Pos] = null;
//        currentStatus[rem2Pos] = null;
//        currentStatus[addPos] = unit;

//        HexTile newTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(addPos));
//        if (newTile != null)
//        {
//            isVisited.Add(newTile);
//        }
//    }

//    public void UpdateCurrentStatusRotation(Vector3 pos, BaseUnit unit)
//    {
//        currentStatus[pos] = unit;
//    }
//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> units;
    public HexTile selectedTile;

    public Dictionary<HexTile, BaseUnit> tileToUnit;
    public Dictionary<Vector3, BaseUnit> currentStatus;

    public HashSet<HexTile> isVisited = new HashSet<HexTile>();

    private void Awake()
    {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    private T GetUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction).First().UnitPrefab;
    }

    public void SpawnObjects()
    {
        tileToUnit = new Dictionary<HexTile, BaseUnit>();
        currentStatus = new Dictionary<Vector3, BaseUnit>();

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

        List<Vector3> scissorList = new List<Vector3> { new Vector3(-3.0f, -1.0f) };
        var scissorCount = scissorList.Count;

        for (int i = 0; i < scissorCount; i++)
        {
            var scissorPrefab = GetUnit<Scissor>(Faction.Scissor);
            BaseUnit spawnedScissor = Instantiate(scissorPrefab);
            HexTile scissorTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(scissorList[i]));

            scissorTile.SetUnit(spawnedScissor);
            tileToUnit[scissorTile] = spawnedScissor;
            currentStatus[scissorList[i]] = spawnedScissor;
            isVisited.Add(scissorTile);
            scissorTile.SetColorToGreen();
        }

        List<Vector3> rockList = new List<Vector3> { new Vector3(0.0f, 1.0f) };
        var rockCount = rockList.Count;

        for (int i = 0; i < rockCount; i++)
        {
            var rockPrefab = GetUnit<Rock>(Faction.Rock);
            BaseUnit spawnedRock = Instantiate(rockPrefab);
            HexTile rockTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(rockList[i]));
            rockTile.SetUnit(spawnedRock);
            tileToUnit[rockTile] = spawnedRock;
            currentStatus[rockList[i]] = spawnedRock;
            isVisited.Add(rockTile);
            rockTile.SetColorToGreen();
        }

        List<Vector3> paperList = new List<Vector3> { new Vector3(-1.5f, -0.5f), new Vector3(1.5f, -1.5f) };
        var paperCount = paperList.Count;

        for (int i = 0; i < paperCount; i++)
        {
            var paperPrefab = GetUnit<Paper>(Faction.Paper);

            BaseUnit spawnedPaper = Instantiate(paperPrefab);
            HexTile paperTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(paperList[i]));

            paperTile.SetUnit(spawnedPaper);
            tileToUnit[paperTile] = spawnedPaper;
            currentStatus[paperList[i]] = spawnedPaper;
            isVisited.Add(paperTile);
            paperTile.SetColorToGreen();
        }

        Debug.Log("Objects Spawned");


        GameManager.Instance.ChangeState(GameState.Tutorial); // Update this to transition correctly after tutorial
    }

    public void SetSelectedTile(HexTile tile)
    {
        selectedTile = tile;
    }

    public void UpdateCurrentStatus(Vector3 rem1Pos, Vector3 rem2Pos, Vector3 addPos)
    {
        BaseUnit unit = currentStatus[rem1Pos];
        currentStatus[rem1Pos] = null;
        currentStatus[rem2Pos] = null;
        currentStatus[addPos] = unit;

        HexTile newTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(addPos));
        if (newTile != null)
        {
            isVisited.Add(newTile);
        }
    }

    public void UpdateCurrentStatusRotation(Vector3 pos, BaseUnit unit)
    {
        currentStatus[pos] = unit;
    }
}
