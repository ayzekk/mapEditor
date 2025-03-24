using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SroMapEditor;

public class Form2 : Form
{
	private IContainer components;

	private Button button1;

	private Button button2;

	private TreeView treeView1;

	public GLControl glControl1;

	private TrackBar trackBar1;

	private Label label1;

	private GroupBox groupBox1;

	private CheckBox checkBox4;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	private TextBox textBox1;

	private ListBox listBox1;

	private Button button3;

	private MapObject viewObj;

	private ObjectNames ONames;

	private int selectedI;

	public int SelectedItem = -1;

	private float rotation;

	private List<string> texNames = new List<string>();

	private List<int> texIDs = new List<int>();

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.glControl1 = new OpenTK.GLControl();
		this.trackBar1 = new System.Windows.Forms.TrackBar();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.button3 = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.trackBar1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.button1.Location = new System.Drawing.Point(529, 476);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(113, 40);
		this.button1.TabIndex = 2;
		this.button1.Text = "Select";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(658, 476);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(113, 40);
		this.button2.TabIndex = 3;
		this.button2.Text = "Cancel";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.treeView1.Location = new System.Drawing.Point(12, 12);
		this.treeView1.Name = "treeView1";
		this.treeView1.Size = new System.Drawing.Size(449, 504);
		this.treeView1.TabIndex = 4;
		this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_AfterSelect);
		this.glControl1.BackColor = System.Drawing.Color.Black;
		this.glControl1.Location = new System.Drawing.Point(470, 130);
		this.glControl1.Name = "glControl1";
		this.glControl1.Size = new System.Drawing.Size(316, 289);
		this.glControl1.TabIndex = 5;
		this.glControl1.VSync = false;
		this.glControl1.Load += new System.EventHandler(glControl1_Load);
		this.trackBar1.Location = new System.Drawing.Point(470, 425);
		this.trackBar1.Maximum = 628;
		this.trackBar1.Name = "trackBar1";
		this.trackBar1.Size = new System.Drawing.Size(316, 45);
		this.trackBar1.TabIndex = 6;
		this.trackBar1.Scroll += new System.EventHandler(trackBar1_Scroll);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(467, 114);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(33, 13);
		this.label1.TabIndex = 8;
		this.label1.Text = "None";
		this.groupBox1.Controls.Add(this.button3);
		this.groupBox1.Controls.Add(this.checkBox4);
		this.groupBox1.Controls.Add(this.checkBox3);
		this.groupBox1.Controls.Add(this.checkBox2);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Location = new System.Drawing.Point(470, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(316, 100);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Search";
		this.checkBox4.AutoSize = true;
		this.checkBox4.Checked = true;
		this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox4.Location = new System.Drawing.Point(228, 46);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(52, 17);
		this.checkBox4.TabIndex = 4;
		this.checkBox4.Text = "Other";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox4.CheckedChanged += new System.EventHandler(checkBox4_CheckedChanged);
		this.checkBox3.AutoSize = true;
		this.checkBox3.Checked = true;
		this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox3.Location = new System.Drawing.Point(163, 46);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(58, 17);
		this.checkBox3.TabIndex = 3;
		this.checkBox3.Text = "Nature";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox3.CheckedChanged += new System.EventHandler(checkBox3_CheckedChanged);
		this.checkBox2.AutoSize = true;
		this.checkBox2.Checked = true;
		this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox2.Location = new System.Drawing.Point(86, 45);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(64, 17);
		this.checkBox2.TabIndex = 2;
		this.checkBox2.Text = "Artifacts";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox2.CheckedChanged += new System.EventHandler(checkBox2_CheckedChanged);
		this.checkBox1.AutoSize = true;
		this.checkBox1.Checked = true;
		this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox1.Location = new System.Drawing.Point(6, 45);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(68, 17);
		this.checkBox1.TabIndex = 1;
		this.checkBox1.Text = "Buildings";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.textBox1.Location = new System.Drawing.Point(6, 19);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(295, 20);
		this.textBox1.TabIndex = 0;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(12, 12);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(449, 498);
		this.listBox1.TabIndex = 10;
		this.listBox1.Visible = false;
		this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
		this.button3.Location = new System.Drawing.Point(228, 69);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 23);
		this.button3.TabIndex = 5;
		this.button3.Text = "Clear";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(798, 528);
		base.Controls.Add(this.listBox1);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.trackBar1);
		base.Controls.Add(this.glControl1);
		base.Controls.Add(this.treeView1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Name = "Form2";
		this.Text = "Choose an item";
		base.Load += new System.EventHandler(Form2_Load);
		((System.ComponentModel.ISupportInitialize)this.trackBar1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public Form2(ObjectNames objNames, int selectedIndex)
	{
		InitializeComponent();
		ONames = objNames;
		selectedI = selectedIndex;
	}

	private void Form2_Load(object sender, EventArgs e)
	{
		treeView1.Nodes.Add(new TreeNode("Buildings"));
		treeView1.Nodes.Add(new TreeNode("Artifacts"));
		treeView1.Nodes.Add(new TreeNode("Nature"));
		treeView1.Nodes.Add(new TreeNode("Other"));
		TreeNode selectedNode = new TreeNode();
		for (int i = 0; i < ONames.names.Length; i++)
		{
			string text = ONames.names[i].Substring(ONames.names[i].IndexOf('\\') + 1);
			text = text.Substring(text.IndexOf('\\') + 1);
			TreeNode treeNode = new TreeNode(text);
			treeNode.Tag = i;
			if (ONames.names[i].Contains("\\bldg\\"))
			{
				treeView1.Nodes[0].Nodes.Add(treeNode);
			}
			else if (ONames.names[i].Contains("\\artifact\\"))
			{
				treeView1.Nodes[1].Nodes.Add(treeNode);
			}
			else if (ONames.names[i].Contains("\\nature\\"))
			{
				treeView1.Nodes[2].Nodes.Add(treeNode);
			}
			else
			{
				treeView1.Nodes[3].Nodes.Add(treeNode);
			}
			if (i == selectedI)
			{
				selectedNode = treeNode;
			}
		}
		treeView1.HideSelection = false;
		treeView1.SelectedNode = selectedNode;
	}

	private void glControl1_Load(object sender, EventArgs e)
	{
		GL.ClearColor(Color.Black);
		int num = glControl1.Width;
		int num2 = glControl1.Height;
		GL.MatrixMode(MatrixMode.Projection);
		GL.LoadIdentity();
		GL.Ortho(-100.0, 100.0, -100.0, 100.0, -1000.0, 1000.0);
		GL.Viewport(0, 0, num, num2);
		GL.Enable(EnableCap.Blend);
		GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		GL.Enable(EnableCap.DepthTest);
	}

	public void RenderGL()
	{
		if (base.Visible)
		{
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			if (!glControl1.Context.IsCurrent)
			{
				glControl1.MakeCurrent();
			}
			GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
			GL.PushMatrix();
			GL.Rotate(-90f, 1f, 0f, 0f);
			GL.Rotate((double)rotation * (180.0 / Math.PI), 0.0, 0.0, 1.0);
			if (viewObj != null)
			{
				viewObj.Draw();
			}
			GL.End();
			GL.PopMatrix();
			glControl1.SwapBuffers();
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (treeView1.Visible)
		{
			SelectedItem = (int)treeView1.SelectedNode.Tag;
		}
		else
		{
			SelectedItem = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
		}
		Close();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		SelectedItem = -1;
		Close();
	}

	private void trackBar1_Scroll(object sender, EventArgs e)
	{
		rotation = (float)trackBar1.Value / 100f;
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (e.Node.Level != 1)
		{
			return;
		}
		viewObj = new MapObject();
		List<MeshTexture[]> list = new List<MeshTexture[]>();
		viewObj.LoadFiles("Data\\", ONames.names[(int)e.Node.Tag]);
		list.Add(viewObj.readMaterial());
		foreach (MeshTexture[] item in list)
		{
			MeshTexture[] array = item;
			foreach (MeshTexture meshTexture in array)
			{
				if (meshTexture != null && !texNames.Contains(meshTexture.name))
				{
					texNames.Add(meshTexture.name);
					texIDs.Add(meshTexture.ID);
				}
			}
		}
		viewObj.FindTex(texNames, texIDs);
		label1.Text = ONames.names[(int)e.Node.Tag];
		GL.MatrixMode(MatrixMode.Projection);
		GL.LoadIdentity();
		GL.Ortho((double)viewObj.boundingBoxp1[0] * 1.2, (double)viewObj.boundingBoxp2[0] * 1.2, (double)(viewObj.boundingBoxp1[1] + 10f) * 1.2, (double)viewObj.boundingBoxp2[1] * 1.2, -300.0, 300.0);
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		setSearchResults();
	}

	private void setSearchResults()
	{
		if (textBox1.Text != "")
		{
			listBox1.Visible = true;
			treeView1.Visible = false;
			List<int> list = new List<int>();
			for (int i = 0; i < ONames.names.Count(); i++)
			{
				if (ONames.names[i].Contains(textBox1.Text))
				{
					if (ONames.names[i].Contains("\\bldg\\") && checkBox1.Checked)
					{
						list.Add(i);
					}
					else if (ONames.names[i].Contains("\\artifact\\") && checkBox2.Checked)
					{
						list.Add(i);
					}
					else if (ONames.names[i].Contains("\\nature\\") && checkBox3.Checked)
					{
						list.Add(i);
					}
					else if (!ONames.names[i].Contains("\\bldg\\") && !ONames.names[i].Contains("\\artifact\\") && !ONames.names[i].Contains("\\nature\\") && checkBox4.Checked)
					{
						list.Add(i);
					}
				}
			}
			listBox1.Items.Clear();
			if (list.Count > 0)
			{
				for (int j = 0; j < list.Count(); j++)
				{
					listBox1.Items.Add(list[j] + " - " + ONames.names[list[j]]);
				}
			}
			else
			{
				listBox1.Items.Add("No objects matching your search were found.");
			}
		}
		else
		{
			listBox1.Visible = false;
			treeView1.Visible = true;
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		setSearchResults();
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		setSearchResults();
	}

	private void checkBox3_CheckedChanged(object sender, EventArgs e)
	{
		setSearchResults();
	}

	private void checkBox4_CheckedChanged(object sender, EventArgs e)
	{
		setSearchResults();
	}

	private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		viewObj = new MapObject();
		List<MeshTexture[]> list = new List<MeshTexture[]>();
		viewObj.LoadFiles("Data\\", listBox1.SelectedItem.ToString().Split(' ')[2]);
		list.Add(viewObj.readMaterial());
		foreach (MeshTexture[] item in list)
		{
			MeshTexture[] array = item;
			foreach (MeshTexture meshTexture in array)
			{
				if (meshTexture != null && !texNames.Contains(meshTexture.name))
				{
					texNames.Add(meshTexture.name);
					texIDs.Add(meshTexture.ID);
				}
			}
		}
		viewObj.FindTex(texNames, texIDs);
		label1.Text = listBox1.SelectedItem.ToString().Split(' ')[2];
		GL.MatrixMode(MatrixMode.Projection);
		GL.LoadIdentity();
		GL.Ortho((double)viewObj.boundingBoxp1[0] * 1.2, (double)viewObj.boundingBoxp2[0] * 1.2, (double)(viewObj.boundingBoxp1[1] + 10f) * 1.2, (double)viewObj.boundingBoxp2[1] * 1.2, -300.0, 300.0);
	}

	private void button3_Click(object sender, EventArgs e)
	{
		textBox1.Text = "";
		setSearchResults();
	}
}
