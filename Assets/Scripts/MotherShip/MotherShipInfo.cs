using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MotherShipInfo : MonoBehaviour
{
    [Header("Stats du vaisseau mère")]

    [Tooltip("Vie maximum du vaisseau")]
    [SerializeField] private float maxHp = 250f;

    [Tooltip("Vie actuelle du vaisseau")]
    [SerializeField] private float currentHp = 250f;

    [Tooltip("Bool pour vérifier si le vaisseau est mort ou non")]
    [SerializeField] private bool isDead = false;

    [Header("UI")]

    [Tooltip("Référence vers l'image de barre de vie du vaisseau")]
    [SerializeField] private Image hpImage;

    [Tooltip("Référence vers le texte de la barre de vie du vaisseau")]
    [SerializeField] private TextMeshProUGUI hpText;

    public bool IsDead { get => isDead; }
    public float MaxHp { get => maxHp; }
    public float CurrentHp { get => currentHp; }

    private void Start() {
        currentHp = maxHp;
        isDead = false;
        UpdateLife();
    }

    /// <summary>
    /// Fonction pour modifier la barre de vie (appelé à chaque fois qu'on prend un dégat)
    /// </summary>
    private void UpdateLife() {
        float percentageHP = ((currentHp * 100) / maxHp) / 100;
        hpImage.fillAmount = percentageHP;
        hpText.text = currentHp.ToString() + " / " + maxHp.ToString();
    }


    /// <summary>
    /// Fonction pour appliquer des dégats au vaisseau
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
    /// Fonction appelé à la mort du vaisseau (anim, suppression du vaisseau, écran de win ou loose ...)
    /// </summary>
    public virtual void Die()
    {
        isDead = true;
        Destroy(gameObject, 5f);
    }
}
