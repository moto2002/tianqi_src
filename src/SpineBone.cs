using Spine;
using System;

public class SpineBone : SpineAttributeBase
{
	public SpineBone(string startsWith = "", string dataField = "")
	{
		this.startsWith = startsWith;
		this.dataField = dataField;
	}

	public static Bone GetBone(string boneName, SkeletonRenderer renderer)
	{
		if (renderer.skeleton == null)
		{
			return null;
		}
		return renderer.skeleton.FindBone(boneName);
	}

	public static BoneData GetBoneData(string boneName, SkeletonDataAsset skeletonDataAsset)
	{
		SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(true);
		return skeletonData.FindBone(boneName);
	}
}
