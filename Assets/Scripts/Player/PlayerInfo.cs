using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("Stats du mob")]
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private bool isDead;

    [Header("UI")]
    [SerializeField] private Image hpImage;
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
