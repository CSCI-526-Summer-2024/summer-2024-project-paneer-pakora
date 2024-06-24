using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject highlightOnSelect;

    public BaseUnit OccupiedUnit;
    public bool isEmpty => this.OccupiedUnit == null;

    public Vector3 posEasy;
    public Vector3 posHard;
    public bool isRotatable;

    //private Color gr = new Color(0.0f, 0.9f, 0.0f, 0.1f);

    public void SetColorToGreen()
    {
        _renderer = GetComponent<SpriteRenderer>();
        //_renderer.color = Color.green;
        //_renderer.color = new Color32(173, 173, 173, 200);
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
        if (GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        HexTile selectedTile = UnitManager.Instance.selectedTile;

        if (UnitManager.Instance.currentStatus[this.posEasy] == null && selectedTile == null)
        {
            //Debug.Log("1");
            UnitManager.Instance.SetSelectedTile(null);
            GameManager.Instance.rotateButton.SetActive(false);
            return;
        }

        else if (UnitManager.Instance.currentStatus[this.posEasy] != null && selectedTile != null)
        {
            Debug.Log("2");
            UnitManager.Instance.SetSelectedTile(null);
            selectedTile.highlightOnSelect.SetActive(false);
            GameManager.Instance.rotateButton.SetActive(false);
            return;
        }

        else if (UnitManager.Instance.currentStatus[this.posEasy] != null && selectedTile == null)
        {
            Debug.Log("3");
            //Debug.Log("mid pos piece on select is: " + UnitManager.Instance.currentStatus[new Vector3(1.5f, 0.5f)]);
            UnitManager.Instance.SetSelectedTile(this);
            this.highlightOnSelect.SetActive(true);

            if (this.isRotatable)
            {
                GameManager.Instance.rotateButton.SetActive(true);
            }
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
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midPos]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        GameManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);
                    }

                    else
                    {
                        Debug.Log("8");
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        GameManager.Instance.rotateButton.SetActive(false);
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
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midPos]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        GameManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);
                    }

                    else
                    {
                        Debug.Log("11");
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        GameManager.Instance.rotateButton.SetActive(false);
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
                        //this.SetUnit(selectedTile.OccupiedUnit);
                        this.SetUnit(UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(UnitManager.Instance.currentStatus[midTile.posEasy]);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        UnitManager.Instance.isVisited.Add(this);
                        GameManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        UnitManager.Instance.piecesRemoved++;
                        UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);
                    }

                    else
                    {
                        Debug.Log("14");
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        GameManager.Instance.rotateButton.SetActive(false);
                        return;
                    }
                }

                else
                {
                    Debug.Log("15");
                    UnitManager.Instance.SetSelectedTile(null);
                    selectedTile.highlightOnSelect.SetActive(false);
                    GameManager.Instance.rotateButton.SetActive(false);
                    return;
                }
            }
            else
            {
                Debug.Log("16");
                UnitManager.Instance.SetSelectedTile(null);
                selectedTile.highlightOnSelect.SetActive(false);
                GameManager.Instance.rotateButton.SetActive(false);
                return;
            }
        }
    }
}
