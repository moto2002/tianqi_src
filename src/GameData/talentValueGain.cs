using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "talentValueGain")]
	[Serializable]
	public class talentValueGain : IExtensible
	{
		private int _minLv;

		private int _step;

		private int _talentValue;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "minLv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "step", DataFormat = DataFormat.TwosComplement)]
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "talentValue", DataFormat = DataFormat.TwosComplement)]
		public int talentValue
		{
			get
			{
				return this._talentValue;
			}
			set
			{
				this._talentValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
