using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Foundation
{
	public class Task : IDisposable
	{
		public static bool DisableMultiThread;

		public static bool LogErrors;

		public object Paramater;

		public TaskStrategy Strategy;

		private Action _action;

		private Delegate _action2;

		protected IEnumerator _routine;

		protected List<Action<Task>> OnComplete;

		private TaskStatus _status;

		public Exception Exception;

		public TaskStatus Status
		{
			get
			{
				return this._status;
			}
			set
			{
				if (this._status == value)
				{
					return;
				}
				this._status = value;
				if (this.IsCompleted)
				{
					this.OnTaskComplete();
				}
			}
		}

		public bool IsRunning
		{
			get
			{
				return !this.IsCompleted;
			}
		}

		public bool IsCompleted
		{
			get
			{
				return this.Status == TaskStatus.Success || this.Status == TaskStatus.Faulted;
			}
		}

		public bool IsFaulted
		{
			get
			{
				return this.Status == TaskStatus.Faulted;
			}
		}

		public bool IsSuccess
		{
			get
			{
				return this.Status == TaskStatus.Success;
			}
		}

		protected Task()
		{
			this.Status = TaskStatus.Created;
		}

		public Task(TaskStrategy mode) : this()
		{
			this.Strategy = mode;
		}

		public Task(Exception ex)
		{
			this.Exception = ex;
			this.Strategy = TaskStrategy.Custom;
			this.Status = TaskStatus.Faulted;
		}

		public Task(Action action) : this()
		{
			this._action = action;
			this.Strategy = TaskStrategy.BackgroundThread;
		}

		public Task(Action action, TaskStrategy mode) : this()
		{
			if (mode == TaskStrategy.Coroutine)
			{
				throw new ArgumentException("Action tasks may not be coroutines");
			}
			this._action = action;
			this.Strategy = mode;
		}

		public Task(IEnumerator action) : this()
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this._routine = action;
			this.Strategy = TaskStrategy.Coroutine;
		}

		public Task(IEnumerator action, object param) : this()
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this._routine = action;
			this.Strategy = TaskStrategy.Coroutine;
			this.Paramater = param;
		}

		public Task(Delegate action, object paramater) : this()
		{
			this._action2 = action;
			this.Strategy = TaskStrategy.BackgroundThread;
			this.Paramater = paramater;
		}

		public Task(Delegate action, object paramater, TaskStrategy mode) : this()
		{
			if (mode == TaskStrategy.Coroutine)
			{
				throw new ArgumentException("Action tasks may not be coroutines");
			}
			this._action2 = action;
			this.Strategy = mode;
			this.Paramater = paramater;
		}

		static Task()
		{
			TaskManager.ConfirmInit();
		}

		protected virtual void Execute()
		{
			try
			{
				if (this._action2 != null)
				{
					this._action2.DynamicInvoke(new object[]
					{
						this.Paramater
					});
				}
				else if (this._action != null)
				{
					this._action.Invoke();
				}
				this.Status = TaskStatus.Success;
			}
			catch (Exception ex)
			{
				this.Exception = ex;
				this.Status = TaskStatus.Faulted;
				if (Task.LogErrors)
				{
					Logger.LogException(ex);
				}
			}
		}

		protected void RunOnBackgroundThread()
		{
			this.Status = TaskStatus.Running;
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.Execute();
			});
		}

		protected void RunOnCurrentThread()
		{
			this.Status = TaskStatus.Running;
			this.Execute();
		}

		protected void RunOnMainThread()
		{
			this.Status = TaskStatus.Running;
			TaskManager.RunOnMainThread(new Action(this.Execute));
		}

		protected void RunAsCoroutine()
		{
			this.Status = TaskStatus.Running;
			TaskManager.StartRoutine(new TaskManager.CoroutineInfo
			{
				Coroutine = this._routine,
				OnComplete = new Action(this.OnRoutineComplete)
			});
		}

		protected virtual void OnTaskComplete()
		{
			if (this.OnComplete != null)
			{
				for (int i = 0; i < this.OnComplete.get_Count(); i++)
				{
					this.OnComplete.get_Item(i).Invoke(this);
				}
			}
		}

		protected void OnRoutineComplete()
		{
			this.Status = TaskStatus.Success;
		}

		public void Start()
		{
			if (this.IsCompleted)
			{
				return;
			}
			switch (this.Strategy)
			{
			case TaskStrategy.BackgroundThread:
				if (Task.DisableMultiThread)
				{
					this.RunOnCurrentThread();
				}
				else
				{
					this.RunOnBackgroundThread();
				}
				break;
			case TaskStrategy.MainThread:
				this.RunOnMainThread();
				break;
			case TaskStrategy.CurrentThread:
				this.RunOnCurrentThread();
				break;
			case TaskStrategy.Coroutine:
				this.RunAsCoroutine();
				break;
			}
		}

		public Task ContinueWith(Action<Task> action)
		{
			if (this.IsCompleted)
			{
				action.Invoke(this);
			}
			else
			{
				if (this.OnComplete == null)
				{
					this.OnComplete = new List<Action<Task>>(2);
				}
				this.OnComplete.Add(action);
			}
			return this;
		}

		public Task ThrowIfFaulted()
		{
			if (this.IsFaulted)
			{
				throw this.Exception;
			}
			return this;
		}

		[DebuggerHidden]
		public IEnumerator WaitRoutine()
		{
			Task.<WaitRoutine>c__IteratorC <WaitRoutine>c__IteratorC = new Task.<WaitRoutine>c__IteratorC();
			<WaitRoutine>c__IteratorC.<>f__this = this;
			return <WaitRoutine>c__IteratorC;
		}

		public Task Wait()
		{
			if (TaskManager.IsMainThread && !Task.DisableMultiThread)
			{
				Logger.LogWarning("Use WaitRoutine in coroutine to wait in main thread");
			}
			Task.Delay(10);
			while (this.IsRunning)
			{
				Task.Delay(10);
			}
			return this;
		}

		public static void Delay(int millisecondTimeout)
		{
			Thread.Sleep(millisecondTimeout);
		}

		public virtual void Dispose()
		{
			this.Status = TaskStatus.Created;
			this.Paramater = null;
			this.Exception = null;
			this._action = null;
			this._action2 = null;
			this._routine = null;
			this.OnComplete = null;
			this._action = null;
		}

		public static Task Run(Action action)
		{
			Task task = new Task(action);
			task.Start();
			return task;
		}

		public static Task RunOnMain(Action action)
		{
			Task task = new Task(action, TaskStrategy.MainThread);
			task.Start();
			return task;
		}

		public static Task RunOnCurrent(Action action)
		{
			Task task = new Task(action, TaskStrategy.CurrentThread);
			task.Start();
			return task;
		}

		public static Task Run<TP>(Action<TP> action, TP param)
		{
			Task task = new Task(action, param, TaskStrategy.CurrentThread);
			task.Start();
			return task;
		}

		public static Task RunOnMain<TP>(Action<TP> action, TP param)
		{
			Task task = new Task(action, param, TaskStrategy.MainThread);
			task.Start();
			return task;
		}

		public static Task RunOnCurrent<TP>(Action<TP> action, TP param)
		{
			Task task = new Task(action, param, TaskStrategy.CurrentThread);
			task.Start();
			return task;
		}

		public static Task RunCoroutine(IEnumerator function)
		{
			Task task = new Task(function);
			task.Start();
			return task;
		}

		public static Task RunCoroutine(Func<IEnumerator> function)
		{
			Task task = new Task(function.Invoke());
			task.Start();
			return task;
		}

		public static Task RunCoroutine(Func<Task, IEnumerator> function)
		{
			Task task = new Task();
			task.Strategy = TaskStrategy.Coroutine;
			task._routine = function.Invoke(task);
			task.Start();
			return task;
		}

		public static Task<TResult> Run<TResult>(Func<TResult> function)
		{
			Task<TResult> task = new Task<TResult>(function);
			task.Start();
			return task;
		}

		public static Task<TResult> Run<TParam, TResult>(Func<TParam, TResult> function, TParam param)
		{
			Task<TResult> task = new Task<TResult>(function, param);
			task.Start();
			return task;
		}

		public static Task<TResult> RunOnMain<TResult>(Func<TResult> function)
		{
			Task<TResult> task = new Task<TResult>(function, TaskStrategy.MainThread);
			task.Start();
			return task;
		}

		public static Task<TResult> RunOnMain<TParam, TResult>(Func<TParam, TResult> function, TParam param)
		{
			Task<TResult> task = new Task<TResult>(function, param, TaskStrategy.MainThread);
			task.Start();
			return task;
		}

		public static Task<TResult> RunOnCurrent<TResult>(Func<TResult> function)
		{
			Task<TResult> task = new Task<TResult>(function, TaskStrategy.CurrentThread);
			task.Start();
			return task;
		}

		public static Task<TResult> RunOnCurrent<TParam, TResult>(Func<TParam, TResult> function, TParam param)
		{
			Task<TResult> task = new Task<TResult>(function, param, TaskStrategy.CurrentThread);
			task.Start();
			return task;
		}

		public static Task<TResult> RunCoroutine<TResult>(IEnumerator function)
		{
			Task<TResult> task = new Task<TResult>(function);
			task.Start();
			return task;
		}

		public static Task<TResult> RunCoroutine<TResult>(Func<IEnumerator> function)
		{
			Task<TResult> task = new Task<TResult>(function.Invoke());
			task.Start();
			return task;
		}

		public static Task<TResult> RunCoroutine<TResult>(Func<Task<TResult>, IEnumerator> function)
		{
			Task<TResult> task = new Task<TResult>();
			task.Strategy = TaskStrategy.Coroutine;
			task.Paramater = task;
			task._routine = function.Invoke(task);
			task.Start();
			return task;
		}
	}
	public class Task<TResult> : Task
	{
		private Func<TResult> _function;

		private Delegate _function2;

		protected new List<Action<Task<TResult>>> OnComplete;

		public TResult Result;

		public Task()
		{
		}

		public Task(TResult result) : this()
		{
			base.Status = TaskStatus.Success;
			this.Strategy = TaskStrategy.Custom;
			this.Result = result;
		}

		public Task(Func<TResult> function) : this()
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			this._function = function;
			this.Strategy = TaskStrategy.BackgroundThread;
		}

		public Task(Delegate function, object param) : this()
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			this._function2 = function;
			this.Paramater = param;
			this.Strategy = TaskStrategy.BackgroundThread;
		}

		public Task(Func<TResult> function, TaskStrategy mode) : this()
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (mode == TaskStrategy.Coroutine)
			{
				throw new ArgumentException("Mode can not be coroutine");
			}
			this._function = function;
			this.Strategy = mode;
		}

		public Task(Delegate function, object param, TaskStrategy mode) : this()
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (mode == TaskStrategy.Coroutine)
			{
				throw new ArgumentException("Mode can not be coroutine");
			}
			this._function2 = function;
			this.Paramater = param;
			this.Strategy = mode;
		}

		public Task(IEnumerator routine)
		{
			if (routine == null)
			{
				throw new ArgumentNullException("routine");
			}
			this._routine = routine;
			this.Strategy = TaskStrategy.Coroutine;
		}

		public Task(Exception ex)
		{
			this.Exception = ex;
			this.Strategy = TaskStrategy.Custom;
			base.Status = TaskStatus.Faulted;
		}

		public Task(TaskStrategy mode) : this()
		{
			this.Strategy = mode;
		}

		protected override void Execute()
		{
			try
			{
				if (this._function2 != null)
				{
					this.Result = (TResult)((object)this._function2.DynamicInvoke(new object[]
					{
						this.Paramater
					}));
				}
				else if (this._function != null)
				{
					this.Result = this._function.Invoke();
				}
				base.Status = TaskStatus.Success;
			}
			catch (Exception ex)
			{
				this.Exception = ex;
				base.Status = TaskStatus.Faulted;
				if (Task.LogErrors)
				{
					Logger.LogException(ex);
				}
			}
		}

		protected override void OnTaskComplete()
		{
			if (this.OnComplete != null)
			{
				for (int i = 0; i < this.OnComplete.get_Count(); i++)
				{
					this.OnComplete.get_Item(i).Invoke(this);
				}
			}
		}

		public Task<TResult> ContinueWith(Action<Task<TResult>> action)
		{
			if (base.IsCompleted)
			{
				action.Invoke(this);
			}
			else
			{
				if (this.OnComplete == null)
				{
					this.OnComplete = new List<Action<Task<TResult>>>(2);
				}
				this.OnComplete.Add(action);
			}
			return this;
		}

		public Task<TResult> ThrowIfFaulted()
		{
			if (base.IsFaulted)
			{
				throw this.Exception;
			}
			return this;
		}

		public Task<TResult> Wait()
		{
			base.Wait();
			return this;
		}

		public override void Dispose()
		{
			base.Dispose();
			this.Result = default(TResult);
			this._function = null;
			this._function2 = null;
			this.OnComplete = null;
		}
	}
}
