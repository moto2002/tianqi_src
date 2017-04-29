using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "zTaoZhuangPeiZhi")]
	[Serializable]
	public class zTaoZhuangPeiZhi : IExtensible
	{
		private int _groupId;

		private int _groupIcon;

		private readonly List<int> _startCondition = new List<int>();

		private readonly List<int> _effectType = new List<int>();

		private readonly List<int> _effectId = new List<int>();

		private readonly List<int> _effectValue = new List<int>();

		private readonly List<int> _depict = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "groupId", DataFormat = DataFormat.TwosComplement)]
		public int groupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				this._groupId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "groupIcon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int groupIcon
		{
			get
			{
				return this._groupIcon;
			}
			set
			{
				this._groupIcon = value;
			}
		}

		[ProtoMember(4, Name = "startCondition", DataFormat = DataFormat.TwosComplement)]
		public List<int> startCondition
		{
			get
			{
				return this._startCondition;
			}
		}

		[ProtoMember(5, Name = "effectType", DataFormat = DataFormat.TwosComplement)]
		public List<int> effectType
		{
			get
			{
				return this._effectType;
			}
		}

		[ProtoMember(6, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public List<int> effectId
		{
			get
			{
				return this._effectId;
			}
		}

		[ProtoMember(7, Name = "effectValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> effectValue
		{
			get
			{
				return this._effectValue;
			}
		}

		[ProtoMember(8, Name = "depict", DataFormat = DataFormat.TwosComplement)]
		public List<int> depict
		{
			get
			{
				return this._depict;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
