using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int damage;

    public float speed;
    public float jumpheight;

    public void Damage(int dmg, string name)
    {
        health -= dmg;
        if (health >= 0)
        {
            Debug.Log("You died from " + name);

        }
    }
}
