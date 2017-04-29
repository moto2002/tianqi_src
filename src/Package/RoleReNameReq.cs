using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(112), ForSend(112), ProtoContract(Name = "RoleReNameReq")]
	[Serializable]
	public class RoleReNameReq : IExtensible
	{
		public static readonly short OP = 112;

		private string _newName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "newName", DataFormat = DataFormat.Default)]
		public string newName
		{
			get
			{
				return this._newName;
			}
			set
			{
				this._newName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
