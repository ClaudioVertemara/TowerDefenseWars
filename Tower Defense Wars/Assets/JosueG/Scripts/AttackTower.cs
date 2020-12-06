using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{
    private List<GameObject> enemyTroops;

    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        enemyTroops = new List<GameObject>();
        fireRate = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTroops.Count != 0)
        {
            StartCoroutine(fire(fireRate));
        }
        //Time.deltaTime
    }

    private IEnumerator fire(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.DrawLine(transform.position, enemyTroops[0].transform.position, Color.white, 0.1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        enemyTroops.Add(col.gameObject);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        enemyTroops.Remove(col.gameObject);
    }
}
