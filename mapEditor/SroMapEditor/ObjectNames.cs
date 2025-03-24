using System;
using System.IO;
using System.Windows.Forms;

namespace SroMapEditor;

public class ObjectNames
{
	public string[] names;

	public ObjectNames()
	{
		if (!File.Exists("Map\\object.ifo"))
		{
			MessageBox.Show("Could not find object.ifo. Terminating application.");
			Environment.Exit(1);
		}
		string[] array = File.ReadAllLines("Map\\object.ifo");
		names = new string[array.Length - 2];
		for (int i = 0; i < array.Length - 2; i++)
		{
			string text = array[i + 2].Substring(array[i + 2].IndexOf(' ') + 1);
			text = text.Substring(text.IndexOf(' ') + 1);
			text = text.Remove(text.Length - 1);
			text = text.Substring(1);
			names[i] = text;
		}
	}
}
