using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialWaypoint : MonoBehaviour
{
    public string message; //Chaine de charactère contenant le message du waypoint
    public TextMeshProUGUI playerMessageText; //Référence vers le texte à modifié dans l'ui du joueur

    private void OnTriggerEnter(Collider other) { //Lorsque le joueur entre en contact on afficher le message du waypoint
        playerMessageText.text = message;
    }
    private void OnTriggerExit(Collider other) { //Lorsque le joueur sort de la zone on désactive le message
        playerMessageText.text = "";
    }
}
