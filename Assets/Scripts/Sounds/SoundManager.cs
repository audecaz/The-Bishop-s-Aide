using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    private static AudioSource[] objectWithSound;

    private void Awake()
    {
        //objectWithSound = FindObjectsOfType<AudioSource>();
        objectWithSound = (AudioSource[])FindObjectsOfType(typeof(AudioSource), true);
        //objectWithSound = FindObjectsOfTypeAll(AudioSource);
        //objectWithSound = GameObject.FindGameObjectsWithTag("Respawn");

        Debug.Log(objectWithSound[0].name);
         
        for (int i = 0; i <= objectWithSound.Length; i++)
        {
            if (objectWithSound[i].name != "Main Camera") //si le son n'est pas la musique d'ambiance
            {
                if (!MainManager.Instance.soundOn)
                {
                    objectWithSound[i].volume = 0;
                }
            }

        }
    }

    public static void soundOnOff()
    {

        for (int i = 0; i <= objectWithSound.Length; i++)
        {
            if (objectWithSound[i].name != "Main Camera") //si le son n'est pas la musique d'ambiance
            {
                if (!MainManager.Instance.soundOn)
                {
                    objectWithSound[i].volume = 0;
                }
                else
                {
                    objectWithSound[i].volume = 1;

                }
            }
            
        }
    }


}
