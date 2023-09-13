using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableEnvironment", order = 1)]
public class EnvironmentScriptable : ScriptableObject
{
    public GameObject ground;
    public Material[] skyboxes ;
    public GameObject sun ;
}

// [System.Serializable]
// public class Environment 
// {
//     public GameObject rankIcon;
//     public Material level ;
// }