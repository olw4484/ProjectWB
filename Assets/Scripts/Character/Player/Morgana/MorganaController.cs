using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class MorganaController : BasePlayerController
{
    [Header("������")]
    public MorganaData data; // ScriptableObject ����

    [Header("����")]
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

        // ȸ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Dodge");
        }

        // ����
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("AttackTrigger");
        }

        // ����
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
