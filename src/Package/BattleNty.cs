using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(398), ForSend(398), ProtoContract(Name = "BattleNty")]
	[Serializable]
	public class BattleNty : IExtensible
	{
		public static readonly short OP = 398;

		private readonly List<BattleAction> _actions = new List<BattleAction>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "actions", DataFormat = DataFormat.Default)]
		public List<BattleAction> actions
		{
			get
			{
				return this._actions;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
