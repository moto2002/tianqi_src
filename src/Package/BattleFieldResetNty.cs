using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(817), ForSend(817), ProtoContract(Name = "BattleFieldResetNty")]
	[Serializable]
	public class BattleFieldResetNty : IExtensible
	{
		public static readonly short OP = 817;

		private readonly List<MapObjInfo> _objs = new List<MapObjInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "objs", DataFormat = DataFormat.Default)]
		public List<MapObjInfo> objs
		{
			get
			{
				return this._objs;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
