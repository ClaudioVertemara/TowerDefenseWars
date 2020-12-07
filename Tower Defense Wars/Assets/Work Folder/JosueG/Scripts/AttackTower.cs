using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{
    private List<GameObject> enemyTroops;

    public float fireRate;

    bool shooting;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        enemyTroops = new List<GameObject>();
        fireRate = 1.5f;
        damage = 1;
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTroops.Count > 0 && !shooting)
        {
            StartCoroutine(fire(fireRate));
        }
        //Time.deltaTime
    }

    private IEnumerator fire(float waitTime)
    {
        shooting = true;
        Debug.DrawLine(transform.position, enemyTroops[0].transform.position, Color.white, 0.1f);
        enemyTroops[0].GetComponent<Troops>().takeDamage(damage);
        if(enemyTroops[0] == null)
        {
            enemyTroops.Remove(enemyTroops[0]);
        }
        yield return new WaitForSeconds(waitTime);
        shooting = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Troops") && col != null) {
            //if the troop is from the same 
            if (col.gameObject.CompareTag(gameObject.tag)) {
                return;
            }
            enemyTroops.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col != null) {
            enemyTroops.Remove(col.gameObject);
        }
    }
}
