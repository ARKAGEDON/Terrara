using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestScript : MonoBehaviour
{
    //CE SCRIPT EST UTILISABLE POUR LES TESTS ET MODIFIABLE POUR CHAQUE TEXTE
    [SerializeField] private TextMeshProUGUI testText;
    [SerializeField] private Rigidbody saberRb;

    // Update is called once per frame
    void Update()
    {
        testText.text = "Vitesse: " + saberRb.velocity.magnitude.ToString();
    }
}
