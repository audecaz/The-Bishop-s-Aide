using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CitySelection : MonoBehaviour
{
    public void PlaceSelection() //selection de la cathédrale
    {
        SceneManager.LoadScene(2);
    }
}
