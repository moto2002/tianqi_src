using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Material")]
	[Serializable]
	public class Material : IExtensible
	{
		private int _id;

		private readonly List<int> _syntheticMaterialID = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, Name = "syntheticMaterialID", DataFormat = DataFormat.TwosComplement)]
		public List<int> syntheticMaterialID
		{
			get
			{
				return this._syntheticMaterialID;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
