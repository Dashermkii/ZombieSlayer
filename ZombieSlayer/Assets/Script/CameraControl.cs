using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float reverse_zoom = 4;
    public Camera cam;
    public Rigidbody2D player;

    Vector2 mousePos;

    Vector3 camPlacement;

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookAt = mousePos - player.position;

        camPlacement.x = lookAt.x / reverse_zoom + player.position.x;
        camPlacement.y = lookAt.y / reverse_zoom + player.position.y;
        camPlacement.z = -10;

        
        cam.transform.position = (camPlacement);

    }
}
