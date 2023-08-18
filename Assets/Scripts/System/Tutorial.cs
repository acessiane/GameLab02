using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    GameObject editMode;
    GameObject placeBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
 
        DontDestroyObject.gameManager.EditMode();
 
        placeBox = GameObject.Find("Select Panel").gameObject;
        if (this.gameObject.name == "Tutorial1")
        {
            placeBox.transform.GetChild(1).gameObject.SetActive(false);

        }
        else
        {
            placeBox.transform.GetChild(1).gameObject.SetActive(true);
            placeBox.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

}
