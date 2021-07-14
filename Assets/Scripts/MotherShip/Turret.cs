using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Information")]

    [Tooltip("Zone dans la quelle la tourelle attaque")]
    [SerializeField] private float AttackRange;

    [Tooltip("Float for damage amount")]
    [SerializeField] private float Damage;

    [Tooltip("Temps d'attente entre chaque attaque")]
    [SerializeField] private float attackCooldown = 2f;

    [Tooltip("Référence vers les missiles")]
    [SerializeField] private GameObject missle;

    [Tooltip("Référence vers le point de tir du missile")]
    [SerializeField] private Transform misslePoint;

    [Tooltip("Référence vers notre vaisseau mère")]
    [SerializeField] private PlayerMotherShip playerMotherShip;

    [Tooltip("Transform de la cible des tourelles")]
    [SerializeField] private Transform target;

    float nextAttackTime;
    

    GameObject[] Enemies;

    // Update is called once per frame
    void Update()
    {
        //Si notre vaisseau mère est encore vivant
        if (!playerMotherShip.IsDead)
        {
            target = CheckNearestMob();

            if (target != null)
            {
                float distance = Vector3.Distance(gameObject.transform.position, target.transform.position); //Calcul de la distance entre la cible et le monstre
                if (distance <= AttackRange)
                {
                    if (Time.time > nextAttackTime)
                    {
                        Attack();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Fonction pour chercher le mob le plus proche
    /// </summary>
    /// <returns>Retourne le transform du mob le plus proche</returns>
    private Transform CheckNearestMob()
    {
        float distance = Mathf.Infinity;
        Transform nearestTarget = null; //Mob le plus proche
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var Enemy in Enemies)
        {
            float targetDistance = Vector3.Distance(gameObject.transform.position, Enemy.transform.position);
            if (targetDistance < distance && targetDistance < AttackRange)
            {
                distance = targetDistance;
                nearestTarget = Enemy.transform;
            }
        }

        return nearestTarget;
    }

    /// <summary>
    /// Fonction d'attaque, on applique les dégats à la cible, augmente le cooldown
    /// </summary>
    public void Attack()
    {
        GameObject go = Instantiate(missle, misslePoint.position, misslePoint.rotation);
        go.GetComponent<Missle>().SetValues(target, Damage);
        nextAttackTime = Time.time + attackCooldown; //Mise en place du cooldown
    }

    /// <summary>
    /// Fonction pour afficher la distance d'attaque de la tourelle
    /// </summary>
	void OnDrawGizmosSelected ()
	{
        //Zone d'attaque
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, AttackRange);
	}
}
