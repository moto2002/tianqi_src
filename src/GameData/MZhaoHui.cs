using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MZhaoHui")]
	[Serializable]
	public class MZhaoHui : IExtensible
	{
		private int _id;

		private int _findTime;

		private int _diamondUnivalent;

		private readonly List<int> _diamondReward = new List<int>();

		private int _goldUnivalent;

		private readonly List<int> _goldReward = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "findTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int findTime
		{
			get
			{
				return this._findTime;
			}
			set
			{
				this._findTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "diamondUnivalent", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int diamondUnivalent
		{
			get
			{
				return this._diamondUnivalent;
			}
			set
			{
				this._diamondUnivalent = value;
			}
		}

		[ProtoMember(5, Name = "diamondReward", DataFormat = DataFormat.TwosComplement)]
		public List<int> diamondReward
		{
			get
			{
				return this._diamondReward;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "goldUnivalent", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goldUnivalent
		{
			get
			{
				return this._goldUnivalent;
			}
			set
			{
				this._goldUnivalent = value;
			}
		}

		[ProtoMember(7, Name = "goldReward", DataFormat = DataFormat.TwosComplement)]
		public List<int> goldReward
		{
			get
			{
				return this._goldReward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
