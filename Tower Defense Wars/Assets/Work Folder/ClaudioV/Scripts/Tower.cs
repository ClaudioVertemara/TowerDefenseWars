using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    AttackTower attackTower;
    Enemy enemy;
    public string towerType;
    [HideInInspector]
    public GameObject towerMenu;
    
    // Troop Info
    public float troopAmount;
    Text troopAmountText;
    public float maxTroopAmount;
    public string troopType;
    Text troopTypeText;

    public RuntimeAnimatorController blueStation;
    public RuntimeAnimatorController redStation;
    public RuntimeAnimatorController blueAttackStation;
    public RuntimeAnimatorController redAttackStation;
    GameObject towerImage;

    public GameOver gameOver;
    CircleCollider2D cc;

    public AudioSource audioImpact;
    public AudioSource audioTakeOver;

    // Start is called before the first frame update
    void Start() {
        troopTower = GetComponent<TroopTower>();
        attackTower = GetComponent<AttackTower>();
        enemy = GetComponent<Enemy>();

        cc = GetComponent<CircleCollider2D>();

        if (towerType == "Troop") {
            attackTower.enabled = false;
            cc.enabled = false;
        } else {
            troopTower.enabled = false;
        }

        if (gameObject.tag != "Red") {
            enemy.enabled = false;
        }

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
    public void ChangeTroopAmount(int amount, string type, string teamColor, bool change) {
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
        if (troopAmount + amount < 0 && !change) {
            gameObject.tag = teamColor;
            if (teamColor == "Blue") {
                towerImage.GetComponent<Animator>().runtimeAnimatorController = blueStation;
                enemy.enabled = false;
            } else if (teamColor == "Red") {
                towerImage.GetComponent<Animator>().runtimeAnimatorController = redStation;
                enemy.enabled = true;
            }

            SetTroopType(type);
            troopTower.UpdateIncreaseAmount();
            gameOver.CheckIfWonOrLost();
            audioTakeOver.Play();
        } else if (!change && teamColor != gameObject.tag) {
            audioImpact.Play();
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

        if (troopAmount >= conversionCost && troopType != type) {
            troopAmount -= conversionCost;
            UpdateTroopText();
            SetTroopType(type);
        }
    }

    public void ChangeTowerType(string type) {
        int conversionCost = 5;

        if (troopAmount >= conversionCost && towerType != type) {
            troopAmount -= conversionCost;
            UpdateTroopText();

            if (type == "Attack") {
                troopTower.enabled = false;
                attackTower.enabled = true;
                cc.enabled = true;
                towerImage.GetComponent<Animator>().runtimeAnimatorController = blueAttackStation;
            } else {
                troopTower.enabled = true;
                attackTower.enabled = false;
                cc.enabled = false;
                towerImage.GetComponent<Animator>().runtimeAnimatorController = blueStation;
            }

            towerType = type;
        }
    }

    public void UpdateTroopText() {
        troopAmountText.text = ((int)troopAmount).ToString();
    }
}
