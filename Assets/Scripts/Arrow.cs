using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Rigidbody2D rigid2D;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void Init(Ashe attacker, Enemy target)
    {

    }

    private void Update()
    {
        var velocity = rigid2D.velocity;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("hit enemy");
    }
}