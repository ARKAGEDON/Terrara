using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    [Header("Info du sabre laser")]
    [SerializeField] private float damage; //Variable des dégats du sabre
    [SerializeField] private GameObject blade; //Référence vers la lame du sabre 
    [SerializeField] private AudioSource audioSource; //Référence vers la source audio
    [SerializeField] private AudioClip[] clips; //Liste des différents clip de son de l'arme (ordre: 0 Démarrage du sabre, 1 Son vibration du sabre, 2 son stop du sabre)
    public float Damage {get => damage;}
    bool humming = false; //Bool pour jouer le son hum

    // Start is called before the first frame update
    void Start()
    {
        blade.SetActive(false);
    }

    private void Update() {
        if (!humming && blade.activeSelf) //Si aucun son est joué et que le sabre est ouvert alors on joue le son de vibration du sabre
        {
            humming = true;
            audioSource.clip = clips[1];
            audioSource.Play();
        }
    }

    /// <summary>
    /// Fonction pour vérifier si la lame du sabre est visible ou non et activer ou non
    /// </summary>
    public void ActiveBlade()
    {
        if (blade.activeSelf)
        {
            audioSource.Stop(); // on stop le son de humming
            audioSource.PlayOneShot(clips[2]); //On joue le son de stop du sabre
            blade.SetActive(false);
            humming = false;
        }
        else
        {
            audioSource.Stop(); //Au cas où un son se joue
            blade.SetActive(true);
            audioSource.PlayOneShot(clips[0]); //On joue le son d'ouverture du sabre
        }
    }
}
