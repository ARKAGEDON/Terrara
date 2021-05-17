using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    [Header("Info du sabre laser")]
    [SerializeField] private float damage; //Variable des dégats du sabre
    [SerializeField] private GameObject blade; //Référence vers la lame du sabre 
    public float Damage {get => damage;}

    // Start is called before the first frame update
    void Start()
    {
        blade.SetActive(false);
    }

    /// <summary>
    /// Fonction pour vérifier si la lame du sabre est visible ou non et activer ou non
    /// </summary>
    public void ActiveBlade()
    {
        if (blade.activeSelf)
            blade.SetActive(false);
        else
            blade.SetActive(true);
    }
}
