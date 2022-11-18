using UnityEngine.Pool;

public interface IPool
{
    ObjectPool<IPool> Pool { get; set; }
    void SetPool(IPool pool);
}