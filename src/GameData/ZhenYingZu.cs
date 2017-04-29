using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhenYingZu")]
	[Serializable]
	public class ZhenYingZu : IExtensible
	{
		private int _Id;

		private int _Group;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Group", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Group
		{
			get
			{
				return this._Group;
			}
			set
			{
				this._Group = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
