using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomCharacter : MonoBehaviour
{
    public static int job; // 0 = pelerin, 1 = artisan, 2 = voleur, 3 = pelerin special, 4 = Nicolas Bachelier, 5 = pèlerin avec corne, 6 = pèlerin avec croco
    public static int ressourceOne; 
    public static int ressourceTwo;

    public static Image skin;
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


    // différentes images
    //pelerin
    public static Sprite pel1;
    public static Sprite pel2;
    public static Sprite pel3;
    public static Sprite pel4;
    public static Sprite pel5;
    public static Sprite pelOr;
    public static Sprite pelFoi;


    //artisan
    public static Sprite artisan1;
    public static Sprite artisan2;
    public static Sprite artisan3;
    public static Sprite artisan4;
    public static Sprite artisan5;


    //spécial
    public static Sprite nicolas;

    void Start()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");
        Transform pelOne = MainManager.Instance.pelerinInfos.transform.GetChild(0);

        //récupération des images
        pel1 = Resources.Load<Sprite>("Pelerins/Pelerins/pelerin1");
        pel2 = Resources.Load<Sprite>("Pelerins/Pelerins/pelerin2");
        pel3 = Resources.Load<Sprite>("Pelerins/Pelerins/pelerin3");
        pel4 = Resources.Load<Sprite>("Pelerins/Pelerins/pelerin4");
        pel5 = Resources.Load<Sprite>("Pelerins/Pelerins/pelerin5");
        pelOr = Resources.Load<Sprite>("Pelerins/Pelerins/pelerin_or");
        pelFoi = Resources.Load<Sprite>("Pelerins/Pelerins/pelerin_foi");


        artisan1 = Resources.Load<Sprite>("Pelerins/Artisans/artisan1");
        artisan2 = Resources.Load<Sprite>("Pelerins/Artisans/artisan2");
        artisan3 = Resources.Load<Sprite>("Pelerins/Artisans/artisan3");
        artisan4 = Resources.Load<Sprite>("Pelerins/Artisans/artisan4");
        artisan5 = Resources.Load<Sprite>("Pelerins/Artisans/artisan5");



        nicolas = Resources.Load<Sprite>("Pelerins/Nicolas Bachelier/nicolasBachelier");

        //Debug.Log(pel1);


        if (pelOne.GetComponent<CharacterInfos>().ressourceOne == 0) //Signifie que premier lancement du jeu, les persos ne sont pas encore générés
        {

            //GenerateNewCharacter();
        }
        else
        {
             //Récupère les infos existantes dans le GameObject du MainManager
            for (int i = 0; i < 4; i++)
            {
                Transform test = MainManager.Instance.pelerinInfos.transform.GetChild(i);
                characters[i].GetComponent<CharacterInfos>().job = test.GetComponent<CharacterInfos>().job;
                characters[i].GetComponent<CharacterInfos>().ressourceOne = test.GetComponent<CharacterInfos>().ressourceOne;
                characters[i].GetComponent<CharacterInfos>().ressourceTwo = test.GetComponent<CharacterInfos>().ressourceTwo;

                int jobLocal = characters[i].GetComponent<CharacterInfos>().job;
                int ressourceOneLocal = characters[i].GetComponent<CharacterInfos>().ressourceOne;
                int ressourceTwoLocal = characters[i].GetComponent<CharacterInfos>().ressourceTwo;

                TextMeshProUGUI jobTextLocal = characters[i].gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI ROneTextLocal = characters[i].gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI RTwoTextLocal = characters[i].gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

                if (jobLocal == 0 || jobLocal == 2) //si pelerin
                {
                    
                    if (MainManager.Instance.Language == "fr")
                    {
                        /*
                        if (jobLocal == 2)
                        {
                            jobTextLocal.SetText("Voleur");
                        }
                        else
                        {
                            jobTextLocal.SetText("Pèlerin");
                        }*/
                        jobTextLocal.SetText("Pèlerin");

                        ROneTextLocal.SetText("OR : +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("FOI : +" + ressourceTwoLocal);
                    }
                    else //eng
                    {
                        /*
                        if (jobLocal == 2)
                        {
                            jobTextLocal.SetText("Thief");
                        }
                        else
                        {
                            jobTextLocal.SetText("Pilgrim");
                        }*/

                        jobTextLocal.SetText("Pilgrim");

                        ROneTextLocal.SetText("GOLD : +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("FAITH : +" + ressourceTwoLocal);
                    }
                        
                }
                else if (jobLocal == 4) //Nicolas Bachelier 
                {
                    jobTextLocal.SetText("Nicolas Bachelier");

                    if (MainManager.Instance.Language == "fr")
                    {
                        RTwoTextLocal.SetText("Architecte");
                    }
                    else
                    {
                        RTwoTextLocal.SetText("Architect");
                    }
                    ROneTextLocal.SetText("");

                }
                else if(jobLocal == 1)// Artisan
                {
                    if (MainManager.Instance.Language == "fr")
                    {
                        jobTextLocal.SetText("Artisan");
                        ROneTextLocal.SetText("SAVOIR FAIRE: +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("OR : -" + ressourceTwoLocal);
                    }
                    else
                    {
                        jobTextLocal.SetText("Craftman");
                        ROneTextLocal.SetText("SKILLS: +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("GOLD : -" + ressourceTwoLocal);
                    }
                        
                }
                else //pelerin spécial
                {
                    //nom pèlerin selon langue
                    if(MainManager.Instance.Language == "fr")
                    {
                        jobTextLocal.SetText("Pèlerin Spé");
                    }
                    else
                    {
                        jobTextLocal.SetText("Special Pilgrim");
                    }

                    if (jobLocal == 3) //pèlerin avec objet classique
                    {
                        if (MainManager.Instance.Language == "fr")
                        {
                            ROneTextLocal.SetText("FOI : +" + ressourceOneLocal);
                            if (ressourceTwoLocal == 12)
                            {
                                RTwoTextLocal.SetText("Calice en or : +12 d'OR");
                            }
                            else
                            {
                                RTwoTextLocal.SetText("Coffre précieux : +15 d'OR");
                            }
                        }
                        else //eng
                        {
                            ROneTextLocal.SetText("FAITH : +" + ressourceOneLocal);
                            if (ressourceTwoLocal == 12)
                            {
                                RTwoTextLocal.SetText("Golden Calice : +12 GOLD");
                            }
                            else
                            {
                                RTwoTextLocal.SetText("Precious Chest : +15 GOLD");
                            }
                        }  
                    }
                    else if (jobLocal == 5) //pelerin licorne
                    {
                        if (MainManager.Instance.Language == "fr")
                        {
                            ROneTextLocal.SetText("FOI : +" + ressourceOneLocal);
                            RTwoTextLocal.SetText("Corne de licorne");
                        }
                        else //eng
                        {
                            ROneTextLocal.SetText("FAITH : +" + ressourceOneLocal);
                            RTwoTextLocal.SetText("Unicorn Horn");
                        }
                    }
                    else if (jobLocal == 6)
                    {
                        if (MainManager.Instance.Language == "fr")
                        {
                            ROneTextLocal.SetText("FOI : +" + ressourceOneLocal);
                            RTwoTextLocal.SetText("Crocodile empaillé");
                        }
                        else // eng
                        {
                            ROneTextLocal.SetText("FAITH : +" + ressourceOneLocal);
                            RTwoTextLocal.SetText("Stuffed Crocodile");
                        }
                            
                    }
                }
  
            }
        }
    }

    public static void GenerateNewCharacter()
    {
        foreach (GameObject character in characters)
        {
            //Debug.Log(character.name);
            
            GenerateJob();
            //ressourceOne = GenerateRessourceValue();
            //ressourceTwo = GenerateRessourceValue();

            if(job == 1)//artisan
            {
                ressourceOne = NewGenerateRessource();
                ressourceTwo = GenerateRessourceValue();
            }
            else if(job != 6 && job != 5)
            {
                ressourceOne = NewGenerateRessource();
                ressourceTwo = NewGenerateRessource();
            }
            else
            {
                ressourceOne = GenerateRessourceValue();
                ressourceTwo = GenerateRessourceValue();
            }
            

            //Debug.Log(character.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>());
            skin = character.gameObject.transform.GetChild(0).GetComponent<Image>();
            jobText = character.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            ROneText = character.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            RTwoText = character.gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

            //Debug.Log(skin);
            //Debug.Log(jobText);
                     
            
            if(job == 4) //Nicolas
            {
                skin.sprite = nicolas;

                jobText.SetText("Nicolas Bachelier");
                character.GetComponent<CharacterInfos>().job = 4;

                if (MainManager.Instance.Language == "fr")
                {
                    RTwoText.SetText("Architecte");
                }
                else
                {
                    RTwoText.SetText("Architect");
                }
                ROneText.SetText("");
            }
            else if(job == 1) // Artisan
            {
                int randomSprite = Random.Range(1, 6); //6 exclu

                //Sprite
                if (randomSprite == 1)
                {
                    skin.sprite = artisan1;
                }
                else if (randomSprite == 2)
                {
                    skin.sprite = artisan2;
                }
                else if(randomSprite == 3)
                {
                    skin.sprite = artisan3;
                }
                else if(randomSprite == 4)
                {
                    skin.sprite = artisan4;
                }
                else
                {
                    skin.sprite = artisan5;
                }

                character.GetComponent<CharacterInfos>().job = 1;

                if (MainManager.Instance.Language == "fr")
                {
                    jobText.SetText("Artisan");
                    ROneText.SetText("SAVOIR FAIRE: +" + ressourceOne);
                    RTwoText.SetText("OR : -" + ressourceTwo);
                }
                else
                {
                    jobText.SetText("Craftman");
                    ROneText.SetText("SKILLS: +" + ressourceOne);
                    RTwoText.SetText("GOLD : -" + ressourceTwo);
                }
                    
            }
            else { //pelerin

                int randomSprite = Random.Range(1, 4); //4 exclu

                //Sprite
                if(ressourceOne > 17) //or > 17
                {
                    skin.sprite = pelOr;
                }
                else if(ressourceTwo > 17)
                {
                    skin.sprite = pelFoi;
                }
                else //pelerin classique
                {
                    if (randomSprite == 1)
                    {
                        skin.sprite = pel1;
                    }
                    else if (randomSprite == 2)
                    {
                        skin.sprite = pel2;
                    }
                    else if (randomSprite == 3)
                    {
                        skin.sprite = pel3;
                    }
                    else if (randomSprite == 4)
                    {
                        skin.sprite = pel4;
                    }
                    else
                    {
                        skin.sprite = pel5;
                    }
                }
                


                if (job == 0 || job == 2) // pelerin "classique"
                {
                    if (MainManager.Instance.Language == "fr")
                    {
                        /*
                        if (job == 2)
                        {
                            jobText.SetText("Voleur");
                            character.GetComponent<CharacterInfos>().job = 2;
                        }
                        else
                        {
                            jobText.SetText("Pèlerin");
                            character.GetComponent<CharacterInfos>().job = 0; //stocke les infos générés directement sur les personnages
                        }*/

                        jobText.SetText("Pèlerin");

                        if (job == 2)
                        {
                            character.GetComponent<CharacterInfos>().job = 2;
                        }
                        else
                        {
                            character.GetComponent<CharacterInfos>().job = 0; //stocke les infos générés directement sur les personnages
                        }

                        ROneText.SetText("OR : +" + ressourceOne);
                        RTwoText.SetText("FOI : +" + ressourceTwo);
                    }
                    else
                    {
                        /*
                        if (job == 2)
                        {
                            jobText.SetText("Thief");
                            character.GetComponent<CharacterInfos>().job = 2;
                        }
                        else
                        {
                            jobText.SetText("Pilgrim");
                            character.GetComponent<CharacterInfos>().job = 0; //stocke les infos générés directement sur les personnages
                        }*/

                        jobText.SetText("Pilgrim");

                        if (job == 2)
                        {
                            character.GetComponent<CharacterInfos>().job = 2;
                        }
                        else
                        {
                            character.GetComponent<CharacterInfos>().job = 0; //stocke les infos générés directement sur les personnages
                        }

                        ROneText.SetText("GOLD : +" + ressourceOne);
                        RTwoText.SetText("FAITH : +" + ressourceTwo);
                    }
                        
                }
                else //pèlerins spéciaux
                {
                    if (MainManager.Instance.Language == "fr")
                    {
                        jobText.SetText("Pèlerin Spé");
                    }
                    else
                    {
                        jobText.SetText("Special Pilgrim");
                    }

                    if (job == 5) // Corne de licorne
                    {
                        character.GetComponent<CharacterInfos>().job = 5;

                        character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;

                        ressourceTwo = 0;
                        character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;

                        if (MainManager.Instance.Language == "fr")
                        {
                            ROneText.SetText("FOI : +" + ressourceOne);
                            RTwoText.SetText("Corne de licorne");
                        }
                        else {
                            ROneText.SetText("FAITH : +" + ressourceOne);
                            RTwoText.SetText("Unicorn Horn");
                        }
                    }
                    else if (job == 6) // crocodile
                    {
                        character.GetComponent<CharacterInfos>().job = 6;

                        character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;

                        ressourceTwo = 0;
                        character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;

                        if (MainManager.Instance.Language == "fr")
                        {
                            ROneText.SetText("FOI : +" + ressourceOne);
                            RTwoText.SetText("Crocodile empaillé");
                        }
                        else
                        {
                            ROneText.SetText("FAITH : +" + ressourceOne);
                            RTwoText.SetText("Stuffed Crocodile");
                        }

                    }

                    else if (job == 3) //objet autre
                    {
                        character.GetComponent<CharacterInfos>().job = 3;

                        character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;

                        if (MainManager.Instance.Language == "fr")
                        {
                            ROneText.SetText("FOI : +" + ressourceOne);

                            int randomObject = Random.Range(0, 2);
                            int randomValue = Random.Range(14, 18);
                            if (randomObject == 0)
                            {
                                character.GetComponent<CharacterInfos>().objectName = "calice";
                                RTwoText.SetText("Calice en or : +"+randomValue+" d'OR");
                                ressourceTwo = randomValue;
                                character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
                            }
                            else
                            {
                                character.GetComponent<CharacterInfos>().objectName = "coffre";
                                RTwoText.SetText("Coffre précieux : +"+randomValue+" d'OR");
                                ressourceTwo = randomValue;
                                character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
                            }
                        }
                        else
                        {
                            ROneText.SetText("FAITH : +" + ressourceOne);

                            int randomObject = Random.Range(0, 2);
                            int randomValue = Random.Range(14, 18);

                            if (randomObject == 0)
                            {
                                RTwoText.SetText("Golden Calice : +"+randomValue+" GOLD");
                                ressourceTwo = randomValue;
                                character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
                            }
                            else
                            {
                                RTwoText.SetText("Precious Chest : +"+randomValue+" GOLD");
                                ressourceTwo = randomValue;
                                character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;
                            }
                        }
                            
                    }
                }
               
            }
           
                        
            
            character.GetComponent<CharacterInfos>().ressourceOne = ressourceOne;
            character.GetComponent<CharacterInfos>().ressourceTwo = ressourceTwo;

        }

        //SKIN
        

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


        //A chaque nouvelle génération de personnages, stocke les nouvelles infos dans le GameObject du MainManager
        for (int i = 0; i < 4; i++)
        {
            Transform test = MainManager.Instance.pelerinInfos.transform.GetChild(i);
            test.GetComponent<CharacterInfos>().job = characters[i].GetComponent<CharacterInfos>().job;
            test.GetComponent<CharacterInfos>().ressourceOne = characters[i].GetComponent<CharacterInfos>().ressourceOne;
            test.GetComponent<CharacterInfos>().ressourceTwo = characters[i].GetComponent<CharacterInfos>().ressourceTwo;
        }

    }

    public static void GenerateJob()
    {

        int diceTotalNicolas = GenerateRolls(nicoDiceNumber);
        int diceTotalHorn = GenerateRolls(hornDiceNumber);
        int diceTotalCroco = GenerateRolls(crocoDiceNumber);

        //Debug.Log(diceTotalCroco);

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
                if (randomNumber < 0.45) //voleur
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
        int randomNumber = Random.Range(5, 11);
        return randomNumber;
    }

    public static int NewGenerateRessource()
    {
        float randomNumberOne = 0;
        float randomNumberTwo = 0;
        float randomNumberThree = 0;

        float minNumber;

        //personnage particulièrement vertueux
        int randomNumber = Random.Range(1, 21); //valeur max exclue

        if (randomNumber == 20)
        {
            minNumber = Random.Range(18, 22);
        }
        else
        {
            // génération de 3 valeurs
            for (int i = 0; i < 2; i++)
            {
                randomNumberOne += Random.Range(3, 10);
            }
            for (int i = 0; i < 2; i++)
            {
                randomNumberTwo += Random.Range(2, 10);
            }
            for (int i = 0; i < 2; i++)
            {
                randomNumberThree += Random.Range(2, 10);
            }

            //révupère la plus petite des 3
            minNumber = Mathf.Min(randomNumberThree, randomNumberTwo, randomNumberThree);
        }

        return (int)minNumber;
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
