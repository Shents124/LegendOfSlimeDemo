using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float angle;
    private Vector3 _direction;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var tanAngle = angle * Mathf.Deg2Rad;
        Fire(speed, tanAngle);
    }

    public void Init(Ashe attacker, Enemy target)
    {
        _direction = target.transform.position - attacker.transform.position;
        _direction.Normalize();
    }

    // private void Update()
    // {
    //     transform.position += _direction * (speed * Time.deltaTime);
    // }
    
    private void Fire(float v0, float angle)
    {
        float x = v0 * Time.deltaTime * Mathf.Cos(angle);
        float y = v0 * Time.deltaTime * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2);
        var force = new Vector3(x, y, 0);
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }
}