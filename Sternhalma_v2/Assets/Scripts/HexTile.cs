using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer; //manages rendering of the Hex tile's sprite
    [SerializeField] private GameObject _highlight;   //This game obj is used to highlight the tile when mouse hvers over it
    [SerializeField] public GameObject highlightOnSelect;

    public BaseUnit OccupiedUnit;   //currently occupied?
    public bool isEmpty => this.OccupiedUnit == null;  //checks if tile is empty by seieng if Occupied unit is null

    public Vector3 posEasy;
    public Vector3 posHard;
    public bool isRotatable;

    public void SetColorToGreen()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color32(172, 250, 211, 200);
    }

    public void SetColorToWhite()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color32(255, 255, 255, 255);
    }

    public String GetColor()
    {
        if (_renderer.color == new Color32(172, 250, 211, 200))
        {
            return "green";
        }
        return "white";
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)      //Assigns a unit to the tile updating the unit's position and linking the unit back to this tile
    {
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        this.OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void SetUnitRotation(BaseUnit unit)  
    {
        if (unit != null && unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        if (unit != null)
        {
            unit.transform.position = transform.position;
        }
        
        this.OccupiedUnit = unit;

        if (unit != null)
        {
            unit.OccupiedTile = this;
        }
        
    }

    public void RemoveUnit(BaseUnit unit)  //does the oposite of the setUnit function
    {
        unit.OccupiedTile = null;
        this.OccupiedUnit = null;
        Destroy(unit.gameObject);
    }

    private void OnMouseDown()
    {
        if (PauseMenu.gameIsPaused)
        {
            return;
        }

        //Check the current Game State

        if (GridManager.selectedLevel == 2 && GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        if (GridManager.selectedLevel == 1 && Tut2_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }
        if (GridManager.selectedLevel == 0 && Tut1_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }
        if (GridManager.selectedLevel == 3 && Tut3_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }
        if (GridManager.selectedLevel == 4 && Level0_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        if (GridManager.selectedLevel == 5 && Level0_5_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        HexTile selectedTile = UnitManager.Instance.selectedTile;

        if (UnitManager.Instance.currentStatus[this.posEasy] == null && selectedTile == null)
        {
            Debug.Log("1");
            UnitManager.Instance.SetSelectedTile(null);
            GridManager.Instance.rotateButton.SetActive(false);
            return;
        }

        else if (UnitManager.Instance.currentStatus[this.posEasy] != null && selectedTile != null)
        {
            GridManager.Instance.rotateButton.SetActive(false);

            Debug.Log("2");
            if (selectedTile == this)
            {
                //Debug.Log("2.1");
                UnitManager.Instance.SetSelectedTile(null);
                selectedTile.highlightOnSelect.SetActive(false);
                //GameManager.Instance.rotateButton.SetActive(false);

                float x = this.posEasy.x;
                float y = this.posEasy.y;

                List<Vector3> potentialPos = new List<Vector3> { new Vector3(x, y+2.0f), new Vector3(x+3.0f, y+1.0f),
                                                            new Vector3(x+3.0f, y-1.0f), new Vector3(x, y-2.0f),
                                                            new Vector3(x-3.0f, y-1.0f), new Vector3(x-3.0f, y+1.0f)};

                Debug.Log("Count of current status: " + UnitManager.Instance.currentStatus.Count);

                for (int i = 0; i < potentialPos.Count; i++)
                {
                    if (UnitManager.Instance.currentStatus.ContainsKey(potentialPos[i]) && UnitManager.Instance.currentStatus[potentialPos[i]] == null)
                    {
                        Debug.Log("Position: " + potentialPos[i]);
                        HexTile tileToHighlight = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(potentialPos[i]));
                        Debug.Log("TileToHighlight:");
                        Debug.Log(tileToHighlight);
                        tileToHighlight.highlightOnSelect.SetActive(false);
                    }
                }

                if (this.isRotatable)
                {
                    //Debug.Log("Before Active: " + GridManager.Instance.rotateButton.activeSelf);
                    GridManager.Instance.rotateButton.SetActive(false);
                    //Debug.Log("After Active: " + GridManager.Instance.rotateButton.activeSelf);
                }
            }

            else
            {
                UnitManager.Instance.SetSelectedTile(null);
                selectedTile.highlightOnSelect.SetActive(false);
                // GameManager.Instance.rotateButton.SetActive(false);

                float x = selectedTile.posEasy.x;
                float y = selectedTile.posEasy.y;

                List<Vector3> potentialPos = new List<Vector3> { new Vector3(x, y+2.0f), new Vector3(x+3.0f, y+1.0f),
                                                            new Vector3(x+3.0f, y-1.0f), new Vector3(x, y-2.0f),
                                                            new Vector3(x-3.0f, y-1.0f), new Vector3(x-3.0f, y+1.0f)};

                Debug.Log("Count of current status: " + UnitManager.Instance.currentStatus.Count);

                for (int i = 0; i < potentialPos.Count; i++)
                {
                    if (UnitManager.Instance.currentStatus.ContainsKey(potentialPos[i]) && UnitManager.Instance.currentStatus[potentialPos[i]] == null)
                    {
                        Debug.Log("Position: " + potentialPos[i]);
                        HexTile tileToHighlight = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(potentialPos[i]));
                        Debug.Log("TileToHighlight:");
                        Debug.Log(tileToHighlight);
                        tileToHighlight.highlightOnSelect.SetActive(false);
                    }
                }

                UnitManager.Instance.SetSelectedTile(this);
                this.highlightOnSelect.SetActive(true);
                AddPotentialHighlight(this.posEasy);

                if (this.isRotatable)
                {
                    //Debug.Log("Before Active: " + GridManager.Instance.rotateButton.activeSelf);
                    GridManager.Instance.rotateButton.SetActive(true);
                    //Debug.Log("After Active: " + GridManager.Instance.rotateButton.activeSelf);
                }

            }
            return;
        }

        else if (UnitManager.Instance.currentStatus[this.posEasy] != null && selectedTile == null)
        {
            Debug.Log("3");
            UnitManager.Instance.SetSelectedTile(this);
            this.highlightOnSelect.SetActive(true);
            //Debug.Log("Highlight Hexagon active?");
            //Debug.Log(this.highlightOnSelect.active);
            Debug.Log(this.isRotatable);
            if (this.isRotatable)
            {
                Debug.Log("Before Active: " + GridManager.Instance.rotateButton.activeSelf);
                GridManager.Instance.rotateButton.SetActive(true);
                Debug.Log("After Active: " + GridManager.Instance.rotateButton.activeSelf);
            }

            AddPotentialHighlight(this.posEasy);

            return;
        }

        else
        {
            Debug.Log("4");
            Vector3 selectedPos = selectedTile.posEasy;
            Vector3 currentPos = this.posEasy;

            if ((selectedPos.x == currentPos.x && ((int)Mathf.Abs(currentPos.y - selectedPos.y) == 2)) ||
                ((int)Mathf.Abs(currentPos.x - selectedPos.x) == 3) && ((int)Mathf.Abs(currentPos.y - selectedPos.y) == 1) &&
                UnitManager.Instance.currentStatus[currentPos] == null
               )
            {
                Debug.Log("5");
                if (UnitManager.Instance.currentStatus[selectedTile.posEasy].Faction == Faction.Rock)
                    {
                        Debug.Log("6");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    HexTile midTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(midPos));

                    if (UnitManager.Instance.currentStatus[midPos] != null && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Scissor)
                    {
                        Debug.Log("7");

                        //Tuple<String, Vector3, Vector3, Vector3> tuple = new Tuple<String, Vector3, Vector3, Vector3>("rock", selectedPos, midPos, currentPos);
                        Dictionary<Vector3, String> tempDict = GetTempDict(UnitManager.Instance.currentStatus);
                        UnitManager.Instance.pastStates.Push(tempDict);
                        UnitManager.Instance.lastTurnedGreen.Push(GetPastColorDict(UnitManager.Instance.currentStatus));
                        UnitManager.Instance.movedUnit.Push("r");
                        UnitManager.Instance.lastGreenPos.Push(currentPos);

                        RemovePotentialHighlight(selectedPos);
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midPos]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        GridManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);

                        UnitManager.Instance.currentScissorCount -= 1;
                        UnitManager.Instance.scissorsLeft.text = UnitManager.Instance.currentScissorCount.ToString();

                        //enable Undo button (now that a move has been made)
                        GridManager.Instance.enableUndo();
                    }

                    else
                    {
                        // Debug.Log("8");
                        // UnitManager.Instance.SetSelectedTile(null);
                        // selectedTile.highlightOnSelect.SetActive(false);
                        //GridManager.Instance.rotateButton.SetActive(false);
                        return;
                    }
                }

                else if (UnitManager.Instance.currentStatus[selectedTile.posEasy].Faction == Faction.Paper)
                {
                    Debug.Log("9");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    HexTile midTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(midPos));

                    if (UnitManager.Instance.currentStatus[midPos] != null && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Rock)
                    {
                        Debug.Log("10");

                        //Tuple<String, Vector3, Vector3, Vector3> tuple = new Tuple<String, Vector3, Vector3, Vector3>("paper", selectedPos, midPos, currentPos);
                        //UnitManager.Instance.pastStates.Push(tuple);
                        Dictionary<Vector3, String> tempDict = GetTempDict(UnitManager.Instance.currentStatus);
                        UnitManager.Instance.pastStates.Push(tempDict);
                        UnitManager.Instance.lastTurnedGreen.Push(GetPastColorDict(UnitManager.Instance.currentStatus));
                        UnitManager.Instance.movedUnit.Push("p");
                        UnitManager.Instance.lastGreenPos.Push(currentPos);

                        RemovePotentialHighlight(selectedPos);
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midPos]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        GridManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);

                        UnitManager.Instance.currentRockCount -= 1;
                        UnitManager.Instance.rocksLeft.text = UnitManager.Instance.currentRockCount.ToString();

                        //enable Undo button (now that a move has been made)
                        GridManager.Instance.enableUndo();
                    }

                    else
                    {
                        // Debug.Log("11");
                        // UnitManager.Instance.SetSelectedTile(null);
                        // selectedTile.highlightOnSelect.SetActive(false);
                        //GridManager.Instance.rotateButton.SetActive(false);
                        return;
                    }
                }

                else if (UnitManager.Instance.currentStatus[selectedTile.posEasy].Faction == Faction.Scissor)
                {
                    Debug.Log("12");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    HexTile midTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(midPos));

                    if (UnitManager.Instance.currentStatus[midPos] != null && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Paper)
                    {
                        Debug.Log("13");

                        //var scissorPrefab = UnitManager.Instance.GetUnit<Scissor>(Faction.Scissor);
                        //BaseUnit spawnedScissor = Instantiate(scissorPrefab);
                        //Tuple<String, Vector3, Vector3, Vector3> tuple = new Tuple<String, Vector3, Vector3, Vector3>("scissor", selectedPos, midPos, currentPos);
                        //UnitManager.Instance.pastStates.Push(tuple);

                        Dictionary<Vector3, String> tempDict = GetTempDict(UnitManager.Instance.currentStatus);
                        UnitManager.Instance.pastStates.Push(tempDict);
                        UnitManager.Instance.lastTurnedGreen.Push(GetPastColorDict(UnitManager.Instance.currentStatus));
                        UnitManager.Instance.movedUnit.Push("s");
                        UnitManager.Instance.lastGreenPos.Push(currentPos);

                        RemovePotentialHighlight(selectedPos);
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midTile.posEasy]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        GridManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);

                        UnitManager.Instance.currentPaperCount -= 1;
                        UnitManager.Instance.papersLeft.text = UnitManager.Instance.currentPaperCount.ToString();

                        //enable Undo button (now that a move has been made)
                        GridManager.Instance.enableUndo();
                    }

                    else
                    {
                        //Debug.Log("14");
                        //UnitManager.Instance.SetSelectedTile(null);
                        //selectedTile.highlightOnSelect.SetActive(false);
                        //GridManager.Instance.rotateButton.SetActive(false);
                        return;
                    }
                }

                else
                {
                    //Debug.Log("15");
                    //UnitManager.Instance.SetSelectedTile(null);
                    //selectedTile.highlightOnSelect.SetActive(false);
                    //GridManager.Instance.rotateButton.SetActive(false);
                    return;
                }
            }
            else
            {
                //Debug.Log("16");
                //UnitManager.Instance.SetSelectedTile(null);
                //selectedTile.highlightOnSelect.SetActive(false);
                //GridManager.Instance.rotateButton.SetActive(false);
                return;
            }
        }
    }

    public bool canJump(BaseUnit base1, BaseUnit base2)
    {
        if (base1.Faction == Faction.Rock && base2 != null && base2.Faction == Faction.Scissor)
        {
            return true;
        }

        if (base1.Faction == Faction.Scissor && base2 != null && base2.Faction == Faction.Paper)
        {
            return true;
        }
        if (base1.Faction == Faction.Paper && base2 != null && base2.Faction == Faction.Rock)
        {
            return true;
        }

        return false;
    }

    public void RemovePotentialHighlight(Vector3 pos)
    {
        float x = pos.x;
        float y = pos.y;

        List<Vector3> potentialPos = new List<Vector3> { new Vector3(x, y+2.0f), new Vector3(x+3.0f, y+1.0f),
                                                            new Vector3(x+3.0f, y-1.0f), new Vector3(x, y-2.0f),
                                                            new Vector3(x-3.0f, y-1.0f), new Vector3(x-3.0f, y+1.0f)};

        for (int i = 0; i < potentialPos.Count; i++)
        {
            if (UnitManager.Instance.currentStatus.ContainsKey(potentialPos[i]) && UnitManager.Instance.currentStatus[potentialPos[i]] == null)
            {
                Debug.Log(potentialPos[i]);
                HexTile tileToHighlight = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(potentialPos[i]));
                tileToHighlight.highlightOnSelect.SetActive(false);
            }
        }
    }

    public void AddPotentialHighlight(Vector3 pos)
    {
        float x = pos.x;
        float y = pos.y;

        List<Vector3> potentialPos = new List<Vector3> { new Vector3(x, y+2.0f), new Vector3(x+3.0f, y+1.0f),
                                                            new Vector3(x+3.0f, y-1.0f), new Vector3(x, y-2.0f),
                                                            new Vector3(x-3.0f, y-1.0f), new Vector3(x-3.0f, y+1.0f)};

        for (int i = 0; i < potentialPos.Count; i++)
        {
            if (((i == 0) && (UnitManager.Instance.currentStatus.ContainsKey(new Vector3(x, y + 1.0f))) && (canJump(UnitManager.Instance.currentStatus[new Vector3(x, y)], UnitManager.Instance.currentStatus[new Vector3(x, y + 1.0f)]))) ||
                    ((i == 1) && (UnitManager.Instance.currentStatus.ContainsKey(new Vector3(x + 1.5f, y + 0.5f))) && (canJump(UnitManager.Instance.currentStatus[new Vector3(x, y)], UnitManager.Instance.currentStatus[new Vector3(x + 1.5f, y + 0.5f)]))) ||
                    ((i == 2) && (UnitManager.Instance.currentStatus.ContainsKey(new Vector3(x + 1.5f, y - 0.5f))) && (canJump(UnitManager.Instance.currentStatus[new Vector3(x, y)], UnitManager.Instance.currentStatus[new Vector3(x + 1.5f, y - 0.5f)]))) ||
                    ((i == 3) && (UnitManager.Instance.currentStatus.ContainsKey(new Vector3(x, y - 1.0f))) && (canJump(UnitManager.Instance.currentStatus[new Vector3(x, y)], UnitManager.Instance.currentStatus[new Vector3(x, y - 1.0f)]))) ||
                    ((i == 4) && (UnitManager.Instance.currentStatus.ContainsKey(new Vector3(x - 1.5f, y - 0.5f))) && (canJump(UnitManager.Instance.currentStatus[new Vector3(x, y)], UnitManager.Instance.currentStatus[new Vector3(x - 1.5f, y - 0.5f)]))) ||
                    ((i == 5) && (UnitManager.Instance.currentStatus.ContainsKey(new Vector3(x - 1.5f, y + 0.5f))) && (canJump(UnitManager.Instance.currentStatus[new Vector3(x, y)], UnitManager.Instance.currentStatus[new Vector3(x - 1.5f, y + 0.5f)]))))
            {
                if (UnitManager.Instance.currentStatus.ContainsKey(potentialPos[i]) && UnitManager.Instance.currentStatus[potentialPos[i]] == null)
                {
                    HexTile tileToHighlight = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(potentialPos[i]));
                    tileToHighlight.highlightOnSelect.SetActive(true);
                }
            }
        }
    }

    public Dictionary<Vector3, String> GetTempDict(Dictionary<Vector3, BaseUnit> currentStatus)
    {
        Dictionary<Vector3, String> tempDict = new Dictionary<Vector3, string>();

        foreach (KeyValuePair<Vector3, BaseUnit> kvp in currentStatus)
        {
            if (kvp.Value != null)
            {
                if (kvp.Value.Faction == Faction.Rock)
                {
                    tempDict[kvp.Key] = "r";
                }

                else if (kvp.Value.Faction == Faction.Paper)
                {
                    tempDict[kvp.Key] = "p";
                }

                if (kvp.Value.Faction == Faction.Scissor)
                {
                    tempDict[kvp.Key] = "s";
                }
            }

            else
            {
                tempDict[kvp.Key] = null;
            }
            
        }

        return tempDict;
    }

    public Dictionary<Vector3, String> GetPastColorDict(Dictionary<Vector3, BaseUnit> currentStatus)
    {
        Dictionary<Vector3, String> pastColorDict = new Dictionary<Vector3, string>();

        foreach (KeyValuePair<Vector3, BaseUnit> kvp in currentStatus)
        {
            Vector3 translatedPos = GridManager.Instance.GetTranslatedPos(kvp.Key);
            HexTile tile = GridManager.Instance.GetTileAtPos(translatedPos);

            pastColorDict[kvp.Key] = tile.GetColor();
        }

        return pastColorDict;
    }
}
