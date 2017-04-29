using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(544), ForSend(544), ProtoContract(Name = "CreateRoleRes")]
	[Serializable]
	public class CreateRoleRes : IExtensible
	{
		public static readonly short OP = 544;

		private RoleBriefInfo _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "info", DataFormat = DataFormat.Default)]
		public RoleBriefInfo info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
