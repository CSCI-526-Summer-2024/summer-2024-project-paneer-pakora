using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tutorial2_GridManager : MonoBehaviour
{
    //[SerializeField] private HexTile _tile;

    public static Tutorial2_GridManager Instance;

    public Tutorial2_HexTile hexPrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float hexSize = 1f;

    private Dictionary<Vector3, Tutorial2_HexTile> posTile;
    private Dictionary<Vector3, Vector3> posTranslator;


    // Start is called before the first frame update
    //void Start()
    //{
    //    GenerateHexGrid();  
    //}

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateHexGrid()
    {
        float hexWidth = hexSize + 0.1f;
        float hexHeight = hexSize * Mathf.Sqrt(3) + 0.1f;

        posTile = new Dictionary<Vector3, Tutorial2_HexTile>();
        posTranslator = new Dictionary<Vector3, Vector3>();

        for (float x = -4.5f; x <= 4.5f; x += 1.5f)
        {
            if (x == -4.5f)
            {
                for (int y = -1; y <= 0; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    Tutorial2_HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
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

            else if (x == -3.0f)
            {
                for (float y = -1.5f; y <= 0.5f; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    Tutorial2_HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    if (y == -1.5f || y == 0.5f)
                    {
                        hex.isRotatable = false;
                    }
                    else
                    {
                        hex.isRotatable = true;
                    }
                }
            }

            else if (x == -1.5f)
            {
                for (int y = -1; y <= 0; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    Tutorial2_HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
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

            else if (x == 0f)
            {
                for (float y = -1.5f; y <= 0.5f; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    Tutorial2_HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    if (y == -1.5f || y == 0.5f)
                    {
                        hex.isRotatable = false;
                    }
                    else
                    {
                        hex.isRotatable = true;
                    }
                }
            }

            else if (x == 1.5f)
            {
                for (int y = -1; y <= 1; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    Tutorial2_HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    if (y == -1 || y == 1)
                    {
                        hex.isRotatable = false;
                    }
                    else
                    {
                        hex.isRotatable = true;
                    }
                }
            }

            else if (x == 3.0f)
            {
                for (float y = -0.5f; y <= 1.5f; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    Tutorial2_HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    hex.posEasy = new Vector3(x, y, 0);
                    hex.posHard = new Vector3(xPos, yPos, 0);
                    posTile[new Vector3(xPos, yPos, 0)] = hex;

                    posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

                    //Debug.Log("Pos");
                    //Debug.Log(xPos + " " + yPos);

                    if (y == -0.5f || y == 1.5f)
                    {
                        hex.isRotatable = false;
                    }
                    else
                    {
                        hex.isRotatable = true;
                    }
                }
            }

            else if (x == 4.5f)
            {
                for (int y = 0; y <= 1; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight;

                    Tutorial2_HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
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
        }

        Tutorial2_GameManager.Instance.ChangeState(GameState.SpawnObjects);
    }


    public Tutorial2_HexTile GetTileAtPos(Vector3 pos)
    {
       if(posTile.TryGetValue(pos, out var tile))
       {
            return tile;
       }
        return null;
    }

    //public Vector3 GetPosOfTile(HexTile tile)
    //{
    //    if (tilePos.TryGetValue(tile, out var pos))
    //    {
    //        return pos;
    //    }
    //    return new Vector3(-100, -100, 0);
    //}

    public Vector3 GetTranslatedPos(Vector3 pos)
    {
        if (posTranslator.TryGetValue(pos, out var upPos))
        {
            return upPos;
        }
        return new Vector3(-100, -100, 0);
    }
}
