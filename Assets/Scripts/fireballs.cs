using UnityEngine;

public class fireballs : MonoBehaviour
{
    [SerializeField] private float apple_speed;
    private bool hit;
    private float direction;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        float movmentSpeed = apple_speed * Time.deltaTime * direction;
        transform.Translate(movmentSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        print("hit");
        animator.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localscaleX = transform.localScale.x;

        if (Mathf.Sign(localscaleX) != _direction)
        {
            localscaleX = -localscaleX;
        }

        transform.localScale = new Vector3(localscaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}