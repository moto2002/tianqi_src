using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "MFKu")]
	[Serializable]
	public class MFKu : IExtensible
	{
		private int _id;

		private int _mf;

		private int _ruleId;

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

		[ProtoMember(2, IsRequired = true, Name = "mf", DataFormat = DataFormat.TwosComplement)]
		public int mf
		{
			get
			{
				return this._mf;
			}
			set
			{
				this._mf = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "ruleId", DataFormat = DataFormat.TwosComplement)]
		public int ruleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
