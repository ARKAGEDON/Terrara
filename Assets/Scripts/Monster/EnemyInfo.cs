using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [Header("Stats du mob")]

    [Tooltip("Vie maximum du monstre")]
    [SerializeField] private float maxHp = 25f;

    [Tooltip("Vie actuelle du monstre")]
    [SerializeField] private float currentHp = 25f;

    [Tooltip("Dégats du monstre")]
    [SerializeField] private float damage = 2f;
    
    [Tooltip("Bool pour vérifier si le monstre est mort ou non")]
    [SerializeField] private bool isDead = false;

    public bool IsDead { get => isDead; }
    public float Damage { get => damage; }
    public float MaxHp { get => maxHp; }
    public float CurrentHp { get => currentHp; }

    private void Start() {
        currentHp = maxHp;
        isDead = false;
    }

    /// <summary>
    /// Fonction pour appliquer des dégats au monstre
    /// </summary>
    /// <param name="_damage">Dégats à appliquer</param>
    public virtual void ApplyDamage(float _damage)
    {
        if (currentHp <= 0)
        {
            Die();
            return;
        }

        currentHp -= _damage;

        if (currentHp <= 0)
            Die();
    }

    /// <summary>
    /// Fonction appelé à la mort du monstre (anim, suppression du monstre ...)
    /// </summary>
    public virtual void Die()
    {
        isDead = true;
        //Jouer animation de mort et désactiver
        Destroy(gameObject, 5f);
    }
}
