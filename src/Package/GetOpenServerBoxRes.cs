using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7024), ForSend(7024), ProtoContract(Name = "GetOpenServerBoxRes")]
	[Serializable]
	public class GetOpenServerBoxRes : IExtensible
	{
		public static readonly short OP = 7024;

		private int _boxFlag = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "boxFlag", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int boxFlag
		{
			get
			{
				return this._boxFlag;
			}
			set
			{
				this._boxFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
