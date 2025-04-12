using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    
    private float attackTimer = Mathf.Infinity;
    private PlayerMovment playermov;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playermov = GetComponent<PlayerMovment>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && attackTimer > attackCooldown && playermov.canAttack())
        {
            //print(playermov.canAttack());
            Attack();
        }

        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        attackTimer = 0;

        fireballs[findFireball()].transform.position = firepoint.position;
        fireballs[findFireball()].GetComponent<fireballs>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
