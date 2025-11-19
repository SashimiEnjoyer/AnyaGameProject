using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InGameInput : MonoBehaviour
{
    [SerializeField] InputActionAsset action;

    GameInput input;

    public UnityAction<float> onMovePressed;
    public UnityAction<float> onMoveStop;
    public UnityAction onSkill1Pressed;
    public UnityAction onSkill2Pressed;
    public UnityAction onSkill3Pressed;
    public UnityAction onJumpPressed;
    public UnityAction onAttackPressed;
    public UnityAction onInteractPressed;
    public UnityAction onPausePressed;


    private void OnDisable()
    {
        input.InGame.HorizontalMove.performed -= MoveInput;
        input.InGame.HorizontalMove.canceled -= MoveStop;
        input.InGame.Skill1.performed -= Skill1Input;
        input.InGame.Skill2.performed -= Skill2Input;
        input.InGame.Skill3.performed -= Skill3Input;
        input.InGame.Jump.performed -= JumpInput;
        input.InGame.Attack.performed -= AttackInput;
        input.InGame.Interact.performed -= InteractInput;
        input.InGame.PauseMenu.performed -= PauseInput;
        input.Disable();
    }

    public void Initialize()
    {
        input = new GameInput();

        input.Enable();

        input.InGame.HorizontalMove.performed += MoveInput;
        input.InGame.HorizontalMove.canceled += MoveStop;
        input.InGame.Skill1.performed += Skill1Input;
        input.InGame.Skill2.performed += Skill2Input;
        input.InGame.Skill3.performed += Skill3Input;
        input.InGame.Jump.performed += JumpInput;
        input.InGame.Attack.performed += AttackInput;
        input.InGame.Interact.performed += InteractInput;
        input.InGame.PauseMenu.performed += PauseInput;
    }

    public void SetInputActive(bool isActive)
    {
        if (isActive)
            input.Enable();
        else
            input.Disable();
    }

    void MoveInput(InputAction.CallbackContext ctx){  onMovePressed?.Invoke(ctx.ReadValue<float>()); }
    void MoveStop(InputAction.CallbackContext ctx) { onMoveStop?.Invoke(0); }
    void Skill1Input(InputAction.CallbackContext ctx) { onSkill1Pressed?.Invoke(); }
    void Skill2Input(InputAction.CallbackContext ctx) { onSkill2Pressed?.Invoke(); }
    void Skill3Input(InputAction.CallbackContext ctx) { onSkill3Pressed?.Invoke(); }
    void JumpInput(InputAction.CallbackContext ctx) { onJumpPressed?.Invoke(); }
    void AttackInput(InputAction.CallbackContext ctx) { onAttackPressed?.Invoke(); }
    void InteractInput(InputAction.CallbackContext ctx) { onInteractPressed?.Invoke(); }
    void PauseInput(InputAction.CallbackContext ctx) { onPausePressed?.Invoke(); }
}
