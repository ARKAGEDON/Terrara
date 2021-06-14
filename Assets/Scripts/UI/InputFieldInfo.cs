using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldInfo : MonoBehaviour
{
    //Script pour contourner la limite d'un seul élément envoyé par le boutton de l'input field
    [Tooltip("Référence vers le texte modifiable de l'inputfield")]
    public GameObject text;
    
    [Tooltip("Référence vers le placeholder de l'inputfield")]
    public GameObject placeHolder;

}
