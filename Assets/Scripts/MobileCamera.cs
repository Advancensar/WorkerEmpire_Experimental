using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileCamera : MonoBehaviour
{
    public bool CityInspectMode;
    public Vector3 CityPos;
    public bool hasLerped = false;

    private static readonly float LerpSpeed = 3f;
    private static readonly Vector3 CameraBounds = new Vector3(50, 20, 50);
    
    private static readonly float PanSpeed = 50f;
    private static readonly float ZoomSpeedTouch = 0.1f;
    private static readonly float ZoomSpeedMouse = 0.5f;

    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, -4f };
    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only


    void Awake()
    {
        cam = GetComponent<Camera>();
        CityPos = transform.position;
    }

    void Update()
    {
        if (!IsPointerOverUIObject())
        {
            if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
            {
                HandleTouch();
            }
            else
            {
                HandleMouse();
            }
            if (CityInspectMode)
            {
                if (hasLerped)
                    CheckBounds();
                else
                    LerpCamera();

            }
        }


    }

    void HandleTouch()
    {
        switch (Input.touchCount)
        {
            
            case 1: // Panning
                wasZoomingLastFrame = false;

                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastPanPosition = touch.position;
                    panFingerId = touch.fingerId;
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                {
                    PanCamera(touch.position);
                }
                break;

            case 2: // Zooming
                Debug.Log("2 touch works?");
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false;
                break;
        }
    }

    void HandleMouse()
    {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }

    void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;

        //pos.x = Mathf.Clamp(transform.position.x, pos.x + x, pos.x + x + 10);
        //pos.z = Mathf.Clamp(transform.position.z, pos.z + z, pos.z + z + 10);
        transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }

        transform.position += transform.forward * 10 * Mathf.Sign(offset);
        //cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void CheckBounds()
    {
        Bounds boundingBox = new Bounds(CityPos, CameraBounds);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundingBox.min.x, boundingBox.max.x),
                                        Mathf.Clamp(transform.position.y, boundingBox.min.y, boundingBox.max.y),
                                        Mathf.Clamp(transform.position.z, boundingBox.min.z, boundingBox.max.z)
        );

    }

    void OnDrawGizmos()
    {
        if (CityInspectMode)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(CityPos, CameraBounds);
        }
    }

    public void LerpCamera()
    {
        var direction = (transform.position - CityPos).normalized;
        var targetPos = new Vector3(-transform.forward.x * 20 + CityPos.x, CityPos.y, -transform.forward.z * 20 + CityPos.z);
        if ((targetPos - transform.position).magnitude > 1)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.x, Time.deltaTime * LerpSpeed), 
                                                Mathf.Lerp(transform.position.y, targetPos.y, Time.deltaTime * LerpSpeed), 
                                                Mathf.Lerp(transform.position.z, targetPos.z, Time.deltaTime * LerpSpeed));

        }
        else
        {
            hasLerped = true;
        }

    }
}

