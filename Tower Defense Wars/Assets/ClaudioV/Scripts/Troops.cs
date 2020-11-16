using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* [Troops Script]
 * Troop Objects that are Spawned & Sent to Other Towers
 */

public class Troops : MonoBehaviour
{
    Text troopsAmountText;
    int troopsAmount;
    string troopsType;
    float speed;

    GameObject toTower;

    // Start is called before the first frame update
    void Awake()
    {
        troopsAmountText = transform.GetChild(0).GetComponent<Text>();

        troopsAmount = 0;
        troopsType = "F";
        speed = 0.2f;
    }

    // Called when Troops are Spawned, Sets Troops Info Up
    public void SpawnTroops(int troopsAmount, string troopsType, GameObject toTower) {
        this.troopsAmount = troopsAmount;
        troopsAmountText.text = troopsAmount.ToString();

        this.troopsType = troopsType;
        SetSpeed();

        this.toTower = toTower;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, toTower.transform.position, speed);

        // Troops Arrived at Tower
        if (transform.position == toTower.transform.position) {
            Tower tower = toTower.GetComponent<Tower>();

            if (toTower.CompareTag("Blue")) {
                tower.ChangeTroopAmount(troopsAmount, troopsType, true);
            } else {
                tower.ChangeTroopAmount(troopsAmount, troopsType, false);
            }

            Destroy(gameObject);
        }
    }

    // Change Speed (Based on Troop Type)
    void SetSpeed() {
        if (troopsType == "F") {
            speed = 0.2f;
        } else if (troopsType == "S") {
            speed = 0.1f;
        }
    }
}
