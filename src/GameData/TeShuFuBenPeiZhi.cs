using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TeShuFuBenPeiZhi")]
	[Serializable]
	public class TeShuFuBenPeiZhi : IExtensible
	{
		private int _id;

		private int _minLv;

		private readonly List<int> _ruleIdForWin = new List<int>();

		private readonly List<int> _ruleIdForLose = new List<int>();

		private int _ruleId;

		private int _picture;

		private readonly List<int> _ItemIds = new List<int>();

		private readonly List<int> _ItemNum = new List<int>();

		private int _descId;

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

		[ProtoMember(3, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(4, Name = "ruleIdForWin", DataFormat = DataFormat.TwosComplement)]
		public List<int> ruleIdForWin
		{
			get
			{
				return this._ruleIdForWin;
			}
		}

		[ProtoMember(5, Name = "ruleIdForLose", DataFormat = DataFormat.TwosComplement)]
		public List<int> ruleIdForLose
		{
			get
			{
				return this._ruleIdForLose;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "ruleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "picture", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		[ProtoMember(8, Name = "ItemIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> ItemIds
		{
			get
			{
				return this._ItemIds;
			}
		}

		[ProtoMember(9, Name = "ItemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> ItemNum
		{
			get
			{
				return this._ItemNum;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "descId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int descId
		{
			get
			{
				return this._descId;
			}
			set
			{
				this._descId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
