using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHitDamage : MonoBehaviour
{
    [SerializeField]
    private float damage; //Variable de dégats de la balle
    private int collat; //Variable du nombre de collat de la balle

    /// <summary>
    /// Fonction pour initialiser des valeurs à la balle
    /// </summary>
    /// <param name="_dammage">Dégats de la balle</param>
    /// <param name="_collat">Nombre de collat que la balle peut faire</param>
    public void InitialiseDamage(float _dammage, int _collat)
    {
        damage = _dammage;
        collat = _collat;
    }

    private void OnTriggerEnter(Collider other) 
    {
        //Collision de la balle
        if (other.CompareTag("Enemy")) //Si elle touche un mob alors dégats
        {
            other.GetComponent<EnemyAI>().ApplyDamage(damage);
            if (collat > 1)
                collat--; //Si on touche un ennemis et qu'on peut encore faire des collatéraux alors on retire juste un au nombre de collatéral restants
            else
                Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyMotherShip"))
        {
            other.GetComponent<EnemyMotherShip>().ApplyDamage(damage);
            if (collat > 1)
                collat--; //Si on touche un ennemis et qu'on peut encore faire des collatéraux alors on retire juste un au nombre de collatéral restants
            else
                Destroy(gameObject);
        }
        else if (!other.CompareTag("Object")) //Sinon on la détruit (toucher un mur etc..)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        //Collision de la balle
        if (other.CompareTag("Enemy")) //Si elle touche un mob alors dégats
        {
            other.GetComponent<EnemyAI>().ApplyDamage(damage);
            if (collat > 1)
                collat--; //Si on touche un ennemis et qu'on peut encore faire des collatéraux alors on retire juste un au nombre de collatéral restants
            else
                Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyMotherShip"))
        {
            other.GetComponent<EnemyMotherShip>().ApplyDamage(damage);
            if (collat > 1)
                collat--; //Si on touche un ennemis et qu'on peut encore faire des collatéraux alors on retire juste un au nombre de collatéral restants
            else
                Destroy(gameObject);
        }
        else if (!other.CompareTag("Object") && !other.CompareTag("Player")) //Sinon on la détruit (toucher un mur etc..)
        {
            Destroy(gameObject);
        }
    }
}