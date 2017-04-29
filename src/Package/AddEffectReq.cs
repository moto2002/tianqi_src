using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(457), ForSend(457), ProtoContract(Name = "AddEffectReq")]
	[Serializable]
	public class AddEffectReq : IExtensible
	{
		public static readonly short OP = 457;

		private long _casterId;

		private int _effectId;

		private long _uniqueId;

		private int _skillId;

		private Pos _pos;

		private Vector2 _vector;

		private readonly List<EffectTargetInfo> _targets = new List<EffectTargetInfo>();

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

		[ProtoMember(2, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "uniqueId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
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

		[ProtoMember(6, IsRequired = true, Name = "vector", DataFormat = DataFormat.Default)]
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

		[ProtoMember(7, Name = "targets", DataFormat = DataFormat.Default)]
		public List<EffectTargetInfo> targets
		{
			get
			{
				return this._targets;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
