using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("Stats du joueur")]
    [Tooltip("Vie maximale du joueur")]
    [SerializeField] private float maxHp;
    [Tooltip("Vie actuelle du joueur")]
    [SerializeField] private float currentHp;
    [Tooltip("Bool pour vérifier si le joueur est mort ou non")]
    [SerializeField] private bool isDead;

    [Header("UI")]
    [Tooltip("Référence vers l'image de barre de vie du joueur")]
    [SerializeField] private Image hpImage;
    [Tooltip("Référence vers le texte de la barre de vie du joueur")]
    [SerializeField] private TextMeshProUGUI hpText;

    public bool IsDead { get => isDead; }

    private void Start() {
        currentHp = maxHp;
        isDead = false;
        UpdateLife();
    }

    private void UpdateLife() {
        float percentageHP = ((currentHp * 100) / maxHp) / 100;
        hpImage.fillAmount = percentageHP;
        hpText.text = currentHp.ToString() + " / " + maxHp.ToString();
    }

    /// <summary>
    /// Fonction pour appliquer des dégats au joueur
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
        UpdateLife();

        if (currentHp <= 0)
            Die();
    }

    /// <summary>
    /// Fonction appelé à la mort du joueur
    /// </summary>
    private void Die()
    {
        isDead = true;
        //Afficher écran de mort
    }
}
