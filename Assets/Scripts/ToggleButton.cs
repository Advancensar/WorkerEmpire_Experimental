using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {    

    public void ToggleActive(GameObject go)
    {
        var content = go.GetComponent<InventoryWindow>().transform.Find("Viewport").Find("Content");
        foreach (Transform t in content)
        {
            if (t.childCount > 0)
            {
                Destroy(t.GetChild(0).gameObject);
            }
        }
        //go.SetActive(!go.activeSelf);
    }
    
}
