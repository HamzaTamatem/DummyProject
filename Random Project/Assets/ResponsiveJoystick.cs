using System;
using UnityEngine;

public class ResponsiveJoystick : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRectTransform;

    private RectTransform _rectTransform;

    private Vector2 originalAnchoredPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        originalAnchoredPosition = _rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Get the first touch (you can iterate through all touches if needed)
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Check if the touch is on the left side of the screen in landscape mode
                if (touch.position.x < Screen.width / 2)
                {
                    // Touch is on the left side
                    Debug.Log("Touch on the left side of the screen.");
                
                    // Convert touch position to ui space position
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, touch.position, null, out Vector2 localUIPosition))
                    {
                        // Now 'localUIPosition' contains the touch position in UI coordinates
                        Debug.Log("UI Position: " + localUIPosition);
                    }
                
                    // Change the position of the joystick
                    _rectTransform.anchoredPosition = localUIPosition;
                }
            } else if (touch.phase == TouchPhase.Ended)
            {
                _rectTransform.anchoredPosition = originalAnchoredPosition;
            }
            
            
        }
    }
}
