using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XPinZhiXiShu")]
	[Serializable]
	public class XPinZhiXiShu : IExtensible
	{
		private int _quality;

		private int _attrNum;

		private readonly List<int> _numProbability = new List<int>();

		private float _qualityValue;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "quality", DataFormat = DataFormat.TwosComplement)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "attrNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrNum
		{
			get
			{
				return this._attrNum;
			}
			set
			{
				this._attrNum = value;
			}
		}

		[ProtoMember(4, Name = "numProbability", DataFormat = DataFormat.TwosComplement)]
		public List<int> numProbability
		{
			get
			{
				return this._numProbability;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "qualityValue", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float qualityValue
		{
			get
			{
				return this._qualityValue;
			}
			set
			{
				this._qualityValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
