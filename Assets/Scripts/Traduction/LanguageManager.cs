using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LanguageManager : MonoBehaviour
{
    public static LanguageManager LanguageInstance;

    public TextString[] texts;

    private void Awake()
    {
        LanguageInstance = this;
    }

    void Start()
    {
        ChangeLanguage();
    }

    // Update is called once per frame
    public void ChangeLanguage()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            if(MainManager.Instance.Language == "fr")
            {
                //texts[i].text.text = texts[i].fr;

                texts[i].text.SetText(texts[i].fr);
            }
            else
            {
                texts[i].text.text = texts[i].eng;
            }
        }
    }
}
