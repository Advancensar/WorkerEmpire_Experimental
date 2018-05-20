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


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        ScaleWithCameraDistance();
    }

    private void OnMouseDown()
    {
        GameManager.Instance.HouseWindow.LoadWindowInfo(transform.parent.GetComponent<House>());
        GameManager.Instance.HouseWindow.OpenWindow();
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
