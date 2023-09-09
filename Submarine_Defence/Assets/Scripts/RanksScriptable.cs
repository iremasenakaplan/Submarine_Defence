using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableRanksList", order = 1)]
public class RanksScriptable : ScriptableObject
{

    [SerializeField] public RankItem[] rankList;
}

[System.Serializable]
public class RankItem 
{
    public Sprite rankIcon;
    public int level ;
}