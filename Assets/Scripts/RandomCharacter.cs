using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    public int job; // 0 = pelerin, 1 = artisan, 2 = voleur
    public int ressourceOne; 
    public int ressourceTwo;

    private TextMeshProUGUI jobText;
    private TextMeshProUGUI ROneText;
    private TextMeshProUGUI RTwoText;

    void Start()
    {
        jobText = this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        ROneText = this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        RTwoText = this.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        GenerateNewCharacter();  
    }

    void GenerateNewCharacter()
    {
        GenerateJob();
        ressourceOne = GenerateRessourceValue();
        ressourceTwo = GenerateRessourceValue();

        //Debug.Log(jobText);
        if(job == 0) //si pelerin
        {
            jobText.SetText("Pèlerin");
            ROneText.SetText("OR : +" + ressourceOne);
            RTwoText.SetText("FOI : +" + ressourceTwo);
        }
        else // Artisan
        {
            jobText.SetText("Artisan");
            ROneText.SetText("SAVOIR FAIRE: +" + ressourceOne);
            RTwoText.SetText("OR : -" + ressourceTwo);
        }
    }

    void GenerateJob()
    {
        float randomNumber = Random.Range(0, 10);

        if(randomNumber < 7) 
        {
            job = 0;
        }
        else
        {
            job = 1;
        }
    }

    int GenerateRessourceValue()
    {
        int randomNumber = Random.Range(5, 10);
        return randomNumber;
    }
}
