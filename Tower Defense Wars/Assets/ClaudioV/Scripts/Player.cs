using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* [Player Script]
 * All Player Controls
 * UI Player Interacts With (Not Tower UI)
 * Sending Troops
 * Clicking on & Interacting w/ Towers
 */

public class Player : MonoBehaviour
{
    public GameObject troops;
    public Transform troopsParent;

    public GameObject selection;
    public GameObject selection2;

    GameObject selectedTower;

    [Header("Percentage Buttons")]
    public Button fullButton;
    public Button threeFourthButton;
    public Button halfButton;
    public Button oneFourthButton;

    float troopPercentage;

    // Start is called before the first frame update
    void Start()
    {
        fullButton.image.color = new Color(0, 1, 0.5681005f, 1);
        troopPercentage = 1f;
    }

    // Tower or Background Clicked On (UI)
    public void Clicked(GameObject obj) {
        if (obj.name == "Background") {
            // Background Clicked On
            selection.SetActive(false);
            selection2.SetActive(false);
            
            if (selectedTower != null) {
                selectedTower.GetComponent<Tower>().towerMenu.SetActive(false);
            }
        } else if (selection.activeInHierarchy && !selection2.activeInHierarchy) {
            // Clicked On Another Tower
            if (selectedTower.transform.position == obj.transform.position) {
                // 1st Tower Clicked on Again
                selection2.SetActive(false);

                if (obj.GetComponent<Tower>().towerMenu.activeInHierarchy) {
                    obj.GetComponent<Tower>().towerMenu.SetActive(false);
                } else {
                    obj.GetComponent<Tower>().towerMenu.SetActive(true);
                }
            } else {
                // 2nd Tower Selected (Send Troops)
                selection2.SetActive(true);
                selection2.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, selection2.transform.position.z);

                selectedTower.GetComponent<Tower>().towerMenu.SetActive(false);

                SendTroops(selectedTower.GetComponent<Tower>(), obj);
            }
        } else if (obj.CompareTag("Blue")) {
            // Tower Selected (Player Owns)
            selectedTower = obj;

            selection.SetActive(true);
            selection2.SetActive(false);
            selection.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, selection.transform.position.z);
        }
    }

    // Set Troop Percentage to Send (UI)
    public void SetTroopPercentage(float percent) {
        troopPercentage = percent;
    }

    // Change Selected Percentage Button Color (UI)
    public void SetPercentColor(Button button) {
        fullButton.image.color = new Color(0, 1, 1, 1);
        threeFourthButton.image.color = new Color(0, 1, 1, 1);
        halfButton.image.color = new Color(0, 1, 1, 1);
        oneFourthButton.image.color = new Color(0, 1, 1, 1);

        button.image.color = new Color(0, 1, 0.5681005f, 1);
    }

    // Send Troops from 1st Tower Selected to 2nd Tower Selected
    void SendTroops(Tower fromTower, GameObject toTower) {
        int troopAmount = fromTower.GetTroopAmount();

        if (troopAmount > 0) {
            GameObject currTroops = Instantiate(troops, fromTower.transform.position, Quaternion.identity, troopsParent);

            string troopType = fromTower.GetTroopType();
            troopAmount = (int)(troopAmount * troopPercentage);

            fromTower.ChangeTroopAmount(troopAmount, troopType, "Blue", false);

            currTroops.GetComponent<Troops>().SpawnTroops(troopAmount, troopType, toTower, "Blue");
        }
    }
}
