using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;
using XEngineActor;

public abstract class ActorPool<T> : IReusePool where T : Actor
{
	protected ObjectPool m_pool;

	public IPipeline pipeline;

	private List<T> Pools = new List<T>();

	public GameObject root;

	public ActorPool()
	{
		this.SetPipeline();
		this.root = new GameObject(base.GetType().get_Name());
		Object.DontDestroyOnLoad(this.root);
		if (this.root.get_name().Equals("ModelPool"))
		{
			this.root.get_transform().set_position(ModelDisplayManager.ModelPoolPosition);
		}
		else
		{
			this.root.get_transform().set_position(Vector3.get_zero());
		}
		this.root.get_transform().set_rotation(Quaternion.get_identity());
		this.root.get_transform().set_localScale(Vector3.get_one());
		this.GetPool().SetRootNode(this.root.get_transform());
	}

	protected abstract void SetPipeline();

	protected virtual ObjectPool GetPool()
	{
		if (this.m_pool == null)
		{
			this.m_pool = new ObjectPool();
		}
		return this.m_pool;
	}

	public T Get(int guid)
	{
		string path = this.pipeline.GetPath(guid);
		if (path == null)
		{
			return (T)((object)null);
		}
		GameObject gameObject = this.GetPool().Get(path);
		if (gameObject == null)
		{
			Debug.LogError(string.Format("马上联系左总，m_loader.Get拿到空值，路径是{0}", path));
			return (T)((object)null);
		}
		return this.SetGameObject(gameObject, guid);
	}

	public int GetAsync(int guid, Action<T> callback)
	{
		string path = this.pipeline.GetPath(guid);
		if (path == null)
		{
			return 0;
		}
		if (callback == null)
		{
			return 0;
		}
		return this.GetPool().Get(path, delegate(GameObject obj)
		{
			if (obj == null)
			{
				Debug.LogError(string.Format("马上联系左总，m_loader.Get拿到空值，路径是{0}", path));
				return;
			}
			callback.Invoke(this.SetGameObject(obj, guid));
		});
	}

	public void DestroyById(int gid, Actor actor)
	{
		if (actor != null)
		{
			this.Pools.Remove(actor as T);
		}
		this.GetPool().PoolRecycle(gid);
	}

	public void DestroyByObj(int gid, Actor actor)
	{
		if (actor != null)
		{
			this.Pools.Remove(actor as T);
			this.GetPool().PoolRecycle(actor.get_gameObject());
		}
	}

	public virtual void Clear()
	{
		this.GetPool().Clear();
	}

	protected T SetGameObject(GameObject obj, int guid)
	{
		if (obj.get_transform().get_parent() != this.root)
		{
			obj.get_transform().set_parent(this.root.get_transform());
		}
		obj.SetActive(false);
		T t = obj.AddUniqueComponent<T>();
		t.resGUID = guid;
		t.InstanceID = t.GetInstanceID();
		t.GameObjectID = obj.GetInstanceID();
		t.pool = this;
		this.Pools.Add(t);
		if (!(t is ActorFX))
		{
			if (!(t is ActorFXSpine))
			{
				obj.SetActive(true);
			}
		}
		if (t is ActorParent && DataReader<AvatarModel>.Contains(guid))
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(guid);
			obj.get_transform().set_localScale(Vector3.get_one() * avatarModel.scale);
			obj.get_transform().set_localPosition(Vector3.get_zero());
			obj.GetComponentsInChildren<Animator>(true)[0].set_cullingMode((avatarModel.alwaysVisible <= 0) ? 1 : 0);
			(t as ActorParent).IsLockModelDir = (avatarModel.lockDirection == 1);
		}
		return t;
	}
}
