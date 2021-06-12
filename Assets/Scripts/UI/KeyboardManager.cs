using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    [Tooltip("Référence vers l'ui de clavier")]
    [SerializeField] private GameObject Keyboard;

    private void Start() {
        Keyboard.SetActive(false);
    }

    /// <summary>
    /// Fonction appelé lorsqu'on appui sur un input field (la mettre dans le select de l'input field)
    /// </summary>
    /// <param name="inputFieldInfo">Référence vers le script d'information de l'inputfield</param>
    public void EnterInputField(InputFieldInfo inputFieldInfo)
    {
        Keyboard.GetComponent<Keyboard>().SetInputFieldText(inputFieldInfo.text, inputFieldInfo.placeHolder);
        Keyboard.SetActive(true);
    }

    /// <summary>
    /// Fonction appelé lorsqu'on quitte l'input field (appelé par le boutton entrée sur le clavier)
    /// </summary>
    public void ExitInputField()
    {
        Keyboard.GetComponent<Keyboard>().SetInputFieldText(null, null); //On met à null tout pour éviter les problèmes avec les inputfield
        Keyboard.SetActive(false);
    }
}