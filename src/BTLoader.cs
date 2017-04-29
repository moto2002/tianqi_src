using AIMind;
using AIRuntime;
using Mono.Xml;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using UnityEngine;
using XEngine.AssetLoader;

public class BTLoader
{
	private static Dictionary<string, BTNode> container;

	static BTLoader()
	{
		BTLoader.container = new Dictionary<string, BTNode>();
	}

	public static void Init()
	{
	}

	public static BTNode GetBehaviorTree(string _id)
	{
		if (!BTLoader.container.ContainsKey(_id))
		{
			BTLoader.LoadXML(_id);
		}
		if (BTLoader.container.ContainsKey(_id))
		{
			return BTLoader.container.get_Item(_id);
		}
		return null;
	}

	public static void LoadXML(string fileName)
	{
		TextAsset textAsset = AssetLoader.LoadAssetNow("BT/BT" + fileName, typeof(Object)) as TextAsset;
		if (textAsset == null)
		{
			return;
		}
		BTNode bTNode = BTLoader.TreeLoader(textAsset.get_text());
		BTLoader.container.Add(fileName, bTNode);
		AssetLoader.UnloadAsset("BT/BT" + fileName, null);
		if (bTNode == null)
		{
			Debuger.Error("创建BTree失败 " + fileName, new object[0]);
		}
	}

	public static BTNode TreeLoader(string xmlstring)
	{
		SecurityParser securityParser = new SecurityParser();
		securityParser.LoadXml(xmlstring);
		SecurityElement securityElement = securityParser.ToXml();
		SecurityElement securityElement2 = (SecurityElement)securityElement.get_Children().get_Item(0);
		if (securityElement2.Attribute("Class").Equals("Brainiac.Design.Nodes.Behavior"))
		{
			BTNode bTNode = new BTNode();
			BTLoader.AddChildNode(bTNode, (SecurityElement)securityElement2.get_Children().get_Item(0));
			return bTNode;
		}
		return null;
	}

	private static void CheckeNode(SecurityElement securityElement)
	{
		if (securityElement.get_Children() != null)
		{
			for (int i = 0; i < securityElement.get_Children().get_Count(); i++)
			{
				SecurityElement securityElement2 = (SecurityElement)securityElement.get_Children().get_Item(i);
				BTLoader.CheckeNode(securityElement2);
			}
		}
	}

	private static void AddChildNode(Command node, SecurityElement securityElement)
	{
		if (securityElement.Attribute("Class") != null)
		{
			Type type = Type.GetType(securityElement.Attribute("Class"));
			ConstructorInfo[] constructors = type.GetConstructors();
			ConstructorInfo constructorInfo = constructors[0];
			ParameterInfo[] parameters = constructorInfo.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				array[i] = parameterInfo.get_ParameterType();
			}
			constructorInfo = type.GetConstructor(array);
			object[] array2 = new object[parameters.Length];
			for (int j = 0; j < array.Length; j++)
			{
				ParameterInfo parameterInfo2 = parameters[j];
				Type type2 = array[j];
				string text = (string)securityElement.get_Attributes().get_Item(parameterInfo2.get_Name());
				if (type2.Equals(typeof(string)))
				{
					array2[j] = text;
				}
				else if (type2.Equals(typeof(int)))
				{
					array2[j] = int.Parse(text);
				}
				else if (type2.Equals(typeof(float)))
				{
					array2[j] = float.Parse(text);
				}
				else if (type2.Equals(typeof(bool)))
				{
					if (text.Equals("True"))
					{
						array2[j] = true;
					}
					else
					{
						array2[j] = false;
					}
				}
				else if (type2.Equals(typeof(LogicalOperator)))
				{
					string[] array3 = text.ToString().Split(new char[]
					{
						':'
					});
					array2[j] = (LogicalOperator)int.Parse(array3[1]);
				}
				else if (type2.Equals(typeof(ComparisonOperator)))
				{
					string[] array4 = text.ToString().Split(new char[]
					{
						':'
					});
					array2[j] = (ComparisonOperator)int.Parse(array4[1]);
				}
			}
			Command command = constructorInfo.Invoke(array2) as Command;
			node.AddChild(command);
			if (securityElement.get_Children() != null)
			{
				for (int k = 0; k < securityElement.get_Children().get_Count(); k++)
				{
					BTLoader.AddChildNode(command, (SecurityElement)securityElement.get_Children().get_Item(k));
				}
			}
		}
		else if (securityElement.Attribute("Identifier") != null && securityElement.get_Children() != null)
		{
			for (int l = 0; l < securityElement.get_Children().get_Count(); l++)
			{
				BTLoader.AddChildNode(node, (SecurityElement)securityElement.get_Children().get_Item(l));
			}
		}
	}
}
