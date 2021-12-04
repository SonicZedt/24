using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public new CameraController camera;
    [HideInInspector] public Vector2 position;

    private void Update() {
        Position();
    }

    private void Position() {
        if(!Touchscreen.current.primaryTouch.press.isPressed) return;
            
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        position = camera.ScreenWorldPoint(touchPosition);
    }
}
