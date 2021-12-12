using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHitDamage : MonoBehaviour
{
    [SerializeField]
    private float damage; //Variable de dégats de la balle

    public void InitialiseDamage(float _dammage) //Ajout des dégats à partir de l'arme
    {
        damage = _dammage;
    }

    private void OnTriggerEnter(Collider other) 
    {
        //Collision de la balle
        if (other.CompareTag("Enemy")) //Si elle touche un mob alors dégats
        {
            other.GetComponent<EnemyAI>().ApplyDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Object")) //Sinon on la détruit (toucher un mur etc..)
        {
            Destroy(gameObject);
        }
    }
}