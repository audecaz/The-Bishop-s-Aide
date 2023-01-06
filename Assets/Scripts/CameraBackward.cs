using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraBackward : MonoBehaviour
{

    public GameObject cameraGame;
    Animator anim;

    private MeshCollider cathedrale;
    public Canvas panelCity;

    public GameObject nomBatiment;
    public GameObject enterBtn;
    public GameObject lockImage;

    public GameObject cathedral;
    public GameObject maisonBrid;
    public GameObject cloitre;

    void Start()
    {
        anim = cameraGame.GetComponent<Animator>();
        cathedrale = GameObject.Find("Cathedrale Sainte Marie").GetComponent<MeshCollider>();
    }

    public void Update()
    {
        if (anim.GetBool("Forward") && MainManager.Instance.tutoActive == 0)
        {
            cathedrale.enabled = true;
            panelCity.enabled = true;
        }
        else if(MainManager.Instance.tutoActive != 0)
        {
            panelCity.enabled = false;
        }
        else
        {
            cathedrale.enabled = false;
            panelCity.enabled = false;

        }
    }

    public void CamBackward()
    {
        if (cameraGame != null)
        {
            if (anim != null && anim.GetBool("Forward"))
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button"); //lance anim du touch button
                EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();

                StartCoroutine(BackwardAfterAnimation());
            }
        }
    }

    public IEnumerator BackwardAfterAnimation()
    {
        yield return new WaitForSeconds(0.4f); //attend 0.5s

        bool forward = anim.GetBool("Forward");
        anim.SetBool("Forward", !forward);

        MainManager.Instance.placeSelected = false;

        nomBatiment.SetActive(false);
        enterBtn.SetActive(false);
        lockImage.SetActive(false);

        maisonBrid.GetComponent<Animation>().Stop();
        cathedral.GetComponent<Animation>().Stop();
        cloitre.GetComponent<Animation>().Stop();
        

        MainManager.Instance.placeSelected = false;
    }

    public void PlaceSelection() //selection de la cathédrale
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button"); //lance anim du touch button
        EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();

        StartCoroutine(EnterAfterAnimation());
    }

    public IEnumerator EnterAfterAnimation()
    {
        yield return new WaitForSeconds(0.4f); //attend 0.5s

        SceneManager.LoadScene(2);

    }
}