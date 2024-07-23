using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideLegend : MonoBehaviour
{
    public HexTile selectedTile;


    // Start is called before the first frame update
    void Start()
    {
        //Image tempLegend = GameObject.Find("tempLegend").GetComponent<Image>();
        //tempLegend.enabled = false;
        if (GridManager.selectedLevel==0) { 
        var paperLeg = GameObject.Find("PaperLegend");
        paperLeg.transform.localScale = new Vector3(0, 0, 0);

        var rockLeg = GameObject.Find("RockLegend");
        rockLeg.transform.localScale = new Vector3(0, 0, 0);

        var scissorLeg = GameObject.Find("ScissorLegend");
        scissorLeg.transform.localScale = new Vector3(0, 0, 0);
    }
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (PauseMenu.gameIsPaused)
        {
            return; 
        }

        if (GridManager.selectedLevel == 0)
        {
            var currentPieceCount = UnitManager.Instance.currentScissorCount + UnitManager.Instance.currentRockCount + UnitManager.Instance.currentPaperCount;

            selectedTile = UnitManager.Instance.selectedTile;
            //Image tempLegend = GameObject.Find("tempLegend").GetComponent<Image>();
            var paperLeg = GameObject.Find("PaperLegend");
            var rockLeg = GameObject.Find("RockLegend");
            var scissorLeg = GameObject.Find("ScissorLegend");

            Debug.Log("CURRENT PIECE COUNT IS");
            Debug.Log(currentPieceCount);

            if (currentPieceCount == 3)
            {
                Tut1_GameManager.Instance.Guide2.SetActive(false);
                Tut1_GameManager.Instance.Guide3.SetActive(true);
            }

            if (currentPieceCount == 2)
            {
                Tut1_GameManager.Instance.Guide4.SetActive(false);
                Tut1_GameManager.Instance.Guide5.SetActive(true);
            }

            if (currentPieceCount == 1)
            {
                Tut1_GameManager.Instance.Guide6.SetActive(false);
            }


            if (selectedTile != null)
            {
                Debug.Log("Show/Hide Legend selectedTile is: " + selectedTile.posEasy);
                Debug.Log(selectedTile);

                // =============================================================
                // Attempting the guide animations
                
                Vector3 firstTile = new Vector3(-3.00f, -1.00f, 0.00f);
                Vector3 secondTile = new Vector3(0.00f, 0.00f, 0.00f);
                Vector3 thirdTile = new Vector3(0.00f, 1.00f, 0.00f);
                Vector3 fifthTile = new Vector3(1.50f, -1.50f, 0.00f);

                if (currentPieceCount == 4)
                {
                    if (selectedTile.posEasy == firstTile)
                    {
                        //if (Tut1_GameManager.Instance.Guide1.activeSelf)
                        //{
                            Tut1_GameManager.Instance.Guide1.SetActive(false);
                            Tut1_GameManager.Instance.Guide2.SetActive(true);
                        //}

                        //else
                        //{
                        //    Tut1_GameManager.Instance.Guide1.SetActive(true);
                        //}

                        // if piece count is 4 and the tile selected is the first tile:
                        // click tile --> guide 1 disappears,  guide 2 appears
                        // user clicks the right tile --> guide 2 disappears
                    }

                    else
                    {
                        Tut1_GameManager.Instance.Guide1.SetActive(true);
                        Tut1_GameManager.Instance.Guide2.SetActive(false);
                    }
                }

                if (currentPieceCount == 3)
                {
                    if (selectedTile.posEasy == thirdTile)
                    {
                        Tut1_GameManager.Instance.Guide3.SetActive(false);
                        Tut1_GameManager.Instance.Guide4.SetActive(true);
                    }

                    else
                    {
                        Tut1_GameManager.Instance.Guide3.SetActive(true);
                        Tut1_GameManager.Instance.Guide4.SetActive(false);
                    }
                }

                if (currentPieceCount == 2)
                {
                    if (selectedTile.posEasy == fifthTile)
                    {
                        Tut1_GameManager.Instance.Guide5.SetActive(false);
                        Tut1_GameManager.Instance.Guide6.SetActive(true);
                    }

                    else
                    {
                        Tut1_GameManager.Instance.Guide5.SetActive(true);
                        Tut1_GameManager.Instance.Guide6.SetActive(false);
                    }
                }


                // =============================================================

                if (selectedTile.OccupiedUnit.Faction == Faction.Rock)
                {
                    Debug.Log("Rock!");
                    //tempLegend.enabled = true;
                    rockLeg.transform.localScale = new Vector3(1, 1, 1);

                    paperLeg.transform.localScale = new Vector3(0, 0, 0);
                    scissorLeg.transform.localScale = new Vector3(0, 0, 0);

                }
                else if (selectedTile.OccupiedUnit.Faction == Faction.Paper)
                {
                    paperLeg.transform.localScale = new Vector3(1, 1, 1);

                    rockLeg.transform.localScale = new Vector3(0, 0, 0);
                    scissorLeg.transform.localScale = new Vector3(0, 0, 0);
                    Debug.Log("Paper!");
                }
                else if (selectedTile.OccupiedUnit.Faction == Faction.Scissor)
                {
                    Debug.Log("Scissor!");
                    scissorLeg.transform.localScale = new Vector3(1, 1, 1);

                    paperLeg.transform.localScale = new Vector3(0, 0, 0);
                    rockLeg.transform.localScale = new Vector3(0, 0, 0);
                }
            }

            else // selectedTile == null
            {
                if (currentPieceCount == 4)
                {
                    Tut1_GameManager.Instance.Guide1.SetActive(true);
                    Tut1_GameManager.Instance.Guide2.SetActive(false);
                }

                if (currentPieceCount == 3)
                {
                    Tut1_GameManager.Instance.Guide3.SetActive(true);
                    Tut1_GameManager.Instance.Guide4.SetActive(false);
                }

                if (currentPieceCount == 2)
                {
                    Tut1_GameManager.Instance.Guide5.SetActive(true);
                    Tut1_GameManager.Instance.Guide6.SetActive(false);
                }

                //tempLegend.enabled = false;
                paperLeg.transform.localScale = new Vector3(0, 0, 0);
                rockLeg.transform.localScale = new Vector3(0, 0, 0);
                scissorLeg.transform.localScale = new Vector3(0, 0, 0);

            }

        }
    }
}
