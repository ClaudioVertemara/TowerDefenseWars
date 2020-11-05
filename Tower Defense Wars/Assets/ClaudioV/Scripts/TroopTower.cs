using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopTower : MonoBehaviour
{
    public float troopAmount;
    Text troopText;

    float troopIncrease;
    float maxTroopAmount;

    // Start is called before the first frame update
    void Start()
    {
        troopText = transform.GetChild(0).GetComponent<Text>();
        troopText.text = ((int)troopAmount).ToString();

        troopIncrease = 0.5f;
        maxTroopAmount = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Blue") && troopAmount <= maxTroopAmount) {
            troopAmount += troopIncrease * Time.deltaTime;
            troopText.text = ((int)troopAmount).ToString();
        }
    }

    public int GetTroopAmount() {
        return (int)troopAmount;
    }

    // Increase or Descrease Amount of Troops in Tower
    // Increase (Change = true) | Decrease (Change = false)
    public void ChangeTroopAmount(int amount, bool change) {
        if (!change) amount *= -1;

        if (troopAmount + amount < 0 && !CompareTag("Blue")) {
            tag = "Blue";
            GetComponent<Image>().color = Color.blue;
        }

        troopAmount = Mathf.Abs(troopAmount + amount);
        troopText.text = ((int)troopAmount).ToString();
    }
}
