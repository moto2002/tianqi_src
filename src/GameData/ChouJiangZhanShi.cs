using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChouJiangZhanShi")]
	[Serializable]
	public class ChouJiangZhanShi : IExtensible
	{
		private int _id;

		private int _modelId;

		private int _explain;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "modelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "explain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int explain
		{
			get
			{
				return this._explain;
			}
			set
			{
				this._explain = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
