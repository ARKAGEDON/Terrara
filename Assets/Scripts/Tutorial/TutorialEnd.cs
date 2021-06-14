using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    [Tooltip("Référence vers le mob du tuto")]
    public EnemyAI enemy;
    
    [Tooltip("Référence vers le canvas de fin pour sortir du niveau")]
    public GameObject EndingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        EndingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsDead == true) //Vérification de la mort ou non de l'ennemi, si oui on affiche le canvas de fin
        {
            EndingCanvas.SetActive(true);
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
