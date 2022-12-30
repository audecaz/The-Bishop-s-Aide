using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityRotation : MonoBehaviour
{
    public Touch touch;
    private float rotationSpeedModifier = 0.1f;

    public GameObject cameraGame;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && cameraGame.GetComponent<Animator>().GetBool("Forward"))
        {
           // Debug.Log("turn city !");
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                transform.Rotate(0f, -touch.deltaPosition.x * rotationSpeedModifier, 0f);
            }
        }
    }


}
