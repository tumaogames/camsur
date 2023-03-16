using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using TMPro;

public class IsometricPlayerMovementController : MonoBehaviourPun
{
    public PhotonView view;
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    public Camera cam;
    public Vector2 movement;

    Rigidbody2D rbody;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine && !NPC.OpenDialogueBox)
        {
            Vector2 currentPos = rbody.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            movement = inputVector * movementSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isoRenderer.SetDirection(movement);
            if (!ChatReturnInput.Instance.typing)
            {
                rbody.MovePosition(newPos);
            }
        }
    }
}
