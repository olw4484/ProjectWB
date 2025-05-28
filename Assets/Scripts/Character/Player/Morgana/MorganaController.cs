using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class MorganaController : BasePlayerController
{
    [Header("데이터")]
    public MorganaData data; // ScriptableObject 연결

    [Header("상태")]
    private Animator animator;
    private CharacterController characterController;
    private Vector3 moveInput;
    private bool isAiming = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleInput();
        Move();
        UpdateAnimator();
    }

    private void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveInput = new Vector3(h, 0, v).normalized;

        // 회피
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Dodge");
        }

        // 공격
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("AttackTrigger");
        }

        // 조준
        isAiming = Input.GetMouseButton(1);
        animator.SetBool("IsAiming", isAiming);
    }

    private void Move()
    {
        Vector3 move = transform.forward * moveInput.z + transform.right * moveInput.x;
        characterController.SimpleMove(move * data.moveSpeed);
    }

    private void UpdateAnimator()
    {
        float moveSpeed = moveInput.magnitude;
        animator.SetFloat("MoveSpeed", moveSpeed);
    }
}
