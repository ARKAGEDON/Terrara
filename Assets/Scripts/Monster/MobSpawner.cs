using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [Header("Spawner Info")]

    [Tooltip("Liste des points de spawn pour les mobs")]
    [SerializeField] private Transform[] spawnPoints;

    [Tooltip("Bool pour vérifier si il y a un nombre aléa de spawner ou non")]
    [SerializeField] private bool randomSpawner = false;

    [Tooltip("Variable pour savoir le nombre min et max de spawner aléatoire")]
    [SerializeField] private int minSpawner, maxSpawner;

    [Tooltip("Temps d'attente entre chaque vague de spawn")]
    [SerializeField] private float cooldownBetweenSpawn = 2f;

    [Header("Entity Info")]

    [Tooltip("Prefab de l'entité à spawn")]
    [SerializeField] private GameObject EntityPrefab;

    [Tooltip("Bool à activer si on veut un nombre aléatoire d'entité (par défaut 1 par spawner)")]
    [SerializeField] private bool randomAmount = false;

    [Tooltip("Bool à activer si on veut un nombre aléatoire d'entité par spawner (par le même nombre par spawner)")]
    [SerializeField] private bool randomAmoutPerSpawner = false;
    
    [Tooltip("Variable pour savoir le min et max d'entité à spawn")]
    [SerializeField] private int minEntity, maxEntity;

    float nextSpawnTime;


    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
            Spawner();
    }

    /// <summary>
    /// Fonction pour instantier les entités à partir des spawners
    /// </summary>
    void Spawner()
    {
        
        if (randomSpawner) //On vérifie si les spawners sont aléatoire ou non
        {
            int spawnerAmount = Random.Range(minSpawner,maxSpawner);
            
            if (randomAmount) //On vérifie si le nombre d'entité est aléatoire ou non
            {
                int entityAmount = Random.Range(minEntity,maxEntity);

                if (randomAmoutPerSpawner) //On vérifie si le nombre d'entité est aléatoire par spawner ou non
                {
                    for (var i = 0; i < spawnerAmount; i++)
                    {
                        entityAmount = Random.Range(minEntity,maxEntity);
                        SpawnEntity(i, entityAmount);
                    }
                    nextSpawnTime += cooldownBetweenSpawn;
                    return;
                }

                for (var i = 0; i < spawnerAmount; i++)
                {
                    SpawnEntity(i, entityAmount);
                }
                nextSpawnTime += cooldownBetweenSpawn;
            }
            else
            {
                for (var i = 0; i < spawnPoints.Length; i++)
                {
                    SpawnEntity(i, 1);
                }
                nextSpawnTime += cooldownBetweenSpawn;
            }
        }
        else
        {
            if (randomAmount) //On vérifie si le nombre d'entité est aléatoire ou non
            {
                int entityAmount = Random.Range(minEntity,minSpawner);

                if (randomAmoutPerSpawner) //On vérifie si le nombre d'entité est aléatoire par spawner ou non
                {
                    for (var i = 0; i < spawnPoints.Length; i++)
                    {
                        entityAmount = Random.Range(minEntity,maxEntity);
                        SpawnEntity(i, entityAmount);
                    }
                    nextSpawnTime += cooldownBetweenSpawn;
                    return;
                }

                for (var i = 0; i < spawnPoints.Length; i++)
                {
                    SpawnEntity(i, entityAmount);
                }
                nextSpawnTime += cooldownBetweenSpawn;
            }
            else
            {
                for (var i = 0; i < spawnPoints.Length; i++)
                {
                    SpawnEntity(i, 1);
                }
                nextSpawnTime += cooldownBetweenSpawn;
            }
        }
    }

    /// <summary>
    /// Fonction pour instantier l'entité
    /// </summary>
    /// <param name="_spawnerId">Id du spawnPoints où instantié l'entité</param>
    /// <param name="_randomEntityAmount">Nombre de fois à instantier l'entité</param>
    void SpawnEntity(int _spawnerId, int _randomEntityAmount)
    {
        for (var i = 0; i < _randomEntityAmount; i++)
        {
            Instantiate(EntityPrefab, Random.insideUnitSphere * 5f + spawnPoints[_spawnerId].position, Quaternion.identity);
        }
    }
}
