using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "SuiJiGuaiWuKu")]
	[Serializable]
	public class SuiJiGuaiWuKu : IExtensible
	{
		private int _librariesId;

		private int _monsterId;

		private int _probability;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "librariesId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int librariesId
		{
			get
			{
				return this._librariesId;
			}
			set
			{
				this._librariesId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "monsterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterId
		{
			get
			{
				return this._monsterId;
			}
			set
			{
				this._monsterId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
