using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2_HexTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject highlightOnSelect;

    public Tutorial2_BaseUnit OccupiedUnit;
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

    public void SetUnit(Tutorial2_BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        this.OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void SetUnitRotation(Tutorial2_BaseUnit unit)
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

    public void RemoveUnit(Tutorial2_BaseUnit unit)
    {
        unit.OccupiedTile = null;
        this.OccupiedUnit = null;
        Destroy(unit.gameObject);
    }

    private void OnMouseDown()
    {
        if (Tutorial2_GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        Tutorial2_HexTile selectedTile = Tutorial2_UnitManager.Instance.selectedTile;

        if (Tutorial2_UnitManager.Instance.currentStatus[this.posEasy] == null && selectedTile == null)
        {
            //Debug.Log("1");
            Tutorial2_UnitManager.Instance.SetSelectedTile(null);
            Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
            return;
        }

        else if (Tutorial2_UnitManager.Instance.currentStatus[this.posEasy] != null && selectedTile != null)
        {
            Debug.Log("2");
            Tutorial2_UnitManager.Instance.SetSelectedTile(null);
            selectedTile.highlightOnSelect.SetActive(false);
            Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
            return;
        }

        else if (Tutorial2_UnitManager.Instance.currentStatus[this.posEasy] != null && selectedTile == null)
        {
            Debug.Log("3");
            //Debug.Log("mid pos piece on select is: " + UnitManager.Instance.currentStatus[new Vector3(1.5f, 0.5f)]);
            Tutorial2_UnitManager.Instance.SetSelectedTile(this);
            this.highlightOnSelect.SetActive(true);

            if (this.isRotatable)
            {
                Tutorial2_GameManager.Instance.rotateButton.SetActive(true);
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
                Tutorial2_UnitManager.Instance.currentStatus[currentPos] == null
               )
            {
                Debug.Log("5");
                if (Tutorial2_UnitManager.Instance.currentStatus[selectedTile.posEasy].Faction == Faction.Rock)
                    {
                        Debug.Log("6");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    Tutorial2_HexTile midTile = Tutorial2_GridManager.Instance.GetTileAtPos(Tutorial2_GridManager.Instance.GetTranslatedPos(midPos));

                    if (Tutorial2_UnitManager.Instance.currentStatus[midPos] != null && Tutorial2_UnitManager.Instance.currentStatus[midPos].Faction == Faction.Scissor)
                    {
                        Debug.Log("7");
                        this.SetUnit(Tutorial2_UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(Tutorial2_UnitManager.Instance.currentStatus[midPos]);
                        Tutorial2_UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        Tutorial2_UnitManager.Instance.isVisited.Add(this);
                        Tutorial2_GameManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        Tutorial2_UnitManager.Instance.tileCoverageMeter.SetProgress(Tutorial2_UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        Tutorial2_UnitManager.Instance.piecesRemoved++;
                        Tutorial2_UnitManager.Instance.piecesRemovedMeter.SetProgress(Tutorial2_UnitManager.Instance.piecesRemoved);
                    }

                    else
                    {
                        Debug.Log("8");
                        Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
                        return;
                    }
                }

                else if (Tutorial2_UnitManager.Instance.currentStatus[selectedTile.posEasy].Faction == Faction.Paper)
                {
                    Debug.Log("9");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    Tutorial2_HexTile midTile = Tutorial2_GridManager.Instance.GetTileAtPos(Tutorial2_GridManager.Instance.GetTranslatedPos(midPos));

                    if (Tutorial2_UnitManager.Instance.currentStatus[midPos] != null && Tutorial2_UnitManager.Instance.currentStatus[midPos].Faction == Faction.Rock)
                    {
                        Debug.Log("10");
                        this.SetUnit(Tutorial2_UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(Tutorial2_UnitManager.Instance.currentStatus[midPos]);
                        Tutorial2_UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        Tutorial2_UnitManager.Instance.isVisited.Add(this);
                        Tutorial2_GameManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        Tutorial2_UnitManager.Instance.tileCoverageMeter.SetProgress(Tutorial2_UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        Tutorial2_UnitManager.Instance.piecesRemoved++;
                        Tutorial2_UnitManager.Instance.piecesRemovedMeter.SetProgress(Tutorial2_UnitManager.Instance.piecesRemoved);
                    }

                    else
                    {
                        Debug.Log("11");
                        Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
                        return;
                    }
                }

                else if (Tutorial2_UnitManager.Instance.currentStatus[selectedTile.posEasy].Faction == Faction.Scissor)
                {
                    Debug.Log("12");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    Tutorial2_HexTile midTile = Tutorial2_GridManager.Instance.GetTileAtPos(Tutorial2_GridManager.Instance.GetTranslatedPos(midPos));

                    if (Tutorial2_UnitManager.Instance.currentStatus[midPos] != null && Tutorial2_UnitManager.Instance.currentStatus[midPos].Faction == Faction.Paper)
                    {
                        Debug.Log("13");
                        //this.SetUnit(selectedTile.OccupiedUnit);
                        this.SetUnit(Tutorial2_UnitManager.Instance.currentStatus[selectedPos]);
                        midTile.RemoveUnit(Tutorial2_UnitManager.Instance.currentStatus[midTile.posEasy]);
                        Tutorial2_UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        this.SetColorToGreen();
                        Tutorial2_UnitManager.Instance.isVisited.Add(this);
                        Tutorial2_GameManager.Instance.rotateButton.SetActive(false);

                        // increment % tiles covered
                        Tutorial2_UnitManager.Instance.tileCoverageMeter.SetProgress(Tutorial2_UnitManager.Instance.isVisited.Count);

                        // update # pieces removed 
                        Tutorial2_UnitManager.Instance.piecesRemoved++;
                        Tutorial2_UnitManager.Instance.piecesRemovedMeter.SetProgress(Tutorial2_UnitManager.Instance.piecesRemoved);
                    }

                    else
                    {
                        Debug.Log("14");
                        Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
                        return;
                    }
                }

                else
                {
                    Debug.Log("15");
                    Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                    selectedTile.highlightOnSelect.SetActive(false);
                    Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
                    return;
                }
            }
            else
            {
                Debug.Log("16");
                Tutorial2_UnitManager.Instance.SetSelectedTile(null);
                selectedTile.highlightOnSelect.SetActive(false);
                Tutorial2_GameManager.Instance.rotateButton.SetActive(false);
                return;
            }
        }
    }
}
