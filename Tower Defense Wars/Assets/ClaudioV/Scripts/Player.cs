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

    // Start is called before the first frame update
    void Start()
    {
        
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

    void SendTroops(TroopTower fromTower, GameObject toTower) {
        GameObject currTroops = Instantiate(troops, fromTower.transform.position, Quaternion.identity, troopsParent);

        int troopAmount = fromTower.GetTroopAmount();
        fromTower.ChangeTroopAmount(troopAmount, false);

        currTroops.GetComponent<Troops>().SpawnTroops(troopAmount, toTower);
    }
}
