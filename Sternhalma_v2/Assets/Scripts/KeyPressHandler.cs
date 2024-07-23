using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyPressHandler : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRKeyPressed();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            OnUKeyPressed();
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

    void OnUKeyPressed()
    {
        if (UnitManager.Instance.isGameEnded)
        {
            Debug.Log("Cannot Undo - Game has ended");
            return;
        }
        Debug.Log("pastStates.Count = " + UnitManager.Instance.pastStates.Count);

        if (UnitManager.Instance.pastStates.Count > 0)
        {
            //foreach (Tuple<String, Vector3, Vector3, Vector3> item in UnitManager.Instance.pastStates)
            //{
            //    Debug.Log(item);
            //}
            //Debug.Log("");

            Dictionary<Vector3, String> lastMove = UnitManager.Instance.pastStates.Pop();
            String lastMovedUnit = UnitManager.Instance.movedUnit.Pop();
            Dictionary<Vector3, String> lastGreen = UnitManager.Instance.lastTurnedGreen.Pop();
            Vector3 lastPos = UnitManager.Instance.lastGreenPos.Pop();

            UnitManager.Instance.SetSelectedTile(null);

            foreach (KeyValuePair<Vector3, String> kvp in lastMove)
            {

                var prefab = (BaseUnit)null;
                BaseUnit spawnedObj = null;

                RemovePotentialHighlight(kvp.Key);

                if (lastMove[kvp.Key] != null)
                {
                    if (lastMove[kvp.Key].Equals("s"))
                    {
                        prefab = UnitManager.Instance.GetUnit<Scissor>(Faction.Scissor);
                        spawnedObj = Instantiate(prefab);

                        //UnitManager.Instance.currentPaperCount += 1;
                        //UnitManager.Instance.papersLeft.text = UnitManager.Instance.currentPaperCount.ToString();

                    }

                    else if (lastMove[kvp.Key].Equals("r"))
                    {
                        prefab = UnitManager.Instance.GetUnit<Rock>(Faction.Rock);
                        spawnedObj = Instantiate(prefab);

                        //UnitManager.Instance.currentScissorCount += 1;
                        //UnitManager.Instance.scissorsLeft.text = UnitManager.Instance.currentScissorCount.ToString();
                    }

                    else
                    {
                        prefab = UnitManager.Instance.GetUnit<Paper>(Faction.Paper);
                        spawnedObj = Instantiate(prefab);

                        //UnitManager.Instance.currentRockCount -= 1;
                        //UnitManager.Instance.rocksLeft.text = UnitManager.Instance.currentRockCount.ToString();
                    }

                    Vector3 translatedPos = GridManager.Instance.GetTranslatedPos(kvp.Key);
                    HexTile tile = GridManager.Instance.GetTileAtPos(translatedPos);
                    tile.highlightOnSelect.SetActive(false);
                    tile.tileBorder.SetActive(false);


                    if (UnitManager.Instance.currentStatus[kvp.Key] == null)
                    {
                        tile.SetUnit(spawnedObj);
                        if (lastGreen[kvp.Key].Equals("green"))
                        {
                            Debug.Log("Vector: "+ kvp.Key + " HERERERERRERER");
                            tile.SetColorToGreen();
                        }

                        else
                        {
                            tile.SetColorToWhite();
                        }
                        
                    }

                    else
                    {
                        tile.RemoveUnit(UnitManager.Instance.currentStatus[kvp.Key]);
                        tile.SetUnit(spawnedObj);

                        if (lastGreen[kvp.Key].Equals("green"))
                        {
                            tile.SetColorToGreen();
                        }
                        else
                        {
                            tile.SetColorToWhite();
                        }
                    }

                    UnitManager.Instance.tileToUnit[tile] = spawnedObj;
                    UnitManager.Instance.currentStatus[kvp.Key] = spawnedObj;
                }

                else
                {
                    Vector3 translatedPos = GridManager.Instance.GetTranslatedPos(kvp.Key);
                    HexTile tile = GridManager.Instance.GetTileAtPos(translatedPos);

                    if (UnitManager.Instance.currentStatus[kvp.Key] != null)
                    {
                        tile.RemoveUnit(UnitManager.Instance.currentStatus[kvp.Key]);
                        UnitManager.Instance.currentStatus[kvp.Key] = null;
                        UnitManager.Instance.tileToUnit[tile] = null;

                        if (lastGreen[kvp.Key].Equals("green"))
                        {
                            tile.SetColorToGreen();
                        }
                        else
                        {
                            tile.SetColorToWhite();
                        }
                    }
                }
            }

            //if (lastMove.Item1.Equals("scissor"))
            //{
            //    prefab1 = UnitManager.Instance.GetUnit<Scissor>(Faction.Scissor);
            //    spawnedObj1 = Instantiate(prefab1);

            //    prefab2 = UnitManager.Instance.GetUnit<Paper>(Faction.Paper);
            //    spawnedObj2 = Instantiate(prefab2);

            //    UnitManager.Instance.currentPaperCount += 1;
            //    UnitManager.Instance.papersLeft.text = UnitManager.Instance.currentPaperCount.ToString();

            //}

            //else if(lastMove.Item1.Equals("rock"))
            //{
            //    prefab1 = UnitManager.Instance.GetUnit<Rock>(Faction.Rock);
            //    spawnedObj1 = Instantiate(prefab1);

            //    prefab2 = UnitManager.Instance.GetUnit<Scissor>(Faction.Scissor);
            //    spawnedObj2 = Instantiate(prefab2);

            //    UnitManager.Instance.currentScissorCount += 1;
            //    UnitManager.Instance.scissorsLeft.text = UnitManager.Instance.currentScissorCount.ToString();
            //}

            //else
            //{
            //    prefab1 = UnitManager.Instance.GetUnit<Paper>(Faction.Paper);
            //    spawnedObj1 = Instantiate(prefab1);

            //    prefab2 = UnitManager.Instance.GetUnit<Rock>(Faction.Rock);
            //    spawnedObj2 = Instantiate(prefab2);

            //    UnitManager.Instance.currentRockCount -= 1;
            //    UnitManager.Instance.rocksLeft.text = UnitManager.Instance.currentRockCount.ToString();
            //}



            //Vector3 translatedPos1 = GridManager.Instance.GetTranslatedPos(lastMove.Item2);
            //Vector3 translatedPos2 = GridManager.Instance.GetTranslatedPos(lastMove.Item3);
            //Vector3 translatedPos3 = GridManager.Instance.GetTranslatedPos(lastMove.Item4);

            //HexTile tile1 = GridManager.Instance.GetTileAtPos(translatedPos1);
            //HexTile tile2 = GridManager.Instance.GetTileAtPos(translatedPos2);
            //HexTile tile3 = GridManager.Instance.GetTileAtPos(translatedPos3);

            //tile1.SetUnit(spawnedObj1);
            //tile2.SetUnit(spawnedObj2);
            //tile3.RemoveUnit(UnitManager.Instance.currentStatus[lastMove.Item4]);

            //UnitManager.Instance.tileToUnit[tile1] = spawnedObj1;
            //UnitManager.Instance.currentStatus[lastMove.Item2] = spawnedObj1;

            //UnitManager.Instance.tileToUnit[tile2] = spawnedObj2;
            //UnitManager.Instance.currentStatus[lastMove.Item3] = spawnedObj2;

            //tile3.SetColorToWhite();
            //UnitManager.Instance.isVisited.Remove(tile3);

            if (lastMovedUnit.Equals("r"))
            {
                UnitManager.Instance.currentScissorCount += 1;
                UnitManager.Instance.scissorsLeft.text = UnitManager.Instance.currentScissorCount.ToString();
            }

            else if(lastMovedUnit.Equals("p"))
            {
                UnitManager.Instance.currentRockCount += 1;
                UnitManager.Instance.rocksLeft.text = UnitManager.Instance.currentRockCount.ToString();
            }

            else
            {
                UnitManager.Instance.currentPaperCount += 1;
                UnitManager.Instance.papersLeft.text = UnitManager.Instance.currentPaperCount.ToString();
            }

            //Vector3 lastGreenTranslatedPos = GridManager.Instance.GetTranslatedPos(lastGreen.Item1);
            //HexTile lastGreenTile = GridManager.Instance.GetTileAtPos(lastGreenTranslatedPos);

            //if (lastGreen.Item2.Equals("white"))
            //{
            //    lastGreenTile.SetColorToWhite();
            //}

            HexTile lastPosTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(lastPos));
            UnitManager.Instance.isVisited.Remove(lastPosTile);
            UnitManager.Instance.tileCoverageMeter.SetProgress(UnitManager.Instance.isVisited.Count);

            UnitManager.Instance.piecesRemoved--;
            UnitManager.Instance.piecesRemovedMeter.SetProgress(UnitManager.Instance.piecesRemoved);

            Debug.Log("pastStates.Count before disable = " + UnitManager.Instance.pastStates.Count);
            if (UnitManager.Instance.pastStates.Count == 0)
            {
                Debug.Log("pastStates = 0; undo disabled");
                GridManager.Instance.disableUndo();
            }

            Timer timer = FindObjectOfType<Timer>();
            timer.timeRemaining -= 30.0f;
            timer.DisplayTimeReduction();

            if (timer.timeRemaining <= 0.0f)
            {
                timer.DisplayTime(timer.timeRemaining);
            }

        }

        else
        {
            Debug.Log("Cannot Undo - Undo Limit Reached");
        }

    }
}
