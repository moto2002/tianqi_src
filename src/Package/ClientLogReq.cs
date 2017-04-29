using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1333), ForSend(1333), ProtoContract(Name = "ClientLogReq")]
	[Serializable]
	public class ClientLogReq : IExtensible
	{
		public static readonly short OP = 1333;

		private string _content;

		private int _level;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "content", DataFormat = DataFormat.Default)]
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

		[ProtoMember(2, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
