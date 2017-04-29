using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1174), ForSend(1174), ProtoContract(Name = "ElementCopyBattleResultNty")]
	[Serializable]
	public class ElementCopyBattleResultNty : IExtensible
	{
		public static readonly short OP = 1174;

		private bool _result;

		private readonly List<CopyReward> _copyRewards = new List<CopyReward>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "result", DataFormat = DataFormat.Default)]
		public bool result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		[ProtoMember(2, Name = "copyRewards", DataFormat = DataFormat.Default)]
		public List<CopyReward> copyRewards
		{
			get
			{
				return this._copyRewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
