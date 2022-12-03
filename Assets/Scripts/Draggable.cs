using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool drag;
    private Vector3 newPos;
    private float test;

    private void Start()
    {
        //test = transform.position.z;
        newPos.z = transform.position.z + 10;
    }
    private void OnMouseDown()
    {
        //Debug.Log("tient");
        drag = true;
    }

    private void OnMouseUp()
    {
        //Debug.Log("lache");
        drag = false;
    }

    private void Update()
    {
        if (drag)
        {
            Touch FingerTouch = Input.GetTouch(0);
            //Debug.Log(FingerTouch.position.x);
            newPos.x = FingerTouch.position.x;
            newPos.y = FingerTouch.position.y;

            transform.position = Camera.main.ScreenToWorldPoint(newPos);
            //transform.Translate(FingerPos);
        }
    }
}
