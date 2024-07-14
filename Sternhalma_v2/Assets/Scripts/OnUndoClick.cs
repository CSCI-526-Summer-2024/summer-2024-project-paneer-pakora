using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUndoClick : MonoBehaviour
{
    // if the undo button is clicked

    private void Start()
    {
        //GridManager.Instance.disableUndo();
        //GridManager.Instance.undoButton.SetActive(false);

        //buttonOutline = GridManager.Instance.undoButton.GetComponent<Image>();
        //undoIcon = GridManager.Instance.undoButton.transform.GetChild(0).GetComponent<Image>();

        //buttonOutline.color = new Color(buttonOutline.color.r, buttonOutline.color.g, buttonOutline.color.b, 0.5f);
        //undoIcon.color = new Color(undoIcon.color.r, undoIcon.color.g, undoIcon.color.b, 0.5f);
    }

    public void OnClick()
    {
        OnUKeyPressed();
    }

    void OnUKeyPressed()
    {
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


                    if (UnitManager.Instance.currentStatus[kvp.Key] == null)
                    {
                        tile.SetUnit(spawnedObj);
                        if (lastGreen[kvp.Key].Equals("green"))
                        {
                            Debug.Log("Vector: " + kvp.Key + " HERERERERRERER");
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

            if (lastMovedUnit.Equals("r"))
            {
                UnitManager.Instance.currentScissorCount += 1;
                UnitManager.Instance.scissorsLeft.text = UnitManager.Instance.currentScissorCount.ToString();
            }

            else if (lastMovedUnit.Equals("p"))
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

            if (UnitManager.Instance.pastStates.Count == 0)
            {
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


}
