using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(873), ForSend(873), ProtoContract(Name = "ChallengeBatchInfoNty")]
	[Serializable]
	public class ChallengeBatchInfoNty : IExtensible
	{
		public static readonly short OP = 873;

		private int _stage;

		private int _batch;

		private int _residueTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "stage", DataFormat = DataFormat.TwosComplement)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "batch", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "residueTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int residueTime
		{
			get
			{
				return this._residueTime;
			}
			set
			{
				this._residueTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
