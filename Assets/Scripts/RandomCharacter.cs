using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    public static int job; // 0 = pelerin, 1 = artisan, 2 = voleur, 3 = pelerin special, 4 = Nicolas Bachelier, 5 = pèlerin avec corne, 6 = pèlerin avec croco
    public static int ressourceOne; 
    public static int ressourceTwo;

    public static TextMeshProUGUI jobText;
    public static TextMeshProUGUI ROneText;
    public static TextMeshProUGUI RTwoText;

    public static GameObject[] characters;

    public static int nicoDiceNumber = 10;
    public static int hornDiceNumber = 10;
    public static int crocoDiceNumber = 10;


    public static bool nicolasPresent = false;
    public static bool hornPresent = false;
    public static bool crocoPresent = false;


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
            if (job == 0 || job == 2) //si pelerin
            {
                if(job == 2) {
                    jobText.SetText("Voleur");
                    character.GetComponent<CharacterInfos>().job = 2;
                }
                else
                {
                    jobText.SetText("Pèlerin");
                    character.GetComponent<CharacterInfos>().job = 0; //stocke les infos générés directement sur les personnages
                }
                
                ROneText.SetText("OR : +" + ressourceOne);
                RTwoText.SetText("FOI : +" + ressourceTwo);
            }
            else if (job == 3)
            {
                jobText.SetText("Pèlerin Spé");
                character.GetComponent<CharacterInfos>().job = 3;

                ROneText.SetText("FOI : +" + ressourceOne);
                character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;

                int randomObject = Random.Range(0, 2);
                if(randomObject == 0)
                {
                    RTwoText.SetText("Calice en or : +12 d'OR");
                    ressourceTwo = 12;
                    character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
                }
                else
                {
                    RTwoText.SetText("Coffre précieux : +15 d'OR");
                    ressourceTwo = 15;
                    character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
                }
            }else if(job == 4)
            {
                jobText.SetText("Nicolas Bachelier");
                character.GetComponent<CharacterInfos>().job = 4;

                ROneText.SetText("Architecte");
                RTwoText.SetText("");
            }
            else if (job == 5)
            {
                jobText.SetText("Pèlerin Spé");
                character.GetComponent<CharacterInfos>().job = 5;

                ROneText.SetText("FOI : +" + ressourceOne);
                character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;

                RTwoText.SetText("Corne de licorne");
                ressourceTwo = 0;
                character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
            }
            else if (job == 6)
            {
                jobText.SetText("Pèlerin Spé");
                character.GetComponent<CharacterInfos>().job = 6;

                ROneText.SetText("FOI : +" + ressourceOne);
                character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;

                RTwoText.SetText("Crocodile empaillé");
                ressourceTwo = 0;
                character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
            }
            else // Artisan
            {
                jobText.SetText("Artisan");
                character.GetComponent<CharacterInfos>().job = 1;
                ROneText.SetText("SAVOIR FAIRE: +" + ressourceOne);
                RTwoText.SetText("OR : -" + ressourceTwo);
            }                
            
            character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;
            character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;

        }

        if(nicolasPresent == true)
        {
            nicoDiceNumber = 10;
            nicolasPresent = false;
        }
        else
        {
            nicoDiceNumber++;
        }

        if (hornPresent == true)
        {
            hornDiceNumber = 10;
            hornPresent = false;
        }
        else
        {
            hornDiceNumber++;
        }

        if (crocoPresent == true)
        {
            crocoDiceNumber = 10;
            crocoPresent = false;
        }
        else
        {
            crocoDiceNumber++;
        }

    }

    public static void GenerateJob()
    {

        int diceTotalNicolas = GenerateRolls(nicoDiceNumber);
        int diceTotalHorn = GenerateRolls(hornDiceNumber);
        int diceTotalCroco = GenerateRolls(crocoDiceNumber);

        Debug.Log(diceTotalCroco);

        if (diceTotalNicolas > 150 && !nicolasPresent && !MainManager.Instance.IsNicolasRecruted)
        {
            job = 4; // Nicolas Bachelier
            nicolasPresent = true;
        }
        else if(diceTotalHorn > 120 && !hornPresent && MainManager.Instance.HornStolen && !MainManager.Instance.HornRetrieved)
        {
            job = 5; //pèlerin spécial avec Corne
            hornPresent = true;
        }
        else if (diceTotalCroco > 130 && !crocoPresent && !MainManager.Instance.IsCrocoHere)
        {
            job = 6; //pèlerin spécial avec croco
            crocoPresent = true;
        }
        else
        {
            float randomNumber = Random.Range(0, 10);

            if (randomNumber < 7)
            {
                randomNumber = Random.Range(0, 10);
                if (randomNumber < 0.5) //voleur
                {
                    job = 2;
                }
                else if (randomNumber < 1.2) //pèlerin spécial
                {
                    job = 3;
                }
                else
                {
                    job = 0;
                }
            }
            else
            {
                job = 1;
            }
        }
        
    }

    public static int GenerateRessourceValue()
    {
        int randomNumber = Random.Range(5, 10);
        return randomNumber;
    }

    public static int GenerateRolls(int diceNum)
    {
        int result = 0;
        for (int i = 0; i < 10; i++)
        {
            int randomNumber = Random.Range(1, diceNum);
            result += randomNumber;
        }
        return result;
    }

}
