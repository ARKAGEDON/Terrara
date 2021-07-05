using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwordHitDamage : MonoBehaviour
{
    [Tooltip("Référence vers le script sword pour récupérer les dégats")]
    [SerializeField] Sword Saber;

    [Tooltip("Référence vers le rigidbody de l'épée pour vérifie la vitesse")]
    [SerializeField] Rigidbody saberRb;
    
    [Tooltip("Bool pour éviter de mettre des dégats en continue si l'épée est dans le monstre")]
    [SerializeField] bool isAttacking = false;

    private void OnTriggerEnter(Collider other) {
        if (!isAttacking && Saber.IsOpen && saberRb.velocity.magnitude > 3) //Si la vitesse est supérieure à 3 et qu'on peut attaquer
        {
            if (other.CompareTag("Enemy")) //On vérifie qu'on à bien touché un ennemis
            {
                isAttacking = true; //On met la bool à true et tant que l'épée est pas sortis on applique plus de dégats
                other.GetComponent<EnemyAI>().ApplyDamage(Saber.Damage);
            }
            else if (other.CompareTag("EnemyMotherShip")) //On vérifie qu'on a bien touché un ennemis
            {
                isAttacking = true; //On met la bool à true et tant que l'épée est pas sortis on applique plus de dégats
                other.GetComponent<EnemyMotherShip>().ApplyDamage(Saber.Damage);
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if (!isAttacking && Saber.IsOpen &&  saberRb.velocity.magnitude > 3) //Si la vitesse est supérieur à 3 et qu'on peut attaquer
        {
            if (other.CompareTag("Enemy")) //On vérifie qu'on a bien touché un ennemis
            {
                isAttacking = true; //On met la bool à true et tant que l'épée est pas sortis on applique plus de dégats
                other.GetComponent<EnemyAI>().ApplyDamage(Saber.Damage);
            }
            else if (other.CompareTag("EnemyMotherShip")) //On vérifie qu'on a bien touché un ennemis
            {
                isAttacking = true; //On met la bool à true et tant que l'épée est pas sortis on applique plus de dégats
                other.GetComponent<EnemyMotherShip>().ApplyDamage(Saber.Damage);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (isAttacking)
        {
            isAttacking = false; //L'épée est sortis du monstre on peut appliqué de nouveau des dégats au prochain coup
        }
    }
}
