using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(40), ForSend(40), ProtoContract(Name = "ResultExperienceCopyNty")]
	[Serializable]
	public class ResultExperienceCopyNty : IExtensible
	{
		public static readonly short OP = 40;

		private bool _isWin;

		private readonly List<DropItem> _item = new List<DropItem>();

		private int _rewardBatch;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isWin", DataFormat = DataFormat.Default)]
		public bool isWin
		{
			get
			{
				return this._isWin;
			}
			set
			{
				this._isWin = value;
			}
		}

		[ProtoMember(2, Name = "item", DataFormat = DataFormat.Default)]
		public List<DropItem> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "rewardBatch", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardBatch
		{
			get
			{
				return this._rewardBatch;
			}
			set
			{
				this._rewardBatch = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
