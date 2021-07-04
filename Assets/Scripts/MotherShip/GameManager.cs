using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Jeu")]

    [Tooltip("Référence vers le joueur")]
    [SerializeField] private GameObject player;

    [Tooltip("Référence vers le vaisseau mère ennemis")]
    [SerializeField] private EnemyMotherShip EMotherShip;

    [Tooltip("Référence vers le vaisseau mère du joueur")]
    [SerializeField] private PlayerMotherShip PMotherShip;

    [Tooltip("Référence vers le playerData")]
    [SerializeField] private PlayerData playerData;

    private PlayerInfo playerInfo;
    bool Called = false; ///Bool pour éviter l'appel plusieurs fois des récompenses 

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();
        EMotherShip = GameObject.FindGameObjectWithTag("EnemyMotherShip").GetComponent<EnemyMotherShip>();
        PMotherShip = GameObject.FindGameObjectWithTag("PlayerMotherShip").GetComponent<PlayerMotherShip>();
        playerData = GameObject.FindGameObjectWithTag("DataManager").GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EMotherShip.IsDead && !Called) //On vérifie que le vaisseau mère est bien mort puis si les boss le sont aussi, si oui alors gagné
        {
            Called = true;
            playerInfo.Win();
            playerData.AddXp(500);
            KillAll();
        }

        if (PMotherShip.IsDead && playerInfo.IsDead && !Called) //On vérifie si le joueur et le vaisseau mère sont mort, si oui alors perdu
        {
            Called = true;
            playerInfo.Loose();
            KillAll();
        }
    }

    /// <summary>
    /// Fonction faites pour tuer toutes les entités Enemy (Vaisseau mère et mobs)
    /// </summary>
    private void KillAll()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy"); //On récupère tout les mobs présent
        foreach (var Enemy in Enemies)
        {
            if (Enemy != null) //On vérifie si ils sont toujours présent puis les tues
                Destroy(Enemy);
        }

        GameObject EnemyMotherShip = GameObject.FindGameObjectWithTag("EnemyMotherShip");
        if (EnemyMotherShip != null) //Si le vaisseau mère ennemis est encore là, alors on le tue
            Destroy(EnemyMotherShip);
    }
}
