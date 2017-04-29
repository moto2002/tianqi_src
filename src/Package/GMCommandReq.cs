using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1331), ForSend(1331), ProtoContract(Name = "GMCommandReq")]
	[Serializable]
	public class GMCommandReq : IExtensible
	{
		public static readonly short OP = 1331;

		private int _sequent;

		private string _content;

		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sequent", DataFormat = DataFormat.TwosComplement)]
		public int sequent
		{
			get
			{
				return this._sequent;
			}
			set
			{
				this._sequent = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "content", DataFormat = DataFormat.Default)]
		public string content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
