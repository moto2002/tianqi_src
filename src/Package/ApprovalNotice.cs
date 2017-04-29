using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(631), ForSend(631), ProtoContract(Name = "ApprovalNotice")]
	[Serializable]
	public class ApprovalNotice : IExtensible
	{
		public static readonly short OP = 631;

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
