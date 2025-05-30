using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : BasePlayerController
{
    [Header("컴포넌트")]
    private Animator animator;
    private CharacterController characterController;
    public CharacterController CharacterController => characterController;

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

    [Header("장비 관련")]
    [SerializeField] private GameObject arrowPrefab;
    public GameObject ArrowPrefab => arrowPrefab;

    private GameObject bow;
    private Transform firePoint;
    public Transform FirePoint => firePoint;
    public GameObject Bow => bow;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        OnDeath += HandleDeath;
        OnRevive += HandleRevive;
        SetState(new IdleState(this));
    }

    protected override void Update()
    {
        if (IsDead) return;

        GetInput();
        HandleStateInput();
        base.Update();
        UpdateAnimator();
    }

    private void OnDisable()
    {
        OnDeath -= HandleDeath;
        OnRevive -= HandleRevive;
    }


    private void GetInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 targetInput = new Vector3(h, 0, v);

        currentInput = Vector3.Lerp(currentInput, targetInput, Time.deltaTime * 90f);
        moveInputRaw = currentInput;
        moveInput = currentInput.normalized;
        inputDir = new Vector2(h, v);

        isAiming = Input.GetMouseButton(1);
    }

    private void HandleStateInput()
    {
        if (IsDead) return;

        if (currentState is IdleState && isAiming)
        {
            SetState(new AimState(this));
        }
        else if (currentState is AimState && !isAiming)
        {
            SetState(new IdleState(this));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState(new DodgeState(this));
            return;
        }
    }

    public override void Move()
    {
        if (IsDead) return;

        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.z);

        if (Camera.main != null)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

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
        if (IsDead) return;

        float damping = 0.1f;
        float delta = Time.deltaTime;

        animator.SetFloat("d1", isAiming ? moveInput.magnitude : 0f, damping, delta);
        animator.SetFloat("MoveSpeed", moveInputRaw.magnitude, damping, delta);
        animator.SetBool("IsAiming", isAiming);
        animator.speed = moveInput.magnitude > 0.1f ? 1.4f : 1.0f;
    }

    public override void TakeDamage(float amount)
    {
        if (IsInvincible || IsDead) return;

        base.TakeDamage(amount);

        if (!IsDead)
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

    public void EquipBow()
    {
        if (bow != null) return;

        GameObject bowPrefab = Resources.Load<GameObject>("Bow");
        if (bowPrefab == null)
        {
            Debug.LogError("Bow 프리팹이 Resources/Bow에 없습니다.");
            return;
        }

        Transform leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        if (leftHand == null)
        {
            Debug.LogError("Left hand bone을 찾을 수 없습니다.");
            return;
        }

        bow = Instantiate(bowPrefab, leftHand);
        bow.transform.localPosition = Vector3.zero;
        bow.transform.localRotation = Quaternion.identity;

        firePoint = bow.transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogWarning("FirePoint 오브젝트를 Bow 내부에서 찾을 수 없습니다.");
        }
    }

    private void HandleDeath()
    {
        SetState(new DieState(this));
    }
    private void HandleRevive()
    {
        SetState(new IdleState(this));
        animator.Play("Idle");        
    }
}
