using System;
using System.Collections.Generic;

namespace Spine
{
	public class AnimationStateData
	{
		internal SkeletonData skeletonData;

		private Dictionary<KeyValuePair<Animation, Animation>, float> animationToMixTime = new Dictionary<KeyValuePair<Animation, Animation>, float>();

		internal float defaultMix;

		public SkeletonData SkeletonData
		{
			get
			{
				return this.skeletonData;
			}
		}

		public float DefaultMix
		{
			get
			{
				return this.defaultMix;
			}
			set
			{
				this.defaultMix = value;
			}
		}

		public AnimationStateData(SkeletonData skeletonData)
		{
			this.skeletonData = skeletonData;
		}

		public void SetMix(string fromName, string toName, float duration)
		{
			Animation animation = this.skeletonData.FindAnimation(fromName);
			if (animation == null)
			{
				throw new ArgumentException("Animation not found: " + fromName);
			}
			Animation animation2 = this.skeletonData.FindAnimation(toName);
			if (animation2 == null)
			{
				throw new ArgumentException("Animation not found: " + toName);
			}
			this.SetMix(animation, animation2, duration);
		}

		public void SetMix(Animation from, Animation to, float duration)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from cannot be null.");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to cannot be null.");
			}
			KeyValuePair<Animation, Animation> keyValuePair = new KeyValuePair<Animation, Animation>(from, to);
			this.animationToMixTime.Remove(keyValuePair);
			this.animationToMixTime.Add(keyValuePair, duration);
		}

		public float GetMix(Animation from, Animation to)
		{
			KeyValuePair<Animation, Animation> keyValuePair = new KeyValuePair<Animation, Animation>(from, to);
			float result;
			if (this.animationToMixTime.TryGetValue(keyValuePair, ref result))
			{
				return result;
			}
			return this.defaultMix;
		}
	}
}
