using System;

public class Message : IMessage
{
	private string m_name;

	private string m_type;

	private object m_body;

	public virtual string Name
	{
		get
		{
			return this.m_name;
		}
	}

	public virtual object Body
	{
		get
		{
			return this.m_body;
		}
		set
		{
			this.m_body = value;
		}
	}

	public virtual string Type
	{
		get
		{
			return this.m_type;
		}
		set
		{
			this.m_type = value;
		}
	}

	public Message(string name) : this(name, null, null)
	{
	}

	public Message(string name, object body) : this(name, body, null)
	{
	}

	public Message(string name, object body, string type)
	{
		this.m_name = name;
		this.m_body = body;
		this.m_type = type;
	}

	public override string ToString()
	{
		string text = "Notification Name: " + this.Name;
		text = text + "\nBody:" + ((this.Body != null) ? this.Body.ToString() : "null");
		return text + "\nType:" + ((this.Type != null) ? this.Type : "null");
	}
}
