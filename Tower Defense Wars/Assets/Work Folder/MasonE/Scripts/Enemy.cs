﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    GameObject toTower;
    public GameObject towers;
    Tower tower;

    public GameObject troops;
    public Transform troopsParent;

    float timer;
    GameObject currentTower;

    // Start is called before the first frame update
    void Awake()
    {
        tower = GetComponent<Tower>();
        timer = Random.Range(7f, 15f);

        int random = Random.Range(2, 13);
        currentTower = towers.transform.GetChild(random).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            SendTroops(tower, TargetTower());
            timer = Random.Range(7f, 15f);
        } else {
            timer -= Time.deltaTime;
        }
    }

    GameObject TargetTower()
    {
        if (currentTower.tag == "Red") {
            while (true) {
                int random = Random.Range(0, 13);
                currentTower = towers.transform.GetChild(random).gameObject;

                if (currentTower.tag != "Red") {
                    break;
                }
            }
        }

        /*for (int i = 0; i < towers.transform.childCount; i++)
        {

            currentTower = towers.transform.GetChild(i).gameObject;
            if (currentTower.tag == "White")
            {
                return currentTower;
            }
            else if (currentTower.tag == "Blue")
            {
                string towerType = currentTower.GetComponent<Tower>().towerType;
                
                if (towerType == "Attack")
                {
                    return currentTower;
                } 
                else if (towerType == "Troop")
                {
                    return currentTower;
                }
            }
        }*/

        return currentTower;
    }

    void SendTroops(Tower fromTower, GameObject toTower)
    {
        int troopAmount = fromTower.GetTroopAmount();

        if (troopAmount > 0) {
            GameObject currTroops = Instantiate(troops, fromTower.transform.position, Quaternion.identity, troopsParent);

            string troopType = fromTower.GetTroopType();

            fromTower.ChangeTroopAmount(troopAmount, troopType, "Red", false);

            currTroops.GetComponent<Troops>().SpawnTroops(troopAmount, troopType, toTower, "Red");
        }
    }
}
