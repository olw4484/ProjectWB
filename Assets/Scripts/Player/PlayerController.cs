using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("ÂüÁ¶")]
    public PlayerData playerData;
    public Animator animator;
    public Transform cameraTransform;
    public GameObject arrowPrefab;
    public Transform shootPoint;

    private CharacterController controller;
    private Vector3 moveDir;
    private bool isDodging;
    private float dodgeDuration = 0.5f;
    private float dodgeTimer = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameManager.Instance.IsPaused) return;

        HandleMovement();
        HandleDodge();
        HandleShoot();
    }

    void HandleMovement()
    {
        if (isDodging) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDir = new Vector3(h, 0, v).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            Vector3 moveWorld = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * moveDir;
            controller.Move(moveWorld * playerData.moveSpeed * Time.deltaTime);
            transform.forward = moveWorld; 

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void HandleDodge()
    {
        if (isDodging)
        {
            dodgeTimer += Time.deltaTime;
            if (dodgeTimer >= dodgeDuration)
            {
                isDodging = false;
                animator.SetBool("isDodging", false);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerData.CanDodge())
        {
            isDodging = true;
            dodgeTimer = 0f;
            playerData.UseStamina(playerData.dodgeCost);

            animator.SetBool("isDodging", true);

            Vector3 dodgeDir = transform.forward;
            controller.Move(dodgeDir * 10f * Time.deltaTime); 
        }
    }

    void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("shoot");
            ShootArrow();
        }
    }

    void ShootArrow()
    {
        if (arrowPrefab == null || shootPoint == null) return;

        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * 30f;
    }
}
