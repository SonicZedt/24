using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 ScreenWorldPoint(Vector2 readValue) {
        return Camera.main.ScreenToWorldPoint(readValue);
    }
    
    public Vector2 WorldViewport(Vector2 readValue) {
        return Camera.main.WorldToViewportPoint(readValue);
    }
    
    public Vector2 ViewportWorldPoint(Vector2 readValue) {
        return Camera.main.ViewportToWorldPoint(readValue);
    }
}
