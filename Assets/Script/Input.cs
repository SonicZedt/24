using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public new CameraController camera;
    
    private bool primaryTouch;
    private Vector2 position;

    public bool PrimaryTouch { get { return primaryTouch; }}
    public Vector2 TouchPosition { get { return position; }}

    private void Update() {
        TouchCondition();
        Position();
    }

    private void TouchCondition() {
        primaryTouch = Touchscreen.current.primaryTouch.press.isPressed;
    }

    private void Position() {
        if(!primaryTouch) return;
            
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        position = camera.ScreenWorldPoint(touchPosition);
    }
}
