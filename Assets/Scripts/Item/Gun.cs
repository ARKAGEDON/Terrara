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

    [Tooltip("Dégats de l'arme")]
    public float damage = 10f;

    [Tooltip("Vitesse de la balle")]
    public float speed = 75f;

    [Tooltip("Cooldown entre chaque tir")]
    public float shootCooldown = 5f;
    private float timeCooldown;

    [Tooltip("Liste des balles des armes, (0 balle classique, 1 balle ultime Pistolet)")]
    public GameObject[] bullets;

    [Header("Info audio")]

    [Tooltip("Source sonore du tir")]
    public AudioSource audioSource;

    [Tooltip("Son du tir")]
    public AudioClip audioClip;

    private GameObject _owner;
    
    private float loadingTime = 0;
    private float loadedTime = 0;

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
        if (loadingTime != 0)
        {
            loadedTime = Time.time - loadingTime;
            loadingTime = 0;
            
            if (loadedTime <= 5f)
            {
                switch (weaponType)
                {
                    //Si l'arme est un simple flingue
                    case WeaponType.Basic:
                        BasicShoot(damage * (loadedTime / 2.5f));
                        break;
                    case WeaponType.Pistol:
                        BasicShoot(damage * (loadedTime / 2.5f));
                        break;
                }
            }
            else if (loadedTime > 5f)
            {
                switch (weaponType)
                {
                    //Si l'arme est un simple flingue
                    case WeaponType.Basic:
                        BasicShoot(damage * 2);
                        break;
                    case WeaponType.Pistol:
                        UltimPistolShoot(damage * 2);
                        break;
                }
            }
            
        }
    }

    /// <summary>
    /// Fonction pour le tir basique
    /// </summary>
    public void BasicShoot(float _damage)
    {
        if (timeCooldown < Time.time) //Vérification que le cooldown est passé
        {
            timeCooldown = Time.time + shootCooldown;
            GameObject tmp_bullet = Instantiate(bullets[0],barrel);
            tmp_bullet.GetComponent<Rigidbody>().velocity = barrel.transform.forward * speed;
            tmp_bullet.GetComponent<GunHitDamage>().InitialiseDamage(_damage);

            audioSource.PlayOneShot(audioClip);
        }
    }

    /// <summary>
    /// Fonction pour le tir ultime du pistolet
    /// </summary>
    public void UltimPistolShoot(float _damage)
    {
        if (timeCooldown < Time.time) //Vérification que le cooldown est passé
        {
            timeCooldown = Time.time + shootCooldown;
            GameObject tmp_bullet = Instantiate(bullets[1],barrel);
            tmp_bullet.GetComponent<Rigidbody>().velocity = barrel.transform.forward * speed;
            tmp_bullet.GetComponent<GunHitDamage>().InitialiseDamage(_damage);

            audioSource.PlayOneShot(audioClip);
        }
    }
    /// <summary>
    /// Fonction pour ajouter le propriétaire de l'arme
    /// </summary>
    public void AddOwner()
    {
        _owner = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Fonction pour enlever le propriétaire de l'arme
    /// </summary>
    public void RemoveOwner()
    {
        _owner = null;
    }

}