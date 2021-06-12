using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    [Tooltip("Bool pour savoir si on doit afficher les mains ou les controllers")]
    public bool showController = false;
    [Tooltip("Référence vers notre casque pour savoir le model et donc quelle controller affiché")]
    public InputDeviceCharacteristics controllerCharacteristics;
    [Tooltip("Liste des models de controllers à affiché")]
    public List<GameObject> controllerPrefabs;
    [Tooltip("Référence vers le models des mains")]
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;
    // Start is called before the first frame update
    void TryInitialise()
    {
        List<InputDevice> devices = new List<InputDevice>(); //On récupère les infos de notre casque (oculus, htc vive etc)
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics,devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0) //Si on trouve plusieurs controllers alors on affiche leurs models
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if(prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Controller not found");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }
    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) //Si on utiliser le trigger alors on joue l'animation
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger",0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) //Si on utiliser le grip alors on joue l'animation
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip",0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialise();
        }
        else
        {
            if(showController) //Si on a décidé d'affiché les controlleurs alors on cache les mains
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else //Sinon les controlleurs sont cachés
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
