using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TongGuanDiaoLuo")]
	[Serializable]
	public class TongGuanDiaoLuo : IExtensible
	{
		private int _id;

		private readonly List<int> _lv = new List<int>();

		private int _ruleId;

		private readonly List<int> _ItemIds = new List<int>();

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

		[ProtoMember(3, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public List<int> lv
		{
			get
			{
				return this._lv;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ruleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, Name = "ItemIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> ItemIds
		{
			get
			{
				return this._ItemIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
