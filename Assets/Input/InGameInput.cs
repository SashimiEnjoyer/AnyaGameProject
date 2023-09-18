using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InGameInput : MonoBehaviour
{
    [SerializeField] InputActionAsset action;
    public static InGameInput instance;
    GameInput input;

    public UnityAction<float> onMovePressed;
    public UnityAction<float> onMoveStop;
    public UnityAction onSkill1Pressed;
    public UnityAction onSkill2Pressed;
    public UnityAction onSkill3Pressed;
    public UnityAction onJumpPressed;
    public UnityAction onAttackPressed;
    public UnityAction onInteractPressed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        input = new GameInput();
    }

    private void OnEnable()
    {
        input.Enable();

        input.InGame.HorizontalMove.performed += MoveInput;
        input.InGame.HorizontalMove.canceled += MoveStop;
        input.InGame.Skill1.performed += Skill1Input;
        input.InGame.Skill2.performed += Skill2Input;
        input.InGame.Skill3.performed += Skill3Input;
        input.InGame.Jump.performed += JumpInput;
        input.InGame.Attack.performed += AttackInput;
        input.InGame.Interact.performed += InteractInput;
    }


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
        input.Disable();
    }

    //[ContextMenu("Check Map")]
    //public void ChangeInputMap()
    //{
    //    Debug.Log(action.actionMaps[0][]);
    //}

    void MoveInput(InputAction.CallbackContext ctx){  onMovePressed?.Invoke(ctx.ReadValue<float>()); }
    void MoveStop(InputAction.CallbackContext ctx) { onMoveStop?.Invoke(0); }
    void Skill1Input(InputAction.CallbackContext ctx) { onSkill1Pressed?.Invoke(); }
    void Skill2Input(InputAction.CallbackContext ctx) { onSkill2Pressed?.Invoke(); }
    void Skill3Input(InputAction.CallbackContext ctx) { onSkill3Pressed?.Invoke(); }
    void JumpInput(InputAction.CallbackContext ctx) { onJumpPressed?.Invoke(); }
    void AttackInput(InputAction.CallbackContext ctx) { onAttackPressed?.Invoke(); } 
    void InteractInput(InputAction.CallbackContext ctx) { onInteractPressed?.Invoke(); }
}
