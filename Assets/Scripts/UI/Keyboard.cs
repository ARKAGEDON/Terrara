using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private GameObject UppercaseKeys; //Référence vers les touches majuscules
    [SerializeField] private GameObject LowercaseKeys; //Référence vers les touches minuscules
    private GameObject inputFieldTexte; //Référence vers le texte de l'inputfield
    private GameObject inputFieldPlaceHolder; //Référence vers le placeholder de l'inputfield
    private string text; //Référence vers le texte de l'inputfield text
    private TextMeshProUGUI displayText;
    public Color displayTextColor = Color.black;

    public Color caretColor = Color.gray;

    private void Start() {
        LowercaseKeys.SetActive(true);
        UppercaseKeys.SetActive(false);
    }

    /// <summary>
    /// Fonction pour ajouter la référence vers le texte de l'inputfield
    /// </summary>
    /// <param name="_inputfieldText">Texte de l'input field</param>
    public void SetInputFieldText(GameObject _inputfieldText, GameObject _inputFieldPlaceHolder)
    {
        inputFieldTexte = _inputfieldText;
        inputFieldPlaceHolder = _inputFieldPlaceHolder;

        if (_inputfieldText != null)
        {
            displayText = inputFieldTexte.GetComponent<TextMeshProUGUI>();
            CheckPlaceHolder();
        }
    }

    /// <summary>
    /// Fonction pour ajouter des lettres au texte de l'inputfield (à mettre sur chaque lettre du clavier)
    /// </summary>
    /// <param name="letter">Lettre à ajouté</param>
    public void AddLetter(string letter)
    {
        if (inputFieldTexte != null)
        {
            text += letter;
            CheckPlaceHolder();
            UpdateDisplayText();
        }
    }

    /// <summary>
    /// Fonction pour retirer des lettres au texte de l'inputfield (à mettre sur la touches retour)
    /// </summary>
    public void RemoveLetter()
    {
        if (inputFieldTexte != null)
        {
            if (text.Length > 0) //Si on a plus de 0 caractères on peut en retirer
                text = text.Substring (0, text.Length - 1);
            CheckPlaceHolder();
            UpdateDisplayText();
        }
    }

    /// <summary>
    /// Fonction pour mettre les touches en majuscules ou minuscules
    /// </summary>
    public void Maj()
    {
        if (UppercaseKeys.activeSelf) //Si les majuscules sont déjà activés alors on les désactive et active les minuscules
        {
            UppercaseKeys.SetActive(false);
            LowercaseKeys.SetActive(true);
        }
        else //sinon on désactive les minuscules et active les majuscules
        {
            UppercaseKeys.SetActive(true);
            LowercaseKeys.SetActive(false);
        }
    }

    /// <summary>
    /// Fonction appelé par la touche entrée pour fermé le clavier
    /// </summary>
    public void Enter()
    {
        KeyboardManager manager = GameObject.FindGameObjectWithTag("KeyboardManager").GetComponent<KeyboardManager>();
        manager.ExitInputField();
    }

    public void CheckPlaceHolder()
    {
        if (text == "") //On vérifie si notre texte est null, si oui on affiche le placeholder
        {
            inputFieldPlaceHolder.SetActive(true);
        }
        else
        {
            inputFieldPlaceHolder.SetActive(false);
        }
    }
    
	/// <summary>
	/// Fonction pour modifier le texte à afficher
	/// </summary>
	private void UpdateDisplayText () {
        displayText.text = text;
	}
}
