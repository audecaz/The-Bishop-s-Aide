using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PopUp_Manager : MonoBehaviour
{
    public static PopUp_Manager InstanceFact;

    public GameObject Bg;
    public GameObject FactEvent;
    public TextMeshProUGUI FactTitle;
    public TextMeshProUGUI FactContent;
    public bool IsActive = false;

    
    public void Start()
    {
        //Debug.Log(FactEvent);
        InstanceFact = this;

        FactTitle = gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        //Debug.Log(gameObject.transform.GetChild(1).GetChild(0).name);
        FactContent = gameObject.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Open() {
        Bg.SetActive(true);
        FactEvent.SetActive(true);
        IsActive = true;
    }

    public void Close()
    {
        Bg.SetActive(false);
        FactEvent.SetActive(false);
        IsActive = false;
    }

    public void PopUpVoleur()
    {
        FactTitle.SetText("Voleur");
        FactContent.SetText("Malheur ! " +
            "\r\nLe p�lerin que vous venez de recruter �tait en r�alit� un voleur d�guis�." +
            "\r\nLa rumeur se r�pand et les gens sont plus m�fiants." +
            "\r\n \r\nVous perdez N d'OR" +
            "\r\nVous perdez N de FOI");
        Open();
    }
}
