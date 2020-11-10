using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        fullButton.image.color = Color.green;
        troopPercentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Tower or Background Clicked On
    public void Clicked(GameObject obj) {
        if (obj.name == "Background") {
            selection.SetActive(false);
            selection2.SetActive(false);
        } else if (selection.activeInHierarchy && !selection2.activeInHierarchy) {
            // 2nd Tower Selected (Send Troops)
            selection2.SetActive(true);
            selection2.transform.position = obj.transform.position;

            SendTroops(selectedTower.GetComponent<TroopTower>(), obj);
        } else if (obj.CompareTag("Blue")) {
            // Tower Selected
            selectedTower = obj;

            selection.SetActive(true);
            selection2.SetActive(false);
            selection.transform.position = obj.transform.position;
        }
    }

    public void SetTroopPercentage(float percent) {
        troopPercentage = percent;
    }

    public void SetPercentColor(Button button) {
        fullButton.image.color = Color.white;
        threeFourthButton.image.color = Color.white;
        halfButton.image.color = Color.white;
        oneFourthButton.image.color = Color.white;

        button.image.color = Color.green;
    }

    void SendTroops(TroopTower fromTower, GameObject toTower) {
        GameObject currTroops = Instantiate(troops, fromTower.transform.position, Quaternion.identity, troopsParent);

        int troopAmount = fromTower.GetTroopAmount();

        troopAmount = (int)(troopAmount * troopPercentage);

        fromTower.ChangeTroopAmount(troopAmount, false);

        currTroops.GetComponent<Troops>().SpawnTroops(troopAmount, toTower);
    }
}
