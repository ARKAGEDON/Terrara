using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnMonster : MonoBehaviour
{
    [Tooltip("Référence vers le mob à activer")]
    [SerializeField] private GameObject mob;

    private void Start() //On désactive le mob
    {
        mob.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) //Lorsque le joueur entre en contact on active le mob
    {
        if (other.CompareTag("Player"))
            mob.SetActive(true);
    }
}
