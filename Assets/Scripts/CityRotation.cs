using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityRotation : MonoBehaviour
{
    public Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotationSpeedModifier = 0.1f;


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotationSpeedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
