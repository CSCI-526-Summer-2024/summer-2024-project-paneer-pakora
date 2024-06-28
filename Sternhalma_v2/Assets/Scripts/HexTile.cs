using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] public GameObject highlightOnSelect;

    public BaseUnit OccupiedUnit;
    public bool isEmpty => this.OccupiedUnit == null;

    public Vector3 posEasy;
    public Vector3 posHard;
    public bool isRotatable;

    public void SetColorToGreen()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color32(172, 250, 211, 200);
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)
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

    public void RemoveUnit(BaseUnit unit)
    {
        unit.OccupiedTile = null;
        this.OccupiedUnit = null;
        Destroy(unit.gameObject);
    }

    private void OnMouseDown()
    {
        if (GridManager.Instance.selectedLevel == 2 && GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        if (GridManager.Instance.selectedLevel == 1 && Tut2_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }
        if (GridManager.Instance.selectedLevel == 0 && Tut1_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }
        HexTile selectedTile = UnitManager.Instance.selectedTile;

        if (UnitManager.Instance.currentStatus[this.posEasy] == null && selectedTile == null)
        {
            Debug.Log("1");
            UnitManager.Instance.SetSelectedTile(null);
            //GridManager.Instance.rotateButton.SetActive(false);
            return;
        }

        else if (UnitManager.Instance.currentStatus[this.posEasy] != null && selectedTile != null)
        {
            Debug.Log("2");
            if (selectedTile == this)
            {
                //Debug.Log("2.1");
                UnitManager.Instance.SetSelectedTile(null);
                selectedTile.highlightOnSelect.SetActive(false);
                // GameManager.Instance.rotateButton.SetActive(false);

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

            if (this.isRotatable)
            {
                //Debug.Log("Before Active: " + GridManager.Instance.rotateButton.activeSelf);
                //GridManager.Instance.rotateButton.SetActive(true);
                //Debug.Log("After Active: "+ GridManager.Instance.rotateButton.activeSelf);
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
                        RemovePotentialHighlight(selectedPos);
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midPos]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        //GridManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);
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
                        RemovePotentialHighlight(selectedPos);
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midPos]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        //GridManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);
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
                        RemovePotentialHighlight(selectedPos);
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midTile.posEasy]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        //GridManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);
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
}
