using System;

namespace Foundation.Core.Databinding
{
	public interface IBindingElement
	{
		BindingContext Context
		{
			get;
		}

		IObservableModel Model
		{
			get;
			set;
		}

		void OnBindingMessage(ObservableMessage message);
	}
}
