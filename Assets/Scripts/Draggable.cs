using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool drag;
    private Vector3 newPos;
    private Vector3 originalPos;
    private float test;
    public bool inSlot = false;

    public GameObject slot;

    private void Start()
    {
        originalPos = transform.position;
        //test = transform.position.z;
        newPos.z = transform.position.z + 8;

    }
    private void OnMouseDown() //fonctionne pour le touch
    {
        if(inSlot == false)
        {
            drag = true;
        }        
    }

    private void OnMouseUp()
    {
        if(Vector3.Distance(transform.position, slot.transform.position) < 8)
        {
            transform.position = slot.transform.position;
            inSlot = true;
        }
        else
        {
            transform.position = originalPos;
        }
        drag = false;
    }

    private void Update()
    {
        if (drag)
        {
            //Debug.Log(Vector3.Distance(transform.position, slot.transform.position));
            Touch FingerTouch = Input.GetTouch(0);
            //Debug.Log(FingerTouch.position.x);
            newPos.x = FingerTouch.position.x;
            newPos.y = FingerTouch.position.y;

            transform.position = Camera.main.ScreenToWorldPoint(newPos);
            //transform.Translate(FingerPos);
        }
    }
}
