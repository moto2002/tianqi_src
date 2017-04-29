using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_NewBatchNty")]
	[Serializable]
	public class BattleAction_NewBatchNty : IExtensible
	{
		private int _batch;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "batch", DataFormat = DataFormat.TwosComplement)]
		public int batch
		{
			get
			{
				return this._batch;
			}
			set
			{
				this._batch = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
