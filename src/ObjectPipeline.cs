using System;

public abstract class ObjectPipeline : IPipeline
{
	public abstract string GetPath(int guid);
}
