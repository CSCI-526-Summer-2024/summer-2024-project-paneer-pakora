using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRotateClick : MonoBehaviour
{
    // if the rotate button is clicked
    public void OnClick()
    {
        OnRKeyPressed();
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
            if (topTile == null) return;
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

            //Debug.Log("Dictionary begins");
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(selectedX, topY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(diagonalX, diagonalY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(diagonalX, -1f * diagonalY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(selectedX, -1.0f * topY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(-1.0f * diagonalX, -1.0f * diagonalY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(-1.0f * diagonalX, diagonalY)]);
            //Debug.Log("Dictionary ends");

            //Debug.Log("end of line");



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


            //Debug.Log("Dictionary begins");
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(selectedX, topY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(diagonalX, diagonalY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(diagonalX, -1f * diagonalY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(selectedX, -1.0f * topY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(-1.0f * diagonalX, -1.0f * diagonalY)]);
            //Debug.Log(UnitManager.Instance.currentStatus[new Vector3(-1.0f * diagonalX, diagonalY)]);
            //Debug.Log("Dictionary ends");

        }
        else
        {
            //Debug.Log("No Tile was selected!");
            return;
        }
    }
}
