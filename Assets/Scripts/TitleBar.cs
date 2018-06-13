using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleBar : MonoBehaviour , IDragHandler, IBeginDragHandler
{
    public Transform Window;
    public Button CloseButton;
    private Vector3 clickPosVector;

    private void Awake()
    {
        CloseButton = transform.Find("Close_Button").GetComponent<Button>();
        CloseButton.onClick.AddListener(OnCloseButtonClick);
    }

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

    public void OnCloseButtonClick()
    {
        transform.parent.gameObject.SetActive(false);
    }

}
