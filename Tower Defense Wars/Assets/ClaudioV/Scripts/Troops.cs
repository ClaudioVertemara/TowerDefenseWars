using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Troops : MonoBehaviour
{
    Text troopsAmountText;
    int troopsAmount;
    float speed;

    GameObject toTower;

    // Start is called before the first frame update
    void Awake()
    {
        troopsAmountText = transform.GetChild(0).GetComponent<Text>();

        troopsAmount = 0;
        speed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, toTower.transform.position, speed);

        // Troops Arrived at Tower
        if (transform.position == toTower.transform.position) {
            TroopTower troopTower = toTower.GetComponent<TroopTower>();

            if (toTower.CompareTag("Blue")) {
                troopTower.ChangeTroopAmount(troopsAmount, true);
            } else {
                troopTower.ChangeTroopAmount(troopsAmount, false);
            }

            Destroy(gameObject);
        }
    }

    public void SpawnTroops(int troopsAmount, GameObject toTower) {
        this.troopsAmount = troopsAmount;
        troopsAmountText.text = troopsAmount.ToString();

        this.toTower = toTower;
    }
}
