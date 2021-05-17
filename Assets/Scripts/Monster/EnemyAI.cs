using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : EnemyInfo
{
    [Header("IA du mob")]
    [SerializeField] GameObject[] players;
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float chaseRange;
    [SerializeField] float attackRange;
    [SerializeField] float attackCooldown;

    float nextAttackTime;
    bool isFocusing;

    private void Start() {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (!IsDead)
        {
            if (!isFocusing) //On vérifie si le monstre à déjà focus un joueur ou non
            {
                target = CheckNearest();
            }

            float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
            if (distance <= chaseRange)
            {
                //On vérifie que le joueur est dans la zone d'attaque du joueur et le cooldown passé sinon on avance
                if (distance <= attackRange && Time.time > nextAttackTime)
                {
                    //Anim d'attaque 
                    target.GetComponent<PlayerInfo>().ApplyDamage(Damage);
                    nextAttackTime = Time.time + attackCooldown; //Mise en place du cooldown
                }
                else
                    agent.SetDestination(target.transform.position);
            }
        }
    }
    
    /// <summary>
    /// Fonction pour chercher le joueur le plus proche
    /// </summary>
    /// <returns>Retourne le transform du joueur le plus proche</returns>
    private Transform CheckNearest()
    {
        float distance = Mathf.Infinity;
        Transform nearestTarget = null; //Joueur le plus proche
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in players)
        {
            float targetDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (targetDistance < distance)
            {
                distance = targetDistance;
                nearestTarget = player.transform;
            }
        }
        return nearestTarget;
    }

    /// <summary>
    /// Fonction de dégats modifié pour pouvoir vérifier le joueur le plus proche si l'ennemi se prend un coup (afin que si un joueur profite de l'agro de son pote l'ennemi change de target)
    /// </summary>
    /// <param name="_damage">Dégats à appliquer au monstre</param>
    public override void ApplyDamage(float _damage)
    {
        target = CheckNearest();
        base.ApplyDamage(_damage);
    }


    /// <summary>
    /// Fonction de mort du mob
    /// </summary>
    public override void Die()
    {
        agent.enabled = false;
        base.Die();
    }

    /// <summary>
    /// Fonction pour afficher les distances d'attaques et de chasse du mob
    /// </summary>
	void OnDrawGizmosSelected ()
	{
        //Zone de chasse
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, chaseRange);

        //Zone d'attaque
        Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
