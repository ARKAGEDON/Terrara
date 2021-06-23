using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotherShip : MotherShipInfo
{
    [Header("Spawn du joueur")]

    [Tooltip("Nombre de fois que le joueur peut rescussiter")]
    [SerializeField] private int playerLifeAmount = 3;

    [Tooltip("Référence vers le joueur")]
    [SerializeField] private GameObject player;
    private PlayerInfo pInfo; //Référence vers le playerinfo du joueur

    [Tooltip("Référence vers le point de respawn du joueur")]
    [SerializeField] private Transform respawnPoint;

    [Tooltip("Temps d'attente entre la mort et le respawn du joueur")]
    [SerializeField] private float RespawnCooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pInfo = player.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pInfo.IsDead && playerLifeAmount > 0)
        {
            StartCoroutine("RespawnPlayer");
        }
    }

    /// <summary>
    /// Fonction appelé lors de la mort du vaisseau mère, nombre de respawn possible mis à 0
    /// </summary>
    public override void Die()
    {
        playerLifeAmount = 0;
        base.Die();
    }

    /// <summary>
    /// Coroutine pour respawn le joueur après un certain temps d'attente
    /// </summary>
    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(RespawnCooldown);
        player.transform.position = respawnPoint.position;
        pInfo.Respawn();
    }
}
