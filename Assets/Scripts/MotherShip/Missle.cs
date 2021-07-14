using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [Header("Information du missile")]

    [Tooltip("Référence vers le transform de la cible")]
    [SerializeField] private Transform target;

    [Tooltip("Float de la vitesse du missile")]
    [SerializeField] private float speed = 1000f;

    [Tooltip("Float des dégats que le missile infligera")]
    [SerializeField] private float damage;

    public void SetValues(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }

    private void Update() {
        Vector3 direction = Vector3.MoveTowards(transform.position, target.position, speed);
        transform.position = transform.position + direction;
    }

    private void OnTriggerEnter(Collider other) {
        if (target.CompareTag("Enemy")) //Si la cible est un mob on applique les dégats et détruit la balle
        {
            target.GetComponent<EnemyAI>().ApplyDamage(damage);
            Destroy(gameObject);
        }
        else //Euh.. t'es quoi?
        {
            Debug.Log("Wtf, what are u bro?");
        }
    }
}
