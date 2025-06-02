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
    //[SerializeField] private Transform cameraPivot;
    //[SerializeField] private float mouseSensitivity = 2f;

    //private float yaw;
    //private float pitch;

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

        //yaw = transform.eulerAngles.y;
        //pitch = 10f;
    }

    protected override void Update()
    {
        if (IsDead) return;

        if (input != null && cameraManager != null)
        {
            cameraManager.SetAiming(input.AimPressed);
        }

        GetInput();
        HandleStateInput();
        base.Update();
        UpdateAnimator();
        Debug.Log("InputManager 업데이트 중");
    }

    private void OnDisable()
    {
        OnDeath -= HandleDeath;
        OnRevive -= HandleRevive;
    }

    private void LateUpdate()
    {
        //HandleCameraRotation();
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
    public void Stagger(float duration)
    {
        SetState(new StaggerState(this));
    }

    //private void HandleCameraRotation()
    //{
    //    
    //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
    //
    //    yaw += mouseX;
    //    pitch -= mouseY;
    //    pitch = Mathf.Clamp(pitch, -30f, 70f); 
    //
    //    // 카메라 피벗 회전
    //    cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0f);
    //
    //    
    //    if (isAiming)
    //    {
    //        Vector3 lookDir = cameraPivot.forward;
    //        lookDir.y = 0f;
    //        if (lookDir.sqrMagnitude > 0.01f)
    //        {
    //            Quaternion targetRot = Quaternion.LookRotation(lookDir);
    //            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 12f);
    //        }
    //    }
    //}

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // 데미지 처리
        TakeDamage(damage);

        // 피격 이펙트 등 처리 필요시
        Debug.Log($"플레이어가 피격됨! 위치: {hitPoint}, 피해량: {damage}");
    }

}
