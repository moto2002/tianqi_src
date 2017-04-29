using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(626), ForSend(626), ProtoContract(Name = "UpdateEffectReq")]
	[Serializable]
	public class UpdateEffectReq : IExtensible
	{
		public static readonly short OP = 626;

		private long _casterId;

		private long _uniqueId;

		private readonly List<EffectTargetInfo> _targets = new List<EffectTargetInfo>();

		private Pos _pos;

		private Vector2 _vector;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "casterId", DataFormat = DataFormat.TwosComplement)]
		public long casterId
		{
			get
			{
				return this._casterId;
			}
			set
			{
				this._casterId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "uniqueId", DataFormat = DataFormat.TwosComplement)]
		public long uniqueId
		{
			get
			{
				return this._uniqueId;
			}
			set
			{
				this._uniqueId = value;
			}
		}

		[ProtoMember(3, Name = "targets", DataFormat = DataFormat.Default)]
		public List<EffectTargetInfo> targets
		{
			get
			{
				return this._targets;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "vector", DataFormat = DataFormat.Default)]
		public Vector2 vector
		{
			get
			{
				return this._vector;
			}
			set
			{
				this._vector = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
