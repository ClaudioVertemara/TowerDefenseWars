using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Text troopAmountText;
    int enemyAmount;
    string enemyType;
    float speed;
    int damage;

    GameObject toTower;

    GameObject goingToTower;
    GameObject Tower;

    // Start is called before the first frame update
    void Awake()
    {
        troopAmountText = transform.GetChild(1).GetComponent<Text>();

        enemyAmount = 0;
        enemyType = "F";
        speed = 0.2f;
        damage = 1;
        
    }

    // Called when Troops are Spawned, Sets Troops Info Up
    public void SpawnTroops(int enemyAmount, string enemyType, GameObject toTower)
    {
        this.enemyAmount = enemyAmount;
        troopAmountText.text = enemyAmount.ToString();

        this.enemyType = enemyType;
        SetSpeed();

        this.toTower = toTower;

        goingToTower = TargetTower();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, goingToTower.transform.position, speed * Time.deltaTime);

        // Troops Arrived at Tower
        if (transform.position == toTower.transform.position)
        {
            Tower tower = toTower.GetComponent<Tower>();

            if (toTower.CompareTag("Blue"))
            {
                tower.ChangeTroopAmount(enemyAmount, enemyType, "Red", true);
            }
            else
            {
                tower.ChangeTroopAmount(enemyAmount, enemyType, "Red", false);
            }

            Destroy(gameObject);
        }
    }

    // Change Speed (Based on Troop Type)
    void SetSpeed()
    {
        if (enemyType == "F")
        {
            speed = 0.2f;
        }
        else if (enemyType == "S")
        {
            speed = 0.1f;
        }
    }

    GameObject TargetTower()
    {
        for (int i = 0; i < Tower.transform.childCount; i++)
        {
            GameObject currentTower = Tower.transform.GetChild(i).gameObject;
            if (currentTower.tag == "White")
            {
                return currentTower;
                break;
            }
            else if (currentTower.tag == "Blue")
            {
                string towerType = currentTower.getComponent<Tower>().towerType;
                
                if (towerType == "Attack")
                {
                    return currentTower;
                } 
                else if (towerType == "Troop")
                {
                    return currentTower;
                }
            }
        }

        return null;
    }
}
