using Spine;
using System;

public interface ISkeletonAnimation
{
	event UpdateBonesDelegate UpdateLocal;

	event UpdateBonesDelegate UpdateWorld;

	event UpdateBonesDelegate UpdateComplete;

	Skeleton Skeleton
	{
		get;
	}

	void LateUpdate();
}
