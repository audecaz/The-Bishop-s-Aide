using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Sounds : MonoBehaviour
{

    public AudioSource source;
    public string name;
    public AudioClip audioClip;

    [Range(0, 1)]
    public int volume;
    
}
