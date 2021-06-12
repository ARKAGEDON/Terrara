using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    [Tooltip("Vitesse du joueur")]
    public float speed = 1f;
    [Tooltip("Controller qui agit le déplacement")]
    public XRNode inputSource;
    [Tooltip("Gravité exercé du joueur")]
    public float gravity = -9.81f;
    [Tooltip("Layer du sol pour gérer les collisions et le saut du joueur")]
    public LayerMask groundLayer;
    [Tooltip("Ajout de hauteur au joueur")]
    public float additionalHeight = 0.2f;

    private float fallingSpeed;
    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource); //On récupère les mouvement du joystick pour savoir où se déplacer
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate() {
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0); //Déplacement du joueur
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);

        //Gravité du joueur
        bool isGrounded = CheckIfGround();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        fallingSpeed += gravity * Time.fixedDeltaTime;
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Fonction pour que notre casque soit repéré dans l'espace (donc si on avance dans la pièce sans notre joystick notre perso avance..)
    /// </summary>
    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
    }

    /// <summary>
    /// Bool qui vérifie si le joueur touche le sol avec le layer ground
    /// </summary>
    /// <returns>True si le joueur touche le sol, False sinon</returns>
    bool CheckIfGround()
    {
        //tell us if on ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
