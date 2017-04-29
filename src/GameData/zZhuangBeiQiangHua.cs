using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "zZhuangBeiQiangHua")]
	[Serializable]
	public class zZhuangBeiQiangHua : IExtensible
	{
		private int _partLv;

		private readonly List<int> _coinType = new List<int>();

		private readonly List<int> _cost = new List<int>();

		private int _proficiency;

		private readonly List<int> _proficiencyInterval = new List<int>();

		private int _openLv;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "partLv", DataFormat = DataFormat.TwosComplement)]
		public int partLv
		{
			get
			{
				return this._partLv;
			}
			set
			{
				this._partLv = value;
			}
		}

		[ProtoMember(3, Name = "coinType", DataFormat = DataFormat.TwosComplement)]
		public List<int> coinType
		{
			get
			{
				return this._coinType;
			}
		}

		[ProtoMember(4, Name = "cost", DataFormat = DataFormat.TwosComplement)]
		public List<int> cost
		{
			get
			{
				return this._cost;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "proficiency", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int proficiency
		{
			get
			{
				return this._proficiency;
			}
			set
			{
				this._proficiency = value;
			}
		}

		[ProtoMember(6, Name = "proficiencyInterval", DataFormat = DataFormat.TwosComplement)]
		public List<int> proficiencyInterval
		{
			get
			{
				return this._proficiencyInterval;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "openLv", DataFormat = DataFormat.TwosComplement)]
		public int openLv
		{
			get
			{
				return this._openLv;
			}
			set
			{
				this._openLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
