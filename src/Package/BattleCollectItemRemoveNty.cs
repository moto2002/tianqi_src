using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1141), ForSend(1141), ProtoContract(Name = "BattleCollectItemRemoveNty")]
	[Serializable]
	public class BattleCollectItemRemoveNty : IExtensible
	{
		public static readonly short OP = 1141;

		private readonly List<int> _removeIdxList = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "removeIdxList", DataFormat = DataFormat.TwosComplement)]
		public List<int> removeIdxList
		{
			get
			{
				return this._removeIdxList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
