using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ClickRoleTime")]
	[Serializable]
	public class ClickRoleTime : IExtensible
	{
		private int _career;

		private int _times;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
