using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "zBuWeiQiangHua")]
	[Serializable]
	public class zBuWeiQiangHua : IExtensible
	{
		private int _partLv;

		private readonly List<int> _coinType = new List<int>();

		private readonly List<int> _cost = new List<int>();

		private int _openStep;

		private int _proficiency;

		private readonly List<int> _proficiencyInterval = new List<int>();

		private int _position;

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

		[ProtoMember(5, IsRequired = true, Name = "openStep", DataFormat = DataFormat.TwosComplement)]
		public int openStep
		{
			get
			{
				return this._openStep;
			}
			set
			{
				this._openStep = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "proficiency", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, Name = "proficiencyInterval", DataFormat = DataFormat.TwosComplement)]
		public List<int> proficiencyInterval
		{
			get
			{
				return this._proficiencyInterval;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
