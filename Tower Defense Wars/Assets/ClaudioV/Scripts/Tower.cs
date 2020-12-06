using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

/* [Tower Script]
 * Contains Troop Info
 * Controls All Tower UI
 * Activates / Deactivates Tower Types
 * * Troop Tower
 * * Defense / Attack Tower
 */

// Troop Types:
// F - Fast: Spawn Faster, Move Faster, Cause Less Damage
// S - Strong: Spawn Slower, Move Slower, Cause More Damage

public class Tower : MonoBehaviour
{
    TroopTower troopTower;
    [HideInInspector]
    public GameObject towerMenu;
    
    // Troop Info
    public float troopAmount;
    Text troopAmountText;
    public float maxTroopAmount;
    public string troopType;
    Text troopTypeText;

    public Sprite blueStation;
    public Sprite redStation;
    public AnimatorController blueTwinkle;
    public AnimatorController redTwinkle;
    GameObject towerImage;

    // Start is called before the first frame update
    void Start() {
        troopTower = GetComponent<TroopTower>();

        towerMenu = transform.GetChild(3).gameObject;
        towerMenu.SetActive(false);

        troopAmountText = transform.GetChild(1).GetComponent<Text>();
        troopAmountText.text = ((int)troopAmount).ToString();

        troopType = "F";
        troopTypeText = transform.GetChild(2).GetComponent<Text>();
        if (!CompareTag("Blue")) troopTypeText.text = "";

        troopTower.UpdateIncreaseAmount();
        maxTroopAmount = 50f;

        towerImage = transform.GetChild(0).gameObject;
    }

    public int GetTroopAmount() {
        return (int)troopAmount;
    }

    // Increase or Descrease Amount of Troops in Tower
    // Increase (Change = true) | Decrease (Change = false)
    public void ChangeTroopAmount(int amount, string type, bool change) {
        // Penelty for Converting Troop Types (Reduce Amount)
        if (troopType != type && CompareTag("Blue")) {
            amount = (int)(amount * 0.5f);
        }

        // Increase Damage (Strong Troop Type)
        if (type == "S" && !CompareTag("Blue")) {
            amount = (int)(amount * 1.25f);
        }

        if (!change) amount *= -1;

        // Tower Got Taken Over
        if (troopAmount + amount < 0 && !CompareTag("Blue")) {
            tag = "Blue";
            //GetComponent<Image>().color = Color.blue;
            towerImage.GetComponent<Image>().sprite = blueStation;
            towerImage.GetComponent<Animator>().runtimeAnimatorController = blueTwinkle;

            SetTroopType(type);
            troopTower.UpdateIncreaseAmount();
        }

        troopAmount = Mathf.Abs(troopAmount + amount);
        UpdateTroopText();
    }

    public string GetTroopType() {
        return troopType;
    }

    // Set the Troop Type this Tower Contains
    public void SetTroopType(string type) {
        troopType = type;

        troopTypeText.text = type.ToString();

        troopTower.UpdateIncreaseAmount();
    }

    // Convert the Troop Type Tower Contains (UI)
    // Adds a Conversion Cost (Decreases Troop Amount)
    public void ConvertTroopType(string type) {
        int conversionCost = 10;

        if (troopAmount >= conversionCost) {
            troopAmount -= conversionCost;
            SetTroopType(type);
        }
    }

    public void UpdateTroopText() {
        troopAmountText.text = ((int)troopAmount).ToString();
    }
}
