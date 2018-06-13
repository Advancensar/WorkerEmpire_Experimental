using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleBar : MonoBehaviour , IDragHandler, IBeginDragHandler
{
    public Transform Window;
    private Vector3 clickPosVector;

    private void OnEnable()
    {
        if (Window == null)
            Window = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        clickPosVector = Window.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Window.position = Input.mousePosition + clickPosVector;
    }

}
