using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    [Tooltip("Référence vers le mob du tuto")]
    [SerializeField] private EnemyAI enemy;
    
    [Tooltip("Référence vers le mur entre le mob et les vaisseaux mères")]
    [SerializeField] private GameObject[] Walls;

    [Tooltip("Référence vers le vaisseau mère ennemis")]
    [SerializeField] private GameObject EMotherShip;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var wall in Walls)
        {
            wall.SetActive(true);
        }
        EMotherShip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsDead == true) //Vérification de la mort ou non de l'ennemi, si oui on affiche le canvas de fin
        {
            foreach (var wall in Walls)
            {
                wall.SetActive(false);
            }
            EMotherShip.SetActive(true);
        }
    }

    /// <summary>
    /// Fonction pour retourner au menu principal
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
