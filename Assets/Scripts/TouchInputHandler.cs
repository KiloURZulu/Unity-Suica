using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputHandler : MonoBehaviour
{
    void Update()
    {
        // Check if there are any touches
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            // Get touch position in screen coordinates
            Vector2 touchPosition = touch.position;

            // Convert touch position to world coordinates
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            // Handle touch based on its phase
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Touch started
                    HandleTouchBegan(worldPosition);
                    break;
                case TouchPhase.Moved:
                    // Touch moved
                    HandleTouchMoved(worldPosition);
                    break;
                case TouchPhase.Stationary:
                    // Touch is stationary
                    HandleTouchStationary(worldPosition);
                    break;
                case TouchPhase.Ended:
                    // Touch ended
                    HandleTouchEnded(worldPosition);
                    break;
                case TouchPhase.Canceled:
                    // Touch canceled
                    HandleTouchCanceled(worldPosition);
                    break;
            }
        }
    }

    void HandleTouchBegan(Vector2 position)
    {
        Debug.Log("Touch Began at: " + position);
        // Add your logic here
    }

    void HandleTouchMoved(Vector2 position)
    {
        Debug.Log("Touch Moved at: " + position);
        // Add your logic here
    }

    void HandleTouchStationary(Vector2 position)
    {
        Debug.Log("Touch Stationary at: " + position);
        // Add your logic here
    }

    void HandleTouchEnded(Vector2 position)
    {
        Debug.Log("Touch Ended at: " + position);
        // Add your logic here
    }

    void HandleTouchCanceled(Vector2 position)
    {
        Debug.Log("Touch Canceled at: " + position);
        // Add your logic here
    }
}
