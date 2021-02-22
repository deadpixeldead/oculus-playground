using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    public void Damage()
    {
        Debug.Log("Health damage 10");
        health = health - 10;

        if (health <= 0)
        {
            Debug.Log("You died");

        }
    }
}
