using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableLevel", order = 1)]
public class LevelScriptable : ScriptableObject
{
    public int startIndex;
    public GameObject[] enemyShips;
    public int killLimit;
    public GameObject environment;
    public bool isFoggy;
}