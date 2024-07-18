using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class GridManager : MonoBehaviour
{
    //[SerializeField] private HexTile _tile;

    public static GridManager Instance;

    public HexTile hexPrefab;
    public int gridWidth = 20;
    public int gridHeight = 20;
    public float hexSize = 1f;

    public Dictionary<Vector3, HexTile> posTile = new Dictionary<Vector3, HexTile>();
    public Dictionary<Vector3, Vector3> posTranslator = new Dictionary<Vector3, Vector3>();

    public static int selectedLevel = -1;
    [SerializeField] public GameObject rotateButton;
    [SerializeField] public GameObject undoButton;


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
        float hexWidth = hexSize + 0.1f*hexSize;
        float hexHeight = hexSize * Mathf.Sqrt(3) + 0.1f*hexSize;

        posTile = new Dictionary<Vector3, HexTile>();
        posTranslator = new Dictionary<Vector3, Vector3>();

        for (float x = -3; x <= 3; x += 1.5f)
        {
            if (x == -3.0f || x == 3.0f)
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

                    hex.isRotatable = false;
                }
            }

            else if (x == -1.5f || x == 1.5f)
            {
                for (float y = -1.5f; y <= 1.5f; y++)
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

        GameManager.Instance.ChangeState(GameState.SpawnObjects);
    }

    // public void GenerateTutorial2_Grid()
    // {
    //     float hexWidth = hexSize + 0.1f;
    //     float hexHeight = hexSize * Mathf.Sqrt(3) + 0.1f;

    //     posTile = new Dictionary<Vector3, HexTile>();
    //     posTranslator = new Dictionary<Vector3, Vector3>();

    //     for (float x = -4.5f; x <= 4.5f; x += 1.5f)
    //     {
    //         if (x == -4.5f)
    //         {
    //             for (int y = -1; y <= 0; y++)
    //             {
    //                 float xPos = x * hexWidth;
    //                 float yPos = y * hexHeight;

    //                 HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
    //                 hex.transform.parent = this.transform;
    //                 hex.name = $"Hex_{x}_{y}";

    //                 hex.posEasy = new Vector3(x, y, 0);
    //                 hex.posHard = new Vector3(xPos, yPos, 0);
    //                 posTile[new Vector3(xPos, yPos, 0)] = hex;

    //                 posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

    //                 //Debug.Log("Pos");
    //                 //Debug.Log(xPos + " " + yPos);

    //                 hex.isRotatable = false;
    //             }
    //         }

    //         else if (x == -3.0f)
    //         {
    //             for (float y = -1.5f; y <= 0.5f; y++)
    //             {
    //                 float xPos = x * hexWidth;
    //                 float yPos = y * hexHeight;

    //                 HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
    //                 hex.transform.parent = this.transform;
    //                 hex.name = $"Hex_{x}_{y}";

    //                 hex.posEasy = new Vector3(x, y, 0);
    //                 hex.posHard = new Vector3(xPos, yPos, 0);
    //                 posTile[new Vector3(xPos, yPos, 0)] = hex;

    //                 posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

    //                 //Debug.Log("Pos");
    //                 //Debug.Log(xPos + " " + yPos);

    //                 if (y == -1.5f || y == 0.5f)
    //                 {
    //                     hex.isRotatable = false;
    //                 }
    //                 else
    //                 {
    //                     hex.isRotatable = true;
    //                 }
    //             }
    //         }

    //         else if (x == -1.5f)
    //         {
    //             for (int y = -1; y <= 0; y++)
    //             {
    //                 float xPos = x * hexWidth;
    //                 float yPos = y * hexHeight;

    //                 HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
    //                 hex.transform.parent = this.transform;
    //                 hex.name = $"Hex_{x}_{y}";

    //                 hex.posEasy = new Vector3(x, y, 0);
    //                 hex.posHard = new Vector3(xPos, yPos, 0);
    //                 posTile[new Vector3(xPos, yPos, 0)] = hex;

    //                 posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

    //                 //Debug.Log("Pos");
    //                 //Debug.Log(xPos + " " + yPos);

    //                 hex.isRotatable = false;
    //             }
    //         }

    //         else if (x == 0f)
    //         {
    //             for (float y = -1.5f; y <= 0.5f; y++)
    //             {
    //                 float xPos = x * hexWidth;
    //                 float yPos = y * hexHeight;

    //                 HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
    //                 hex.transform.parent = this.transform;
    //                 hex.name = $"Hex_{x}_{y}";

    //                 hex.posEasy = new Vector3(x, y, 0);
    //                 hex.posHard = new Vector3(xPos, yPos, 0);
    //                 posTile[new Vector3(xPos, yPos, 0)] = hex;

    //                 posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

    //                 //Debug.Log("Pos");
    //                 //Debug.Log(xPos + " " + yPos);

    //                 if (y == -1.5f || y == 0.5f)
    //                 {
    //                     hex.isRotatable = false;
    //                 }
    //                 else
    //                 {
    //                     hex.isRotatable = true;
    //                 }
    //             }
    //         }

    //         else if (x == 1.5f)
    //         {
    //             for (int y = -1; y <= 1; y++)
    //             {
    //                 float xPos = x * hexWidth;
    //                 float yPos = y * hexHeight;

    //                 HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
    //                 hex.transform.parent = this.transform;
    //                 hex.name = $"Hex_{x}_{y}";

    //                 hex.posEasy = new Vector3(x, y, 0);
    //                 hex.posHard = new Vector3(xPos, yPos, 0);
    //                 posTile[new Vector3(xPos, yPos, 0)] = hex;

    //                 posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

    //                 //Debug.Log("Pos");
    //                 //Debug.Log(xPos + " " + yPos);

    //                 if (y == -1 || y == 1)
    //                 {
    //                     hex.isRotatable = false;
    //                 }
    //                 else
    //                 {
    //                     hex.isRotatable = true;
    //                 }
    //             }
    //         }

    //         else if (x == 3.0f)
    //         {
    //             for (float y = -0.5f; y <= 1.5f; y++)
    //             {
    //                 float xPos = x * hexWidth;
    //                 float yPos = y * hexHeight;

    //                 HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
    //                 hex.transform.parent = this.transform;
    //                 hex.name = $"Hex_{x}_{y}";

    //                 hex.posEasy = new Vector3(x, y, 0);
    //                 hex.posHard = new Vector3(xPos, yPos, 0);
    //                 posTile[new Vector3(xPos, yPos, 0)] = hex;

    //                 posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

    //                 //Debug.Log("Pos");
    //                 //Debug.Log(xPos + " " + yPos);

    //                 if (y == -0.5f || y == 1.5f)
    //                 {
    //                     hex.isRotatable = false;
    //                 }
    //                 else
    //                 {
    //                     hex.isRotatable = true;
    //                 }
    //             }
    //         }

    //         else if (x == 4.5f)
    //         {
    //             for (int y = 0; y <= 1; y++)
    //             {
    //                 float xPos = x * hexWidth;
    //                 float yPos = y * hexHeight;

    //                 HexTile hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
    //                 hex.transform.parent = this.transform;
    //                 hex.name = $"Hex_{x}_{y}";

    //                 hex.posEasy = new Vector3(x, y, 0);
    //                 hex.posHard = new Vector3(xPos, yPos, 0);
    //                 posTile[new Vector3(xPos, yPos, 0)] = hex;

    //                 posTranslator[new Vector3(x, y, 0)] = new Vector3(xPos, yPos, 0);

    //                 //Debug.Log("Pos");
    //                 //Debug.Log(xPos + " " + yPos);

    //                 hex.isRotatable = false;
    //             }
    //         }
    //     }

    //     GameManager.Instance.ChangeState(GameState.SpawnObjects);
    // }

    public void disableUndo()
    {
        Image buttonOutline = undoButton.GetComponent<Image>();
        Image undoIcon = undoButton.transform.GetChild(0).GetComponent<Image>();
        Image hotkeyOutline = undoButton.transform.GetChild(1).GetComponent<Image>();


        buttonOutline.color = new Color(buttonOutline.color.r, buttonOutline.color.g, buttonOutline.color.b, 0.5f);
        undoIcon.color = new Color(undoIcon.color.r, undoIcon.color.g, undoIcon.color.b, 0.5f);
        hotkeyOutline.color = new Color(hotkeyOutline.color.r, hotkeyOutline.color.g, hotkeyOutline.color.b, 0.5f);
    }

    public void enableUndo()
    {
        Timer timer = FindObjectOfType<Timer>();
        float timeLeft = timer.timeRemaining - 30.0f;

        // do a check before enabling the Undo button
        // if the time left after making a move is insufficient for an undo, keep Undo disabled
        if (timeLeft <= 0.0f)
        {
            disableUndo();
            return;
        }

        Image buttonOutline = undoButton.GetComponent<Image>();
        Image undoIcon = undoButton.transform.GetChild(0).GetComponent<Image>();
        Image hotkeyOutline = undoButton.transform.GetChild(1).GetComponent<Image>();


        buttonOutline.color = new Color(buttonOutline.color.r, buttonOutline.color.g, buttonOutline.color.b, 1f);
        undoIcon.color = new Color(undoIcon.color.r, undoIcon.color.g, undoIcon.color.b, 1f);
        hotkeyOutline.color = new Color(hotkeyOutline.color.r, hotkeyOutline.color.g, hotkeyOutline.color.b, 1f);

    }

    public HexTile GetTileAtPos(Vector3 pos)
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
        return new Vector3(-100, -100, 0); //when pos not found
    }

    public void setSelectedLevel(int level)
    {
        selectedLevel = level;
    }

    public void printSelectedLevel()
    {
        Debug.Log(selectedLevel);
    }
}
