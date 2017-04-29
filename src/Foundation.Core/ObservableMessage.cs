using System;
using UnityEngine;

namespace Foundation.Core
{
	[Serializable]
	public class ObservableMessage : IDisposable
	{
		[SerializeField]
		public object Sender;

		[SerializeField]
		public string Name;

		[SerializeField]
		public object Value;

		public T CastValue<T>()
		{
			return (T)((object)this.Value);
		}

		public void Dispose()
		{
			this.Name = null;
			this.Value = (this.Sender = null);
		}

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"BindingMessage ",
				this.Name,
				" ",
				this.Value
			});
		}
	}
}
