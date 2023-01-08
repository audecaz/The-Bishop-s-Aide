using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectTouchObject : MonoBehaviour
{
    public GameObject TitleObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {

                    if (hit.collider.gameObject.CompareTag("Objet"))
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        TitleObject.SetActive(true);

                        if (hit.collider.gameObject.name == "Organ")
                        {
                            if (MainManager.Instance.IsOrganPlaced)
                            {
                                if(MainManager.Instance.Language == "fr")
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Orgue d'angle");
                                }
                                else
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Corner Organ");
                                }
                            }
                            else
                            {
                                TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("???");
                            }
                        }
                        else if(hit.collider.gameObject.name == "Choir")
                        {
                            if (MainManager.Instance.IsChoirPlaced)
                            {
                                if (MainManager.Instance.Language == "fr")
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Choeur sculpté");
                                }
                                else
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Sculpted Choir");
                                }
                            }
                            else 
                            {
                                TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("???");
                            }
                        }
                        else if (hit.collider.gameObject.name == "Croco")
                        {
                            if (MainManager.Instance.IsCrocoPlaced)
                            {
                                if (MainManager.Instance.Language == "fr")
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Crocodile empaillé");
                                }
                                else
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Stuffed Crocodile");
                                }
                            }
                            else
                            {
                                TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("???");
                            }
                        }
                        else //horn
                        {
                            if (MainManager.Instance.IsHornPlaced )
                            {
                                if (MainManager.Instance.Language == "fr")
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Baton pastoral");
                                }
                                else
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Pastoral staff");
                                }
                            }
                            else if(MainManager.Instance.tutoActive != 1)
                            {
                                if (MainManager.Instance.Language == "fr")
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Baton pastoral volé");
                                }
                                else
                                {
                                    TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("Stolen pastoral staff");
                                }
                            }
                            else
                            {
                                TitleObject.transform.GetComponent<TextMeshProUGUI>().SetText("");
                            }
                        }

                    }
                    else
                    {
                        TitleObject.SetActive(false);

                    }
                }
            }
        }
    }
}
