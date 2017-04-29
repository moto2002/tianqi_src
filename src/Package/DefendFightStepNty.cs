using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(765), ForSend(765), ProtoContract(Name = "DefendFightStepNty")]
	[Serializable]
	public class DefendFightStepNty : IExtensible
	{
		public static readonly short OP = 765;

		private DefendFightStep.DFS _step;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "step", DataFormat = DataFormat.TwosComplement)]
		public DefendFightStep.DFS step
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
