using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class City_Effects : MonoBehaviour
{
    public static City_Effects CityFxInstance;

    public GameObject burnedHouse;
    public GameObject houseSmoke;
    public GameObject houseSparkle;

    public Material matNormalHouse;
    public Material matBurnedHouse;

    private float countdown = 0;
    /*
    public void Start()
    {
        houseSmoke.Stop();
        houseSparkle.Stop();
    }*/

    private void Awake()
    {
        CityFxInstance = this;
    }

    public void Update()
    {
        Debug.Log(countdown);
        if (houseSparkle.GetComponent<ParticleSystem>().isPlaying && countdown < 150)
        {
            countdown += Time.deltaTime;
        }
        else
        {
            houseSparkle.GetComponent<ParticleSystem>().Stop();
        }
    }

    public void CityFireOn()
    {
        Debug.Log("test");
        burnedHouse.GetComponent<Renderer>().material = matBurnedHouse;
        houseSmoke.SetActive(true);
        houseSmoke.GetComponent<ParticleSystem>().Play();
    }

    public void CityFireOff()
    {
        Debug.Log("off!");
        burnedHouse.GetComponent<Renderer>().material = matNormalHouse;
        houseSmoke.SetActive(false);
        houseSmoke.GetComponent<ParticleSystem>().Stop();
        houseSparkle.SetActive(true);

        countdown = 0;
        houseSparkle.GetComponent<ParticleSystem>().Play();
    }
}
