using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("Référence vers le playerData contenant les info du joueur")]
    [SerializeField] PlayerData playerData;
    bool isRegistered; //bool pour vérifier si le joueur est déjà enregistré ou non

    [Header("UI")]

    [Tooltip("Liste des différents panel, ordre à suivre: 0 = Register, 1 = Menu, 2 = Settings")]
    [SerializeField] private GameObject[] Panels;

    [Header("PlayerInfo UI")]
    [Tooltip("Référence vers le texte de niveau du joueur")]
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [Tooltip("Référence vers le texte d'exp du joueur")]
    [SerializeField] private TextMeshProUGUI playerXpText;
    [Tooltip("Référence vers le texte de nom du joueur")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [Tooltip("Référence vers le texte de l'inputfield pour le nom du joueur au login")]
    [SerializeField] private TextMeshProUGUI playerinputField;

    [Header("Campaign UI")]
    [SerializeField] private GameObject[] Campaigns; //Liste des différentes campagnes disponibles


    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.FindGameObjectWithTag("DataManager").GetComponent<PlayerData>();

        isRegistered = checkRegister(); //On vérifie si le joueur est login
        HidePanels(); //on cache tout les panels
        HideCampaigns(); //on cache toutes les campagnes

        if (!isRegistered)
            Panels[0].SetActive(true);
        else
            Panels[1].SetActive(true);

        UpdateMenu();
    }

    /// <summary>
    /// Fonction pour update l'ui du menu
    /// </summary>
    private void UpdateMenu() 
    {
        if (isRegistered) //On vérifie si le joueur est login avant d'update
        {
            Panels[1].SetActive(true);
            playerLevelText.text = "Niveau : " + playerData.PlayerLevel.ToString();
            playerNameText.text = playerData.PlayerName;
            playerXpText.text = playerData.CurrentXp.ToString() + "/" + playerData.MaxXp.ToString();
            Campaigns[0].SetActive(true);
            return;
        }
        Panels[0].SetActive(true);
    }

    /// <summary>
    /// Fonction booléenne vérifiant si le joueur à un pseudo ou non
    /// </summary>
    /// <returns>True si le joueur a un pseudo, False sinon</returns>
    bool checkRegister()
    {
        if (playerData.PlayerName != null && playerData.PlayerName != "" && playerData.PlayerName != " ")
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Fonction pour cacher tout les panels
    /// </summary>
    private void HidePanels()
    {
        for (var i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
    }

    /// <summary>
    /// Fonction pour valider le choix de son pseudo
    /// </summary>
    public void Validate() {
        playerData.AddPlayerName(playerinputField.text);
        isRegistered = checkRegister();
        UpdateMenu();
    }

    /// <summary>
    /// Fonction pour cacher toutes les campagnes
    /// </summary>
    private void HideCampaigns()
    {
        for (var i = 0; i < Panels.Length; i++)
        {
            Campaigns[i].SetActive(false);
        }
    }

    /// <summary>
    /// Fonction pour changer de campagne avec la flèche de droite
    /// </summary>
    /// <param name="_currentCampaignId">Id de la campagne, commence par 0 comme dans les listes</param>
    public void NextCampaign(int _currentCampaignId)
    {
        if (_currentCampaignId == Campaigns.Length - 1 ) //On vérifie que c'est pas la dernière campagne de la liste
        {
            HideCampaigns();
            Campaigns[0].SetActive(true);
            return;
        }
        HideCampaigns(); //Sinon on active la campagne suivante
        Campaigns[_currentCampaignId + 1].SetActive(true); 
    }

    /// <summary>
    /// Fonction pour changer de campagne avec la flèche gauche
    /// </summary>
    /// <param name="_currentCampaignId">Id de la campagne, commence par 0 comme dans les listes</param>
    public void PreviousCampaign(int _currentCampaignId)
    {
        if (_currentCampaignId == 0) //On vérifie si c'est pas la première campagne
        {
            HideCampaigns();
            Campaigns[Campaigns.Length -1].SetActive(true);
            return;
        }
        HideCampaigns();
        Campaigns[_currentCampaignId - 1].SetActive(true);
    }

    /// <summary>
    /// Fonction pour jouer la scène
    /// </summary>
    /// <param name="_scene">Nom de la scène que l'on veut</param>
    public void play(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    /// <summary>
    /// Fonction pour reset les playerPrefs
    /// </summary>
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
