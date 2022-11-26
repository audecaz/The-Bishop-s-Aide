using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    public static int job; // 0 = pelerin, 1 = artisan, 2 = voleur
    public static int ressourceOne; 
    public static int ressourceTwo;

    public static TextMeshProUGUI jobText;
    public static TextMeshProUGUI ROneText;
    public static TextMeshProUGUI RTwoText;

    public static GameObject[] characters;

    

    void Start()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");

        GenerateNewCharacter();
       
    }

    public static void GenerateNewCharacter()
    {
        foreach (GameObject character in characters)
        {
            //Debug.Log(character.name);
            
            GenerateJob();
            ressourceOne = GenerateRessourceValue();
            ressourceTwo = GenerateRessourceValue();

            //Debug.Log(character.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>());
            jobText = character.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            ROneText = character.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            RTwoText = character.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();


            //Debug.Log(jobText);
            if (job == 0) //si pelerin
            {
                jobText.SetText("Pèlerin");
                character.GetComponent<CharacterInfos>().job = 0; //stocke les infos générés directement sur les personnages
                ROneText.SetText("OR : +" + ressourceOne);
                character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;
                RTwoText.SetText("FOI : +" + ressourceTwo);
                character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
            }
            else // Artisan
            {
                jobText.SetText("Artisan");
                character.GetComponent<CharacterInfos>().job = 1;
                ROneText.SetText("SAVOIR FAIRE: +" + ressourceOne);
                character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;
                RTwoText.SetText("OR : -" + ressourceTwo);
                character.GetComponent<CharacterInfos>().ressourceTwo = -ressourceTwo;
            }
        }
        
    }

    public static void GenerateJob()
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

    public static int GenerateRessourceValue()
    {
        int randomNumber = Random.Range(5, 10);
        return randomNumber;
    }
}
