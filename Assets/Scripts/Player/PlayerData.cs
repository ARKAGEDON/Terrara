using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [Tooltip("Niveau du joueur sauvegardé dans les playerprefs")]
    [SerializeField] private int playerLevel;

    [Tooltip("Nom du joueur sauvegardé dans les playerprefs")]
    [SerializeField] private string playerName;

    [Tooltip("Exp du joueur sauvegardé dans les playerprefs")]
    [SerializeField] private float currentXp;
    
    [Tooltip("Exp Max avant changement de niveau du joueur sauvegardé dans les playerprefs")]
    [SerializeField] private float maxXp;

    //Variable public en lecture seul
    public int PlayerLevel { get => playerLevel; }
    public string PlayerName { get => playerName; }
    public float CurrentXp { get => currentXp; }
    public float MaxXp { get => maxXp; }

    //Fonction pour récupérer le nom et le niveau du joueur stocké dans ses préférences
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("Level"))
            playerLevel = PlayerPrefs.GetInt("Level");
        if (playerLevel == 0) //Si aucun niveau n'est sauvegardé alors on met le niveau à défaut et l'enregistre dans les data du joueur
        {
            playerLevel = 1;
            PlayerPrefs.SetInt("Level", playerLevel);
            PlayerPrefs.Save();       
        }

        if (PlayerPrefs.HasKey("CurrentXp"))
            currentXp = PlayerPrefs.GetFloat("CurrentXp");

        if (PlayerPrefs.HasKey("MaxXp"))
            maxXp = PlayerPrefs.GetFloat("MaxXp");
        if (maxXp == 0 || playerLevel == 1) //Si aucun xp max n'est trouvé alors on met l'exp max à défaut et l'enregistre dans les data du joueur
        {
            maxXp = 100;
            PlayerPrefs.SetFloat("MaxXp", maxXp);
            PlayerPrefs.Save();
        }
        else
        {
            maxXp = 100 * Mathf.Pow(2, playerLevel-2);
            PlayerPrefs.SetFloat("MaxXp", maxXp);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("Name"))
            playerName = PlayerPrefs.GetString("Name");
    }

    /// <summary>
    /// Fonction pour ajouter des niveaux
    /// </summary>
    /// <param name="_level">Niveau à ajouter</param>
    public void AddLevel(int _level)
    {
        playerLevel += _level;
        PlayerPrefs.SetInt("Level", playerLevel);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Fonction pour ajouter de l'exp
    /// </summary>
    /// <param name="_xp">Exp à ajouter</param>
    public void AddXp(int _xp)
    {
        currentXp += _xp;
        if (currentXp >= maxXp) //Si de l'exp en trop on monte de niveau
        {
            while (currentXp >= maxXp)
            {
                AddLevel(1);
                currentXp = currentXp - maxXp;
                maxXp = 100 * Mathf.Pow(2, playerLevel-2);
                if (currentXp < 0)
                {
                    currentXp = 0;
                }
            }
        }
        PlayerPrefs.SetFloat("CurrentXp", currentXp);
        PlayerPrefs.SetFloat("MaxXp", maxXp);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Fonction pour modifier le pseudo
    /// </summary>
    /// <param name="_name">Nom du joueur à ajouter</param>
    public void AddPlayerName(string _name)
    {
        playerName = _name;
        PlayerPrefs.SetString("Name", playerName);
        PlayerPrefs.Save();
    }
}
