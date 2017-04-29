using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RenWuYinDaoPeiZhi")]
	[Serializable]
	public class RenWuYinDaoPeiZhi : IExtensible
	{
		private int _id;

		private int _taskId;

		private int _taskEffects;

		private int _guide;

		private int _explain;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "taskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskId
		{
			get
			{
				return this._taskId;
			}
			set
			{
				this._taskId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "taskEffects", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskEffects
		{
			get
			{
				return this._taskEffects;
			}
			set
			{
				this._taskEffects = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "guide", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int guide
		{
			get
			{
				return this._guide;
			}
			set
			{
				this._guide = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "explain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int explain
		{
			get
			{
				return this._explain;
			}
			set
			{
				this._explain = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
