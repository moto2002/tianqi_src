using System;

namespace Spine
{
	public class BoneData
	{
		internal BoneData parent;

		internal string name;

		internal float length;

		internal float x;

		internal float y;

		internal float rotation;

		internal float scaleX = 1f;

		internal float scaleY = 1f;

		internal bool flipX;

		internal bool flipY;

		internal bool inheritScale = true;

		internal bool inheritRotation = true;

		public BoneData Parent
		{
			get
			{
				return this.parent;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public float Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		public float Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
			}
		}

		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		public float ScaleY
		{
			get
			{
				return this.scaleY;
			}
			set
			{
				this.scaleY = value;
			}
		}

		public bool FlipX
		{
			get
			{
				return this.flipX;
			}
			set
			{
				this.flipX = value;
			}
		}

		public bool FlipY
		{
			get
			{
				return this.flipY;
			}
			set
			{
				this.flipY = value;
			}
		}

		public bool InheritScale
		{
			get
			{
				return this.inheritScale;
			}
			set
			{
				this.inheritScale = value;
			}
		}

		public bool InheritRotation
		{
			get
			{
				return this.inheritRotation;
			}
			set
			{
				this.inheritRotation = value;
			}
		}

		public BoneData(string name, BoneData parent)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name cannot be null.");
			}
			this.name = name;
			this.parent = parent;
		}

		public override string ToString()
		{
			return this.name;
		}
	}
}
