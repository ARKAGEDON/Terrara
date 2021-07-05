using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Info du sabre laser")]

    [Tooltip("Dégats qu'inflige le sabre")]
    [SerializeField] private float damage = 5f; 

    [Tooltip("Référence vers le renderer de la lame")]
    [SerializeField] private Renderer blade;

    [Tooltip("Référence vers le renderer du manche")]
    [SerializeField] private Renderer handleLights;

    [Tooltip("Référence vers les matériaux (ordre: 0 Matériaux quand le sabre et éteint, 1 Matériaux quand le sabre est allumé")]
    [SerializeField] private Material[] materials;

    [Tooltip("Référence vers la source audio du sabre")]
    [SerializeField] private AudioSource audioSource; 

    [Tooltip("Liste des différents clip de son de l'arme (ordre: 0 Démarrage du sabre, 1 Son vibration du sabre, 2 son stop du sabre)")]
    [SerializeField] private AudioClip[] clips;

    public float Damage {get => damage;}
    public bool IsOpen { get => isOpen; }

    bool humming = false; //Bool pour jouer le son hum
    bool isOpen = false; //Bool pour vérifier si la lame est activer ou non


    // Start is called before the first frame update
    void Start()
    {
        ChangeMaterial(0);
        isOpen = false;
    }

    private void Update() {
        if (!humming && IsOpen) //Si aucun son est joué et que le sabre est ouvert alors on joue le son de vibration du sabre
        {
            humming = true;
            audioSource.clip = clips[1];
            audioSource.Play();
        }
    }

    /// <summary>
    /// Fonction pour activer la lame du sabre laser (on vérifie si elle l'est et change les matériaux en fonction) 
    /// </summary>
    public void ActiveBlade()
    {
        if (IsOpen)
        {
            audioSource.Stop(); // on stop le son de humming
            audioSource.PlayOneShot(clips[2]); //On joue le son de stop du sabre
            ChangeMaterial(0);
            humming = false;
            isOpen = false;
        }
        else
        {
            audioSource.Stop(); //Au cas où un son se joue
            ChangeMaterial(1);
            audioSource.PlayOneShot(clips[0]); //On joue le son d'ouverture du sabre
            isOpen = true;
        }
    }

    /// <summary>
    /// Fonction pour changer les matériaux d'un renderer
    /// </summary>
    /// <param name="_id">Id du matériaux dans la liste définis dans l'inspector</param>
    private void ChangeMaterial(int _id)
    {
        //Changement des matériaux de la lame
        Material[] mats = blade.materials;
        mats[1] = materials[_id];
        blade.materials = mats;

        //Changement des matériaux du manche
        mats = handleLights.materials;
        mats[0] = materials[_id];
        handleLights.materials = mats;
    }
}
