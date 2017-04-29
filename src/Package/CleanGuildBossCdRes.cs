using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(72), ForSend(72), ProtoContract(Name = "CleanGuildBossCdRes")]
	[Serializable]
	public class CleanGuildBossCdRes : IExtensible
	{
		public static readonly short OP = 72;

		private int _rmCleanTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "rmCleanTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rmCleanTimes
		{
			get
			{
				return this._rmCleanTimes;
			}
			set
			{
				this._rmCleanTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
