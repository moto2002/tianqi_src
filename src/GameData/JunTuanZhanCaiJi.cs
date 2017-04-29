using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanCaiJi")]
	[Serializable]
	public class JunTuanZhanCaiJi : IExtensible
	{
		private int _CollectionId;

		private int _ModelId;

		private readonly List<int> _CoordinatePoint = new List<int>();

		private readonly List<int> _TouchRange = new List<int>();

		private readonly List<int> _CollectionRange = new List<int>();

		private int _Name;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "CollectionId", DataFormat = DataFormat.TwosComplement)]
		public int CollectionId
		{
			get
			{
				return this._CollectionId;
			}
			set
			{
				this._CollectionId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ModelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ModelId
		{
			get
			{
				return this._ModelId;
			}
			set
			{
				this._ModelId = value;
			}
		}

		[ProtoMember(4, Name = "CoordinatePoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> CoordinatePoint
		{
			get
			{
				return this._CoordinatePoint;
			}
		}

		[ProtoMember(5, Name = "TouchRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> TouchRange
		{
			get
			{
				return this._TouchRange;
			}
		}

		[ProtoMember(6, Name = "CollectionRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> CollectionRange
		{
			get
			{
				return this._CollectionRange;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "Name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
