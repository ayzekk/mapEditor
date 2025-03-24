using System;
using System.Collections.Generic;
using System.IO;

namespace SroMapEditor;

internal class OFile
{
	public List<MapObject> objects = new List<MapObject>();

	private List<MapObject> AllObjects = new List<MapObject>();

	public int OX;

	public int OY;

	public OFile(string path)
	{
		BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
		string[] array = path.Split('\\');
		OX = int.Parse(array[array.Length - 1].Remove(array[array.Length - 1].IndexOf(".o2")));
		OY = int.Parse(array[array.Length - 2]);
		List<int> list = new List<int>();
		binaryReader.BaseStream.Position = 12L;
		for (int i = 0; i < 144; i++)
		{
			short num = binaryReader.ReadInt16();
			for (int j = 0; j < num; j++)
			{
				MapObject mapObject = new MapObject
				{
					groups = { i },
					nameI = binaryReader.ReadInt32(),
					X = binaryReader.ReadSingle(),
					Y = binaryReader.ReadSingle(),
					Z = binaryReader.ReadSingle()
				};
				string text = binaryReader.ReadByte().ToString("X2");
				mapObject.Unknown1 = text + binaryReader.ReadByte().ToString("X2");
				float num2;
				for (num2 = binaryReader.ReadSingle(); num2 < 0f; num2 += (float)Math.PI * 2f)
				{
				}
				while (num2 > (float)Math.PI * 2f)
				{
					num2 -= (float)Math.PI * 2f;
				}
				mapObject.Theta = num2;
				mapObject.ID = binaryReader.ReadInt32();
				string text2 = binaryReader.ReadByte().ToString("X2");
				mapObject.Unknown2 = text2 + binaryReader.ReadByte().ToString("X2");
				int num3 = binaryReader.ReadByte();
				int num4 = binaryReader.ReadByte();
				mapObject.X += (num3 - OX) * 1920;
				mapObject.Z += (num4 - OY) * 1920;
				AllObjects.Add(mapObject);
				if (!list.Contains(mapObject.ID))
				{
					objects.Add(mapObject);
					list.Add(mapObject.ID);
				}
				else
				{
					objects[list.IndexOf(mapObject.ID)].groups.Add(i);
				}
			}
		}
		objects.Reverse();
		binaryReader.Close();
	}
}
