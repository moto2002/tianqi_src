using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(125), ForSend(125), ProtoContract(Name = "GuildWarStepNty")]
	[Serializable]
	public class GuildWarStepNty : IExtensible
	{
		public static readonly short OP = 125;

		private GuildWarTimeStep.GWTS _step;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "step", DataFormat = DataFormat.TwosComplement)]
		public GuildWarTimeStep.GWTS step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
