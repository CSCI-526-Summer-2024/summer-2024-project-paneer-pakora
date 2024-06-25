using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressHandler : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRKeyPressed();
        }
    }

    void OnRKeyPressed()
    {
        
        HexTile selectedTile = UnitManager.Instance.selectedTile;

        if (selectedTile)
        {
            float selectedX = selectedTile.posEasy.x;
            float selectedY = selectedTile.posEasy.y;

            Vector3 top = new Vector3(selectedX, selectedY + 1.0f);
            Vector3 topRight = new Vector3(selectedX + 1.5f, selectedY + 0.5f);
            Vector3 bottomRight = new Vector3(selectedX + 1.5f, selectedY - 0.5f);
            Vector3 bottom = new Vector3(selectedX, selectedY - 1.0f);
            Vector3 bottomLeft = new Vector3(selectedX - 1.5f, selectedY - 0.5f);
            Vector3 topLeft = new Vector3(selectedX - 1.5f, selectedY + 0.5f);

            HexTile topTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(top));
            if(topTile == null) return;
            BaseUnit topPiece = UnitManager.Instance.currentStatus[top];

            HexTile topRightTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(topRight));
            if (topRightTile == null) return;
            BaseUnit topRightPiece = UnitManager.Instance.currentStatus[topRight];

            HexTile bottomRightTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(bottomRight));
            if (bottomRightTile == null) return;
            BaseUnit bottomRightPiece = UnitManager.Instance.currentStatus[bottomRight];

            HexTile bottomTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(bottom));
            if (bottomTile == null) return;
            BaseUnit bottomPiece = UnitManager.Instance.currentStatus[bottom];

            HexTile bottomLeftTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(bottomLeft));
            if (bottomLeftTile == null) return;
            BaseUnit bottomLeftPiece = UnitManager.Instance.currentStatus[bottomLeft];

            HexTile topLeftTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(topLeft));
            if (topLeftTile == null) return;
            BaseUnit topLeftPiece = UnitManager.Instance.currentStatus[topLeft];

            RemovePotentialHighlight(selectedTile.posEasy);

            // Rotation
            topTile.SetUnitRotation(topLeftPiece);
            topLeftTile.SetUnitRotation(bottomLeftPiece);
            bottomLeftTile.SetUnitRotation(bottomPiece);
            bottomTile.SetUnitRotation(bottomRightPiece);
            bottomRightTile.SetUnitRotation(topRightPiece);
            topRightTile.SetUnitRotation(topPiece);

            UnitManager.Instance.UpdateCurrentStatusRotation(top, topLeftPiece);
            UnitManager.Instance.UpdateCurrentStatusRotation(topLeft, bottomLeftPiece);
            UnitManager.Instance.UpdateCurrentStatusRotation(bottomLeft, bottomPiece);
            UnitManager.Instance.UpdateCurrentStatusRotation(bottom, bottomRightPiece);
            UnitManager.Instance.UpdateCurrentStatusRotation(bottomRight, topRightPiece);
            UnitManager.Instance.UpdateCurrentStatusRotation(topRight, topPiece);

            AddPotentialHighlight(selectedTile.posEasy);
            return;

        }
        else
        {
            //Debug.Log("No Tile was selected!");
            return;
        }
    }

    private void RemovePotentialHighlight(Vector3 pos)
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

    private void AddPotentialHighlight(Vector3 pos)
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

    private bool canJump(BaseUnit base1, BaseUnit base2)
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
}
