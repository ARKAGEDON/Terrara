using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType {Basic, Pistol, Pump, Sniper};
public class Gun : MonoBehaviour
{
    [Header("Info sur l'arme")]

    [Tooltip("Position du bout du pistolet (là où on tire la balle)")]
    public Transform barrel;

    [Tooltip("Nom de l'arme")]
    public string GunName = "Gun";

    [Tooltip("Type de l'arme")]
    public WeaponType weaponType = WeaponType.Basic;

    [Tooltip("Nombre de balle par tir (pour les pompes etc)")]
    public int amountOfProjectiles = 1;

    [Tooltip("Nombre de ricochet/collat par tir")]
    public int amountOfCollat = 1;

    [Tooltip("Dégats de l'arme")]
    public float damage = 10f;

    [Tooltip("Vitesse de la balle")]
    public float speed = 75f;

    [Tooltip("Cooldown entre chaque tir")]
    public float shootCooldown = 5f;
    private float timeCooldown;

    [Tooltip("Liste des balles des armes, (0 balle classique, 1 balle ultime)")]
    public GameObject[] bullets;

    [Header("Info audio")]

    [Tooltip("Source sonore du tir")]
    public AudioSource audioSource;

    [Tooltip("Son du tir")]
    public AudioClip audioClip;

    
    private float loadingTime = 0;
    private float loadedTime = 0;

    private List<GameObject> Enemies;

    /// <summary>
    /// Fonction pour commencer le chargement de l'arme
    /// </summary>
    public void StartLoading()
    {
        if (timeCooldown < Time.time)
        {
            loadingTime = Time.time;
            //Afficher l'animation de chargement de l'arme;
        }
    }

    /// <summary>
    /// Fonction pour stopper le chargement de l'arme
    /// </summary>
    public void StopLoading()
    {
        if (loadingTime != 0) //Vérification si le joueur à un peu chargé l'arme
        {
            loadedTime = Time.time - loadingTime; //Loadedtime vaut le temps qui est chargé
            loadingTime = 0;
            
            if (loadedTime <= 5f)
            {
                switch (weaponType)
                {
                    case WeaponType.Basic: //Si l'arme est un simple flingue
                        BasicShoot(damage * (loadedTime / 2.5f),0);
                        break;
                    case WeaponType.Pistol: //Si l'arme est un pistolet
                        BasicShoot(damage * (loadedTime / 2.5f),0);
                        break;
                    case WeaponType.Pump: //Si l'arme est un pompe
                        MultiShoot(damage * (loadedTime / 2.5f));
                        break;
                }
            }
            else if (loadedTime > 5f)
            {
                switch (weaponType)
                {
                    case WeaponType.Basic: //Si l'arme est un simple flingue
                        BasicShoot(damage * 2f,0);
                        break;
                    case WeaponType.Pistol: //Si l'arme est un pistolet
                        BasicShoot(damage * 2f,1);
                        break;
                    case WeaponType.Pump: //Si l'arme est un pompe
                        UltimPumpShoot(damage * 2f);
                        break;
                }
            }
            
        }
    }

    /// <summary>
    /// Fonction pour le tir basique
    /// </summary>
    /// <param name="_damage">Dégats de chaque balle</param>
    /// <param name="bulletId">Identifiant de la balle à initialiser</param>
    public void BasicShoot(float _damage, int bulletId)
    {
        if (timeCooldown < Time.time) //Vérification que le cooldown est passé
        {
            timeCooldown = Time.time + shootCooldown; //Ajout du cooldown pour éviter le spam

            //Initialisation de la balle et son
            GameObject tmp_bullet = Instantiate(bullets[0],barrel);
            tmp_bullet.GetComponent<Rigidbody>().velocity = barrel.transform.forward * speed;
            tmp_bullet.GetComponent<GunHitDamage>().InitialiseDamage(_damage,amountOfCollat);

            audioSource.PlayOneShot(audioClip);
        }
    }

    /// <summary>
    /// Fonction pour le tir multiple
    /// </summary>
    /// <param name="_damage">Dégats de chaque balle</param>
    public void MultiShoot(float _damage)
    {
        if (timeCooldown < Time.time) //Vérification que le cooldown est passé
        {
            timeCooldown = Time.time + shootCooldown; //Ajout du cooldown

            //Boucle pour tout les projectiles
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                Vector3 direction = barrel.transform.forward; // Récupération de la visée initiale
                Vector3 spread = barrel.transform.up * Random.Range(-1f, 1f); // Ajout de spread random en haut et en bas
                spread += barrel.transform.right * Random.Range(-1f, 1f); //Ajout de spread random en gauche et à droite

                // Utilisation du normalized pour arrondir le spread car l'ajout random va créer un speard carré, puis re randomisation du radius
                direction += spread.normalized * Random.Range(0f, 0.2f);

                //Initialisation de la balle
                GameObject tmp_bullet = Instantiate(bullets[0],barrel);
                tmp_bullet.GetComponent<Rigidbody>().velocity = direction * speed;
                tmp_bullet.GetComponent<GunHitDamage>().InitialiseDamage(_damage,amountOfCollat);
            }

            audioSource.PlayOneShot(audioClip);
        } 
    }

    /// <summary>
    /// Fonction pour le tir ultime du pompe (Attention la balle ultime du pompe doit être un missile (voir classe missile))
    /// </summary>
    /// <param name="_damage">Dégats de chaque balle</param>
    public void UltimPumpShoot(float _damage)
    {
        if (timeCooldown < Time.time) //Vérification que le cooldown est passé
        {
            timeCooldown = Time.time + shootCooldown;
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                Transform target = CheckNearestMob();

                if (target != null)
                {
                    GameObject go = Instantiate(bullets[1], barrel.position, barrel.rotation);
                    go.GetComponent<Missle>().SetValues(target, _damage);
                }
            }

            audioSource.PlayOneShot(audioClip);
        }
    }

    /// <summary>
    /// Fonction pour chercher le mob le plus proche
    /// </summary>
    /// <returns>Retourne le transform du mob le plus proche</returns>
    private Transform CheckNearestMob()
    {
        float distance = Mathf.Infinity;
        GameObject nearestTarget = null; //Mob le plus proche
        Enemies =  new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (var Enemy in Enemies)
        {
            float targetDistance = Vector3.Distance(gameObject.transform.position, Enemy.transform.position);
            if (targetDistance < distance)
            {
                distance = targetDistance;
                nearestTarget = Enemy;
            }
        }
        Enemies.Remove(nearestTarget);

        return nearestTarget.transform;
    }

}