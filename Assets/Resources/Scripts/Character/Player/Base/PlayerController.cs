using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : BasePlayerController, IHitReceiver
{
    [Header("컴포넌트")]
    private Animator animator;
    private CharacterController characterController;
    public CharacterController CharacterController => characterController;
    [SerializeField] private InputManager input;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float mouseSensitivity = 2f;
    private float yaw;
    private float pitch;
    //[SerializeField] private Transform leftHandSlot;
    [SerializeField] private PlayerStatus playerStatus;

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

        if (playerStatus == null)
        {
            playerStatus = GetComponent<PlayerStatus>();
            if (playerStatus == null)
                Debug.LogError("PlayerStatus 컴포넌트를 찾을 수 없습니다.");
        }
    }

    protected override void Start()
    {
        base.Start();
        EquipBow();
        OnDeath += HandleDeath;
        OnRevive += HandleRevive;
        yaw = cameraPivot.eulerAngles.y;
        pitch = cameraPivot.eulerAngles.x;
        SetState(new IdleState(this));


    }

    protected override void Update()
    {
        if (IsDead) return;

        isAiming = playerStatus.IsAiming.Value;
        //cameraManager?.SetAiming(isAiming);

        
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

    private void LateUpdate()
    {
        HandleCameraRotation();
        CameraToChar();
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
        HandleAimingInput();
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

        if (currentState is AimState && Input.GetMouseButtonDown(0))
        {
            SetState(new DrawState(this));
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

            moveDir = (camForward * moveInput.z) + (camRight * moveInput.x);
        }

        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }

        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
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

        if (isAiming)
        {
            animator.SetFloat("Horizontal", inputDir.x, 0.1f, Time.deltaTime);
            animator.SetFloat("Vertical", inputDir.y, 0.1f, Time.deltaTime);
        }

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

        GameObject bowPrefab = Resources.Load<GameObject>("Prefabs/Items/Bow");
        if (bowPrefab == null)
        {
            Debug.LogError("Bow 프리팹이 없습니다.");
            return;
        }

        Transform hand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        bow = Instantiate(bowPrefab, hand);
        bow.transform.localPosition = new Vector3(-0.175f, 0.1f, -0.1f);
        bow.transform.localRotation = Quaternion.Euler(-24.446f, -89.878f, -26.257f);

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
    public void Stagger(float duration)
    {
        SetState(new StaggerState(this));
    }

    private void HandleCameraRotation()
    {
        if (!isAiming) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -40f, 60f); // 상하 제한

        cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void CameraToChar()
    {
        if (isAiming)
        {
            // 카메라가 바라보는 방향을 기준으로 캐릭터 회전
            Vector3 lookDirection = cameraPivot.forward;
            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 12f);
            }
        }

    }


    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // 데미지 처리
        TakeDamage(damage);

        // 피격 이펙트 등 처리 필요시
        Debug.Log($"플레이어가 피격됨! 위치: {hitPoint}, 피해량: {damage}");
    }

    public void FireArrow()
    {
        Debug.Log("FireArrow 호출됨");

        if (FirePoint == null || ArrowPrefab == null)
        {
            Debug.LogWarning("FirePoint 또는 ArrowPrefab이 설정되지 않았습니다.");
            return;
        }

        GameObject arrow = Instantiate(ArrowPrefab, FirePoint.position, FirePoint.rotation);
        Debug.Log("화살 인스턴스 생성됨");

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(FirePoint.forward * 30f, ForceMode.Impulse);
            Debug.Log("화살에 힘 가해짐");
        }
        else
        {
            Debug.LogWarning("화살 프리팹에 Rigidbody 없음");
        }
    }


    private void HandleAimingInput()
    {
        if (Input.GetMouseButtonDown(1))
            playerStatus.IsAiming.Value = true;

        if (Input.GetMouseButtonUp(1))
            playerStatus.IsAiming.Value = false;
    }

    public void OnDrawEnd()
    {
        Debug.Log("애니메이션 이벤트: Draw 끝 - Fire로 전환");
        SetState(new FireState(this));
    }

    public void OnFireMid()
    {
        Debug.Log("애니메이션 이벤트: 화살 발사!");
        FireArrow();
    }

    public void OnFireEnd()
    {
        Debug.Log("애니메이션 이벤트: Fire 끝 - Recover로 전환");
        SetState(new RecoverState(this));
    }

    public void OnRecoverEnd()
    {
        Debug.Log("애니메이션 이벤트: Recover 끝 - Aim 유지 또는 Idle로 전환");
        SetState(IsAiming ? new AimState(this) : new IdleState(this));
    }


}
