using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(761), ForSend(761), ProtoContract(Name = "LookUpBuddyInfoReq")]
	[Serializable]
	public class LookUpBuddyInfoReq : IExtensible
	{
		public static readonly short OP = 761;

		private string _name;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
