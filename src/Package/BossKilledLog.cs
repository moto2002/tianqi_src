using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BossKilledLog")]
	[Serializable]
	public class BossKilledLog : IExtensible
	{
		private int _dateTimeSec;

		private string _roleName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dateTimeSec", DataFormat = DataFormat.TwosComplement)]
		public int dateTimeSec
		{
			get
			{
				return this._dateTimeSec;
			}
			set
			{
				this._dateTimeSec = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "roleName", DataFormat = DataFormat.Default)]
		public string roleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
