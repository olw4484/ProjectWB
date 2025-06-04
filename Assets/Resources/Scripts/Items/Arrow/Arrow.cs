using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody rb;

    private Vector3 shootDirection = Vector3.forward;

    public void SetDirection(Vector3 direction)
    {
        shootDirection = direction.normalized;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = shootDirection * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
