using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health;
    int damage;
    string type;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setHealth(int health)
    {
        this.health = health;
    }

    int getHealth()
    {
        return health;
    }

    void setDamage(int damage)
    {
        this.damage = damage;
    }

    int getDamage()
    {
        return damage;
    }

    void setType(string type)
    {
        this.type = type;
    }

    string getType()
    {
        return type; ;
    }

    void setSpeed(float speed)
    {
        this.speed = speed;
    }

    float getSpeed()
    {
        return speed;
    }
}
