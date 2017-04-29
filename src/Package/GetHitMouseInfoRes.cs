using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5832), ForSend(5832), ProtoContract(Name = "GetHitMouseInfoRes")]
	[Serializable]
	public class GetHitMouseInfoRes : IExtensible
	{
		public static readonly short OP = 5832;

		private int _hadTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hadTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hadTimes
		{
			get
			{
				return this._hadTimes;
			}
			set
			{
				this._hadTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
