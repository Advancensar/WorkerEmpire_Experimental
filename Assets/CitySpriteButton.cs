using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpriteButton : MonoBehaviour {

    public GameObject StorageWindow;

    float minimumDistance = 10;
    float maximumDistance = 100;
    float minimumDistanceScale = 1.15f;
    float maximumDistanceScale = 0.25f;

    private void Awake()
    {
        StorageWindow = GameObject.Find("StorageWindow");
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        //ScaleWithCameraDistance();
    }

    private void OnMouseDown()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //if (!StorageWindow.activeSelf)
            //{
            //    StorageWindow.SetActive(true);
            //}
            //Change this send info of the clicked town
            var mc = Camera.main.GetComponent<MobileCamera>();
            mc.CityPos = new Vector3(0f, 40f, 0f) + transform.position;
            mc.CityInspectMode = !mc.CityInspectMode;
            mc.hasLerped = false;
            StorageWindow.GetComponent<InventoryWindow>().LoadWindowInfo(transform.parent.GetComponent<CityStorage>().Inventory);

        }
        //var mobileCamera = Camera.main.transform.GetComponent<MobileCamera>();
        //mobileCamera.cityPos = transform.position;
        //mobileCamera.cityViewMode = true;
        //transform.translate camera zoom
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
