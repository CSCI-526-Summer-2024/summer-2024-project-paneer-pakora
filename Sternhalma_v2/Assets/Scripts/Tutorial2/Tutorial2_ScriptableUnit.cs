using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]

public class Tutorial2_ScriptableUnit : ScriptableObject
{
    public Faction Faction;
    public Tutorial2_BaseUnit UnitPrefab;

}

//public enum Faction
//{
//    Rock = 0,
//    Paper = 1,
//    Scissor = 2
//}
