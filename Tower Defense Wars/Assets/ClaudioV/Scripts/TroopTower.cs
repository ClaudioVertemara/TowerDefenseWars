using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopTower : MonoBehaviour
{
    float troopAmount;
    Text troopText;

    float troopIncrease;
    float maxTroopAmount;

    // Start is called before the first frame update
    void Start()
    {
        troopAmount = 0;
        troopText = transform.GetChild(0).GetComponent<Text>();

        troopIncrease = 0.5f;
        maxTroopAmount = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Blue")) {
            troopAmount += troopIncrease * Time.deltaTime;
            troopText.text = ((int)troopAmount).ToString();
        }

        troopAmount = Mathf.Clamp(troopAmount, 0, maxTroopAmount);
    }
}
