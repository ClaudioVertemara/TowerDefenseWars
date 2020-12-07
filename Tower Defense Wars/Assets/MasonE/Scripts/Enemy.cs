using System.Collections;
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

    // Start is called before the first frame update
    void Awake()
    {
        tower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tower.troopAmount >= 5)
        {
            SendTroops(tower, TargetTower());
        }
    }

    GameObject TargetTower()
    {
        for (int i = 0; i < towers.transform.childCount; i++)
        {
            GameObject currentTower = towers.transform.GetChild(i).gameObject;
            if (currentTower.tag == "White")
            {
                return currentTower;
                break;
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
        }

        return null;
    }

    void SendTroops(Tower fromTower, GameObject toTower)
    {
        GameObject currTroops = Instantiate(troops, fromTower.transform.position, Quaternion.identity, troopsParent);

        string troopType = fromTower.GetTroopType();
        int troopAmount = fromTower.GetTroopAmount();

        fromTower.ChangeTroopAmount(troopAmount, troopType, "Red", false);

        currTroops.GetComponent<Troops>().SpawnTroops(troopAmount, troopType, toTower, "Red");
    }
}
