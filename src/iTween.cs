using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class iTween : MonoBehaviour
{
	public enum EaseType
	{
		easeInQuad,
		easeOutQuad,
		easeInOutQuad,
		easeInCubic,
		easeOutCubic,
		easeInOutCubic,
		easeInQuart,
		easeOutQuart,
		easeInOutQuart,
		easeInQuint,
		easeOutQuint,
		easeInOutQuint,
		easeInSine,
		easeOutSine,
		easeInOutSine,
		easeInExpo,
		easeOutExpo,
		easeInOutExpo,
		easeInCirc,
		easeOutCirc,
		easeInOutCirc,
		linear,
		spring,
		easeInBounce,
		easeOutBounce,
		easeInOutBounce,
		easeInBack,
		easeOutBack,
		easeInOutBack,
		easeInElastic,
		easeOutElastic,
		easeInOutElastic,
		punch
	}

	public enum LoopType
	{
		none,
		loop,
		pingPong
	}

	public enum NamedValueColor
	{
		_Color,
		_SpecColor,
		_Emission,
		_ReflectColor
	}

	public static class Defaults
	{
		public static float time = 1f;

		public static float delay = 0f;

		public static iTween.NamedValueColor namedColorValue = iTween.NamedValueColor._Color;

		public static iTween.LoopType loopType = iTween.LoopType.none;

		public static iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

		public static float lookSpeed = 3f;

		public static bool isLocal = false;

		public static Space space = 1;

		public static bool orientToPath = false;

		public static Color color = Color.get_white();

		public static float updateTimePercentage = 0.05f;

		public static float updateTime = 1f * iTween.Defaults.updateTimePercentage;

		public static int cameraFadeDepth = 999999;

		public static float lookAhead = 0.05f;

		public static bool useRealTime = false;

		public static Vector3 up = Vector3.get_up();
	}

	private class CRSpline
	{
		public Vector3[] pts;

		public CRSpline(params Vector3[] pts)
		{
			this.pts = new Vector3[pts.Length];
			Array.Copy(pts, this.pts, pts.Length);
		}

		public Vector3 Interp(float t)
		{
			int num = this.pts.Length - 3;
			int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
			float num3 = t * (float)num - (float)num2;
			Vector3 vector = this.pts[num2];
			Vector3 vector2 = this.pts[num2 + 1];
			Vector3 vector3 = this.pts[num2 + 2];
			Vector3 vector4 = this.pts[num2 + 3];
			return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
		}
	}

	private delegate float EasingFunction(float start, float end, float Value);

	private delegate void ApplyTween();

	public static List<Hashtable> tweens = new List<Hashtable>();

	private static GameObject cameraFade;

	public string id;

	public string type;

	public string method;

	public iTween.EaseType easeType;

	public float time;

	public float delay;

	public iTween.LoopType loopType;

	public bool isRunning;

	public bool isPaused;

	public string _name;

	private float runningTime;

	private float percentage;

	private float delayStarted;

	private bool kinematic;

	private bool isLocal;

	private bool loop;

	private bool reverse;

	private bool wasPaused;

	private bool physics;

	private Hashtable tweenArguments;

	private Space space;

	private iTween.EasingFunction ease;

	private iTween.ApplyTween apply;

	private AudioSource audioSource;

	private Vector3[] vector3s;

	private Vector2[] vector2s;

	private Color[,] colors;

	private float[] floats;

	private Rect[] rects;

	private iTween.CRSpline path;

	private Vector3 preUpdate;

	private Vector3 postUpdate;

	private iTween.NamedValueColor namedcolorvalue;

	private float lastRealTime;

	private bool useRealTime;

	private Transform thisTransform;

	private iTween(Hashtable h)
	{
		this.tweenArguments = h;
	}

	public static void Init(GameObject target)
	{
		iTween.MoveBy(target, Vector3.get_zero(), 0f);
	}

	public static void CameraFadeFrom(float amount, float time)
	{
		if (iTween.cameraFade)
		{
			iTween.CameraFadeFrom(iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}
		else
		{
			Debuger.Error("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.", new object[0]);
		}
	}

	public static void CameraFadeFrom(Hashtable args)
	{
		if (iTween.cameraFade)
		{
			iTween.ColorFrom(iTween.cameraFade, args);
		}
		else
		{
			Debuger.Error("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.", new object[0]);
		}
	}

	public static void CameraFadeTo(float amount, float time)
	{
		if (iTween.cameraFade)
		{
			iTween.CameraFadeTo(iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}
		else
		{
			Debuger.Error("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.", new object[0]);
		}
	}

	public static void CameraFadeTo(Hashtable args)
	{
		if (iTween.cameraFade)
		{
			iTween.ColorTo(iTween.cameraFade, args);
		}
		else
		{
			Debuger.Error("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.", new object[0]);
		}
	}

	public static void ValueTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("onupdate") || !args.Contains("from") || !args.Contains("to"))
		{
			Debuger.Error("iTween Error: ValueTo() requires an 'onupdate' callback function and a 'from' and 'to' property.  The supplied 'onupdate' callback must accept a single argument that is the same type as the supplied 'from' and 'to' properties!", new object[0]);
			return;
		}
		args.set_Item("type", "value");
		if (args.get_Item("from").GetType() == typeof(Vector2))
		{
			args.set_Item("method", "vector2");
		}
		else if (args.get_Item("from").GetType() == typeof(Vector3))
		{
			args.set_Item("method", "vector3");
		}
		else if (args.get_Item("from").GetType() == typeof(Rect))
		{
			args.set_Item("method", "rect");
		}
		else if (args.get_Item("from").GetType() == typeof(float))
		{
			args.set_Item("method", "float");
		}
		else
		{
			if (args.get_Item("from").GetType() != typeof(Color))
			{
				Debuger.Error("iTween Error: ValueTo() only works with interpolating Vector3s, Vector2s, floats, ints, Rects and Colors!", new object[0]);
				return;
			}
			args.set_Item("method", "color");
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		iTween.Launch(target, args);
	}

	public static void FadeFrom(GameObject target, float alpha, float time)
	{
		iTween.FadeFrom(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	public static void FadeFrom(GameObject target, Hashtable args)
	{
		iTween.ColorFrom(target, args);
	}

	public static void FadeTo(GameObject target, float alpha, float time)
	{
		iTween.FadeTo(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	public static void FadeTo(GameObject target, Hashtable args)
	{
		iTween.ColorTo(target, args);
	}

	public static void ColorFrom(GameObject target, Color color, float time)
	{
		iTween.ColorFrom(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	public static void ColorFrom(GameObject target, Hashtable args)
	{
		Color color = default(Color);
		Color color2 = default(Color);
		args = iTween.CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args.get_Item("includechildren"))
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					Hashtable hashtable = (Hashtable)args.Clone();
					hashtable.set_Item("ischild", true);
					iTween.ColorFrom(transform.get_gameObject(), hashtable);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		if (target.GetComponent<GUITexture>())
		{
			color = (color2 = target.GetComponent<GUITexture>().get_color());
		}
		else if (target.GetComponent<GUIText>())
		{
			color = (color2 = target.GetComponent<GUIText>().get_material().get_color());
		}
		else if (target.GetComponent<Renderer>())
		{
			color = (color2 = target.GetComponent<Renderer>().get_material().get_color());
		}
		else if (target.GetComponent<Light>())
		{
			color = (color2 = target.GetComponent<Light>().get_color());
		}
		if (args.Contains("color"))
		{
			color = (Color)args.get_Item("color");
		}
		else
		{
			if (args.Contains("r"))
			{
				color.r = (float)args.get_Item("r");
			}
			if (args.Contains("g"))
			{
				color.g = (float)args.get_Item("g");
			}
			if (args.Contains("b"))
			{
				color.b = (float)args.get_Item("b");
			}
			if (args.Contains("a"))
			{
				color.a = (float)args.get_Item("a");
			}
		}
		if (args.Contains("amount"))
		{
			color.a = (float)args.get_Item("amount");
			args.Remove("amount");
		}
		else if (args.Contains("alpha"))
		{
			color.a = (float)args.get_Item("alpha");
			args.Remove("alpha");
		}
		if (target.GetComponent<GUITexture>())
		{
			target.GetComponent<GUITexture>().set_color(color);
		}
		else if (target.GetComponent<GUIText>())
		{
			target.GetComponent<GUIText>().get_material().set_color(color);
		}
		else if (target.GetComponent<Renderer>())
		{
			target.GetComponent<Renderer>().get_material().set_color(color);
		}
		else if (target.GetComponent<Light>())
		{
			target.GetComponent<Light>().set_color(color);
		}
		args.set_Item("color", color2);
		args.set_Item("type", "color");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void ColorTo(GameObject target, Color color, float time)
	{
		iTween.ColorTo(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	public static void ColorTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args.get_Item("includechildren"))
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					Hashtable hashtable = (Hashtable)args.Clone();
					hashtable.set_Item("ischild", true);
					iTween.ColorTo(transform.get_gameObject(), hashtable);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args.set_Item("type", "color");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void AudioFrom(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioFrom(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	public static void AudioFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		AudioSource audioSource;
		if (args.Contains("audiosource"))
		{
			audioSource = (AudioSource)args.get_Item("audiosource");
		}
		else
		{
			if (!target.GetComponent<AudioSource>())
			{
				Debuger.Error("iTween Error: AudioFrom requires an AudioSource.", new object[0]);
				return;
			}
			audioSource = target.GetComponent<AudioSource>();
		}
		Vector2 vector;
		Vector2 vector2;
		vector.x = (vector2.x = audioSource.get_volume());
		vector.y = (vector2.y = audioSource.get_pitch());
		if (args.Contains("volume"))
		{
			vector2.x = (float)args.get_Item("volume");
		}
		if (args.Contains("pitch"))
		{
			vector2.y = (float)args.get_Item("pitch");
		}
		audioSource.set_volume(vector2.x);
		audioSource.set_pitch(vector2.y);
		args.set_Item("volume", vector.x);
		args.set_Item("pitch", vector.y);
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args.set_Item("type", "audio");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void AudioTo(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioTo(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	public static void AudioTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args.set_Item("type", "audio");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void Stab(GameObject target, AudioClip audioclip, float delay)
	{
		iTween.Stab(target, iTween.Hash(new object[]
		{
			"audioclip",
			audioclip,
			"delay",
			delay
		}));
	}

	public static void Stab(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "stab");
		iTween.Launch(target, args);
	}

	public static void LookFrom(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookFrom(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	public static void LookFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3 eulerAngles = target.get_transform().get_eulerAngles();
		if (args.get_Item("looktarget").GetType() == typeof(Transform))
		{
			Transform arg_77_0 = target.get_transform();
			Transform arg_77_1 = (Transform)args.get_Item("looktarget");
			Vector3? vector = (Vector3?)args.get_Item("up");
			arg_77_0.LookAt(arg_77_1, (!vector.get_HasValue()) ? iTween.Defaults.up : vector.get_Value());
		}
		else if (args.get_Item("looktarget").GetType() == typeof(Vector3))
		{
			Transform arg_E4_0 = target.get_transform();
			Vector3 arg_E4_1 = (Vector3)args.get_Item("looktarget");
			Vector3? vector2 = (Vector3?)args.get_Item("up");
			arg_E4_0.LookAt(arg_E4_1, (!vector2.get_HasValue()) ? iTween.Defaults.up : vector2.get_Value());
		}
		if (args.Contains("axis"))
		{
			Vector3 eulerAngles2 = target.get_transform().get_eulerAngles();
			string text = (string)args.get_Item("axis");
			if (text != null)
			{
				if (iTween.<>f__switch$map1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("x", 0);
					dictionary.Add("y", 1);
					dictionary.Add("z", 2);
					iTween.<>f__switch$map1 = dictionary;
				}
				int num;
				if (iTween.<>f__switch$map1.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						eulerAngles2.y = eulerAngles.y;
						eulerAngles2.z = eulerAngles.z;
						break;
					case 1:
						eulerAngles2.x = eulerAngles.x;
						eulerAngles2.z = eulerAngles.z;
						break;
					case 2:
						eulerAngles2.x = eulerAngles.x;
						eulerAngles2.y = eulerAngles.y;
						break;
					}
				}
			}
			target.get_transform().set_eulerAngles(eulerAngles2);
		}
		args.set_Item("rotation", eulerAngles);
		args.set_Item("type", "rotate");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void LookTo(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookTo(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	public static void LookTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("looktarget") && args.get_Item("looktarget").GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args.get_Item("looktarget");
			args.set_Item("position", new Vector3(transform.get_position().x, transform.get_position().y, transform.get_position().z));
			args.set_Item("rotation", new Vector3(transform.get_eulerAngles().x, transform.get_eulerAngles().y, transform.get_eulerAngles().z));
		}
		args.set_Item("type", "look");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void MoveTo(GameObject target, Vector3 position, float time)
	{
		iTween.MoveTo(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	public static void MoveTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("position") && args.get_Item("position").GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args.get_Item("position");
			args.set_Item("position", new Vector3(transform.get_position().x, transform.get_position().y, transform.get_position().z));
			args.set_Item("rotation", new Vector3(transform.get_eulerAngles().x, transform.get_eulerAngles().y, transform.get_eulerAngles().z));
			args.set_Item("scale", new Vector3(transform.get_localScale().x, transform.get_localScale().y, transform.get_localScale().z));
		}
		args.set_Item("type", "move");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void MoveFrom(GameObject target, Vector3 position, float time)
	{
		iTween.MoveFrom(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	public static void MoveFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args.get_Item("islocal");
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		if (args.Contains("path"))
		{
			Vector3[] array2;
			if (args.get_Item("path").GetType() == typeof(Vector3[]))
			{
				Vector3[] array = (Vector3[])args.get_Item("path");
				array2 = new Vector3[array.Length];
				Array.Copy(array, array2, array.Length);
			}
			else
			{
				Transform[] array3 = (Transform[])args.get_Item("path");
				array2 = new Vector3[array3.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array2[i] = array3[i].get_position();
				}
			}
			if (array2[array2.Length - 1] != target.get_transform().get_position())
			{
				Vector3[] array4 = new Vector3[array2.Length + 1];
				Array.Copy(array2, array4, array2.Length);
				if (flag)
				{
					array4[array4.Length - 1] = target.get_transform().get_localPosition();
					target.get_transform().set_localPosition(array4[0]);
				}
				else
				{
					array4[array4.Length - 1] = target.get_transform().get_position();
					target.get_transform().set_position(array4[0]);
				}
				args.set_Item("path", array4);
			}
			else
			{
				if (flag)
				{
					target.get_transform().set_localPosition(array2[0]);
				}
				else
				{
					target.get_transform().set_position(array2[0]);
				}
				args.set_Item("path", array2);
			}
		}
		else
		{
			Vector3 vector2;
			Vector3 vector;
			if (flag)
			{
				vector = (vector2 = target.get_transform().get_localPosition());
			}
			else
			{
				vector = (vector2 = target.get_transform().get_position());
			}
			if (args.Contains("position"))
			{
				if (args.get_Item("position").GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args.get_Item("position");
					vector = transform.get_position();
				}
				else if (args.get_Item("position").GetType() == typeof(Vector3))
				{
					vector = (Vector3)args.get_Item("position");
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					vector.x = (float)args.get_Item("x");
				}
				if (args.Contains("y"))
				{
					vector.y = (float)args.get_Item("y");
				}
				if (args.Contains("z"))
				{
					vector.z = (float)args.get_Item("z");
				}
			}
			if (flag)
			{
				target.get_transform().set_localPosition(vector);
			}
			else
			{
				target.get_transform().set_position(vector);
			}
			args.set_Item("position", vector2);
		}
		args.set_Item("type", "move");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void MoveAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.MoveAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void MoveAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "move");
		args.set_Item("method", "add");
		iTween.Launch(target, args);
	}

	public static void MoveBy(GameObject target, Vector3 amount, float time)
	{
		iTween.MoveBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void MoveBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "move");
		args.set_Item("method", "by");
		iTween.Launch(target, args);
	}

	public static void ScaleTo(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleTo(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	public static void ScaleTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("scale") && args.get_Item("scale").GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args.get_Item("scale");
			args.set_Item("position", new Vector3(transform.get_position().x, transform.get_position().y, transform.get_position().z));
			args.set_Item("rotation", new Vector3(transform.get_eulerAngles().x, transform.get_eulerAngles().y, transform.get_eulerAngles().z));
			args.set_Item("scale", new Vector3(transform.get_localScale().x, transform.get_localScale().y, transform.get_localScale().z));
		}
		args.set_Item("type", "scale");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void ScaleFrom(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleFrom(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	public static void ScaleFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3 localScale2;
		Vector3 localScale = localScale2 = target.get_transform().get_localScale();
		if (args.Contains("scale"))
		{
			if (args.get_Item("scale").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args.get_Item("scale");
				localScale = transform.get_localScale();
			}
			else if (args.get_Item("scale").GetType() == typeof(Vector3))
			{
				localScale = (Vector3)args.get_Item("scale");
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				localScale.x = (float)args.get_Item("x");
			}
			if (args.Contains("y"))
			{
				localScale.y = (float)args.get_Item("y");
			}
			if (args.Contains("z"))
			{
				localScale.z = (float)args.get_Item("z");
			}
		}
		target.get_transform().set_localScale(localScale);
		args.set_Item("scale", localScale2);
		args.set_Item("type", "scale");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void ScaleAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.ScaleAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void ScaleAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "scale");
		args.set_Item("method", "add");
		iTween.Launch(target, args);
	}

	public static void ScaleBy(GameObject target, Vector3 amount, float time)
	{
		iTween.ScaleBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void ScaleBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "scale");
		args.set_Item("method", "by");
		iTween.Launch(target, args);
	}

	public static void RotateTo(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateTo(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	public static void RotateTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("rotation") && args.get_Item("rotation").GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args.get_Item("rotation");
			args.set_Item("position", new Vector3(transform.get_position().x, transform.get_position().y, transform.get_position().z));
			args.set_Item("rotation", new Vector3(transform.get_eulerAngles().x, transform.get_eulerAngles().y, transform.get_eulerAngles().z));
			args.set_Item("scale", new Vector3(transform.get_localScale().x, transform.get_localScale().y, transform.get_localScale().z));
		}
		args.set_Item("type", "rotate");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void RotateFrom(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateFrom(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	public static void RotateFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args.get_Item("islocal");
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		Vector3 vector2;
		Vector3 vector;
		if (flag)
		{
			vector = (vector2 = target.get_transform().get_localEulerAngles());
		}
		else
		{
			vector = (vector2 = target.get_transform().get_eulerAngles());
		}
		if (args.Contains("rotation"))
		{
			if (args.get_Item("rotation").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args.get_Item("rotation");
				vector = transform.get_eulerAngles();
			}
			else if (args.get_Item("rotation").GetType() == typeof(Vector3))
			{
				vector = (Vector3)args.get_Item("rotation");
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				vector.x = (float)args.get_Item("x");
			}
			if (args.Contains("y"))
			{
				vector.y = (float)args.get_Item("y");
			}
			if (args.Contains("z"))
			{
				vector.z = (float)args.get_Item("z");
			}
		}
		if (flag)
		{
			target.get_transform().set_localEulerAngles(vector);
		}
		else
		{
			target.get_transform().set_eulerAngles(vector);
		}
		args.set_Item("rotation", vector2);
		args.set_Item("type", "rotate");
		args.set_Item("method", "to");
		iTween.Launch(target, args);
	}

	public static void RotateAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.RotateAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void RotateAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "rotate");
		args.set_Item("method", "add");
		iTween.Launch(target, args);
	}

	public static void RotateBy(GameObject target, Vector3 amount, float time)
	{
		iTween.RotateBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void RotateBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "rotate");
		args.set_Item("method", "by");
		iTween.Launch(target, args);
	}

	public static void ShakePosition(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakePosition(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void ShakePosition(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "shake");
		args.set_Item("method", "position");
		iTween.Launch(target, args);
	}

	public static void ShakeScale(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakeScale(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void ShakeScale(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "shake");
		args.set_Item("method", "scale");
		iTween.Launch(target, args);
	}

	public static void ShakeRotation(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakeRotation(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void ShakeRotation(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "shake");
		args.set_Item("method", "rotation");
		iTween.Launch(target, args);
	}

	public static void PunchPosition(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchPosition(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void PunchPosition(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "punch");
		args.set_Item("method", "position");
		args.set_Item("easetype", iTween.EaseType.punch);
		iTween.Launch(target, args);
	}

	public static void PunchRotation(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchRotation(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void PunchRotation(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "punch");
		args.set_Item("method", "rotation");
		args.set_Item("easetype", iTween.EaseType.punch);
		iTween.Launch(target, args);
	}

	public static void PunchScale(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchScale(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	public static void PunchScale(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args.set_Item("type", "punch");
		args.set_Item("method", "scale");
		args.set_Item("easetype", iTween.EaseType.punch);
		iTween.Launch(target, args);
	}

	private void GenerateTargets()
	{
		string text = this.type;
		if (text != null)
		{
			if (iTween.<>f__switch$mapB == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
				dictionary.Add("value", 0);
				dictionary.Add("color", 1);
				dictionary.Add("audio", 2);
				dictionary.Add("move", 3);
				dictionary.Add("scale", 4);
				dictionary.Add("rotate", 5);
				dictionary.Add("shake", 6);
				dictionary.Add("punch", 7);
				dictionary.Add("look", 8);
				dictionary.Add("stab", 9);
				iTween.<>f__switch$mapB = dictionary;
			}
			int num;
			if (iTween.<>f__switch$mapB.TryGetValue(text, ref num))
			{
				switch (num)
				{
				case 0:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map2 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(5);
							dictionary.Add("float", 0);
							dictionary.Add("vector2", 1);
							dictionary.Add("vector3", 2);
							dictionary.Add("color", 3);
							dictionary.Add("rect", 4);
							iTween.<>f__switch$map2 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map2.TryGetValue(text2, ref num2))
						{
							switch (num2)
							{
							case 0:
								this.GenerateFloatTargets();
								this.apply = new iTween.ApplyTween(this.ApplyFloatTargets);
								break;
							case 1:
								this.GenerateVector2Targets();
								this.apply = new iTween.ApplyTween(this.ApplyVector2Targets);
								break;
							case 2:
								this.GenerateVector3Targets();
								this.apply = new iTween.ApplyTween(this.ApplyVector3Targets);
								break;
							case 3:
								this.GenerateColorTargets();
								this.apply = new iTween.ApplyTween(this.ApplyColorTargets);
								break;
							case 4:
								this.GenerateRectTargets();
								this.apply = new iTween.ApplyTween(this.ApplyRectTargets);
								break;
							}
						}
					}
					break;
				}
				case 1:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map3 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(1);
							dictionary.Add("to", 0);
							iTween.<>f__switch$map3 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map3.TryGetValue(text2, ref num2))
						{
							if (num2 == 0)
							{
								this.GenerateColorToTargets();
								this.apply = new iTween.ApplyTween(this.ApplyColorToTargets);
							}
						}
					}
					break;
				}
				case 2:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map4 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(1);
							dictionary.Add("to", 0);
							iTween.<>f__switch$map4 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map4.TryGetValue(text2, ref num2))
						{
							if (num2 == 0)
							{
								this.GenerateAudioToTargets();
								this.apply = new iTween.ApplyTween(this.ApplyAudioToTargets);
							}
						}
					}
					break;
				}
				case 3:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map5 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
							dictionary.Add("to", 0);
							dictionary.Add("by", 1);
							dictionary.Add("add", 1);
							iTween.<>f__switch$map5 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map5.TryGetValue(text2, ref num2))
						{
							if (num2 != 0)
							{
								if (num2 == 1)
								{
									this.GenerateMoveByTargets();
									this.apply = new iTween.ApplyTween(this.ApplyMoveByTargets);
								}
							}
							else if (this.tweenArguments.Contains("path"))
							{
								this.GenerateMoveToPathTargets();
								this.apply = new iTween.ApplyTween(this.ApplyMoveToPathTargets);
							}
							else
							{
								this.GenerateMoveToTargets();
								this.apply = new iTween.ApplyTween(this.ApplyMoveToTargets);
							}
						}
					}
					break;
				}
				case 4:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map6 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
							dictionary.Add("to", 0);
							dictionary.Add("by", 1);
							dictionary.Add("add", 2);
							iTween.<>f__switch$map6 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map6.TryGetValue(text2, ref num2))
						{
							switch (num2)
							{
							case 0:
								this.GenerateScaleToTargets();
								this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
								break;
							case 1:
								this.GenerateScaleByTargets();
								this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
								break;
							case 2:
								this.GenerateScaleAddTargets();
								this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
								break;
							}
						}
					}
					break;
				}
				case 5:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map7 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
							dictionary.Add("to", 0);
							dictionary.Add("add", 1);
							dictionary.Add("by", 2);
							iTween.<>f__switch$map7 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map7.TryGetValue(text2, ref num2))
						{
							switch (num2)
							{
							case 0:
								this.GenerateRotateToTargets();
								this.apply = new iTween.ApplyTween(this.ApplyRotateToTargets);
								break;
							case 1:
								this.GenerateRotateAddTargets();
								this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
								break;
							case 2:
								this.GenerateRotateByTargets();
								this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
								break;
							}
						}
					}
					break;
				}
				case 6:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map8 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
							dictionary.Add("position", 0);
							dictionary.Add("scale", 1);
							dictionary.Add("rotation", 2);
							iTween.<>f__switch$map8 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map8.TryGetValue(text2, ref num2))
						{
							switch (num2)
							{
							case 0:
								this.GenerateShakePositionTargets();
								this.apply = new iTween.ApplyTween(this.ApplyShakePositionTargets);
								break;
							case 1:
								this.GenerateShakeScaleTargets();
								this.apply = new iTween.ApplyTween(this.ApplyShakeScaleTargets);
								break;
							case 2:
								this.GenerateShakeRotationTargets();
								this.apply = new iTween.ApplyTween(this.ApplyShakeRotationTargets);
								break;
							}
						}
					}
					break;
				}
				case 7:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$map9 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
							dictionary.Add("position", 0);
							dictionary.Add("rotation", 1);
							dictionary.Add("scale", 2);
							iTween.<>f__switch$map9 = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$map9.TryGetValue(text2, ref num2))
						{
							switch (num2)
							{
							case 0:
								this.GeneratePunchPositionTargets();
								this.apply = new iTween.ApplyTween(this.ApplyPunchPositionTargets);
								break;
							case 1:
								this.GeneratePunchRotationTargets();
								this.apply = new iTween.ApplyTween(this.ApplyPunchRotationTargets);
								break;
							case 2:
								this.GeneratePunchScaleTargets();
								this.apply = new iTween.ApplyTween(this.ApplyPunchScaleTargets);
								break;
							}
						}
					}
					break;
				}
				case 8:
				{
					string text2 = this.method;
					if (text2 != null)
					{
						if (iTween.<>f__switch$mapA == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(1);
							dictionary.Add("to", 0);
							iTween.<>f__switch$mapA = dictionary;
						}
						int num2;
						if (iTween.<>f__switch$mapA.TryGetValue(text2, ref num2))
						{
							if (num2 == 0)
							{
								this.GenerateLookToTargets();
								this.apply = new iTween.ApplyTween(this.ApplyLookToTargets);
							}
						}
					}
					break;
				}
				case 9:
					this.GenerateStabTargets();
					this.apply = new iTween.ApplyTween(this.ApplyStabTargets);
					break;
				}
			}
		}
	}

	private void GenerateRectTargets()
	{
		this.rects = new Rect[3];
		this.rects[0] = (Rect)this.tweenArguments.get_Item("from");
		this.rects[1] = (Rect)this.tweenArguments.get_Item("to");
	}

	private void GenerateColorTargets()
	{
		this.colors = new Color[1, 3];
		this.colors[0, 0] = (Color)this.tweenArguments.get_Item("from");
		this.colors[0, 1] = (Color)this.tweenArguments.get_Item("to");
	}

	private void GenerateVector3Targets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (Vector3)this.tweenArguments.get_Item("from");
		this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("to");
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateVector2Targets()
	{
		this.vector2s = new Vector2[3];
		this.vector2s[0] = (Vector2)this.tweenArguments.get_Item("from");
		this.vector2s[1] = (Vector2)this.tweenArguments.get_Item("to");
		if (this.tweenArguments.Contains("speed"))
		{
			Vector3 vector = new Vector3(this.vector2s[0].x, this.vector2s[0].y, 0f);
			Vector3 vector2 = new Vector3(this.vector2s[1].x, this.vector2s[1].y, 0f);
			float num = Math.Abs(Vector3.Distance(vector, vector2));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateFloatTargets()
	{
		this.floats = new float[3];
		this.floats[0] = (float)this.tweenArguments.get_Item("from");
		this.floats[1] = (float)this.tweenArguments.get_Item("to");
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(this.floats[0] - this.floats[1]);
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateColorToTargets()
	{
		if (base.GetComponent<GUITexture>())
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<GUITexture>().get_color());
		}
		else if (base.GetComponent<GUIText>())
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<GUIText>().get_material().get_color());
		}
		else if (base.GetComponent<Renderer>())
		{
			this.colors = new Color[base.GetComponent<Renderer>().get_materials().Length, 3];
			for (int i = 0; i < base.GetComponent<Renderer>().get_materials().Length; i++)
			{
				this.colors[i, 0] = base.GetComponent<Renderer>().get_materials()[i].GetColor(this.namedcolorvalue.ToString());
				this.colors[i, 1] = base.GetComponent<Renderer>().get_materials()[i].GetColor(this.namedcolorvalue.ToString());
			}
		}
		else if (base.GetComponent<Light>())
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<Light>().get_color());
		}
		else
		{
			this.colors = new Color[1, 3];
		}
		if (this.tweenArguments.Contains("color"))
		{
			for (int j = 0; j < this.colors.GetLength(0); j++)
			{
				this.colors[j, 1] = (Color)this.tweenArguments.get_Item("color");
			}
		}
		else
		{
			if (this.tweenArguments.Contains("r"))
			{
				for (int k = 0; k < this.colors.GetLength(0); k++)
				{
					this.colors[k, 1].r = (float)this.tweenArguments.get_Item("r");
				}
			}
			if (this.tweenArguments.Contains("g"))
			{
				for (int l = 0; l < this.colors.GetLength(0); l++)
				{
					this.colors[l, 1].g = (float)this.tweenArguments.get_Item("g");
				}
			}
			if (this.tweenArguments.Contains("b"))
			{
				for (int m = 0; m < this.colors.GetLength(0); m++)
				{
					this.colors[m, 1].b = (float)this.tweenArguments.get_Item("b");
				}
			}
			if (this.tweenArguments.Contains("a"))
			{
				for (int n = 0; n < this.colors.GetLength(0); n++)
				{
					this.colors[n, 1].a = (float)this.tweenArguments.get_Item("a");
				}
			}
		}
		if (this.tweenArguments.Contains("amount"))
		{
			for (int num = 0; num < this.colors.GetLength(0); num++)
			{
				this.colors[num, 1].a = (float)this.tweenArguments.get_Item("amount");
			}
		}
		else if (this.tweenArguments.Contains("alpha"))
		{
			for (int num2 = 0; num2 < this.colors.GetLength(0); num2++)
			{
				this.colors[num2, 1].a = (float)this.tweenArguments.get_Item("alpha");
			}
		}
	}

	private void GenerateAudioToTargets()
	{
		this.vector2s = new Vector2[3];
		if (this.tweenArguments.Contains("audiosource"))
		{
			this.audioSource = (AudioSource)this.tweenArguments.get_Item("audiosource");
		}
		else if (base.GetComponent<AudioSource>())
		{
			this.audioSource = base.GetComponent<AudioSource>();
		}
		else
		{
			Debuger.Error("iTween Error: AudioTo requires an AudioSource.", new object[0]);
			this.Dispose();
		}
		this.vector2s[0] = (this.vector2s[1] = new Vector2(this.audioSource.get_volume(), this.audioSource.get_pitch()));
		if (this.tweenArguments.Contains("volume"))
		{
			this.vector2s[1].x = (float)this.tweenArguments.get_Item("volume");
		}
		if (this.tweenArguments.Contains("pitch"))
		{
			this.vector2s[1].y = (float)this.tweenArguments.get_Item("pitch");
		}
	}

	private void GenerateStabTargets()
	{
		if (this.tweenArguments.Contains("audiosource"))
		{
			this.audioSource = (AudioSource)this.tweenArguments.get_Item("audiosource");
		}
		else if (base.GetComponent<AudioSource>())
		{
			this.audioSource = base.GetComponent<AudioSource>();
		}
		else
		{
			base.get_gameObject().AddComponent<AudioSource>();
			this.audioSource = base.GetComponent<AudioSource>();
			this.audioSource.set_playOnAwake(false);
		}
		this.audioSource.set_clip((AudioClip)this.tweenArguments.get_Item("audioclip"));
		if (this.tweenArguments.Contains("pitch"))
		{
			this.audioSource.set_pitch((float)this.tweenArguments.get_Item("pitch"));
		}
		if (this.tweenArguments.Contains("volume"))
		{
			this.audioSource.set_volume((float)this.tweenArguments.get_Item("volume"));
		}
		this.time = this.audioSource.get_clip().get_length() / this.audioSource.get_pitch();
	}

	private void GenerateLookToTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.get_eulerAngles();
		if (this.tweenArguments.Contains("looktarget"))
		{
			if (this.tweenArguments.get_Item("looktarget").GetType() == typeof(Transform))
			{
				Transform arg_AF_0 = this.thisTransform;
				Transform arg_AF_1 = (Transform)this.tweenArguments.get_Item("looktarget");
				Vector3? vector = (Vector3?)this.tweenArguments.get_Item("up");
				arg_AF_0.LookAt(arg_AF_1, (!vector.get_HasValue()) ? iTween.Defaults.up : vector.get_Value());
			}
			else if (this.tweenArguments.get_Item("looktarget").GetType() == typeof(Vector3))
			{
				Transform arg_12B_0 = this.thisTransform;
				Vector3 arg_12B_1 = (Vector3)this.tweenArguments.get_Item("looktarget");
				Vector3? vector2 = (Vector3?)this.tweenArguments.get_Item("up");
				arg_12B_0.LookAt(arg_12B_1, (!vector2.get_HasValue()) ? iTween.Defaults.up : vector2.get_Value());
			}
		}
		else
		{
			Debuger.Error("iTween Error: LookTo needs a 'looktarget' property!", new object[0]);
			this.Dispose();
		}
		this.vector3s[1] = this.thisTransform.get_eulerAngles();
		this.thisTransform.set_eulerAngles(this.vector3s[0]);
		if (this.tweenArguments.Contains("axis"))
		{
			string text = (string)this.tweenArguments.get_Item("axis");
			if (text != null)
			{
				if (iTween.<>f__switch$mapC == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("x", 0);
					dictionary.Add("y", 1);
					dictionary.Add("z", 2);
					iTween.<>f__switch$mapC = dictionary;
				}
				int num;
				if (iTween.<>f__switch$mapC.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						this.vector3s[1].y = this.vector3s[0].y;
						this.vector3s[1].z = this.vector3s[0].z;
						break;
					case 1:
						this.vector3s[1].x = this.vector3s[0].x;
						this.vector3s[1].z = this.vector3s[0].z;
						break;
					case 2:
						this.vector3s[1].x = this.vector3s[0].x;
						this.vector3s[1].y = this.vector3s[0].y;
						break;
					}
				}
			}
		}
		this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
		if (this.tweenArguments.Contains("speed"))
		{
			float num2 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num2 / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateMoveToPathTargets()
	{
		Vector3[] array2;
		if (this.tweenArguments.get_Item("path").GetType() == typeof(Vector3[]))
		{
			Vector3[] array = (Vector3[])this.tweenArguments.get_Item("path");
			if (array.Length == 1)
			{
				Debuger.Error("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!", new object[0]);
				this.Dispose();
			}
			array2 = new Vector3[array.Length];
			Array.Copy(array, array2, array.Length);
		}
		else
		{
			Transform[] array3 = (Transform[])this.tweenArguments.get_Item("path");
			if (array3.Length == 1)
			{
				Debuger.Error("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!", new object[0]);
				this.Dispose();
			}
			array2 = new Vector3[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array2[i] = array3[i].get_position();
			}
		}
		bool flag;
		int num;
		if (this.thisTransform.get_position() != array2[0])
		{
			if (!this.tweenArguments.Contains("movetopath") || (bool)this.tweenArguments.get_Item("movetopath"))
			{
				flag = true;
				num = 3;
			}
			else
			{
				flag = false;
				num = 2;
			}
		}
		else
		{
			flag = false;
			num = 2;
		}
		this.vector3s = new Vector3[array2.Length + num];
		if (flag)
		{
			this.vector3s[1] = this.thisTransform.get_position();
			num = 2;
		}
		else
		{
			num = 1;
		}
		Array.Copy(array2, 0, this.vector3s, num, array2.Length);
		this.vector3s[0] = this.vector3s[1] + (this.vector3s[1] - this.vector3s[2]);
		this.vector3s[this.vector3s.Length - 1] = this.vector3s[this.vector3s.Length - 2] + (this.vector3s[this.vector3s.Length - 2] - this.vector3s[this.vector3s.Length - 3]);
		if (this.vector3s[1] == this.vector3s[this.vector3s.Length - 2])
		{
			Vector3[] array4 = new Vector3[this.vector3s.Length];
			Array.Copy(this.vector3s, array4, this.vector3s.Length);
			array4[0] = array4[array4.Length - 3];
			array4[array4.Length - 1] = array4[2];
			this.vector3s = new Vector3[array4.Length];
			Array.Copy(array4, this.vector3s, array4.Length);
		}
		this.path = new iTween.CRSpline(this.vector3s);
		if (this.tweenArguments.Contains("speed"))
		{
			float num2 = iTween.PathLength(this.vector3s);
			this.time = num2 / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateMoveToTargets()
	{
		this.vector3s = new Vector3[3];
		if (this.isLocal)
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.get_localPosition());
		}
		else
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.get_position());
		}
		if (this.tweenArguments.Contains("position"))
		{
			if (this.tweenArguments.get_Item("position").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments.get_Item("position");
				this.vector3s[1] = transform.get_position();
			}
			else if (this.tweenArguments.get_Item("position").GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("position");
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments.get_Item("orienttopath"))
		{
			this.tweenArguments.set_Item("looktarget", this.vector3s[1]);
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateMoveByTargets()
	{
		this.vector3s = new Vector3[6];
		this.vector3s[4] = this.thisTransform.get_eulerAngles();
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.get_position()));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = this.vector3s[0] + (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = this.vector3s[0].x + (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = this.vector3s[0].y + (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = this.vector3s[0].z + (float)this.tweenArguments.get_Item("z");
			}
		}
		this.thisTransform.Translate(this.vector3s[1], this.space);
		this.vector3s[5] = this.thisTransform.get_position();
		this.thisTransform.set_position(this.vector3s[0]);
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments.get_Item("orienttopath"))
		{
			this.tweenArguments.set_Item("looktarget", this.vector3s[1]);
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateScaleToTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.thisTransform.get_localScale());
		if (this.tweenArguments.Contains("scale"))
		{
			if (this.tweenArguments.get_Item("scale").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments.get_Item("scale");
				this.vector3s[1] = transform.get_localScale();
			}
			else if (this.tweenArguments.get_Item("scale").GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("scale");
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateScaleByTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.thisTransform.get_localScale());
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = Vector3.Scale(this.vector3s[1], (Vector3)this.tweenArguments.get_Item("amount"));
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] expr_B3_cp_0 = this.vector3s;
				int expr_B3_cp_1 = 1;
				expr_B3_cp_0[expr_B3_cp_1].x = expr_B3_cp_0[expr_B3_cp_1].x * (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] expr_F5_cp_0 = this.vector3s;
				int expr_F5_cp_1 = 1;
				expr_F5_cp_0[expr_F5_cp_1].y = expr_F5_cp_0[expr_F5_cp_1].y * (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] expr_137_cp_0 = this.vector3s;
				int expr_137_cp_1 = 1;
				expr_137_cp_0[expr_137_cp_1].z = expr_137_cp_0[expr_137_cp_1].z * (float)this.tweenArguments.get_Item("z");
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateScaleAddTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.thisTransform.get_localScale());
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] expr_A8_cp_0 = this.vector3s;
				int expr_A8_cp_1 = 1;
				expr_A8_cp_0[expr_A8_cp_1].x = expr_A8_cp_0[expr_A8_cp_1].x + (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] expr_EA_cp_0 = this.vector3s;
				int expr_EA_cp_1 = 1;
				expr_EA_cp_0[expr_EA_cp_1].y = expr_EA_cp_0[expr_EA_cp_1].y + (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] expr_12C_cp_0 = this.vector3s;
				int expr_12C_cp_1 = 1;
				expr_12C_cp_0[expr_12C_cp_1].z = expr_12C_cp_0[expr_12C_cp_1].z + (float)this.tweenArguments.get_Item("z");
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateRotateToTargets()
	{
		this.vector3s = new Vector3[3];
		if (this.isLocal)
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.get_localEulerAngles());
		}
		else
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.get_eulerAngles());
		}
		if (this.tweenArguments.Contains("rotation"))
		{
			if (this.tweenArguments.get_Item("rotation").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments.get_Item("rotation");
				this.vector3s[1] = transform.get_eulerAngles();
			}
			else if (this.tweenArguments.get_Item("rotation").GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("rotation");
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
		this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateRotateAddTargets()
	{
		this.vector3s = new Vector3[5];
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.get_eulerAngles()));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] expr_BC_cp_0 = this.vector3s;
				int expr_BC_cp_1 = 1;
				expr_BC_cp_0[expr_BC_cp_1].x = expr_BC_cp_0[expr_BC_cp_1].x + (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] expr_FE_cp_0 = this.vector3s;
				int expr_FE_cp_1 = 1;
				expr_FE_cp_0[expr_FE_cp_1].y = expr_FE_cp_0[expr_FE_cp_1].y + (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] expr_140_cp_0 = this.vector3s;
				int expr_140_cp_1 = 1;
				expr_140_cp_0[expr_140_cp_1].z = expr_140_cp_0[expr_140_cp_1].z + (float)this.tweenArguments.get_Item("z");
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateRotateByTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.get_eulerAngles()));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += Vector3.Scale((Vector3)this.tweenArguments.get_Item("amount"), new Vector3(360f, 360f, 360f));
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] expr_D5_cp_0 = this.vector3s;
				int expr_D5_cp_1 = 1;
				expr_D5_cp_0[expr_D5_cp_1].x = expr_D5_cp_0[expr_D5_cp_1].x + 360f * (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] expr_11D_cp_0 = this.vector3s;
				int expr_11D_cp_1 = 1;
				expr_11D_cp_0[expr_11D_cp_1].y = expr_11D_cp_0[expr_11D_cp_1].y + 360f * (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] expr_165_cp_0 = this.vector3s;
				int expr_165_cp_1 = 1;
				expr_165_cp_0[expr_165_cp_1].z = expr_165_cp_0[expr_165_cp_1].z + 360f * (float)this.tweenArguments.get_Item("z");
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments.get_Item("speed");
		}
	}

	private void GenerateShakePositionTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[3] = this.thisTransform.get_eulerAngles();
		this.vector3s[0] = this.thisTransform.get_position();
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
	}

	private void GenerateShakeScaleTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.get_localScale();
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
	}

	private void GenerateShakeRotationTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.get_eulerAngles();
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
	}

	private void GeneratePunchPositionTargets()
	{
		this.vector3s = new Vector3[5];
		this.vector3s[4] = this.thisTransform.get_eulerAngles();
		this.vector3s[0] = this.thisTransform.get_position();
		this.vector3s[1] = (this.vector3s[3] = Vector3.get_zero());
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
	}

	private void GeneratePunchRotationTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[0] = this.thisTransform.get_eulerAngles();
		this.vector3s[1] = (this.vector3s[3] = Vector3.get_zero());
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
	}

	private void GeneratePunchScaleTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.get_localScale();
		this.vector3s[1] = Vector3.get_zero();
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments.get_Item("amount");
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments.get_Item("x");
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments.get_Item("y");
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments.get_Item("z");
			}
		}
	}

	private void ApplyRectTargets()
	{
		this.rects[2].set_x(this.ease(this.rects[0].get_x(), this.rects[1].get_x(), this.percentage));
		this.rects[2].set_y(this.ease(this.rects[0].get_y(), this.rects[1].get_y(), this.percentage));
		this.rects[2].set_width(this.ease(this.rects[0].get_width(), this.rects[1].get_width(), this.percentage));
		this.rects[2].set_height(this.ease(this.rects[0].get_height(), this.rects[1].get_height(), this.percentage));
		this.tweenArguments.set_Item("onupdateparams", this.rects[2]);
		if (this.percentage == 1f)
		{
			this.tweenArguments.set_Item("onupdateparams", this.rects[1]);
		}
	}

	private void ApplyColorTargets()
	{
		this.colors[0, 2].r = this.ease(this.colors[0, 0].r, this.colors[0, 1].r, this.percentage);
		this.colors[0, 2].g = this.ease(this.colors[0, 0].g, this.colors[0, 1].g, this.percentage);
		this.colors[0, 2].b = this.ease(this.colors[0, 0].b, this.colors[0, 1].b, this.percentage);
		this.colors[0, 2].a = this.ease(this.colors[0, 0].a, this.colors[0, 1].a, this.percentage);
		this.tweenArguments.set_Item("onupdateparams", this.colors[0, 2]);
		if (this.percentage == 1f)
		{
			this.tweenArguments.set_Item("onupdateparams", this.colors[0, 1]);
		}
	}

	private void ApplyVector3Targets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.tweenArguments.set_Item("onupdateparams", this.vector3s[2]);
		if (this.percentage == 1f)
		{
			this.tweenArguments.set_Item("onupdateparams", this.vector3s[1]);
		}
	}

	private void ApplyVector2Targets()
	{
		this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
		this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
		this.tweenArguments.set_Item("onupdateparams", this.vector2s[2]);
		if (this.percentage == 1f)
		{
			this.tweenArguments.set_Item("onupdateparams", this.vector2s[1]);
		}
	}

	private void ApplyFloatTargets()
	{
		this.floats[2] = this.ease(this.floats[0], this.floats[1], this.percentage);
		this.tweenArguments.set_Item("onupdateparams", this.floats[2]);
		if (this.percentage == 1f)
		{
			this.tweenArguments.set_Item("onupdateparams", this.floats[1]);
		}
	}

	private void ApplyColorToTargets()
	{
		for (int i = 0; i < this.colors.GetLength(0); i++)
		{
			this.colors[i, 2].r = this.ease(this.colors[i, 0].r, this.colors[i, 1].r, this.percentage);
			this.colors[i, 2].g = this.ease(this.colors[i, 0].g, this.colors[i, 1].g, this.percentage);
			this.colors[i, 2].b = this.ease(this.colors[i, 0].b, this.colors[i, 1].b, this.percentage);
			this.colors[i, 2].a = this.ease(this.colors[i, 0].a, this.colors[i, 1].a, this.percentage);
		}
		if (base.GetComponent<GUITexture>())
		{
			base.GetComponent<GUITexture>().set_color(this.colors[0, 2]);
		}
		else if (base.GetComponent<GUIText>())
		{
			base.GetComponent<GUIText>().get_material().set_color(this.colors[0, 2]);
		}
		else if (base.GetComponent<Renderer>())
		{
			for (int j = 0; j < this.colors.GetLength(0); j++)
			{
				base.GetComponent<Renderer>().get_materials()[j].SetColor(this.namedcolorvalue.ToString(), this.colors[j, 2]);
			}
		}
		else if (base.GetComponent<Light>())
		{
			base.GetComponent<Light>().set_color(this.colors[0, 2]);
		}
		if (this.percentage == 1f)
		{
			if (base.GetComponent<GUITexture>())
			{
				base.GetComponent<GUITexture>().set_color(this.colors[0, 1]);
			}
			else if (base.GetComponent<GUIText>())
			{
				base.GetComponent<GUIText>().get_material().set_color(this.colors[0, 1]);
			}
			else if (base.GetComponent<Renderer>())
			{
				for (int k = 0; k < this.colors.GetLength(0); k++)
				{
					base.GetComponent<Renderer>().get_materials()[k].SetColor(this.namedcolorvalue.ToString(), this.colors[k, 1]);
				}
			}
			else if (base.GetComponent<Light>())
			{
				base.GetComponent<Light>().set_color(this.colors[0, 1]);
			}
		}
	}

	private void ApplyAudioToTargets()
	{
		this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
		this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
		this.audioSource.set_volume(this.vector2s[2].x);
		this.audioSource.set_pitch(this.vector2s[2].y);
		if (this.percentage == 1f)
		{
			this.audioSource.set_volume(this.vector2s[1].x);
			this.audioSource.set_pitch(this.vector2s[1].y);
		}
	}

	private void ApplyStabTargets()
	{
	}

	private void ApplyMoveToPathTargets()
	{
		this.preUpdate = this.thisTransform.get_position();
		float num = this.ease(0f, 1f, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.set_localPosition(this.path.Interp(Mathf.Clamp(num, 0f, 1f)));
		}
		else
		{
			this.thisTransform.set_position(this.path.Interp(Mathf.Clamp(num, 0f, 1f)));
		}
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments.get_Item("orienttopath"))
		{
			float num2;
			if (this.tweenArguments.Contains("lookahead"))
			{
				num2 = (float)this.tweenArguments.get_Item("lookahead");
			}
			else
			{
				num2 = iTween.Defaults.lookAhead;
			}
			float num3 = this.ease(0f, 1f, Mathf.Min(1f, this.percentage + num2));
			this.tweenArguments.set_Item("looktarget", this.path.Interp(Mathf.Clamp(num3, 0f, 1f)));
		}
		this.postUpdate = this.thisTransform.get_position();
		if (this.physics)
		{
			this.thisTransform.set_position(this.preUpdate);
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	private void ApplyMoveToTargets()
	{
		this.preUpdate = this.thisTransform.get_position();
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.set_localPosition(this.vector3s[2]);
		}
		else
		{
			this.thisTransform.set_position(this.vector3s[2]);
		}
		if (this.percentage == 1f)
		{
			if (this.isLocal)
			{
				this.thisTransform.set_localPosition(this.vector3s[1]);
			}
			else
			{
				this.thisTransform.set_position(this.vector3s[1]);
			}
		}
		this.postUpdate = this.thisTransform.get_position();
		if (this.physics)
		{
			this.thisTransform.set_position(this.preUpdate);
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	private void ApplyMoveByTargets()
	{
		this.preUpdate = this.thisTransform.get_position();
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.thisTransform.get_eulerAngles();
			this.thisTransform.set_eulerAngles(this.vector3s[4]);
		}
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.thisTransform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.thisTransform.set_eulerAngles(eulerAngles);
		}
		this.postUpdate = this.thisTransform.get_position();
		if (this.physics)
		{
			this.thisTransform.set_position(this.preUpdate);
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	private void ApplyScaleToTargets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.thisTransform.set_localScale(this.vector3s[2]);
		if (this.percentage == 1f)
		{
			this.thisTransform.set_localScale(this.vector3s[1]);
		}
	}

	private void ApplyLookToTargets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.set_localRotation(Quaternion.Euler(this.vector3s[2]));
		}
		else
		{
			this.thisTransform.set_rotation(Quaternion.Euler(this.vector3s[2]));
		}
	}

	private void ApplyRotateToTargets()
	{
		this.preUpdate = this.thisTransform.get_eulerAngles();
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.set_localRotation(Quaternion.Euler(this.vector3s[2]));
		}
		else
		{
			this.thisTransform.set_rotation(Quaternion.Euler(this.vector3s[2]));
		}
		if (this.percentage == 1f)
		{
			if (this.isLocal)
			{
				this.thisTransform.set_localRotation(Quaternion.Euler(this.vector3s[1]));
			}
			else
			{
				this.thisTransform.set_rotation(Quaternion.Euler(this.vector3s[1]));
			}
		}
		this.postUpdate = this.thisTransform.get_eulerAngles();
		if (this.physics)
		{
			this.thisTransform.set_eulerAngles(this.preUpdate);
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	private void ApplyRotateAddTargets()
	{
		this.preUpdate = this.thisTransform.get_eulerAngles();
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.thisTransform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		this.postUpdate = this.thisTransform.get_eulerAngles();
		if (this.physics)
		{
			this.thisTransform.set_eulerAngles(this.preUpdate);
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	private void ApplyShakePositionTargets()
	{
		if (this.isLocal)
		{
			this.preUpdate = this.thisTransform.get_localPosition();
		}
		else
		{
			this.preUpdate = this.thisTransform.get_position();
		}
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.thisTransform.get_eulerAngles();
			this.thisTransform.set_eulerAngles(this.vector3s[3]);
		}
		if (this.percentage == 0f)
		{
			this.thisTransform.Translate(this.vector3s[1], this.space);
		}
		if (this.isLocal)
		{
			this.thisTransform.set_localPosition(this.vector3s[0]);
		}
		else
		{
			this.thisTransform.set_position(this.vector3s[0]);
		}
		float num = 1f - this.percentage;
		this.vector3s[2].x = Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		if (this.isLocal)
		{
			Transform expr_1C6 = this.thisTransform;
			expr_1C6.set_localPosition(expr_1C6.get_localPosition() + this.vector3s[2]);
		}
		else
		{
			Transform expr_1F2 = this.thisTransform;
			expr_1F2.set_position(expr_1F2.get_position() + this.vector3s[2]);
		}
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.thisTransform.set_eulerAngles(eulerAngles);
		}
		this.postUpdate = this.thisTransform.get_position();
		if (this.physics)
		{
			this.thisTransform.set_position(this.preUpdate);
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	private void ApplyShakeScaleTargets()
	{
		if (this.percentage == 0f)
		{
			this.thisTransform.set_localScale(this.vector3s[1]);
		}
		this.thisTransform.set_localScale(this.vector3s[0]);
		float num = 1f - this.percentage;
		this.vector3s[2].x = Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		Transform expr_112 = this.thisTransform;
		expr_112.set_localScale(expr_112.get_localScale() + this.vector3s[2]);
	}

	private void ApplyShakeRotationTargets()
	{
		this.preUpdate = this.thisTransform.get_eulerAngles();
		if (this.percentage == 0f)
		{
			this.thisTransform.Rotate(this.vector3s[1], this.space);
		}
		this.thisTransform.set_eulerAngles(this.vector3s[0]);
		float num = 1f - this.percentage;
		this.vector3s[2].x = Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		this.thisTransform.Rotate(this.vector3s[2], this.space);
		this.postUpdate = this.thisTransform.get_eulerAngles();
		if (this.physics)
		{
			this.thisTransform.set_eulerAngles(this.preUpdate);
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	private void ApplyPunchPositionTargets()
	{
		this.preUpdate = this.thisTransform.get_position();
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.thisTransform.get_eulerAngles();
			this.thisTransform.set_eulerAngles(this.vector3s[4]);
		}
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.thisTransform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.thisTransform.set_eulerAngles(eulerAngles);
		}
		this.postUpdate = this.thisTransform.get_position();
		if (this.physics)
		{
			this.thisTransform.set_position(this.preUpdate);
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	private void ApplyPunchRotationTargets()
	{
		this.preUpdate = this.thisTransform.get_eulerAngles();
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.thisTransform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		this.postUpdate = this.thisTransform.get_eulerAngles();
		if (this.physics)
		{
			this.thisTransform.set_eulerAngles(this.preUpdate);
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	private void ApplyPunchScaleTargets()
	{
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.thisTransform.set_localScale(this.vector3s[0] + this.vector3s[2]);
	}

	[DebuggerHidden]
	private IEnumerator TweenDelay()
	{
		iTween.<TweenDelay>c__IteratorE <TweenDelay>c__IteratorE = new iTween.<TweenDelay>c__IteratorE();
		<TweenDelay>c__IteratorE.<>f__this = this;
		return <TweenDelay>c__IteratorE;
	}

	private void TweenStart()
	{
		this.CallBack("onstart");
		if (!this.loop)
		{
			this.ConflictCheck();
			this.GenerateTargets();
		}
		if (this.type == "stab")
		{
			this.audioSource.PlayOneShot(this.audioSource.get_clip());
		}
		if (this.type == "move" || this.type == "scale" || this.type == "rotate" || this.type == "punch" || this.type == "shake" || this.type == "curve" || this.type == "look")
		{
			this.EnableKinematic();
		}
		this.isRunning = true;
	}

	[DebuggerHidden]
	private IEnumerator TweenRestart()
	{
		iTween.<TweenRestart>c__IteratorF <TweenRestart>c__IteratorF = new iTween.<TweenRestart>c__IteratorF();
		<TweenRestart>c__IteratorF.<>f__this = this;
		return <TweenRestart>c__IteratorF;
	}

	private void TweenUpdate()
	{
		this.apply();
		this.CallBack("onupdate");
		this.UpdatePercentage();
	}

	private void TweenComplete()
	{
		this.isRunning = false;
		if (this.percentage > 0.5f)
		{
			this.percentage = 1f;
		}
		else
		{
			this.percentage = 0f;
		}
		this.apply();
		if (this.type == "value")
		{
			this.CallBack("onupdate");
		}
		if (this.loopType == iTween.LoopType.none)
		{
			this.Dispose();
		}
		else
		{
			this.TweenLoop();
		}
		this.CallBack("oncomplete");
	}

	private void TweenLoop()
	{
		this.DisableKinematic();
		iTween.LoopType loopType = this.loopType;
		if (loopType != iTween.LoopType.loop)
		{
			if (loopType == iTween.LoopType.pingPong)
			{
				this.reverse = !this.reverse;
				this.runningTime = 0f;
				base.StartCoroutine("TweenRestart");
			}
		}
		else
		{
			this.percentage = 0f;
			this.runningTime = 0f;
			this.apply();
			base.StartCoroutine("TweenRestart");
		}
	}

	public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
	{
		Rect result = new Rect(iTween.FloatUpdate(currentValue.get_x(), targetValue.get_x(), speed), iTween.FloatUpdate(currentValue.get_y(), targetValue.get_y(), speed), iTween.FloatUpdate(currentValue.get_width(), targetValue.get_width(), speed), iTween.FloatUpdate(currentValue.get_height(), targetValue.get_height(), speed));
		return result;
	}

	public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
	{
		Vector3 vector = targetValue - currentValue;
		currentValue += vector * speed * Time.get_deltaTime();
		return currentValue;
	}

	public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
	{
		Vector2 vector = targetValue - currentValue;
		currentValue += vector * speed * Time.get_deltaTime();
		return currentValue;
	}

	public static float FloatUpdate(float currentValue, float targetValue, float speed)
	{
		float num = targetValue - currentValue;
		currentValue += num * speed * Time.get_deltaTime();
		return currentValue;
	}

	public static void FadeUpdate(GameObject target, Hashtable args)
	{
		args.set_Item("a", args.get_Item("alpha"));
		iTween.ColorUpdate(target, args);
	}

	public static void FadeUpdate(GameObject target, float alpha, float time)
	{
		iTween.FadeUpdate(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	public static void ColorUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Color[] array = new Color[4];
		if (!args.Contains("includechildren") || (bool)args.get_Item("includechildren"))
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					iTween.ColorUpdate(transform.get_gameObject(), args);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		float num;
		if (args.Contains("time"))
		{
			num = (float)args.get_Item("time");
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		if (target.GetComponent<GUITexture>())
		{
			array[0] = (array[1] = target.GetComponent<GUITexture>().get_color());
		}
		else if (target.GetComponent<GUIText>())
		{
			array[0] = (array[1] = target.GetComponent<GUIText>().get_material().get_color());
		}
		else if (target.GetComponent<Renderer>())
		{
			array[0] = (array[1] = target.GetComponent<Renderer>().get_material().get_color());
		}
		else if (target.GetComponent<Light>())
		{
			array[0] = (array[1] = target.GetComponent<Light>().get_color());
		}
		if (args.Contains("color"))
		{
			array[1] = (Color)args.get_Item("color");
		}
		else
		{
			if (args.Contains("r"))
			{
				array[1].r = (float)args.get_Item("r");
			}
			if (args.Contains("g"))
			{
				array[1].g = (float)args.get_Item("g");
			}
			if (args.Contains("b"))
			{
				array[1].b = (float)args.get_Item("b");
			}
			if (args.Contains("a"))
			{
				array[1].a = (float)args.get_Item("a");
			}
		}
		array[3].r = Mathf.SmoothDamp(array[0].r, array[1].r, ref array[2].r, num);
		array[3].g = Mathf.SmoothDamp(array[0].g, array[1].g, ref array[2].g, num);
		array[3].b = Mathf.SmoothDamp(array[0].b, array[1].b, ref array[2].b, num);
		array[3].a = Mathf.SmoothDamp(array[0].a, array[1].a, ref array[2].a, num);
		if (target.GetComponent<GUITexture>())
		{
			target.GetComponent<GUITexture>().set_color(array[3]);
		}
		else if (target.GetComponent<GUIText>())
		{
			target.GetComponent<GUIText>().get_material().set_color(array[3]);
		}
		else if (target.GetComponent<Renderer>())
		{
			target.GetComponent<Renderer>().get_material().set_color(array[3]);
		}
		else if (target.GetComponent<Light>())
		{
			target.GetComponent<Light>().set_color(array[3]);
		}
	}

	public static void ColorUpdate(GameObject target, Color color, float time)
	{
		iTween.ColorUpdate(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	public static void AudioUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector2[] array = new Vector2[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args.get_Item("time");
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		AudioSource audioSource;
		if (args.Contains("audiosource"))
		{
			audioSource = (AudioSource)args.get_Item("audiosource");
		}
		else
		{
			if (!target.GetComponent<AudioSource>())
			{
				Debuger.Error("iTween Error: AudioUpdate requires an AudioSource.", new object[0]);
				return;
			}
			audioSource = target.GetComponent<AudioSource>();
		}
		array[0] = (array[1] = new Vector2(audioSource.get_volume(), audioSource.get_pitch()));
		if (args.Contains("volume"))
		{
			array[1].x = (float)args.get_Item("volume");
		}
		if (args.Contains("pitch"))
		{
			array[1].y = (float)args.get_Item("pitch");
		}
		array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
		audioSource.set_volume(array[3].x);
		audioSource.set_pitch(array[3].y);
	}

	public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioUpdate(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	public static void RotateUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 eulerAngles = target.get_transform().get_eulerAngles();
		float num;
		if (args.Contains("time"))
		{
			num = (float)args.get_Item("time");
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args.get_Item("islocal");
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		if (flag)
		{
			array[0] = target.get_transform().get_localEulerAngles();
		}
		else
		{
			array[0] = target.get_transform().get_eulerAngles();
		}
		if (args.Contains("rotation"))
		{
			if (args.get_Item("rotation").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args.get_Item("rotation");
				array[1] = transform.get_eulerAngles();
			}
			else if (args.get_Item("rotation").GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args.get_Item("rotation");
			}
		}
		array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
		if (flag)
		{
			target.get_transform().set_localEulerAngles(array[3]);
		}
		else
		{
			target.get_transform().set_eulerAngles(array[3]);
		}
		if (target.GetComponent<Rigidbody>() != null)
		{
			Vector3 eulerAngles2 = target.get_transform().get_eulerAngles();
			target.get_transform().set_eulerAngles(eulerAngles);
			target.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(eulerAngles2));
		}
	}

	public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateUpdate(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	public static void ScaleUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args.get_Item("time");
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		array[0] = (array[1] = target.get_transform().get_localScale());
		if (args.Contains("scale"))
		{
			if (args.get_Item("scale").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args.get_Item("scale");
				array[1] = transform.get_localScale();
			}
			else if (args.get_Item("scale").GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args.get_Item("scale");
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				array[1].x = (float)args.get_Item("x");
			}
			if (args.Contains("y"))
			{
				array[1].y = (float)args.get_Item("y");
			}
			if (args.Contains("z"))
			{
				array[1].z = (float)args.get_Item("z");
			}
		}
		array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
		target.get_transform().set_localScale(array[3]);
	}

	public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleUpdate(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	public static void MoveUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 position = target.get_transform().get_position();
		float num;
		if (args.Contains("time"))
		{
			num = (float)args.get_Item("time");
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args.get_Item("islocal");
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		if (flag)
		{
			array[0] = (array[1] = target.get_transform().get_localPosition());
		}
		else
		{
			array[0] = (array[1] = target.get_transform().get_position());
		}
		if (args.Contains("position"))
		{
			if (args.get_Item("position").GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args.get_Item("position");
				array[1] = transform.get_position();
			}
			else if (args.get_Item("position").GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args.get_Item("position");
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				array[1].x = (float)args.get_Item("x");
			}
			if (args.Contains("y"))
			{
				array[1].y = (float)args.get_Item("y");
			}
			if (args.Contains("z"))
			{
				array[1].z = (float)args.get_Item("z");
			}
		}
		array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
		if (args.Contains("orienttopath") && (bool)args.get_Item("orienttopath"))
		{
			args.set_Item("looktarget", array[3]);
		}
		if (args.Contains("looktarget"))
		{
			iTween.LookUpdate(target, args);
		}
		if (flag)
		{
			target.get_transform().set_localPosition(array[3]);
		}
		else
		{
			target.get_transform().set_position(array[3]);
		}
		if (target.GetComponent<Rigidbody>() != null)
		{
			Vector3 position2 = target.get_transform().get_position();
			target.get_transform().set_position(position);
			target.GetComponent<Rigidbody>().MovePosition(position2);
		}
	}

	public static void MoveUpdate(GameObject target, Vector3 position, float time)
	{
		iTween.MoveUpdate(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	public static void LookUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[5];
		float num;
		if (args.Contains("looktime"))
		{
			num = (float)args.get_Item("looktime");
			num *= iTween.Defaults.updateTimePercentage;
		}
		else if (args.Contains("time"))
		{
			num = (float)args.get_Item("time") * 0.15f;
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		array[0] = target.get_transform().get_eulerAngles();
		if (args.Contains("looktarget"))
		{
			if (args.get_Item("looktarget").GetType() == typeof(Transform))
			{
				Transform arg_100_0 = target.get_transform();
				Transform arg_100_1 = (Transform)args.get_Item("looktarget");
				Vector3? vector = (Vector3?)args.get_Item("up");
				arg_100_0.LookAt(arg_100_1, (!vector.get_HasValue()) ? iTween.Defaults.up : vector.get_Value());
			}
			else if (args.get_Item("looktarget").GetType() == typeof(Vector3))
			{
				Transform arg_16D_0 = target.get_transform();
				Vector3 arg_16D_1 = (Vector3)args.get_Item("looktarget");
				Vector3? vector2 = (Vector3?)args.get_Item("up");
				arg_16D_0.LookAt(arg_16D_1, (!vector2.get_HasValue()) ? iTween.Defaults.up : vector2.get_Value());
			}
			array[1] = target.get_transform().get_eulerAngles();
			target.get_transform().set_eulerAngles(array[0]);
			array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
			target.get_transform().set_eulerAngles(array[3]);
			if (args.Contains("axis"))
			{
				array[4] = target.get_transform().get_eulerAngles();
				string text = (string)args.get_Item("axis");
				if (text != null)
				{
					if (iTween.<>f__switch$mapD == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
						dictionary.Add("x", 0);
						dictionary.Add("y", 1);
						dictionary.Add("z", 2);
						iTween.<>f__switch$mapD = dictionary;
					}
					int num2;
					if (iTween.<>f__switch$mapD.TryGetValue(text, ref num2))
					{
						switch (num2)
						{
						case 0:
							array[4].y = array[0].y;
							array[4].z = array[0].z;
							break;
						case 1:
							array[4].x = array[0].x;
							array[4].z = array[0].z;
							break;
						case 2:
							array[4].x = array[0].x;
							array[4].y = array[0].y;
							break;
						}
					}
				}
				target.get_transform().set_eulerAngles(array[4]);
			}
			return;
		}
		Debuger.Error("iTween Error: LookUpdate needs a 'looktarget' property!", new object[0]);
	}

	public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookUpdate(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	public static float PathLength(Transform[] path)
	{
		Vector3[] array = new Vector3[path.Length];
		float num = 0f;
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].get_position();
		}
		Vector3[] pts = iTween.PathControlPointGenerator(array);
		Vector3 vector = iTween.Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int j = 1; j <= num2; j++)
		{
			float t = (float)j / (float)num2;
			Vector3 vector2 = iTween.Interp(pts, t);
			num += Vector3.Distance(vector, vector2);
			vector = vector2;
		}
		return num;
	}

	public static float PathLength(Vector3[] path)
	{
		float num = 0f;
		Vector3[] pts = iTween.PathControlPointGenerator(path);
		Vector3 vector = iTween.Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int i = 1; i <= num2; i++)
		{
			float t = (float)i / (float)num2;
			Vector3 vector2 = iTween.Interp(pts, t);
			num += Vector3.Distance(vector, vector2);
			vector = vector2;
		}
		return num;
	}

	public static Texture2D CameraTexture(Color color)
	{
		Texture2D texture2D = new Texture2D(Screen.get_width(), Screen.get_height(), 5, false);
		Color[] array = new Color[Screen.get_width() * Screen.get_height()];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		texture2D.SetPixels(array);
		texture2D.Apply();
		return texture2D;
	}

	public static void PutOnPath(GameObject target, Vector3[] path, float percent)
	{
		target.get_transform().set_position(iTween.Interp(iTween.PathControlPointGenerator(path), percent));
	}

	public static void PutOnPath(Transform target, Vector3[] path, float percent)
	{
		target.set_position(iTween.Interp(iTween.PathControlPointGenerator(path), percent));
	}

	public static void PutOnPath(GameObject target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].get_position();
		}
		target.get_transform().set_position(iTween.Interp(iTween.PathControlPointGenerator(array), percent));
	}

	public static void PutOnPath(Transform target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].get_position();
		}
		target.set_position(iTween.Interp(iTween.PathControlPointGenerator(array), percent));
	}

	public static Vector3 PointOnPath(Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].get_position();
		}
		return iTween.Interp(iTween.PathControlPointGenerator(array), percent);
	}

	public static void DrawLine(Vector3[] line)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawLine(Vector3[] line, Color color)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, color, "gizmos");
		}
	}

	public static void DrawLine(Transform[] line)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].get_position();
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawLine(Transform[] line, Color color)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].get_position();
			}
			iTween.DrawLineHelper(array, color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Vector3[] line)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Vector3[] line, Color color)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Transform[] line)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].get_position();
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Transform[] line, Color color)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].get_position();
			}
			iTween.DrawLineHelper(array, color, "gizmos");
		}
	}

	public static void DrawLineHandles(Vector3[] line)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "handles");
		}
	}

	public static void DrawLineHandles(Vector3[] line, Color color)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, color, "handles");
		}
	}

	public static void DrawLineHandles(Transform[] line)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].get_position();
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "handles");
		}
	}

	public static void DrawLineHandles(Transform[] line, Color color)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].get_position();
			}
			iTween.DrawLineHelper(array, color, "handles");
		}
	}

	public static Vector3 PointOnPath(Vector3[] path, float percent)
	{
		return iTween.Interp(iTween.PathControlPointGenerator(path), percent);
	}

	public static void DrawPath(Vector3[] path)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawPath(Vector3[] path, Color color)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, color, "gizmos");
		}
	}

	public static void DrawPath(Transform[] path)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].get_position();
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawPath(Transform[] path, Color color)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].get_position();
			}
			iTween.DrawPathHelper(array, color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Vector3[] path)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Vector3[] path, Color color)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Transform[] path)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].get_position();
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Transform[] path, Color color)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].get_position();
			}
			iTween.DrawPathHelper(array, color, "gizmos");
		}
	}

	public static void DrawPathHandles(Vector3[] path)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "handles");
		}
	}

	public static void DrawPathHandles(Vector3[] path, Color color)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, color, "handles");
		}
	}

	public static void DrawPathHandles(Transform[] path)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].get_position();
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "handles");
		}
	}

	public static void DrawPathHandles(Transform[] path, Color color)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].get_position();
			}
			iTween.DrawPathHelper(array, color, "handles");
		}
	}

	public static void CameraFadeDepth(int depth)
	{
		if (iTween.cameraFade)
		{
			iTween.cameraFade.get_transform().set_position(new Vector3(iTween.cameraFade.get_transform().get_position().x, iTween.cameraFade.get_transform().get_position().y, (float)depth));
		}
	}

	public static void CameraFadeDestroy()
	{
		if (iTween.cameraFade)
		{
			Object.Destroy(iTween.cameraFade);
		}
	}

	public static void CameraFadeSwap(Texture2D texture)
	{
		if (iTween.cameraFade)
		{
			iTween.cameraFade.GetComponent<GUITexture>().set_texture(texture);
		}
	}

	public static GameObject CameraFadeAdd(Texture2D texture, int depth)
	{
		if (iTween.cameraFade)
		{
			return null;
		}
		iTween.cameraFade = new GameObject("iTween Camera Fade");
		iTween.cameraFade.get_transform().set_position(new Vector3(0.5f, 0.5f, (float)depth));
		iTween.cameraFade.AddComponent<GUITexture>();
		iTween.cameraFade.GetComponent<GUITexture>().set_texture(texture);
		iTween.cameraFade.GetComponent<GUITexture>().set_color(new Color(0.5f, 0.5f, 0.5f, 0f));
		return iTween.cameraFade;
	}

	public static GameObject CameraFadeAdd(Texture2D texture)
	{
		if (iTween.cameraFade)
		{
			return null;
		}
		iTween.cameraFade = new GameObject("iTween Camera Fade");
		iTween.cameraFade.get_transform().set_position(new Vector3(0.5f, 0.5f, (float)iTween.Defaults.cameraFadeDepth));
		iTween.cameraFade.AddComponent<GUITexture>();
		iTween.cameraFade.GetComponent<GUITexture>().set_texture(texture);
		iTween.cameraFade.GetComponent<GUITexture>().set_color(new Color(0.5f, 0.5f, 0.5f, 0f));
		return iTween.cameraFade;
	}

	public static GameObject CameraFadeAdd()
	{
		if (iTween.cameraFade)
		{
			return null;
		}
		iTween.cameraFade = new GameObject("iTween Camera Fade");
		iTween.cameraFade.get_transform().set_position(new Vector3(0.5f, 0.5f, (float)iTween.Defaults.cameraFadeDepth));
		iTween.cameraFade.AddComponent<GUITexture>();
		iTween.cameraFade.GetComponent<GUITexture>().set_texture(iTween.CameraTexture(Color.get_black()));
		iTween.cameraFade.GetComponent<GUITexture>().set_color(new Color(0.5f, 0.5f, 0.5f, 0f));
		return iTween.cameraFade;
	}

	public static void Resume(GameObject target)
	{
		Component[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i] as iTween;
			iTween.set_enabled(true);
		}
	}

	public static void Resume(GameObject target, bool includechildren)
	{
		iTween.Resume(target);
		if (includechildren)
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					iTween.Resume(transform.get_gameObject(), true);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public static void Resume(GameObject target, string type)
	{
		Component[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i] as iTween;
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				iTween.set_enabled(true);
			}
		}
	}

	public static void Resume(GameObject target, string type, bool includechildren)
	{
		Component[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i] as iTween;
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				iTween.set_enabled(true);
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					iTween.Resume(transform.get_gameObject(), type, true);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public static void Resume()
	{
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			GameObject target = (GameObject)hashtable.get_Item("target");
			iTween.Resume(target);
		}
	}

	public static void Resume(string type)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			GameObject gameObject = (GameObject)hashtable.get_Item("target");
			arrayList.Insert(arrayList.get_Count(), gameObject);
		}
		for (int j = 0; j < arrayList.get_Count(); j++)
		{
			iTween.Resume((GameObject)arrayList.get_Item(j), type);
		}
	}

	public static void Pause(GameObject target)
	{
		iTween[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i];
			if (iTween.delay > 0f)
			{
				iTween.delay -= Time.get_time() - iTween.delayStarted;
				iTween.StopCoroutine("TweenDelay");
			}
			iTween.isPaused = true;
			iTween.set_enabled(false);
		}
	}

	public static void Pause(GameObject target, bool includechildren)
	{
		iTween.Pause(target);
		if (includechildren)
		{
			for (int i = 0; i < target.get_transform().get_childCount(); i++)
			{
				iTween.Pause(target.get_transform().GetChild(i).get_gameObject(), true);
			}
		}
	}

	public static void Pause(GameObject target, string type)
	{
		Component[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i] as iTween;
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				if (iTween.delay > 0f)
				{
					iTween.delay -= Time.get_time() - iTween.delayStarted;
					iTween.StopCoroutine("TweenDelay");
				}
				iTween.isPaused = true;
				iTween.set_enabled(false);
			}
		}
	}

	public static void Pause(GameObject target, string type, bool includechildren)
	{
		iTween[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i];
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				if (iTween.delay > 0f)
				{
					iTween.delay -= Time.get_time() - iTween.delayStarted;
					iTween.StopCoroutine("TweenDelay");
				}
				iTween.isPaused = true;
				iTween.set_enabled(false);
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					iTween.Pause(transform.get_gameObject(), type, true);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public static void Pause()
	{
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			GameObject target = (GameObject)hashtable.get_Item("target");
			iTween.Pause(target);
		}
	}

	public static void Pause(string type)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			GameObject gameObject = (GameObject)hashtable.get_Item("target");
			arrayList.Insert(arrayList.get_Count(), gameObject);
		}
		for (int j = 0; j < arrayList.get_Count(); j++)
		{
			iTween.Pause((GameObject)arrayList.get_Item(j), type);
		}
	}

	public static int Count()
	{
		return iTween.tweens.get_Count();
	}

	public static int Count(string type)
	{
		int num = 0;
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			string text = (string)hashtable.get_Item("type") + (string)hashtable.get_Item("method");
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	public static int Count(GameObject target)
	{
		Component[] components = target.GetComponents<iTween>();
		return components.Length;
	}

	public static int Count(GameObject target, string type)
	{
		int num = 0;
		iTween[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i];
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	public static void Stop()
	{
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			GameObject target = (GameObject)hashtable.get_Item("target");
			iTween.Stop(target);
		}
		iTween.tweens.Clear();
	}

	public static void Stop(string type)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			GameObject gameObject = (GameObject)hashtable.get_Item("target");
			arrayList.Insert(arrayList.get_Count(), gameObject);
		}
		for (int j = 0; j < arrayList.get_Count(); j++)
		{
			iTween.Stop((GameObject)arrayList.get_Item(j), type);
		}
	}

	public static void StopByName(string name)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			GameObject gameObject = (GameObject)hashtable.get_Item("target");
			arrayList.Insert(arrayList.get_Count(), gameObject);
		}
		for (int j = 0; j < arrayList.get_Count(); j++)
		{
			iTween.StopByName((GameObject)arrayList.get_Item(j), name);
		}
	}

	public static void Stop(GameObject target)
	{
		iTween[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].Dispose();
		}
	}

	public static void Stop(GameObject target, bool includechildren)
	{
		iTween.Stop(target);
		if (includechildren)
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					iTween.Stop(transform.get_gameObject(), true);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public static void Stop(GameObject target, string type)
	{
		Component[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i] as iTween;
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				iTween.Dispose();
			}
		}
	}

	public static void StopByName(GameObject target, string name)
	{
		iTween[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i];
			if (iTween._name == name)
			{
				iTween.Dispose();
			}
		}
	}

	public static void Stop(GameObject target, string type, bool includechildren)
	{
		iTween[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i];
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.get_Length());
			if (text.ToLower() == type.ToLower())
			{
				iTween.Dispose();
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					iTween.Stop(transform.get_gameObject(), type, true);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public static void StopByName(GameObject target, string name, bool includechildren)
	{
		iTween[] components = target.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i];
			if (iTween._name == name)
			{
				iTween.Dispose();
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					iTween.StopByName(transform.get_gameObject(), name, true);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public static Hashtable Hash(params object[] args)
	{
		Hashtable hashtable = new Hashtable(args.Length / 2);
		if (args.Length % 2 != 0)
		{
			Debuger.Error("Tween Error: Hash requires an even number of arguments!", new object[0]);
			return null;
		}
		for (int i = 0; i < args.Length - 1; i += 2)
		{
			hashtable.Add(args[i], args[i + 1]);
		}
		return hashtable;
	}

	private void Awake()
	{
		this.thisTransform = base.get_transform();
		this.RetrieveArgs();
		this.lastRealTime = Time.get_realtimeSinceStartup();
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		iTween.<Start>c__Iterator10 <Start>c__Iterator = new iTween.<Start>c__Iterator10();
		<Start>c__Iterator.<>f__this = this;
		return <Start>c__Iterator;
	}

	private void Update()
	{
		if (this.isRunning && !this.physics)
		{
			if (!this.reverse)
			{
				if (this.percentage < 1f)
				{
					this.TweenUpdate();
				}
				else
				{
					this.TweenComplete();
				}
			}
			else if (this.percentage > 0f)
			{
				this.TweenUpdate();
			}
			else
			{
				this.TweenComplete();
			}
		}
	}

	private void FixedUpdate()
	{
		if (this.isRunning && this.physics)
		{
			if (!this.reverse)
			{
				if (this.percentage < 1f)
				{
					this.TweenUpdate();
				}
				else
				{
					this.TweenComplete();
				}
			}
			else if (this.percentage > 0f)
			{
				this.TweenUpdate();
			}
			else
			{
				this.TweenComplete();
			}
		}
	}

	private void LateUpdate()
	{
		if (this.tweenArguments.Contains("looktarget") && this.isRunning && (this.type == "move" || this.type == "shake" || this.type == "punch"))
		{
			iTween.LookUpdate(base.get_gameObject(), this.tweenArguments);
		}
	}

	private void OnEnable()
	{
		if (this.isRunning)
		{
			this.EnableKinematic();
		}
		if (this.isPaused)
		{
			this.isPaused = false;
			if (this.delay > 0f)
			{
				this.wasPaused = true;
				this.ResumeDelay();
			}
		}
	}

	private void OnDisable()
	{
		this.DisableKinematic();
	}

	private static void DrawLineHelper(Vector3[] line, Color color, string method)
	{
		Gizmos.set_color(color);
		for (int i = 0; i < line.Length - 1; i++)
		{
			if (method == "gizmos")
			{
				Gizmos.DrawLine(line[i], line[i + 1]);
			}
			else if (method == "handles")
			{
				Debuger.Error("iTween Error: Drawing a line with Handles is temporarily disabled because of compatability issues with Unity 2.6!", new object[0]);
			}
		}
	}

	private static void DrawPathHelper(Vector3[] path, Color color, string method)
	{
		Vector3[] pts = iTween.PathControlPointGenerator(path);
		Vector3 vector = iTween.Interp(pts, 0f);
		Gizmos.set_color(color);
		int num = path.Length * 20;
		for (int i = 1; i <= num; i++)
		{
			float t = (float)i / (float)num;
			Vector3 vector2 = iTween.Interp(pts, t);
			if (method == "gizmos")
			{
				Gizmos.DrawLine(vector2, vector);
			}
			else if (method == "handles")
			{
				Debuger.Error("iTween Error: Drawing a path with Handles is temporarily disabled because of compatability issues with Unity 2.6!", new object[0]);
			}
			vector = vector2;
		}
	}

	private static Vector3[] PathControlPointGenerator(Vector3[] path)
	{
		int num = 2;
		Vector3[] array = new Vector3[path.Length + num];
		Array.Copy(path, 0, array, 1, path.Length);
		array[0] = array[1] + (array[1] - array[2]);
		array[array.Length - 1] = array[array.Length - 2] + (array[array.Length - 2] - array[array.Length - 3]);
		if (array[1] == array[array.Length - 2])
		{
			Vector3[] array2 = new Vector3[array.Length];
			Array.Copy(array, array2, array.Length);
			array2[0] = array2[array2.Length - 3];
			array2[array2.Length - 1] = array2[2];
			array = new Vector3[array2.Length];
			Array.Copy(array2, array, array2.Length);
		}
		return array;
	}

	private static Vector3 Interp(Vector3[] pts, float t)
	{
		int num = pts.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 vector = pts[num2];
		Vector3 vector2 = pts[num2 + 1];
		Vector3 vector3 = pts[num2 + 2];
		Vector3 vector4 = pts[num2 + 3];
		return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
	}

	private static void Launch(GameObject target, Hashtable args)
	{
		if (!args.Contains("id"))
		{
			args.set_Item("id", iTween.GenerateID());
		}
		if (!args.Contains("target"))
		{
			args.set_Item("target", target);
		}
		iTween.tweens.Insert(0, args);
		target.AddComponent<iTween>();
	}

	private static Hashtable CleanArgs(Hashtable args)
	{
		Hashtable hashtable = new Hashtable(args.get_Count());
		Hashtable hashtable2 = new Hashtable(args.get_Count());
		IDictionaryEnumerator enumerator = args.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.get_Current();
				hashtable.Add(dictionaryEntry.get_Key(), dictionaryEntry.get_Value());
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		IDictionaryEnumerator enumerator2 = hashtable.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)enumerator2.get_Current();
				if (dictionaryEntry2.get_Value().GetType() == typeof(int))
				{
					int num = (int)dictionaryEntry2.get_Value();
					float num2 = (float)num;
					args.set_Item(dictionaryEntry2.get_Key(), num2);
				}
				if (dictionaryEntry2.get_Value().GetType() == typeof(double))
				{
					double num3 = (double)dictionaryEntry2.get_Value();
					float num4 = (float)num3;
					args.set_Item(dictionaryEntry2.get_Key(), num4);
				}
			}
		}
		finally
		{
			IDisposable disposable2 = enumerator2 as IDisposable;
			if (disposable2 != null)
			{
				disposable2.Dispose();
			}
		}
		IDictionaryEnumerator enumerator3 = args.GetEnumerator();
		try
		{
			while (enumerator3.MoveNext())
			{
				DictionaryEntry dictionaryEntry3 = (DictionaryEntry)enumerator3.get_Current();
				hashtable2.Add(dictionaryEntry3.get_Key().ToString().ToLower(), dictionaryEntry3.get_Value());
			}
		}
		finally
		{
			IDisposable disposable3 = enumerator3 as IDisposable;
			if (disposable3 != null)
			{
				disposable3.Dispose();
			}
		}
		args = hashtable2;
		return args;
	}

	private static string GenerateID()
	{
		return Guid.NewGuid().ToString();
	}

	private void RetrieveArgs()
	{
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			if ((GameObject)hashtable.get_Item("target") == base.get_gameObject())
			{
				this.tweenArguments = hashtable;
				break;
			}
		}
		this.id = (string)this.tweenArguments.get_Item("id");
		this.type = (string)this.tweenArguments.get_Item("type");
		this._name = (string)this.tweenArguments.get_Item("name");
		this.method = (string)this.tweenArguments.get_Item("method");
		if (this.tweenArguments.Contains("time"))
		{
			this.time = (float)this.tweenArguments.get_Item("time");
		}
		else
		{
			this.time = iTween.Defaults.time;
		}
		if (base.GetComponent<Rigidbody>() != null)
		{
			this.physics = true;
		}
		if (this.tweenArguments.Contains("delay"))
		{
			this.delay = (float)this.tweenArguments.get_Item("delay");
		}
		else
		{
			this.delay = iTween.Defaults.delay;
		}
		if (this.tweenArguments.Contains("namedcolorvalue"))
		{
			if (this.tweenArguments.get_Item("namedcolorvalue").GetType() == typeof(iTween.NamedValueColor))
			{
				this.namedcolorvalue = (iTween.NamedValueColor)((int)this.tweenArguments.get_Item("namedcolorvalue"));
			}
			else
			{
				try
				{
					this.namedcolorvalue = (iTween.NamedValueColor)((int)Enum.Parse(typeof(iTween.NamedValueColor), (string)this.tweenArguments.get_Item("namedcolorvalue"), true));
				}
				catch
				{
					Debuger.Warning("iTween: Unsupported namedcolorvalue supplied! Default will be used.", new object[0]);
					this.namedcolorvalue = iTween.NamedValueColor._Color;
				}
			}
		}
		else
		{
			this.namedcolorvalue = iTween.Defaults.namedColorValue;
		}
		if (this.tweenArguments.Contains("looptype"))
		{
			if (this.tweenArguments.get_Item("looptype").GetType() == typeof(iTween.LoopType))
			{
				this.loopType = (iTween.LoopType)((int)this.tweenArguments.get_Item("looptype"));
			}
			else
			{
				try
				{
					this.loopType = (iTween.LoopType)((int)Enum.Parse(typeof(iTween.LoopType), (string)this.tweenArguments.get_Item("looptype"), true));
				}
				catch
				{
					Debuger.Warning("iTween: Unsupported loopType supplied! Default will be used.", new object[0]);
					this.loopType = iTween.LoopType.none;
				}
			}
		}
		else
		{
			this.loopType = iTween.LoopType.none;
		}
		if (this.tweenArguments.Contains("easetype"))
		{
			if (this.tweenArguments.get_Item("easetype").GetType() == typeof(iTween.EaseType))
			{
				this.easeType = (iTween.EaseType)((int)this.tweenArguments.get_Item("easetype"));
			}
			else
			{
				try
				{
					this.easeType = (iTween.EaseType)((int)Enum.Parse(typeof(iTween.EaseType), (string)this.tweenArguments.get_Item("easetype"), true));
				}
				catch
				{
					Debuger.Warning("iTween: Unsupported easeType supplied! Default will be used.", new object[0]);
					this.easeType = iTween.Defaults.easeType;
				}
			}
		}
		else
		{
			this.easeType = iTween.Defaults.easeType;
		}
		if (this.tweenArguments.Contains("space"))
		{
			if (this.tweenArguments.get_Item("space").GetType() == typeof(Space))
			{
				this.space = (int)this.tweenArguments.get_Item("space");
			}
			else
			{
				try
				{
					this.space = (int)Enum.Parse(typeof(Space), (string)this.tweenArguments.get_Item("space"), true);
				}
				catch
				{
					Debuger.Warning("iTween: Unsupported space supplied! Default will be used.", new object[0]);
					this.space = iTween.Defaults.space;
				}
			}
		}
		else
		{
			this.space = iTween.Defaults.space;
		}
		if (this.tweenArguments.Contains("islocal"))
		{
			this.isLocal = (bool)this.tweenArguments.get_Item("islocal");
		}
		else
		{
			this.isLocal = iTween.Defaults.isLocal;
		}
		if (this.tweenArguments.Contains("ignoretimescale"))
		{
			this.useRealTime = (bool)this.tweenArguments.get_Item("ignoretimescale");
		}
		else
		{
			this.useRealTime = iTween.Defaults.useRealTime;
		}
		this.GetEasingFunction();
	}

	private void GetEasingFunction()
	{
		switch (this.easeType)
		{
		case iTween.EaseType.easeInQuad:
			this.ease = new iTween.EasingFunction(this.easeInQuad);
			break;
		case iTween.EaseType.easeOutQuad:
			this.ease = new iTween.EasingFunction(this.easeOutQuad);
			break;
		case iTween.EaseType.easeInOutQuad:
			this.ease = new iTween.EasingFunction(this.easeInOutQuad);
			break;
		case iTween.EaseType.easeInCubic:
			this.ease = new iTween.EasingFunction(this.easeInCubic);
			break;
		case iTween.EaseType.easeOutCubic:
			this.ease = new iTween.EasingFunction(this.easeOutCubic);
			break;
		case iTween.EaseType.easeInOutCubic:
			this.ease = new iTween.EasingFunction(this.easeInOutCubic);
			break;
		case iTween.EaseType.easeInQuart:
			this.ease = new iTween.EasingFunction(this.easeInQuart);
			break;
		case iTween.EaseType.easeOutQuart:
			this.ease = new iTween.EasingFunction(this.easeOutQuart);
			break;
		case iTween.EaseType.easeInOutQuart:
			this.ease = new iTween.EasingFunction(this.easeInOutQuart);
			break;
		case iTween.EaseType.easeInQuint:
			this.ease = new iTween.EasingFunction(this.easeInQuint);
			break;
		case iTween.EaseType.easeOutQuint:
			this.ease = new iTween.EasingFunction(this.easeOutQuint);
			break;
		case iTween.EaseType.easeInOutQuint:
			this.ease = new iTween.EasingFunction(this.easeInOutQuint);
			break;
		case iTween.EaseType.easeInSine:
			this.ease = new iTween.EasingFunction(this.easeInSine);
			break;
		case iTween.EaseType.easeOutSine:
			this.ease = new iTween.EasingFunction(this.easeOutSine);
			break;
		case iTween.EaseType.easeInOutSine:
			this.ease = new iTween.EasingFunction(this.easeInOutSine);
			break;
		case iTween.EaseType.easeInExpo:
			this.ease = new iTween.EasingFunction(this.easeInExpo);
			break;
		case iTween.EaseType.easeOutExpo:
			this.ease = new iTween.EasingFunction(this.easeOutExpo);
			break;
		case iTween.EaseType.easeInOutExpo:
			this.ease = new iTween.EasingFunction(this.easeInOutExpo);
			break;
		case iTween.EaseType.easeInCirc:
			this.ease = new iTween.EasingFunction(this.easeInCirc);
			break;
		case iTween.EaseType.easeOutCirc:
			this.ease = new iTween.EasingFunction(this.easeOutCirc);
			break;
		case iTween.EaseType.easeInOutCirc:
			this.ease = new iTween.EasingFunction(this.easeInOutCirc);
			break;
		case iTween.EaseType.linear:
			this.ease = new iTween.EasingFunction(this.linear);
			break;
		case iTween.EaseType.spring:
			this.ease = new iTween.EasingFunction(this.spring);
			break;
		case iTween.EaseType.easeInBounce:
			this.ease = new iTween.EasingFunction(this.easeInBounce);
			break;
		case iTween.EaseType.easeOutBounce:
			this.ease = new iTween.EasingFunction(this.easeOutBounce);
			break;
		case iTween.EaseType.easeInOutBounce:
			this.ease = new iTween.EasingFunction(this.easeInOutBounce);
			break;
		case iTween.EaseType.easeInBack:
			this.ease = new iTween.EasingFunction(this.easeInBack);
			break;
		case iTween.EaseType.easeOutBack:
			this.ease = new iTween.EasingFunction(this.easeOutBack);
			break;
		case iTween.EaseType.easeInOutBack:
			this.ease = new iTween.EasingFunction(this.easeInOutBack);
			break;
		case iTween.EaseType.easeInElastic:
			this.ease = new iTween.EasingFunction(this.easeInElastic);
			break;
		case iTween.EaseType.easeOutElastic:
			this.ease = new iTween.EasingFunction(this.easeOutElastic);
			break;
		case iTween.EaseType.easeInOutElastic:
			this.ease = new iTween.EasingFunction(this.easeInOutElastic);
			break;
		}
	}

	private void UpdatePercentage()
	{
		if (this.useRealTime)
		{
			this.runningTime += Time.get_realtimeSinceStartup() - this.lastRealTime;
		}
		else
		{
			this.runningTime += Time.get_deltaTime();
		}
		if (this.reverse)
		{
			this.percentage = 1f - this.runningTime / this.time;
		}
		else
		{
			this.percentage = this.runningTime / this.time;
		}
		this.lastRealTime = Time.get_realtimeSinceStartup();
	}

	private void CallBack(string callbackType)
	{
		if (this.tweenArguments.Contains(callbackType) && !this.tweenArguments.Contains("ischild"))
		{
			GameObject gameObject;
			if (this.tweenArguments.Contains(callbackType + "target"))
			{
				gameObject = (GameObject)this.tweenArguments.get_Item(callbackType + "target");
			}
			else
			{
				gameObject = base.get_gameObject();
			}
			if (this.tweenArguments.get_Item(callbackType).GetType() == typeof(string))
			{
				gameObject.SendMessage((string)this.tweenArguments.get_Item(callbackType), this.tweenArguments.get_Item(callbackType + "params"), 1);
			}
			else
			{
				Debuger.Error("iTween Error: Callback method references must be passed as a String!", new object[0]);
				Object.Destroy(this);
			}
		}
	}

	private void Dispose()
	{
		for (int i = 0; i < iTween.tweens.get_Count(); i++)
		{
			Hashtable hashtable = iTween.tweens.get_Item(i);
			if ((string)hashtable.get_Item("id") == this.id)
			{
				iTween.tweens.RemoveAt(i);
				break;
			}
		}
		Object.Destroy(this);
	}

	private void ConflictCheck()
	{
		Component[] components = base.GetComponents<iTween>();
		for (int i = 0; i < components.Length; i++)
		{
			iTween iTween = components[i] as iTween;
			if (iTween.type == "value")
			{
				return;
			}
			if (iTween.isRunning && iTween.type == this.type)
			{
				if (iTween.method != this.method)
				{
					return;
				}
				if (iTween.tweenArguments.get_Count() != this.tweenArguments.get_Count())
				{
					iTween.Dispose();
					return;
				}
				IDictionaryEnumerator enumerator = this.tweenArguments.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.get_Current();
						if (!iTween.tweenArguments.Contains(dictionaryEntry.get_Key()))
						{
							iTween.Dispose();
							return;
						}
						if (!iTween.tweenArguments.get_Item(dictionaryEntry.get_Key()).Equals(this.tweenArguments.get_Item(dictionaryEntry.get_Key())) && (string)dictionaryEntry.get_Key() != "id")
						{
							iTween.Dispose();
							return;
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this.Dispose();
			}
		}
	}

	private void EnableKinematic()
	{
	}

	private void DisableKinematic()
	{
	}

	private void ResumeDelay()
	{
		base.StartCoroutine("TweenDelay");
	}

	private float linear(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, value);
	}

	private float clerp(float start, float end, float value)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) * 0.5f);
		float result;
		if (end - start < -num3)
		{
			float num4 = (num2 - start + end) * value;
			result = start + num4;
		}
		else if (end - start > num3)
		{
			float num4 = -(num2 - end + start) * value;
			result = start + num4;
		}
		else
		{
			result = start + (end - start) * value;
		}
		return result;
	}

	private float spring(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * 3.14159274f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
		return start + (end - start) * value;
	}

	private float easeInQuad(float start, float end, float value)
	{
		end -= start;
		return end * value * value + start;
	}

	private float easeOutQuad(float start, float end, float value)
	{
		end -= start;
		return -end * value * (value - 2f) + start;
	}

	private float easeInOutQuad(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value + start;
		}
		value -= 1f;
		return -end * 0.5f * (value * (value - 2f) - 1f) + start;
	}

	private float easeInCubic(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value + start;
	}

	private float easeOutCubic(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value + 1f) + start;
	}

	private float easeInOutCubic(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value * value + start;
		}
		value -= 2f;
		return end * 0.5f * (value * value * value + 2f) + start;
	}

	private float easeInQuart(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value + start;
	}

	private float easeOutQuart(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return -end * (value * value * value * value - 1f) + start;
	}

	private float easeInOutQuart(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value * value * value + start;
		}
		value -= 2f;
		return -end * 0.5f * (value * value * value * value - 2f) + start;
	}

	private float easeInQuint(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value * value + start;
	}

	private float easeOutQuint(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value * value * value + 1f) + start;
	}

	private float easeInOutQuint(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value * value * value * value + start;
		}
		value -= 2f;
		return end * 0.5f * (value * value * value * value * value + 2f) + start;
	}

	private float easeInSine(float start, float end, float value)
	{
		end -= start;
		return -end * Mathf.Cos(value * 1.57079637f) + end + start;
	}

	private float easeOutSine(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Sin(value * 1.57079637f) + start;
	}

	private float easeInOutSine(float start, float end, float value)
	{
		end -= start;
		return -end * 0.5f * (Mathf.Cos(3.14159274f * value) - 1f) + start;
	}

	private float easeInExpo(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
	}

	private float easeOutExpo(float start, float end, float value)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * value) + 1f) + start;
	}

	private float easeInOutExpo(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}
		value -= 1f;
		return end * 0.5f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
	}

	private float easeInCirc(float start, float end, float value)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
	}

	private float easeOutCirc(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - value * value) + start;
	}

	private float easeInOutCirc(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return -end * 0.5f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}
		value -= 2f;
		return end * 0.5f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
	}

	private float easeInBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		return end - this.easeOutBounce(0f, end, num - value) + start;
	}

	private float easeOutBounce(float start, float end, float value)
	{
		value /= 1f;
		end -= start;
		if (value < 0.363636374f)
		{
			return end * (7.5625f * value * value) + start;
		}
		if (value < 0.727272749f)
		{
			value -= 0.545454562f;
			return end * (7.5625f * value * value + 0.75f) + start;
		}
		if ((double)value < 0.90909090909090906)
		{
			value -= 0.8181818f;
			return end * (7.5625f * value * value + 0.9375f) + start;
		}
		value -= 0.954545438f;
		return end * (7.5625f * value * value + 0.984375f) + start;
	}

	private float easeInOutBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		if (value < num * 0.5f)
		{
			return this.easeInBounce(0f, end, value * 2f) * 0.5f + start;
		}
		return this.easeOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
	}

	private float easeInBack(float start, float end, float value)
	{
		end -= start;
		value /= 1f;
		float num = 1.70158f;
		return end * value * value * ((num + 1f) * value - num) + start;
	}

	private float easeOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value -= 1f;
		return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
	}

	private float easeInOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value /= 0.5f;
		if (value < 1f)
		{
			num *= 1.525f;
			return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
		}
		value -= 2f;
		num *= 1.525f;
		return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
	}

	private float punch(float amplitude, float value)
	{
		if (value == 0f)
		{
			return 0f;
		}
		if (value == 1f)
		{
			return 0f;
		}
		float num = 0.3f;
		float num2 = num / 6.28318548f * Mathf.Asin(0f);
		return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num2) * 6.28318548f / num);
	}

	private float easeInElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
		}
		return -(num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.28318548f / num2)) + start;
	}

	private float easeOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 * 0.25f;
		}
		else
		{
			num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
		}
		return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * 6.28318548f / num2) + end + start;
	}

	private float easeInOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num * 0.5f) == 2f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
		}
		if (value < 1f)
		{
			return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.28318548f / num2)) + start;
		}
		return num3 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.28318548f / num2) * 0.5f + end + start;
	}
}
