using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TeamJoinCD")]
	[Serializable]
	public class TeamJoinCD : IExtensible
	{
		private int _teamId;

		private int _cd;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int teamId
		{
			get
			{
				return this._teamId;
			}
			set
			{
				this._teamId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "cd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cd
		{
			get
			{
				return this._cd;
			}
			set
			{
				this._cd = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
