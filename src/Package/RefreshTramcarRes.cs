using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5730), ForSend(5730), ProtoContract(Name = "RefreshTramcarRes")]
	[Serializable]
	public class RefreshTramcarRes : IExtensible
	{
		public static readonly short OP = 5730;

		private int _curQuality;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "curQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curQuality
		{
			get
			{
				return this._curQuality;
			}
			set
			{
				this._curQuality = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
