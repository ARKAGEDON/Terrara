using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    [Header("Stats du joueur")]

    [Tooltip("Vie maximale du joueur")]
    [SerializeField] private float maxHp = 25f;

    [Tooltip("Vie actuelle du joueur")]
    [SerializeField] private float currentHp = 25f;

    [Tooltip("Bool pour vérifier si le joueur est mort ou non")]
    [SerializeField] private bool isDead = false;
    private bool dontChase = false; //Bool pour éviter que le mob chasse le joueur

    [Tooltip("Liste des composants à désactiver à la mort du joueur")]
    [SerializeField] private Behaviour[] componentsToDisable;

    [Header("UI")]

    [Tooltip("Référence vers l'image de barre de vie du joueur")]
    [SerializeField] private Image hpImage;

    [Tooltip("Référence vers le texte de la barre de vie du joueur")]
    [SerializeField] private TextMeshProUGUI hpText;

    [Tooltip("Référence vers le panel de mort du joueur")]
    [SerializeField] private GameObject deathPanel;

    [Tooltip("Référence vers le panel de win du joueur")]
    [SerializeField] private GameObject winPanel;

    [Tooltip("Référence vers le panel de loose du joueur")]
    [SerializeField] private GameObject loosePanel;

    public bool IsDead { get => isDead; }
    public bool DontChase { get => dontChase; }
    private void Start() {
        Respawn(); //On initialise le joueur à ses valeurs par défaut
        loosePanel.SetActive(false);
        winPanel.SetActive(false);
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
        dontChase = true;
        DisableComponents();
        deathPanel.SetActive(true);
    }

    /// <summary>
    /// Fonction pour faire respawn le joueur
    /// </summary>
    public void Respawn()
    {
        currentHp = maxHp;
        isDead = false;
        dontChase = false;
        deathPanel.SetActive(false);
        UpdateLife();
        EnableComponents();
    }

    /// <summary>
    /// Fonction pour désactiver les composants mis dans la liste
    /// </summary>
    private void DisableComponents()
    {
        // boucle pour désactiver les components du joueur
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    /// <summary>
    /// Fonction pour activer les composants mis dans la liste
    /// </summary>
    private void EnableComponents()
    {
        // boucle pour activer les components du joueur
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = true;
        }
    }

    /// <summary>
    /// Fonction à appelé lorsque le joueur à gagné
    /// </summary>
    public void Win()
    {
        dontChase = true;
        DisableComponents();
        winPanel.SetActive(true);
    }

    /// <summary>
    /// Fonction à appelé lorsque le joueur à perdu
    /// </summary>
    public void Loose()
    {
        dontChase = true;
        DisableComponents();
        loosePanel.SetActive(true);
    }

    /// <summary>
    /// Fonction pour retourner au menu principal
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
