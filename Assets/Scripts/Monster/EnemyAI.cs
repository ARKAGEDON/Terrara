using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class EnemyAI : EnemyInfo
{
    [Header("IA du mob")]

    [Tooltip("Liste de joueurs trouvés par le monstre")]
    [SerializeField] private GameObject[] players;

    [Tooltip("Transform de la cible de le monstre")]
    [SerializeField] private Transform target;

    [Tooltip("Référence du NavMeshAgent de le monstre")]
    [SerializeField] private NavMeshAgent agent;

    [Tooltip("Référence de l'Animator de le monstre")]
    [SerializeField] private Animator animator;

    [Tooltip("Distance dans la quelle le monstre suit le joueur")]
    [SerializeField] private float chaseRange = 5f;

    [Tooltip("Distance dans la quelle le monstre peut attaquer")]
    [SerializeField] private float attackRange = 2f;

    [Tooltip("Temps d'attente entre chaque attaque")]
    [SerializeField] private float attackCooldown = 2f;

    [Header("UI du mob")]

    [Tooltip("Référence vers le texte qui affiche les dégats subis")]
    [SerializeField] private TextMeshProUGUI damageText;
    
    [Tooltip("Référence l'image de la barre de vie")]
    [SerializeField] private Image healthBar;

    float nextAttackTime;

    [Header("Special case")]

    [Tooltip("A activer si vous voulez que le mob ne focus que le joueur le plus proche dès son spawn")]
    [SerializeField] private bool isFocusing;

    private void Start() {
        agent = gameObject.GetComponent<NavMeshAgent>();
        float percentageHP = ((CurrentHp * 100) / MaxHp) / 100;
        healthBar.fillAmount = percentageHP;

        //Si on veut que le mob focus directement un joueur
        if (isFocusing == true)
        {
            target = CheckNearestPlayer();
        }
    }

    private void Update() 
    {
        if (!IsDead)
        {
            if (!isFocusing) //On vérifie si le monstre à déjà focus un joueur ou non
            {
                target = CheckNearest();
            }

            if (target != null)
            {
                float distance = Vector3.Distance(gameObject.transform.position, target.transform.position); //Calcul de la distance entre la cible et le monstre
                //On vérifie que le joueur est dans la zone d'attaque du joueur et le cooldown passé sinon on avance
                if (distance <= attackRange)
                {
                    animator.SetFloat ("Speed", agent.velocity.magnitude/agent.speed,.1f,Time.deltaTime);
                    if (Time.time > nextAttackTime)
                    {
                        animator.SetBool("Attack", true);
                        Attack();
                    }
                }
                else
                {
                    animator.SetFloat ("Speed", agent.velocity.magnitude/agent.speed,.1f,Time.deltaTime); //Ajout de la vitesse actuel du mob dans l'animator pour les animations de déplacement
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Smash")) //Vérification si le joueur n'est pas en train d'attaquer pour éviter l'effet de slide
                    {
                        agent.SetDestination(target.transform.position);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Fonction pour chercher l'entité la plus proche
    /// </summary>
    /// <returns>Retourne le transform de l'entité la plus proche</returns>
    private Transform CheckNearest()
    {
        float distance = Mathf.Infinity; //Float pour trouvé l'entité la plus proche (la distance entre cet entité et le mob)
        Transform nearestTarget = null; //Entité la plus proche
        players = GameObject.FindGameObjectsWithTag("Player");
        //Ajout de notre vaisseau mère
        GameObject playerMotherShip = GameObject.FindGameObjectWithTag("PlayerMotherShip");

        //L'ennemi va d'abord se diriger vers notre vaisseau mère si ce dernier n'est pas mort
        if (!playerMotherShip.GetComponent<PlayerMotherShip>().IsDead)
        {
            float motherShipDistance = Vector3.Distance(gameObject.transform.position, playerMotherShip.transform.position); //Distance entre le mob et notre vaisseau mère
            distance = motherShipDistance; //Distance la plus petite trouvée pour l'instant est celle entre le vaisseau mère et le mob
            nearestTarget = playerMotherShip.transform; //Notre vaisseau mère deviens la cible du mobs
        }

        foreach (var player in players)
        {
            float targetDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (targetDistance < distance && targetDistance < chaseRange) //Si le joueur se trouve dans la zone d'attaque du mobs et que la distance entre le mob et le joueur est plus petite que celle déjà trouvée
            {
                distance = targetDistance; //Alors la distance mini est celle entre le mob et ce joueur
                nearestTarget = player.transform; //Ce joueur deviens la cible
            }
        }

        return nearestTarget;
    }

    /// <summary>
    /// Fonction pour chercher le joueur le plus proche
    /// </summary>
    /// <returns>Retourne le transform du joueur le plus proche</returns>
    private Transform CheckNearestPlayer()
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
        //Le mob cible focus celui qui lui a mis des dégats
        target = CheckNearestPlayer();
        isFocusing = true;

        IEnumerator coroutine = ShowDamage(_damage);
        StartCoroutine(coroutine);
        base.ApplyDamage(_damage);
    }

    /// <summary>
    /// Fonction appelé par l'animation d'attaque, on applique les dégats à la cible, augmente le cooldown et désactive l'animation
    /// </summary>
    public void Attack()
    {
        if (target.CompareTag("Player")) //Si la cible est un joueur
        {
            target.GetComponent<PlayerInfo>().ApplyDamage(Damage);
        }
        else if (target.CompareTag("PlayerMotherShip")) //Si la cible est notre vaisseau mère
        {
            target.GetComponent<PlayerMotherShip>().ApplyDamage(Damage);
        }
        else //Euh.. t'es quoi?
        {
            Debug.Log("Wtf, what are u bro?");
        }

        nextAttackTime = Time.time + attackCooldown; //Mise en place du cooldown
        animator.SetBool("Attack", false);
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

    /// <summary>
    /// Coroutine pour afficher les dégats pris par le mob
    /// </summary>
    /// <param name="_damage">Dégats pris par le monstre à affiché</param>
    public IEnumerator ShowDamage(float _damage)
    {
        damageText.text = "-" + _damage.ToString();
        yield return new WaitForSeconds(0.5f);
        damageText.text = "";
        float percentageHP = ((CurrentHp * 100) / MaxHp) / 100;
        healthBar.fillAmount = percentageHP;
    }
}
