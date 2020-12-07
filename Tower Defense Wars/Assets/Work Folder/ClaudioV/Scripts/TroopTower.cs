using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* [Troop Tower Script]
 * Towers that Spawn Troops
 */

public class TroopTower : MonoBehaviour
{
    Tower tower;

    float troopIncrease;

    void Awake() {
        tower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn Troops if Tower Not Neutral & Troop Spawn Limit Not Reached
        if (!CompareTag("White") && tower.troopAmount <= tower.maxTroopAmount) {
            tower.troopAmount += troopIncrease * Time.deltaTime;
            tower.UpdateTroopText();
        }
    }

    // Update Amount of Troops are Spawned (Based on Troop Type)
    public void UpdateIncreaseAmount() {
        if (tower.troopType == "F") {
            troopIncrease = 0.5f;
        } else {
            troopIncrease = 0.25f;
        }
    }
}
