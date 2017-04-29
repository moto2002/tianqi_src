using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MemoryFlopRankInfo")]
	[Serializable]
	public class MemoryFlopRankInfo : IExtensible
	{
		private long _id;

		private string _name;

		private int _passTime;

		private int _challengeTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
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

		[ProtoMember(3, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "passTime", DataFormat = DataFormat.TwosComplement)]
		public int passTime
		{
			get
			{
				return this._passTime;
			}
			set
			{
				this._passTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "challengeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int challengeTime
		{
			get
			{
				return this._challengeTime;
			}
			set
			{
				this._challengeTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
