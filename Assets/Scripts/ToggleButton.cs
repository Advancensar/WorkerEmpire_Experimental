using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {    

    public void ToggleActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
    
}
