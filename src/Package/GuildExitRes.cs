using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3671), ForSend(3671), ProtoContract(Name = "GuildExitRes")]
	[Serializable]
	public class GuildExitRes : IExtensible
	{
		public static readonly short OP = 3671;

		private int _contribution;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "contribution", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int contribution
		{
			get
			{
				return this._contribution;
			}
			set
			{
				this._contribution = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
