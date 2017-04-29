using System;

namespace Foundation.Core
{
	public interface IObservableModel
	{
		event Action<ObservableMessage> OnBindingUpdate;

		void RaiseBindingUpdate(string memberName, object paramater);

		object GetValue(string memberName);

		object GetValue(string memberName, object paramater);

		void Command(string memberName);

		void Command(string memberName, object paramater);
	}
}
