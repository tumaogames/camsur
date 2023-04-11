using UnityEngine;
using Photon.Pun;

public class IsometricPlayerMovementController : MonoBehaviourPun
{
    public PhotonView view;
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    public Camera cam;
    public Vector2 movement;
    public bool isOnAndroid;
    float horizontalInput;
    float verticalInput;
    public SimpleTouchController rightController;

    Rigidbody2D rbody;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (Application.platform == RuntimePlatform.Android)
        {
            isOnAndroid = true;
            rightController = GameObject.Find("SimpleTouch Joystick").GetComponent<SimpleTouchController>();
        }
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
            if (!isOnAndroid)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            else
            {
                horizontalInput = rightController.GetTouchPosition.x;
                verticalInput = rightController.GetTouchPosition.y;
            }
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            movement = inputVector * movementSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isoRenderer.SetDirection(movement);
            if (!ChatManager.Instance.typing)
            {
                rbody.MovePosition(newPos);
            }
        }
    }
}
