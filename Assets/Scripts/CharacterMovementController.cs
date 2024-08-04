using UnityEngine;


public class CharacterMovementController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterInputHandler inputHandler;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = CharacterInputHandler.Instance;
    }

    void Update()
    {
        if (inputHandler == null)
        {
            Debug.LogError("CharacterInputHandler instance not found!");
            return;
        }

        Vector2 input = inputHandler.MoveInput;
        float speed = inputHandler.SprintValue > 0 ? sprintSpeed : walkSpeed;

        Vector3 move = transform.right * input.x + transform.forward * input.y;
        moveDirection.x = move.x * speed;
        moveDirection.z = move.z * speed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
