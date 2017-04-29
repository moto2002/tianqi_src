using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(151), ForSend(151), ProtoContract(Name = "OpenServerActStatusNty")]
	[Serializable]
	public class OpenServerActStatusNty : IExtensible
	{
		public static readonly short OP = 151;

		private bool _isOpenActivity;

		private readonly List<int> _unLockActTypes = new List<int>();

		private int _nowDay;

		private readonly List<TargetTaskInfo> _targetInfos = new List<TargetTaskInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isOpenActivity", DataFormat = DataFormat.Default)]
		public bool isOpenActivity
		{
			get
			{
				return this._isOpenActivity;
			}
			set
			{
				this._isOpenActivity = value;
			}
		}

		[ProtoMember(2, Name = "unLockActTypes", DataFormat = DataFormat.TwosComplement)]
		public List<int> unLockActTypes
		{
			get
			{
				return this._unLockActTypes;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "nowDay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nowDay
		{
			get
			{
				return this._nowDay;
			}
			set
			{
				this._nowDay = value;
			}
		}

		[ProtoMember(4, Name = "targetInfos", DataFormat = DataFormat.Default)]
		public List<TargetTaskInfo> targetInfos
		{
			get
			{
				return this._targetInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
