using System;

namespace Spine
{
	public class FlipYTimeline : FlipXTimeline
	{
		public FlipYTimeline(int frameCount) : base(frameCount)
		{
		}

		protected override void SetFlip(Bone bone, bool flip)
		{
			bone.flipY = flip;
		}
	}
}
