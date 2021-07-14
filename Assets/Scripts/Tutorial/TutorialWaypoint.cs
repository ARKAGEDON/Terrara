using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialWaypoint : MonoBehaviour
{
    [Tooltip("Chaine de charactère contenant le message du waypoint")]
    public string message;

    [Tooltip("Référence vers le texte à modifié dans l'ui du joueur")]
    public TextMeshProUGUI playerMessageText;
    
    private void OnTriggerEnter(Collider other) //Lorsque le joueur entre en contact on afficher le message du waypoint
    {
        if (other.CompareTag("Player"))
            playerMessageText.text = message;
    }
    private void OnTriggerExit(Collider other)//Lorsque le joueur sort de la zone on désactive le message
    {
        if (other.CompareTag("Player"))
            playerMessageText.text = "";
    }
}
