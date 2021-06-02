using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwordHitDamage : MonoBehaviour
{
    [SerializeField] LightSaber lSaber; //Référence vers le script light saber pour récupérer les dégats
    //[SerializeField] TextMeshProUGUI texte;
    [SerializeField] bool isAttacking = false;
    int nbCoup;

    private void OnTriggerEnter(Collider other) {
        if (!isAttacking)
        {
            if (other.CompareTag("Enemy"))
            {
                isAttacking = true;
                other.GetComponent<EnemyAI>().ApplyDamage(lSaber.Damage);
                /*nbCoup ++;
                texte.text = "Coups: " + nbCoup.ToString() + " Dégats: " + lSaber.Damage.ToString();*/
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if (!isAttacking)
        {
            if (other.CompareTag("Enemy"))
            {
                isAttacking = true;
                other.GetComponent<EnemyAI>().ApplyDamage(lSaber.Damage);
                /*nbCoup ++;
                texte.text = "Coups: " + nbCoup.ToString() + " Dégats: " + lSaber.Damage.ToString();*/
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (isAttacking)
        {
            isAttacking = false;
        }
    }
}
