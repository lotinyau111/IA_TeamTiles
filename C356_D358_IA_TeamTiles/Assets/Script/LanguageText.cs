using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageText : MonoBehaviour
{

    public readonly string[] wrongLoginPw = { "Invaild passwoord while login.", "u2", "u3" };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    public string getDisplayValue(string[] text, int langIndex)
    {
       // string[][] displayText = { u1 };
    ///   for (int i = 0; i < displayText.Length; i++) {
    ///       if (displayText[i][0] == text)
    ///       {
    ///           return displayText[i][langIndex];
    ///       }
    ///   }
       // return null;
       return text[langIndex];
    }

}
