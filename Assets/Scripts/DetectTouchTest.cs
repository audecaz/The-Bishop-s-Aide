using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DetectTouchTest : MonoBehaviour
{

    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    if(hit.collider.gameObject.CompareTag("Character"))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        RandomCharacter.GenerateNewCharacter();
                        
                    }
                }
            }
        }
    }
} //Rechercher canvas et touch, pk raycast pas bloqué par l'UI

