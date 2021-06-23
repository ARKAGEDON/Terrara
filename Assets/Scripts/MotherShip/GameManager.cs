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

    // Start is called before the first frame update
    void Start()
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
        if (EMotherShip.IsDead) //On vérifie que le vaisseau mère est bien mort puis si les boss le sont aussi, si oui alors gagné
        {
            playerInfo.Win();
            playerData.AddXp(500);
        }

        if (PMotherShip.IsDead && playerInfo.IsDead) //On vérifie si le joueur et le vaisseau mère sont mort, si oui alors perdu
        {
            playerInfo.Loose();
        }
    }
}
