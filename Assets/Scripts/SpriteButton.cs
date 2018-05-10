using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpriteButton : MonoBehaviour
{
    float minimumDistance = 10;
    float maximumDistance = 100;
    float minimumDistanceScale = 1.15f;
    float maximumDistanceScale = 0.25f;

    HouseWindow HouseWindow;

    // Use this for initialization
    void Start()
    {
        HouseWindow = GameObject.Find("HouseWindow").GetComponent<HouseWindow>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        ScaleWithCameraDistance();
    }

    private void OnMouseDown()
    {
        HouseWindow.LoadWindowInfo(transform.parent.GetComponent<House>());
        Debug.Log("Something");
    }

    void ScaleWithCameraDistance()
    {
        var distance = (transform.position - Camera.main.transform.position).magnitude;
        var norm = (distance - minimumDistance) / (maximumDistance - minimumDistance);
        norm = Mathf.Clamp01(norm);

        var minScale = Vector3.one * maximumDistanceScale;
        var maxScale = Vector3.one * minimumDistanceScale;

        transform.localScale = Vector3.Lerp(minScale, maxScale, norm);
    }


}
