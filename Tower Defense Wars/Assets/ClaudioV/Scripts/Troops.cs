using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* [Troops Script]
 * Troop Objects that are Spawned & Sent to Other Towers
 */

public class Troops : MonoBehaviour
{
    public AudioClip rocketPower;
    public float volume = 0.5f;
    public bool toggle = false;


    Text troopsAmountText;
    int troopsAmount;
    string troopsType;
    float speed;

    GameObject toTower;

    // Start is called before the first frame update
    void Awake()
    {
        troopsAmountText = transform.GetChild(1).GetComponent<Text>();

        troopsAmount = 0;
        troopsType = "F";
        SetSpeed();
    }

    // Called when Troops are Spawned, Sets Troops Info Up
    public void SpawnTroops(int troopsAmount, string troopsType, GameObject toTower) {
        this.troopsAmount = troopsAmount;
        troopsAmountText.text = troopsAmount.ToString();

        this.troopsType = troopsType;
        SetSpeed();

        this.toTower = toTower;
        toggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 target = new Vector3(toTower.transform.position.x, toTower.transform.position.y, 0.5f);
        transform.position = Vector3.MoveTowards(transform.position, toTower.transform.position, speed * Time.deltaTime);

        // Rotate Troops Ship to Face Tower
        Vector3 rtarget = Vector3.zero;
        rtarget.x = toTower.transform.position.x - transform.position.x;
        rtarget.y = toTower.transform.position.y - transform.position.y;

        float angle = Mathf.Atan2(rtarget.y, rtarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // Troops Arrived at Tower
        if (transform.position == toTower.transform.position) {
            Tower tower = toTower.GetComponent<Tower>();

            if (toTower.CompareTag("Blue")) {
                tower.ChangeTroopAmount(troopsAmount, troopsType, true);
            } else {
                tower.ChangeTroopAmount(troopsAmount, troopsType, false);
            }
            toggle = false;

            Destroy(gameObject);
        }
    }

    // Change Speed (Based on Troop Type)
    void SetSpeed() {
        if (troopsType == "F") {
            speed = 1f;
        } else if (troopsType == "S") {
            speed = 0.5f;
        }
    }




}
