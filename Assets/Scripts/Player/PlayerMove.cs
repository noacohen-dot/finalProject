using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody2D rb;
    public Vector2 moveInput;
    Animator animator;
    InputSystem inputActions;
    SpriteRenderer sprite;
    private Vector2 lastMoveDirection;
    float significantMovementX = 0.01f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ValidateComponent(rb, nameof(rb));
       animator = GetComponent<Animator>();
        ValidateComponent(animator, nameof(animator));
        sprite = GetComponent<SpriteRenderer>();
        ValidateComponent(sprite, nameof(sprite));
    }
    private void ValidateComponent<T>(T component, string name)
    {
        if (component == null)
        {
            Debug.LogError($"{name} is null!");
        }
    }
    private void Awake()
    {
        inputActions = new();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerormed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnMovePerormed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>(); 
        animator.SetBool("IsWalking", true);
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
        if (moveInput.x > significantMovementX)
            sprite.flipX = false;
        else if (moveInput.x < -significantMovementX)
            sprite.flipX = true;
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput; 
        }
    }
    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveInput = Vector2.zero;
        animator.SetBool("IsWalking", false);
        animator.SetFloat("LastInputX", lastMoveDirection.x);
        animator.SetFloat("LastInputY", lastMoveDirection.y);
    }
   
    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= OnMovePerormed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
    }
}
