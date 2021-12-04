using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public new CameraController camera;
    
    private Input input;

    void Start() {
        input = gameObject.GetComponent<Input>();    
    }

    void Update() {
    }
}
