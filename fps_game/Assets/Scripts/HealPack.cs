using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour, IItem
{
    public float health = 50;
    public void Use(GameObject target)
    {
        Debug.Log("체력이 회복했다: " + health);
    }
}
