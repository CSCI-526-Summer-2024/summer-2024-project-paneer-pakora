using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public HexTile OccupiedTile = null;
    public Faction Faction;

    public void SetActive(bool b)
    {
        this.SetActive(b);
    }
}
