using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    #region Fields
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset characterControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMap = "Character";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string spring = "Sprint";

    private InputAction moveAction;
    private InputAction springAction;
    #endregion

    #region Propertys
    public Vector2 MoveInput { get; private set; }
    public float SprintValue { get; private set; }
    public static CharacterInputHandler Instance { get; private set; }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = characterControls.FindActionMap(actionMap).FindAction(move);
        springAction = characterControls.FindActionMap(actionMap).FindAction(spring);
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        springAction.performed += context => SprintValue = context.ReadValue<float>(); ;
        springAction.canceled += context => SprintValue = 0f;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        springAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        springAction.Disable();
    }
    #endregion
}