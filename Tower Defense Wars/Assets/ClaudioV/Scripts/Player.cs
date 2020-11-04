using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked(GameObject obj) {
        if (obj.name == "Background") {
            selection.SetActive(false);
        } else if (obj.CompareTag("Blue")) {
            selection.SetActive(true);
            selection.transform.position = obj.transform.position;
        }
    }
}
