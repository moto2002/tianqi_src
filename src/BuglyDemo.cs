using System;
using UnityEngine;

public class BuglyDemo : MonoBehaviour
{
	private int selGridIntCurrent = 5;

	private int selGridIntDefault = -1;

	private Vector2 scrollPosition = Vector2.get_zero();

	private string[] selGridItems = new string[]
	{
		"0.Exception",
		"1.SystemException",
		"2.ApplicationException",
		"3.ArgumentException",
		"4.FormatException",
		"5....",
		"6.MemberAccessException",
		"7.FileAccessException",
		"8.MethodAccessException",
		"9.MissingMemberException",
		"10.MissingMethodException",
		"11.MissingFieldException",
		"12.IndexOutOfException",
		"13.ArrayTypeMismatchException",
		"14.RankException",
		"15.IOException",
		"16.DirectionNotFoundException",
		"17.FileNotFoundException",
		"18.EndOfStreamException",
		"19.FileLoadException",
		"20.PathTooLongException",
		"21.ArithmeticException",
		"22.NotFiniteNumberException",
		"23.DivideByZeroException",
		"24.OutOfMemoryException",
		"25.NullReferenceException",
		"26.InvalidCastException",
		"27.InvalidOperationException",
		"28.DoCrash",
		"29."
	};

	private GUIStyle styleTitle;

	private GUIStyle styleContent;

	private static float StandardScreenWidth = 640f;

	private static float StandardScreenHeight = 960f;

	private float guiRatioX;

	private float guiRatioY;

	private float screenWidth;

	private float screenHeight;

	private Vector3 scaleGUIs;

	private void Awake()
	{
		BuglyAgent.DebugLog("Demo.Awake()", "Screen: {0} x {1}", new object[]
		{
			Screen.get_width(),
			Screen.get_height()
		});
		this.screenWidth = (float)Screen.get_width();
		this.screenHeight = (float)Screen.get_height();
		this.guiRatioX = this.screenWidth / BuglyDemo.StandardScreenWidth * 1f;
		this.guiRatioY = this.screenHeight / BuglyDemo.StandardScreenHeight * 1f;
		this.scaleGUIs = new Vector3(this.guiRatioX, this.guiRatioY, 1f);
	}

	private void Start()
	{
		BuglyAgent.PrintLog(LogSeverity.LogInfo, "Demo Start()", new object[0]);
		this.SetupGUIStyle();
		this.InitBuglySDK();
		BuglyAgent.PrintLog(LogSeverity.LogWarning, "Init bugly sdk done", new object[0]);
		BuglyAgent.SetScene(3450);
	}

	private void InitBuglySDK()
	{
		BuglyAgent.PrintLog(LogSeverity.LogInfo, "Init the bugly sdk", new object[0]);
	}

	private void OnGUI()
	{
		this.StyledGUI();
		GUILayout.BeginArea(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()));
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		GUILayout.Label("Bugly Unity Demo", this.styleTitle, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Label("Uncaught Exceptions:", this.styleContent, new GUILayoutOption[0]);
		GUILayout.Space(20f);
		this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[]
		{
			GUILayout.Width((float)Screen.get_width()),
			GUILayout.Height((float)(Screen.get_height() - 100))
		});
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Space(40f);
		this.selGridIntCurrent = GUILayout.SelectionGrid(this.selGridIntCurrent, this.selGridItems, 2, new GUILayoutOption[0]);
		GUILayout.Space(40f);
		GUILayout.EndHorizontal();
		GUILayout.EndScrollView();
		if (this.selGridIntCurrent != this.selGridIntDefault)
		{
			this.selGridIntDefault = this.selGridIntCurrent;
			this.TrigException(this.selGridIntCurrent);
		}
		GUILayout.EndVertical();
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void TrigException(int selGridInt)
	{
		BuglyAgent.PrintLog(LogSeverity.LogWarning, "Trigge Exception: {0}", new object[]
		{
			selGridInt
		});
		switch (selGridInt)
		{
		case 0:
			this.throwException(new Exception("Non-fatal error, an base C# exception"));
			return;
		case 1:
			this.throwException(new SystemException("Fatal error, a system exception"));
			return;
		case 2:
			this.throwException(new ApplicationException("Fatal error, an application exception"));
			return;
		case 3:
			this.throwException(new ArgumentException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 4:
			this.throwException(new FormatException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 5:
			return;
		case 6:
			this.throwException(new MemberAccessException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 7:
			this.throwException(new FieldAccessException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 8:
			this.throwException(new MethodAccessException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 9:
			this.throwException(new MissingMemberException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 10:
			this.throwException(new MissingMethodException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 11:
			this.throwException(new MissingFieldException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 12:
			this.throwException(new IndexOutOfRangeException(string.Format("Non-Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 13:
			this.throwException(new ArrayTypeMismatchException(string.Format("Non-Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 14:
			this.throwException(new RankException(string.Format("Non-Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
			try
			{
				this.throwException(new Exception(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			}
			catch (Exception e)
			{
				BuglyAgent.ReportException(e, "Caught an exception.");
			}
			return;
		case 21:
			this.throwException(new ArithmeticException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 22:
			this.throwException(new NotFiniteNumberException(string.Format("Fatal error, {0} ", this.selGridItems[selGridInt])));
			return;
		case 23:
		{
			int num = 0;
			num = 2 / num;
			return;
		}
		case 24:
			this.throwException(new OutOfMemoryException("Fatal error, OOM"));
			return;
		case 25:
			this.findGameObject();
			return;
		case 26:
		{
			Exception ex = null;
			IndexOutOfRangeException ex2 = (IndexOutOfRangeException)ex;
			Console.Write(string.Empty + ex2);
			return;
		}
		case 27:
			this.findGameObjectByTag();
			return;
		case 28:
			this.DoCrash();
			return;
		}
		try
		{
			this.throwException(new OutOfMemoryException("Fatal error, out of memory"));
		}
		catch (Exception ex3)
		{
			Debug.LogException(ex3);
		}
	}

	private void findGameObjectByTag()
	{
		Console.Write("it will throw UnityException");
		GameObject gameObject = GameObject.FindWithTag("test");
		string name = gameObject.get_name();
		Console.Write(name);
	}

	private void findGameObject()
	{
		Console.Write("it will throw NullReferenceException");
		GameObject gameObject = GameObject.Find("test");
		string name = gameObject.get_name();
		Console.Write(name);
	}

	private void throwException(Exception e)
	{
		if (e == null)
		{
			return;
		}
		BuglyAgent.PrintLog(LogSeverity.LogWarning, "Throw exception: {0}", new object[]
		{
			e.ToString()
		});
		this.testDeepFrame(e);
	}

	private void testDeepFrame(Exception e)
	{
		throw e;
	}

	private void DoCrash()
	{
		Console.Write("it will Crash...");
		this.DoCrash();
	}

	private void SetupGUIStyle()
	{
		this.styleTitle = new GUIStyle();
		this.styleTitle.set_fontSize(28);
		this.styleTitle.set_fontStyle(1);
		this.styleContent = new GUIStyle();
		this.styleContent.set_fontSize(20);
		this.styleContent.set_fontStyle(2);
	}

	public void StyledGUI()
	{
		GUI.set_color(Color.get_gray());
		GUI.get_skin().get_label().set_fontSize(20);
		GUI.get_skin().get_button().set_fontSize(20);
	}
}
