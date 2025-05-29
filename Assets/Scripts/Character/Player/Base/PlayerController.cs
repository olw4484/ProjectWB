using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : BasePlayerController
{
    [Header("컴포넌트")]
    private Animator animator;
    private CharacterController characterController;

    [Header("활 관련")]
    [SerializeField] private Transform firePoint;

    [Header("공격 관련")]
    [SerializeField] private GameObject arrowPrefab;
    public GameObject ArrowPrefab => arrowPrefab;
    public Transform FirePoint => firePoint;

    [Header("이동 관련")]
    [SerializeField] private float moveSpeed = 3.0f;
    private Vector2 inputDir;
    private Vector3 currentInput;
    private Vector3 moveInput;
    private Vector3 moveInputRaw;
    public override Vector2 InputDir => inputDir;
    public Vector3 MoveInput => moveInput;

    [Header("상태 관련")]
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
        SetState(new IdleState(this)); // 초기 상태
    }

    protected override void Update()
    {
        GetInput();         // 입력 처리
        HandleStateInput(); // 상태 전환 조건 처리
        base.Update();      // 상태 Update 호출
        UpdateAnimator();   // 애니메이터 파라미터 갱신
    }


    private void GetInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 targetInput = new Vector3(h, 0, v);

        currentInput = Vector3.Lerp(currentInput, targetInput, Time.deltaTime * 40f);
        moveInputRaw = currentInput;                     
        moveInput = currentInput.normalized;             
        inputDir = new Vector2(h, v);

        Debug.Log($"[입력] H: {h}, V: {v}");
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

        // 회피 입력 예시 (공용)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState(new DodgeState(this));
            return;
        }

        // 발사 입력은 DrawState / AimState 내부에서 처리
    }

    public override void Move()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.z);

        // 카메라 기준으로 방향 보정
        if (Camera.main != null)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            // 입력을 카메라 방향으로 변환
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

        // Aim용 파라미터 (d1)
        animator.SetFloat("d1", isAiming ? moveInput.magnitude : 0f, damping, delta);

        // 일반 이동용 파라미터 (MoveSpeed)
        animator.SetFloat("MoveSpeed", moveInputRaw.magnitude, damping, delta);

        // 조준 상태
        animator.SetBool("IsAiming", isAiming);

        // 애니메이션 속도 조절
        animator.speed = moveInput.magnitude > 0.1f ? 1.4f : 1.0f;

        Debug.Log($"[Animator] MoveSpeed = {moveInput.magnitude}");

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
