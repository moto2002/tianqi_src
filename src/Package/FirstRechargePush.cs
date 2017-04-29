using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2298), ForSend(2298), ProtoContract(Name = "FirstRechargePush")]
	[Serializable]
	public class FirstRechargePush : IExtensible
	{
		public static readonly short OP = 2298;

		private int _flag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "flag", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flag
		{
			get
			{
				return this._flag;
			}
			set
			{
				this._flag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
