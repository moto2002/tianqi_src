using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "HSV")]
	[Serializable]
	public class HSV : IExtensible
	{
		private int _id;

		private readonly List<float> _colour = new List<float>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(4, Name = "colour", DataFormat = DataFormat.FixedSize)]
		public List<float> colour
		{
			get
			{
				return this._colour;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
