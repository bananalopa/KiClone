using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Kingdom
{
	public class GenericPool<T>  where T : MonoBehaviour, IPoolable
	{
		private ObjectPool<T> pool;
		private DiContainer container;
		private GameObject poolParent;
		protected GameObject prefab;
		
		protected GenericPool(DiContainer container)
		{
			this.container = container;
			poolParent = new GameObject(typeof(T).Name+"PoolParent");
			pool = new ObjectPool<T>(Create, null, null, OnDestroyPoolObject, true, 200, int.MaxValue);
		}
		
		protected virtual T Create()
		{
			var poolable = container.InstantiatePrefab(prefab).GetComponent<T>();
			poolable.transform.SetParent(poolParent.transform);
			poolable.name = typeof(T).Name+"PoolElement";
			return poolable;
		}
		
		public virtual T Get()
		{
			var poolable = pool.Get();
			poolable.Activate();
			return poolable;
		}
		
		public virtual void Release(T poolable)
		{
			poolable.Deactivate();
			poolable.transform.position = new Vector3(10000, 10000, 10000);
			pool.Release(poolable);
		}
		
		protected virtual void OnDestroyPoolObject(T poolable)
		{
			poolable.Destroy();
		}
	}
}