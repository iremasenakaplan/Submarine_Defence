using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableShip", order = 1)]
public class ShipScriptable : ScriptableObject
{
    public string name;
    public string gunCode;
    public int hp;
    public int damage;
    public int zoom;
    public float fireRate;
    public int price;
    public GameObject menuGun;
    public GameObject gameGun;
    public Sprite flag;
}