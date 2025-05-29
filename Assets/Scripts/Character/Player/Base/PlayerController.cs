using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : BasePlayerController
{
    [Header("������Ʈ")]
    private Animator animator;
    private CharacterController characterController;

    [Header("Ȱ ����")]
    [SerializeField] private Transform firePoint;
    public Transform FirePoint => firePoint;

    [Header("�̵� ����")]
    [SerializeField] private float moveSpeed = 3.0f;
    private Vector2 inputDir;
    private Vector3 currentInput;
    private Vector3 moveInput;
    public override Vector2 InputDir => inputDir;
    public Vector3 MoveInput => moveInput;

    [Header("���� ����")]
    private bool isAiming = false;
    public bool IsAiming => isAiming;

    public Animator Animator => animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        SetState(new IdleState(this)); // �ʱ� ����
    }

    protected override void Update()
    {
        GetInput();         // �Է� ó��
        HandleStateInput(); // ���� ��ȯ ���� ó��
        base.Update();      // ���� Update ȣ��
        UpdateAnimator();   // �ִϸ����� �Ķ���� ����
    }

    private void GetInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 targetInput = new Vector3(h, 0, v);
        currentInput = Vector3.Lerp(currentInput, targetInput, Time.deltaTime * 90f);
        moveInput = currentInput.normalized;
        inputDir = new Vector2(h, v);

        Debug.Log($"[�Է�] H: {h}, V: {v}");

        // ���콺 ��Ŭ�� �� ����
        isAiming = Input.GetMouseButton(1);
    }

    private void HandleStateInput()
    {
        if (currentState is IdleState && isAiming)
        {
            SetState(new AimState(this));
        }
        else if (currentState is AimState && !isAiming)
        {
            SetState(new IdleState(this));
        }

        // ȸ�� �Է� ���� (����)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState(new DodgeState(this));
            return;
        }

        // �߻� �Է��� DrawState / AimState ���ο��� ó��
    }

    public override void Move()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.z);

        // ī�޶� �������� ���� ����
        if (Camera.main != null)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            // �Է��� ī�޶� �������� ��ȯ
            moveDir = camForward * moveInput.z + camRight * moveInput.x;
        }

        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }

    }


    private void UpdateAnimator()
    {
        float damping = 0.1f;
        float delta = Time.deltaTime;

        //animator.SetFloat("Horizontal", moveInput.x, damping, delta);
        //animator.SetFloat("Vertical", moveInput.z, damping, delta);
        animator.SetFloat("MoveSpeed", moveInput.magnitude, damping, delta);
        animator.SetBool("IsAiming", isAiming);

        if (moveInput.magnitude > 0.1f)
        {
            animator.speed = 1.4f; 
        }
        else
        {
            animator.speed = 1.0f; 
        }
    }

    public override void TakeDamage(float amount)
    {
        if (IsInvincible || isDead) return;

        base.TakeDamage(amount);

        if (!isDead)
        {
            SetState(new StaggerState(this));
        }
    }


    public bool IsInvincible { get; private set; }

    public void SetInvincible(float duration)
    {
        IsInvincible = true;
        StartCoroutine(DisableInvincibilityAfter(duration));
    }

    private IEnumerator DisableInvincibilityAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        IsInvincible = false;
    }
}
