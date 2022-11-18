using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Arrow prefab;
    private ObjectPool<Arrow> _pool;
    
    // Start is called before the first frame update
    void Start()
    {
        _pool = new ObjectPool<Arrow>(CreateEnemy, OnTakeArrow, OnReturnArrow, null, false, 20);
    }

    public Arrow GetBullet()
    {
        return _pool.Get();
    }

    private void OnReturnArrow(Arrow obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnTakeArrow(Arrow obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Arrow CreateEnemy()
    {
        var arrow = Instantiate(prefab, transform, true);
        arrow.SetPool(_pool);
        return arrow;
    }
}