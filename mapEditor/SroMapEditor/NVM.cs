using System;
using System.Collections.Generic;
using System.IO;

namespace SroMapEditor;

internal class NVM
{
	public int entryNum;

	public string filepath;

	private NVMEntity[] entities;

	private byte[] otherData;

	public NVM(string path)
	{
		filepath = path;
		BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
		binaryReader.BaseStream.Position = 12L;
		entryNum = binaryReader.ReadInt16();
		entities = new NVMEntity[entryNum];
		for (int i = 0; i < entryNum; i++)
		{
			NVMEntity nVMEntity = new NVMEntity
			{
				id = binaryReader.ReadInt32(),
				X = binaryReader.ReadSingle(),
				Y = binaryReader.ReadSingle(),
				Z = binaryReader.ReadSingle(),
				UK1 = binaryReader.ReadInt16(),
				UK2 = binaryReader.ReadSingle(),
				UK3 = binaryReader.ReadInt16(),
				UK4 = binaryReader.ReadInt16(),
				UK5 = binaryReader.ReadInt16(),
				Grid = binaryReader.ReadInt16()
			};
			int num = binaryReader.ReadInt16();
			nVMEntity.xtra = binaryReader.ReadBytes(num * 6);
			entities[i] = nVMEntity;
		}
		otherData = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length - (int)binaryReader.BaseStream.Position);
		binaryReader.Close();
	}

	public void setEntities(List<MapObject> objects, string pathPref, string[] bsrPaths)
	{
		List<NVMEntity> list = new List<NVMEntity>();
		for (int i = 0; i < objects.Count; i++)
		{
			if (!bsrPaths[objects[i].nameI].Contains(".bsr"))
			{
				continue;
			}
			BinaryReader binaryReader = new BinaryReader(File.Open(pathPref + bsrPaths[objects[i].nameI], FileMode.Open));
			binaryReader.BaseStream.Position = 68L;
			int num = binaryReader.ReadInt32();
			binaryReader.BaseStream.Position += 48 + num;
			int num2 = binaryReader.ReadInt32();
			binaryReader.Close();
			if (num2 != 0)
			{
				NVMEntity item = default(NVMEntity);
				item.id = objects[i].nameI;
				item.X = objects[i].X;
				item.Y = objects[i].Y;
				item.Z = objects[i].Z;
				if (objects[i].Unknown1 == "FFFF")
				{
					item.UK1 = -1;
				}
				else
				{
					item.UK1 = 0;
				}
				item.UK2 = objects[i].Theta;
				item.UK3 = (short)objects[i].ID;
				item.UK4 = 0;
				if (objects[i].Unknown2 != "0100")
				{
					item.UK5 = 1;
				}
				else
				{
					item.UK5 = 0;
				}
				list.Add(item);
			}
		}
		entities = list.ToArray();
	}

	public void saveNVM(string path, int OX, int OY)
	{
		BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create));
		binaryWriter.Write("JMXVNVM 1000".ToCharArray());
		binaryWriter.Write((short)entities.Length);
		for (int i = 0; i < entities.Length; i++)
		{
			binaryWriter.Write(entities[i].id);
			float num = entities[i].X;
			if (entities[i].X > 1920f)
			{
				num -= 1920f;
			}
			else if (entities[i].X < 0f)
			{
				num += 1920f;
			}
			binaryWriter.Write(num);
			binaryWriter.Write(entities[i].Y);
			float num2 = entities[i].Z;
			if (entities[i].Z > 1920f)
			{
				num2 -= 1920f;
			}
			else if (entities[i].Z < 0f)
			{
				num2 += 1920f;
			}
			binaryWriter.Write(num2);
			binaryWriter.Write(entities[i].UK1);
			binaryWriter.Write(entities[i].UK2);
			binaryWriter.Write(entities[i].UK3);
			binaryWriter.Write(entities[i].UK4);
			binaryWriter.Write(entities[i].UK5);
			binaryWriter.Write((byte)((double)OX + Math.Floor(entities[i].X / 1920f)));
			binaryWriter.Write((byte)((double)OY + Math.Floor(entities[i].Z / 1920f)));
			binaryWriter.Write((short)0);
		}
		binaryWriter.Write(otherData);
		binaryWriter.Close();
	}
}
