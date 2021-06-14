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
    [SerializeField] private int currentXp;
    
    [Tooltip("Exp Max avant changement de niveau du joueur sauvegardé dans les playerprefs")]
    [SerializeField] private int maxXp;

    //Variable public en lecture seul
    public int PlayerLevel { get => playerLevel; }
    public string PlayerName { get => playerName; }
    public int CurrentXp { get => currentXp; }
    public int MaxXp { get => maxXp; }

    //Fonction pour récupérer le nom et le niveau du joueur stocké dans ses préférences
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("Level"))
            playerLevel = PlayerPrefs.GetInt("Level");
            
        if (PlayerPrefs.HasKey("CurrentXp"))
            currentXp = PlayerPrefs.GetInt("CurrentXp");
        if (PlayerPrefs.HasKey("MaxXp"))
            maxXp = PlayerPrefs.GetInt("MaxXp");

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
            AddLevel(1);
            AddXp(currentXp-maxXp);
            return;
        }
        PlayerPrefs.SetInt("CurrentXp", currentXp);
        PlayerPrefs.SetInt("MaxXp", maxXp);
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
