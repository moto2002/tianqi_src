using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Runes_template")]
	[Serializable]
	public class Runes_template : IExtensible
	{
		private int _id;

		private int _skillExtendId;

		private readonly List<int> _damageIncreaseId = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "skillExtendId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillExtendId
		{
			get
			{
				return this._skillExtendId;
			}
			set
			{
				this._skillExtendId = value;
			}
		}

		[ProtoMember(4, Name = "damageIncreaseId", DataFormat = DataFormat.TwosComplement)]
		public List<int> damageIncreaseId
		{
			get
			{
				return this._damageIncreaseId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
