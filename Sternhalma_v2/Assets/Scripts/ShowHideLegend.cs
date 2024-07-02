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

        var paperLeg = GameObject.Find("PaperLegend");
        paperLeg.transform.localScale = new Vector3(0, 0, 0);

        var rockLeg = GameObject.Find("RockLegend");
        rockLeg.transform.localScale = new Vector3(0, 0, 0);

        var scissorLeg = GameObject.Find("ScissorLegend");
        scissorLeg.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (GridManager.selectedLevel == 0)
        {
            selectedTile = UnitManager.Instance.selectedTile;
            //Image tempLegend = GameObject.Find("tempLegend").GetComponent<Image>();
            var paperLeg = GameObject.Find("PaperLegend");
            var rockLeg = GameObject.Find("RockLegend");
            var scissorLeg = GameObject.Find("ScissorLegend");

            if (selectedTile != null)
            {
                Debug.Log("Show/Hide Legend selectedTile is: " + selectedTile.posEasy);
                Debug.Log(selectedTile);

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

            else
            {
                //tempLegend.enabled = false;
                paperLeg.transform.localScale = new Vector3(0, 0, 0);
                rockLeg.transform.localScale = new Vector3(0, 0, 0);
                scissorLeg.transform.localScale = new Vector3(0, 0, 0);

            }
        }
    }
}
