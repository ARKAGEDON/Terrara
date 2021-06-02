using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    public EnemyAI enemy; //Référence vers le mob
    public GameObject EndingCanvas; //Référence vers le canvas de fin pour sortir du niveau

    // Start is called before the first frame update
    void Start()
    {
        EndingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsDead == true)
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
