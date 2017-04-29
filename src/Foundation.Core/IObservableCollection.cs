using System;
using System.Collections.Generic;

namespace Foundation.Core
{
	public interface IObservableCollection
	{
		event Action<object> OnObjectAdd;

		event Action<object> OnObjectRemove;

		event Action<object> OnObjectHide;

		event Action<object> OnObjectOpen;

		event Action OnObjectHideAll;

		event Action OnObjectOpenAll;

		event Func<object, object> OnObjectGet;

		event Action OnClear;

		IEnumerable<object> GetObjects();
	}
}
