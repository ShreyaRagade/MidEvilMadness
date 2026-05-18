using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;

    private InputAction menuOpenCloseAction;

    private InputAction navigateMenuAction;

    public bool MenuOpenCloseInput { get; private set; }

    public bool NavigateMenuInput { get; private set; }

    public static InputManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = FindFirstObjectByType<PlayerInput>();
        menuOpenCloseAction = playerInput.actions["MenuOpenClose"];

        // navigateMenuAction = playerInput.actions["Navigate"];

    }

    private void Update()
    {
        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();

        // NavigateMenuInput = navigateMenuAction.WasPressedThisFrame();


    }
}
