using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SroMapEditor;

public class Form1 : Form
{
	private IContainer components;

	private GLControl glControl1;

	private Button button1;

	private Button button2;

	private OpenFileDialog openFileDialog1;

	private Label label1;

	private ListBox listBox1;

	private ListBox listBox2;

	private Label label2;

	private ListBox listBox3;

	private Label label3;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private NumericUpDown numericUpDown1;

	private TrackBar trackBar1;

	private Label label6;

	private Label label5;

	private Label label4;

	private NumericUpDown numericUpDown3;

	private NumericUpDown numericUpDown2;

	private TextBox textBox1;

	private Label label7;

	private Button button3;

	private Label label10;

	private Label label9;

	private Label label8;

	private TextBox textBox2;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	private GroupBox groupBox3;

	private Button button6;

	private Button button5;

	private Button button4;

	private SaveFileDialog saveFileDialog1;

	private CheckBox checkBox5;

	private CheckBox checkBox4;

	private CheckBox checkBox6;

	private Button button7;

	private Button button8;

	private CheckBox checkBox7;

	private FolderBrowserDialog folderBrowserDialog1;

	private SaveFileDialog saveFileDialog2;

	private bool mapLoaded;

	private ObjectNames ObjNames;

	private List<MapObject> Objects;

	private string pathPref = "Data\\";

	private List<string> texNames = new List<string>();

	private List<int> texIDs = new List<int>();

	private bool dragging;

	private Point dragStart;

	private double zoom = 1.0;

	private int rotHor;

	private int rotVert;

	private int selectedObj = -1;

	private Form2 form2;

	private Terrain terrainmap;

	private int OX;

	private int OY;

	private NVM nvmFile;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SroMapEditor.Form1));
		this.glControl1 = new OpenTK.GLControl();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.label1 = new System.Windows.Forms.Label();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.listBox2 = new System.Windows.Forms.ListBox();
		this.label2 = new System.Windows.Forms.Label();
		this.listBox3 = new System.Windows.Forms.ListBox();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.button3 = new System.Windows.Forms.Button();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
		this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.trackBar1 = new System.Windows.Forms.TrackBar();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.button6 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.button7 = new System.Windows.Forms.Button();
		this.button8 = new System.Windows.Forms.Button();
		this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.trackBar1).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.glControl1.BackColor = System.Drawing.Color.Black;
		this.glControl1.Location = new System.Drawing.Point(0, 0);
		this.glControl1.Name = "glControl1";
		this.glControl1.Size = new System.Drawing.Size(745, 630);
		this.glControl1.TabIndex = 0;
		this.glControl1.VSync = false;
		this.glControl1.Load += new System.EventHandler(glControl1_Load);
		this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(onGLMouseDown);
		this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(onGLMouseMove);
		this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(onGLMouseUp);
		this.button1.Location = new System.Drawing.Point(754, 3);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(148, 22);
		this.button1.TabIndex = 1;
		this.button1.Text = "Open Map";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Enabled = false;
		this.button2.Location = new System.Drawing.Point(754, 24);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(148, 22);
		this.button2.TabIndex = 2;
		this.button2.Text = "Save Map";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.openFileDialog1.Filter = "Object Maps|*.o2";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(908, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(49, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buildings";
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(911, 26);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(145, 225);
		this.listBox1.TabIndex = 4;
		this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
		this.listBox2.FormattingEnabled = true;
		this.listBox2.Location = new System.Drawing.Point(911, 273);
		this.listBox2.Name = "listBox2";
		this.listBox2.Size = new System.Drawing.Size(145, 173);
		this.listBox2.TabIndex = 6;
		this.listBox2.SelectedIndexChanged += new System.EventHandler(listBox2_SelectedIndexChanged);
		this.label2.Location = new System.Drawing.Point(908, 257);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(49, 13);
		this.label2.TabIndex = 5;
		this.label2.Text = "Nature";
		this.listBox3.FormattingEnabled = true;
		this.listBox3.Location = new System.Drawing.Point(911, 467);
		this.listBox3.Name = "listBox3";
		this.listBox3.Size = new System.Drawing.Size(145, 147);
		this.listBox3.TabIndex = 8;
		this.listBox3.SelectedIndexChanged += new System.EventHandler(listBox3_SelectedIndexChanged);
		this.label3.Location = new System.Drawing.Point(908, 451);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(49, 13);
		this.label3.TabIndex = 7;
		this.label3.Text = "Other";
		this.groupBox1.Controls.Add(this.checkBox7);
		this.groupBox1.Controls.Add(this.checkBox5);
		this.groupBox1.Controls.Add(this.checkBox4);
		this.groupBox1.Controls.Add(this.checkBox3);
		this.groupBox1.Controls.Add(this.checkBox2);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Location = new System.Drawing.Point(751, 86);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(154, 154);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Options";
		this.checkBox7.AutoSize = true;
		this.checkBox7.Location = new System.Drawing.Point(12, 131);
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.Size = new System.Drawing.Size(80, 17);
		this.checkBox7.TabIndex = 5;
		this.checkBox7.Text = "Show NVM";
		this.checkBox7.UseVisualStyleBackColor = true;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Location = new System.Drawing.Point(12, 110);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(120, 17);
		this.checkBox5.TabIndex = 4;
		this.checkBox5.Text = "Highlight unknown2";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Location = new System.Drawing.Point(12, 87);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(102, 17);
		this.checkBox4.TabIndex = 3;
		this.checkBox4.Text = "Highlight Fading";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Location = new System.Drawing.Point(12, 63);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(75, 17);
		this.checkBox3.TabIndex = 2;
		this.checkBox3.Text = "Show Grid";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.Checked = true;
		this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox2.Location = new System.Drawing.Point(12, 40);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(92, 17);
		this.checkBox2.TabIndex = 1;
		this.checkBox2.Text = "Show Objects";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Checked = true;
		this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox1.Location = new System.Drawing.Point(12, 19);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(89, 17);
		this.checkBox1.TabIndex = 0;
		this.checkBox1.Text = "Show Terrain";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.checkBox6);
		this.groupBox2.Controls.Add(this.button3);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.textBox2);
		this.groupBox2.Controls.Add(this.textBox1);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.numericUpDown3);
		this.groupBox2.Controls.Add(this.numericUpDown2);
		this.groupBox2.Controls.Add(this.numericUpDown1);
		this.groupBox2.Controls.Add(this.trackBar1);
		this.groupBox2.Enabled = false;
		this.groupBox2.Location = new System.Drawing.Point(754, 362);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(148, 256);
		this.groupBox2.TabIndex = 10;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Selected Object";
		this.checkBox6.AutoSize = true;
		this.checkBox6.Location = new System.Drawing.Point(9, 83);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(95, 17);
		this.checkBox6.TabIndex = 14;
		this.checkBox6.Text = "Distance Fade";
		this.checkBox6.UseVisualStyleBackColor = true;
		this.checkBox6.CheckedChanged += new System.EventHandler(checkBox6_CheckedChanged);
		this.button3.Location = new System.Drawing.Point(20, 56);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(105, 23);
		this.button3.TabIndex = 13;
		this.button3.Text = "Change Type";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(46, 41);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(33, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "None";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(6, 41);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(34, 13);
		this.label9.TabIndex = 11;
		this.label9.Text = "Type:";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(6, 22);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(58, 13);
		this.label8.TabIndex = 10;
		this.label8.Text = "Unique ID:";
		this.textBox2.Location = new System.Drawing.Point(62, 19);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(80, 20);
		this.textBox2.TabIndex = 9;
		this.textBox1.Location = new System.Drawing.Point(62, 186);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(80, 20);
		this.textBox1.TabIndex = 8;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(6, 188);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(50, 13);
		this.label7.TabIndex = 7;
		this.label7.Text = "Rotation:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 154);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(17, 13);
		this.label6.TabIndex = 6;
		this.label6.Text = "Z:";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 130);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(17, 13);
		this.label5.TabIndex = 5;
		this.label5.Text = "Y:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 106);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(17, 13);
		this.label4.TabIndex = 4;
		this.label4.Text = "X:";
		this.numericUpDown3.Location = new System.Drawing.Point(37, 104);
		this.numericUpDown3.Maximum = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.numericUpDown3.Minimum = new decimal(new int[4] { 10000000, 0, 0, -2147483648 });
		this.numericUpDown3.Name = "numericUpDown3";
		this.numericUpDown3.Size = new System.Drawing.Size(105, 20);
		this.numericUpDown3.TabIndex = 3;
		this.numericUpDown3.ValueChanged += new System.EventHandler(numericUpDown3_ValueChanged);
		this.numericUpDown2.Location = new System.Drawing.Point(37, 128);
		this.numericUpDown2.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.numericUpDown2.Minimum = new decimal(new int[4] { 10000000, 0, 0, -2147483648 });
		this.numericUpDown2.Name = "numericUpDown2";
		this.numericUpDown2.Size = new System.Drawing.Size(105, 20);
		this.numericUpDown2.TabIndex = 2;
		this.numericUpDown2.ValueChanged += new System.EventHandler(numericUpDown2_ValueChanged);
		this.numericUpDown1.Location = new System.Drawing.Point(37, 152);
		this.numericUpDown1.Maximum = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.numericUpDown1.Minimum = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Size = new System.Drawing.Size(105, 20);
		this.numericUpDown1.TabIndex = 1;
		this.numericUpDown1.ValueChanged += new System.EventHandler(numericUpDown1_ValueChanged);
		this.trackBar1.Location = new System.Drawing.Point(20, 209);
		this.trackBar1.Maximum = 628;
		this.trackBar1.Name = "trackBar1";
		this.trackBar1.Size = new System.Drawing.Size(104, 45);
		this.trackBar1.TabIndex = 0;
		this.trackBar1.Scroll += new System.EventHandler(trackBar1_Scroll);
		this.groupBox3.Controls.Add(this.button6);
		this.groupBox3.Controls.Add(this.button5);
		this.groupBox3.Controls.Add(this.button4);
		this.groupBox3.Enabled = false;
		this.groupBox3.Location = new System.Drawing.Point(751, 246);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(151, 110);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Modify";
		this.button6.Location = new System.Drawing.Point(7, 77);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(138, 23);
		this.button6.TabIndex = 2;
		this.button6.Text = "Delete Current";
		this.button6.UseVisualStyleBackColor = true;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.button5.Location = new System.Drawing.Point(7, 48);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(138, 23);
		this.button5.TabIndex = 1;
		this.button5.Text = "Clone Current";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.button4.Location = new System.Drawing.Point(7, 19);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(138, 23);
		this.button4.TabIndex = 0;
		this.button4.Text = "Add Object";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.saveFileDialog1.Filter = "Object Map|*.o2";
		this.button7.Location = new System.Drawing.Point(754, 45);
		this.button7.Name = "button7";
		this.button7.Size = new System.Drawing.Size(148, 22);
		this.button7.TabIndex = 12;
		this.button7.Text = "Open NVM";
		this.button7.UseVisualStyleBackColor = true;
		this.button7.Click += new System.EventHandler(button7_Click);
		this.button8.Location = new System.Drawing.Point(754, 66);
		this.button8.Name = "button8";
		this.button8.Size = new System.Drawing.Size(148, 22);
		this.button8.TabIndex = 13;
		this.button8.Text = "Save NVM";
		this.button8.UseVisualStyleBackColor = true;
		this.button8.Click += new System.EventHandler(button8_Click);
		this.saveFileDialog2.Filter = "Collision map|*.nvm";
		this.saveFileDialog2.Title = "Save collisionmap";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1068, 630);
		base.Controls.Add(this.button8);
		base.Controls.Add(this.button7);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.listBox3);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.listBox2);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.listBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.glControl1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Form1";
		this.Text = "SRO Map Editor";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.trackBar1).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public Form1()
	{
		mapLoaded = false;
		ObjNames = new ObjectNames();
		base.MouseWheel += onMouseWheel;
		InitializeComponent();
	}

	private void glControl1_Load(object sender, EventArgs e)
	{
		GL.ClearColor(Color.Black);
		int num = glControl1.Width;
		int num2 = glControl1.Height;
		GL.MatrixMode(MatrixMode.Projection);
		GL.LoadIdentity();
		GL.Ortho(0.0, 1920.0, 0.0, 1920.0, -5000.0, 5000.0);
		GL.Viewport(0, 0, num, num2);
		GL.PointSize(3f);
		GL.Enable(EnableCap.Blend);
		GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		GL.Enable(EnableCap.DepthTest);
		Application.Idle += Application_Idle;
	}

	private void Application_Idle(object sender, EventArgs e)
	{
		while (glControl1.IsIdle)
		{
			if (form2 == null)
			{
				Render();
			}
			else
			{
				form2.RenderGL();
			}
		}
	}

	private void Render()
	{
		if (!glControl1.Context.IsCurrent)
		{
			glControl1.MakeCurrent();
		}
		GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
		GL.MatrixMode(MatrixMode.Modelview);
		GL.LoadIdentity();
		new List<int>();
		if (mapLoaded)
		{
			GL.PushMatrix();
			GL.Translate(960f, 960f, 0f);
			GL.Scale(zoom, zoom, zoom);
			GL.Rotate((double)rotHor / 100.0, 0.0, 1.0, 0.0);
			GL.Rotate((double)rotVert / 100.0, 1.0, 0.0, 0.0);
			GL.Translate(-960f, -960f, 0f);
			if (terrainmap != null && checkBox1.Checked)
			{
				terrainmap.Draw();
			}
			if (checkBox2.Checked)
			{
				for (int i = 0; i < Objects.Count; i++)
				{
					GL.PushMatrix();
					GL.Translate(Objects[i].X, Objects[i].Z, Objects[i].Y);
					GL.Rotate((double)Objects[i].Theta * (180.0 / Math.PI), 0.0, 0.0, 1.0);
					if (checkBox4.Checked && Objects[i].DistFade)
					{
						GL.Color3((byte)50, byte.MaxValue, (byte)50);
					}
					if (checkBox5.Checked && Objects[i].Unknown2 == "0100")
					{
						GL.Color3((byte)50, (byte)50, byte.MaxValue);
					}
					if (i == selectedObj)
					{
						GL.Color3(byte.MaxValue, (byte)100, (byte)100);
					}
					Objects[i].Draw();
					if (i == selectedObj)
					{
						GL.Color3(byte.MaxValue, byte.MaxValue, byte.MaxValue);
						Objects[i].drawBoundingBox();
					}
					GL.PopMatrix();
					if (checkBox3.Checked && i == selectedObj)
					{
						Objects[i].drawGroups();
					}
					GL.Color3(byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
			}
			if (checkBox3.Checked)
			{
				if (rotHor != 0)
				{
					rotHor = 0;
				}
				if (rotVert != 0)
				{
					rotVert = 0;
				}
				GL.Color3(byte.MaxValue, (byte)0, byte.MaxValue);
				int num = 6;
				int num2 = 24;
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num; k++)
					{
						GL.Begin(BeginMode.LineStrip);
						GL.Vertex3(j * (1920 / num2), k * (1920 / num), 500);
						GL.Vertex3((j + 1) * (1920 / num2), k * (1920 / num), 500);
						GL.Vertex3((j + 1) * (1920 / num2), (k + 1) * (1920 / num), 500);
						GL.Vertex3(j * (1920 / num2), (k + 1) * (1920 / num), 500);
						GL.End();
					}
				}
				GL.Color3(byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
			GL.PopMatrix();
		}
		glControl1.SwapBuffers();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (openFileDialog1.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string path = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.LastIndexOf('.')) + ".m";
		if (File.Exists(path))
		{
			terrainmap = new Terrain(path);
		}
		else
		{
			MessageBox.Show("Terrain not found.");
		}
		OFile oFile = new OFile(openFileDialog1.FileName);
		OX = oFile.OX;
		OY = oFile.OY;
		Text = "SRO Map Viewer - X: " + OX + " Y:" + OY;
		Objects = oFile.objects;
		List<MeshTexture[]> list = new List<MeshTexture[]>();
		foreach (MapObject @object in Objects)
		{
			if (ObjNames.names[@object.nameI].Contains(".bsr"))
			{
				@object.LoadFiles(pathPref, ObjNames.names[@object.nameI]);
				list.Add(@object.readMaterial());
			}
		}
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
		foreach (MapObject object2 in Objects)
		{
			object2.FindTex(texNames, texIDs);
		}
		mapLoaded = true;
		groupBox3.Enabled = true;
		button2.Enabled = true;
		fillLists();
	}

	private void onGLMouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right && !dragging)
		{
			dragging = true;
			dragStart = e.Location;
		}
	}

	private void onMouseWheel(object sender, MouseEventArgs e)
	{
		if (mapLoaded && PointToClient(e.Location).X < glControl1.Width && PointToClient(e.Location).Y < glControl1.Height)
		{
			zoom += (double)e.Delta / 300.0;
			if (zoom < 0.1)
			{
				zoom = 0.1;
			}
		}
	}

	private void onGLMouseMove(object sender, MouseEventArgs e)
	{
		if (dragging)
		{
			rotHor += e.Location.X - dragStart.X;
			rotVert += e.Location.Y - dragStart.Y;
		}
	}

	private void onGLMouseUp(object sender, MouseEventArgs e)
	{
		dragging = false;
	}

	private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (listBox1.SelectedIndex != -1)
		{
			selectedObj = int.Parse(((string)listBox1.SelectedItem).Split(' ')[0]);
			listBox2.SelectedIndex = -1;
			listBox3.SelectedIndex = -1;
			groupBox2.Enabled = true;
			textBox2.Text = Objects[selectedObj].ID.ToString();
			label10.Text = ((string)listBox1.SelectedItem).Split(' ')[2];
			textBox1.Text = Objects[selectedObj].Theta.ToString();
			numericUpDown1.Value = (int)Objects[selectedObj].Z;
			numericUpDown2.Value = (int)Objects[selectedObj].Y;
			numericUpDown3.Value = (int)Objects[selectedObj].X;
			checkBox6.Checked = Objects[selectedObj].DistFade;
			if (Objects[selectedObj].Theta < 0f)
			{
				Objects[selectedObj].Theta += (float)Math.PI * 2f;
			}
			trackBar1.Value = (int)(Objects[selectedObj].Theta * 100f);
		}
	}

	private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (listBox2.SelectedIndex != -1)
		{
			selectedObj = int.Parse(((string)listBox2.SelectedItem).Split(' ')[0]);
			listBox1.SelectedIndex = -1;
			listBox3.SelectedIndex = -1;
			groupBox2.Enabled = true;
			textBox2.Text = Objects[selectedObj].ID.ToString();
			label10.Text = ((string)listBox2.SelectedItem).Split(' ')[2];
			textBox1.Text = Objects[selectedObj].Theta.ToString();
			numericUpDown1.Value = (decimal)Objects[selectedObj].Z;
			numericUpDown2.Value = (decimal)Objects[selectedObj].Y;
			numericUpDown3.Value = (decimal)Objects[selectedObj].X;
			checkBox6.Checked = Objects[selectedObj].DistFade;
			if (Objects[selectedObj].Theta < 0f)
			{
				Objects[selectedObj].Theta += (float)Math.PI * 2f;
			}
			trackBar1.Value = (int)(Objects[selectedObj].Theta * 100f);
		}
	}

	private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (listBox3.SelectedIndex != -1)
		{
			selectedObj = int.Parse(((string)listBox3.SelectedItem).Split(' ')[0]);
			listBox2.SelectedIndex = -1;
			listBox1.SelectedIndex = -1;
			groupBox2.Enabled = true;
			textBox2.Text = Objects[selectedObj].ID.ToString();
			label10.Text = ((string)listBox3.SelectedItem).Split(' ')[2];
			textBox1.Text = Objects[selectedObj].Theta.ToString();
			numericUpDown1.Value = (decimal)Objects[selectedObj].Z;
			numericUpDown2.Value = (decimal)Objects[selectedObj].Y;
			numericUpDown3.Value = (decimal)Objects[selectedObj].X;
			checkBox6.Checked = Objects[selectedObj].DistFade;
			if (Objects[selectedObj].Theta < 0f)
			{
				Objects[selectedObj].Theta += (float)Math.PI * 2f;
			}
			trackBar1.Value = (int)(Objects[selectedObj].Theta * 100f);
		}
	}

	private void trackBar1_Scroll(object sender, EventArgs e)
	{
		Objects[selectedObj].setRotation((float)trackBar1.Value / 100f);
		textBox1.Text = ((float)trackBar1.Value / 100f).ToString();
	}

	private void numericUpDown3_ValueChanged(object sender, EventArgs e)
	{
		Objects[selectedObj].X = (float)numericUpDown3.Value;
	}

	private void numericUpDown2_ValueChanged(object sender, EventArgs e)
	{
		Objects[selectedObj].Y = (float)numericUpDown2.Value;
	}

	private void numericUpDown1_ValueChanged(object sender, EventArgs e)
	{
		Objects[selectedObj].Z = (float)numericUpDown1.Value;
	}

	private void button3_Click(object sender, EventArgs e)
	{
		form2 = new Form2(ObjNames, Objects[selectedObj].nameI);
		form2.ShowDialog();
		int selectedItem = form2.SelectedItem;
		form2 = null;
		if (!glControl1.Context.IsCurrent)
		{
			glControl1.MakeCurrent();
		}
		if (selectedItem == -1)
		{
			return;
		}
		Objects[selectedObj].nameI = selectedItem;
		Objects[selectedObj].LoadFiles(pathPref, ObjNames.names[selectedItem]);
		List<MeshTexture[]> list = new List<MeshTexture[]>();
		list.Add(Objects[selectedObj].readMaterial());
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
		Objects[selectedObj].FindTex(texNames, texIDs);
		fillLists();
	}

	private void button4_Click(object sender, EventArgs e)
	{
		form2 = new Form2(ObjNames, -1);
		form2.ShowDialog();
		int selectedItem = form2.SelectedItem;
		form2 = null;
		if (!glControl1.Context.IsCurrent)
		{
			glControl1.MakeCurrent();
		}
		if (selectedItem == -1)
		{
			return;
		}
		MapObject mapObject = new MapObject();
		mapObject.nameI = selectedItem;
		mapObject.LoadFiles(pathPref, ObjNames.names[selectedItem]);
		List<MeshTexture[]> list = new List<MeshTexture[]>();
		list.Add(mapObject.readMaterial());
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
		mapObject.FindTex(texNames, texIDs);
		mapObject.X = 0f;
		mapObject.Y = 0f;
		mapObject.Z = 0f;
		mapObject.Theta = 0f;
		mapObject.ID = genUniqueID();
		mapObject.DistFade = true;
		Objects.Add(mapObject);
		selectedObj = Objects.Count - 1;
		fillLists();
	}

	private void button5_Click(object sender, EventArgs e)
	{
		MapObject mapObject = new MapObject();
		mapObject.nameI = Objects[selectedObj].nameI;
		mapObject.LoadFiles(pathPref, ObjNames.names[mapObject.nameI]);
		List<MeshTexture[]> list = new List<MeshTexture[]>();
		list.Add(mapObject.readMaterial());
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
		mapObject.FindTex(texNames, texIDs);
		mapObject.X = Objects[selectedObj].X;
		mapObject.Y = Objects[selectedObj].Y;
		mapObject.Z = Objects[selectedObj].Z;
		mapObject.Theta = Objects[selectedObj].Theta;
		mapObject.ID = genUniqueID();
		Objects.Add(mapObject);
		selectedObj = Objects.Count - 1;
		fillLists();
	}

	private void button6_Click(object sender, EventArgs e)
	{
		if (selectedObj != -1)
		{
			Objects.RemoveAt(selectedObj);
			selectedObj = -1;
			fillLists();
		}
		else
		{
			MessageBox.Show("Select an object first!");
		}
	}

	private void fillLists()
	{
		listBox1.Items.Clear();
		listBox2.Items.Clear();
		listBox3.Items.Clear();
		for (int i = 0; i < Objects.Count; i++)
		{
			MapObject mapObject = Objects[i];
			string text = ObjNames.names[mapObject.nameI];
			if (text.Contains("\\bldg\\"))
			{
				listBox1.Items.Add(i + " - " + text.Substring(text.LastIndexOf('\\') + 1));
				if (i == selectedObj)
				{
					listBox1.SelectedIndex = listBox1.Items.Count - 1;
				}
			}
			else if (text.Contains("\\nature\\"))
			{
				listBox2.Items.Add(i + " - " + text.Substring(text.LastIndexOf('\\') + 1));
				if (i == selectedObj)
				{
					listBox2.SelectedIndex = listBox2.Items.Count - 1;
				}
			}
			else
			{
				listBox3.Items.Add(i + " - " + text.Substring(text.LastIndexOf('\\') + 1));
				if (i == selectedObj)
				{
					listBox3.SelectedIndex = listBox3.Items.Count - 1;
				}
			}
		}
	}

	private int genUniqueID()
	{
		int i = 0;
		List<int> list = new List<int>();
		foreach (MapObject @object in Objects)
		{
			list.Add(@object.ID);
		}
		for (; list.Contains(i); i++)
		{
		}
		return i;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			saveMap(saveFileDialog1.FileName);
		}
	}

	private void saveMap(string path)
	{
		List<int>[] array = new List<int>[144];
		for (int i = 0; i < Objects.Count; i++)
		{
			if (Objects[i].groups == null)
			{
				Objects[i].calcGroups();
			}
			else if (Objects[i].groups.Count == 0)
			{
				Objects[i].calcGroups();
			}
			for (int j = 0; j < Objects[i].groups.Count; j++)
			{
				if (array[Objects[i].groups[j]] == null)
				{
					array[Objects[i].groups[j]] = new List<int>();
				}
				array[Objects[i].groups[j]].Add(i);
			}
		}
		BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create));
		binaryWriter.Write("JMXVMAPO1001".ToCharArray());
		for (int k = 0; k < 144; k++)
		{
			if (array[k] == null)
			{
				binaryWriter.Write((short)0);
				continue;
			}
			binaryWriter.Write((short)array[k].Count);
			for (int l = 0; l < array[k].Count; l++)
			{
				MapObject mapObject = Objects[array[k][l]];
				binaryWriter.Write(mapObject.nameI);
				float num = mapObject.X;
				if (mapObject.X < 0f)
				{
					num += 1920f;
				}
				if (mapObject.X > 1920f)
				{
					num -= 1920f;
				}
				binaryWriter.Write(num);
				binaryWriter.Write(mapObject.Y);
				float num2 = mapObject.Z;
				if (mapObject.Z < 0f)
				{
					num2 += 1920f;
				}
				if (mapObject.Z > 1920f)
				{
					num2 -= 1920f;
				}
				binaryWriter.Write(num2);
				if (!mapObject.DistFade)
				{
					binaryWriter.Write(new byte[2] { 255, 255 });
				}
				else
				{
					binaryWriter.Write((short)0);
				}
				binaryWriter.Write(mapObject.Theta);
				binaryWriter.Write(mapObject.ID);
				if (mapObject.Unknown1 == "0100")
				{
					binaryWriter.Write((short)1);
				}
				else
				{
					binaryWriter.Write((short)1);
				}
				binaryWriter.Write((byte)((double)OX + Math.Floor(mapObject.X / 1920f)));
				binaryWriter.Write((byte)((double)OY + Math.Floor(mapObject.Z / 1920f)));
			}
		}
		binaryWriter.Close();
		MessageBox.Show("File saved");
	}

	private void checkBox6_CheckedChanged(object sender, EventArgs e)
	{
		Objects[selectedObj].DistFade = checkBox6.Checked;
	}

	private void button7_Click(object sender, EventArgs e)
	{
		if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
		{
			nvmFile = new NVM(folderBrowserDialog1.SelectedPath + "\\nv_" + OY.ToString("X").ToLower() + OX.ToString("X").ToLower() + ".nvm");
		}
	}

	private void button8_Click(object sender, EventArgs e)
	{
		saveFileDialog2.FileName = nvmFile.filepath;
		nvmFile.setEntities(Objects, pathPref, ObjNames.names);
		if (saveFileDialog2.ShowDialog() == DialogResult.OK)
		{
			nvmFile.saveNVM(saveFileDialog2.FileName, OX, OY);
		}
	}
}
