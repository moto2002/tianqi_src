using System;
using XEngineActor;

public interface IReusePool
{
	void DestroyById(int guid, Actor actor);

	void DestroyByObj(int guid, Actor actor);

	void Clear();
}
