using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotherShip : MotherShipInfo
{
    [Header("Spawn des mobs")]
    
    [Tooltip("Liste des spawners que le vaisseau activera si il est encore en vie")]
    [SerializeField] private GameObject[] mobSpawners;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var mobSpawner in mobSpawners) //On active tout les spawners au début
        {
            mobSpawner.SetActive(true);
        }
    }
    /// <summary>
    /// Fonction appelé lors de la mort du vaisseau mère, désactivation des spawners
    /// </summary>
    public override void Die()
    {
        foreach (var mobSpawner in mobSpawners)
        {
            mobSpawner.SetActive(false);
        }
        base.Die();
    }
}
