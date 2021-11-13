
namespace RayTracingRoom
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb = new System.Windows.Forms.PictureBox();
            this.groupBox_cube1 = new System.Windows.Forms.GroupBox();
            this.rb_cube1_transparent = new System.Windows.Forms.RadioButton();
            this.rb_cube1_mirror = new System.Windows.Forms.RadioButton();
            this.rb_cube1_fill = new System.Windows.Forms.RadioButton();
            this.groupBox_cube2 = new System.Windows.Forms.GroupBox();
            this.rb_cube2_transparent = new System.Windows.Forms.RadioButton();
            this.rb_cube2_mirror = new System.Windows.Forms.RadioButton();
            this.rb_cube2_fill = new System.Windows.Forms.RadioButton();
            this.groupBox_sphere = new System.Windows.Forms.GroupBox();
            this.rb_sphere_transparent = new System.Windows.Forms.RadioButton();
            this.rb_sphere_mirror = new System.Windows.Forms.RadioButton();
            this.rb_sphere_fill = new System.Windows.Forms.RadioButton();
            this.groupBox_sides = new System.Windows.Forms.GroupBox();
            this.checkBox_up = new System.Windows.Forms.CheckBox();
            this.checkBox_down = new System.Windows.Forms.CheckBox();
            this.checkBox_back = new System.Windows.Forms.CheckBox();
            this.checkBox_right = new System.Windows.Forms.CheckBox();
            this.checkBox_front = new System.Windows.Forms.CheckBox();
            this.checkBox_left = new System.Windows.Forms.CheckBox();
            this.button_draw = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Z = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_set_light_pos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.groupBox_cube1.SuspendLayout();
            this.groupBox_cube2.SuspendLayout();
            this.groupBox_sphere.SuspendLayout();
            this.groupBox_sides.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(685, 685);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            // 
            // groupBox_cube1
            // 
            this.groupBox_cube1.Controls.Add(this.rb_cube1_transparent);
            this.groupBox_cube1.Controls.Add(this.rb_cube1_mirror);
            this.groupBox_cube1.Controls.Add(this.rb_cube1_fill);
            this.groupBox_cube1.Location = new System.Drawing.Point(6, 690);
            this.groupBox_cube1.Name = "groupBox_cube1";
            this.groupBox_cube1.Size = new System.Drawing.Size(210, 40);
            this.groupBox_cube1.TabIndex = 1;
            this.groupBox_cube1.TabStop = false;
            this.groupBox_cube1.Text = "Cube 1";
            // 
            // rb_cube1_transparent
            // 
            this.rb_cube1_transparent.AutoSize = true;
            this.rb_cube1_transparent.Location = new System.Drawing.Point(122, 15);
            this.rb_cube1_transparent.Name = "rb_cube1_transparent";
            this.rb_cube1_transparent.Size = new System.Drawing.Size(86, 19);
            this.rb_cube1_transparent.TabIndex = 2;
            this.rb_cube1_transparent.Text = "Transparent";
            this.rb_cube1_transparent.UseVisualStyleBackColor = true;
            // 
            // rb_cube1_mirror
            // 
            this.rb_cube1_mirror.AutoSize = true;
            this.rb_cube1_mirror.Location = new System.Drawing.Point(58, 15);
            this.rb_cube1_mirror.Name = "rb_cube1_mirror";
            this.rb_cube1_mirror.Size = new System.Drawing.Size(58, 19);
            this.rb_cube1_mirror.TabIndex = 1;
            this.rb_cube1_mirror.Text = "Mirror";
            this.rb_cube1_mirror.UseVisualStyleBackColor = true;
            // 
            // rb_cube1_fill
            // 
            this.rb_cube1_fill.AutoSize = true;
            this.rb_cube1_fill.Checked = true;
            this.rb_cube1_fill.Location = new System.Drawing.Point(12, 15);
            this.rb_cube1_fill.Name = "rb_cube1_fill";
            this.rb_cube1_fill.Size = new System.Drawing.Size(40, 19);
            this.rb_cube1_fill.TabIndex = 0;
            this.rb_cube1_fill.TabStop = true;
            this.rb_cube1_fill.Text = "Fill";
            this.rb_cube1_fill.UseVisualStyleBackColor = true;
            // 
            // groupBox_cube2
            // 
            this.groupBox_cube2.Controls.Add(this.rb_cube2_transparent);
            this.groupBox_cube2.Controls.Add(this.rb_cube2_mirror);
            this.groupBox_cube2.Controls.Add(this.rb_cube2_fill);
            this.groupBox_cube2.Location = new System.Drawing.Point(236, 691);
            this.groupBox_cube2.Name = "groupBox_cube2";
            this.groupBox_cube2.Size = new System.Drawing.Size(210, 40);
            this.groupBox_cube2.TabIndex = 3;
            this.groupBox_cube2.TabStop = false;
            this.groupBox_cube2.Text = "Cube 2";
            // 
            // rb_cube2_transparent
            // 
            this.rb_cube2_transparent.AutoSize = true;
            this.rb_cube2_transparent.Checked = true;
            this.rb_cube2_transparent.Location = new System.Drawing.Point(122, 15);
            this.rb_cube2_transparent.Name = "rb_cube2_transparent";
            this.rb_cube2_transparent.Size = new System.Drawing.Size(86, 19);
            this.rb_cube2_transparent.TabIndex = 2;
            this.rb_cube2_transparent.TabStop = true;
            this.rb_cube2_transparent.Text = "Transparent";
            this.rb_cube2_transparent.UseVisualStyleBackColor = true;
            // 
            // rb_cube2_mirror
            // 
            this.rb_cube2_mirror.AutoSize = true;
            this.rb_cube2_mirror.Location = new System.Drawing.Point(58, 15);
            this.rb_cube2_mirror.Name = "rb_cube2_mirror";
            this.rb_cube2_mirror.Size = new System.Drawing.Size(58, 19);
            this.rb_cube2_mirror.TabIndex = 1;
            this.rb_cube2_mirror.Text = "Mirror";
            this.rb_cube2_mirror.UseVisualStyleBackColor = true;
            // 
            // rb_cube2_fill
            // 
            this.rb_cube2_fill.AutoSize = true;
            this.rb_cube2_fill.Location = new System.Drawing.Point(12, 15);
            this.rb_cube2_fill.Name = "rb_cube2_fill";
            this.rb_cube2_fill.Size = new System.Drawing.Size(40, 19);
            this.rb_cube2_fill.TabIndex = 0;
            this.rb_cube2_fill.Text = "Fill";
            this.rb_cube2_fill.UseVisualStyleBackColor = true;
            // 
            // groupBox_sphere
            // 
            this.groupBox_sphere.Controls.Add(this.rb_sphere_transparent);
            this.groupBox_sphere.Controls.Add(this.rb_sphere_mirror);
            this.groupBox_sphere.Controls.Add(this.rb_sphere_fill);
            this.groupBox_sphere.Location = new System.Drawing.Point(467, 691);
            this.groupBox_sphere.Name = "groupBox_sphere";
            this.groupBox_sphere.Size = new System.Drawing.Size(210, 40);
            this.groupBox_sphere.TabIndex = 3;
            this.groupBox_sphere.TabStop = false;
            this.groupBox_sphere.Text = "Sphere";
            // 
            // rb_sphere_transparent
            // 
            this.rb_sphere_transparent.AutoSize = true;
            this.rb_sphere_transparent.Location = new System.Drawing.Point(122, 15);
            this.rb_sphere_transparent.Name = "rb_sphere_transparent";
            this.rb_sphere_transparent.Size = new System.Drawing.Size(86, 19);
            this.rb_sphere_transparent.TabIndex = 2;
            this.rb_sphere_transparent.Text = "Transparent";
            this.rb_sphere_transparent.UseVisualStyleBackColor = true;
            // 
            // rb_sphere_mirror
            // 
            this.rb_sphere_mirror.AutoSize = true;
            this.rb_sphere_mirror.Checked = true;
            this.rb_sphere_mirror.Location = new System.Drawing.Point(58, 15);
            this.rb_sphere_mirror.Name = "rb_sphere_mirror";
            this.rb_sphere_mirror.Size = new System.Drawing.Size(58, 19);
            this.rb_sphere_mirror.TabIndex = 1;
            this.rb_sphere_mirror.TabStop = true;
            this.rb_sphere_mirror.Text = "Mirror";
            this.rb_sphere_mirror.UseVisualStyleBackColor = true;
            // 
            // rb_sphere_fill
            // 
            this.rb_sphere_fill.AutoSize = true;
            this.rb_sphere_fill.Location = new System.Drawing.Point(12, 15);
            this.rb_sphere_fill.Name = "rb_sphere_fill";
            this.rb_sphere_fill.Size = new System.Drawing.Size(40, 19);
            this.rb_sphere_fill.TabIndex = 0;
            this.rb_sphere_fill.Text = "Fill";
            this.rb_sphere_fill.UseVisualStyleBackColor = true;
            // 
            // groupBox_sides
            // 
            this.groupBox_sides.Controls.Add(this.checkBox_up);
            this.groupBox_sides.Controls.Add(this.checkBox_down);
            this.groupBox_sides.Controls.Add(this.checkBox_back);
            this.groupBox_sides.Controls.Add(this.checkBox_right);
            this.groupBox_sides.Controls.Add(this.checkBox_front);
            this.groupBox_sides.Controls.Add(this.checkBox_left);
            this.groupBox_sides.Location = new System.Drawing.Point(27, 735);
            this.groupBox_sides.Name = "groupBox_sides";
            this.groupBox_sides.Size = new System.Drawing.Size(631, 40);
            this.groupBox_sides.TabIndex = 4;
            this.groupBox_sides.TabStop = false;
            this.groupBox_sides.Text = "Mirror sides ";
            // 
            // checkBox_up
            // 
            this.checkBox_up.AutoSize = true;
            this.checkBox_up.Location = new System.Drawing.Point(582, 17);
            this.checkBox_up.Name = "checkBox_up";
            this.checkBox_up.Size = new System.Drawing.Size(41, 19);
            this.checkBox_up.TabIndex = 5;
            this.checkBox_up.Text = "Up";
            this.checkBox_up.UseVisualStyleBackColor = true;
            // 
            // checkBox_down
            // 
            this.checkBox_down.AutoSize = true;
            this.checkBox_down.Location = new System.Drawing.Point(473, 17);
            this.checkBox_down.Name = "checkBox_down";
            this.checkBox_down.Size = new System.Drawing.Size(57, 19);
            this.checkBox_down.TabIndex = 4;
            this.checkBox_down.Text = "Down";
            this.checkBox_down.UseVisualStyleBackColor = true;
            // 
            // checkBox_back
            // 
            this.checkBox_back.AutoSize = true;
            this.checkBox_back.Location = new System.Drawing.Point(352, 17);
            this.checkBox_back.Name = "checkBox_back";
            this.checkBox_back.Size = new System.Drawing.Size(51, 19);
            this.checkBox_back.TabIndex = 3;
            this.checkBox_back.Text = "Back";
            this.checkBox_back.UseVisualStyleBackColor = true;
            // 
            // checkBox_right
            // 
            this.checkBox_right.AutoSize = true;
            this.checkBox_right.Location = new System.Drawing.Point(230, 17);
            this.checkBox_right.Name = "checkBox_right";
            this.checkBox_right.Size = new System.Drawing.Size(54, 19);
            this.checkBox_right.TabIndex = 2;
            this.checkBox_right.Text = "Right";
            this.checkBox_right.UseVisualStyleBackColor = true;
            // 
            // checkBox_front
            // 
            this.checkBox_front.AutoSize = true;
            this.checkBox_front.Location = new System.Drawing.Point(111, 17);
            this.checkBox_front.Name = "checkBox_front";
            this.checkBox_front.Size = new System.Drawing.Size(54, 19);
            this.checkBox_front.TabIndex = 1;
            this.checkBox_front.Text = "Front";
            this.checkBox_front.UseVisualStyleBackColor = true;
            // 
            // checkBox_left
            // 
            this.checkBox_left.AutoSize = true;
            this.checkBox_left.Location = new System.Drawing.Point(6, 17);
            this.checkBox_left.Name = "checkBox_left";
            this.checkBox_left.Size = new System.Drawing.Size(46, 19);
            this.checkBox_left.TabIndex = 0;
            this.checkBox_left.Text = "Left";
            this.checkBox_left.UseVisualStyleBackColor = true;
            // 
            // button_draw
            // 
            this.button_draw.Location = new System.Drawing.Point(6, 781);
            this.button_draw.Name = "button_draw";
            this.button_draw.Size = new System.Drawing.Size(116, 23);
            this.button_draw.TabIndex = 5;
            this.button_draw.Text = "Draw scene";
            this.button_draw.UseVisualStyleBackColor = true;
            this.button_draw.Click += new System.EventHandler(this.button_draw_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 785);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Light position:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 786);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "X";
            // 
            // textBox_X
            // 
            this.textBox_X.Location = new System.Drawing.Point(272, 782);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(29, 23);
            this.textBox_X.TabIndex = 8;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Location = new System.Drawing.Point(323, 782);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(29, 23);
            this.textBox_Y.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 786);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Y";
            // 
            // textBox_Z
            // 
            this.textBox_Z.Location = new System.Drawing.Point(379, 782);
            this.textBox_Z.Name = "textBox_Z";
            this.textBox_Z.Size = new System.Drawing.Size(29, 23);
            this.textBox_Z.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 786);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Z";
            // 
            // button_set_light_pos
            // 
            this.button_set_light_pos.Location = new System.Drawing.Point(418, 781);
            this.button_set_light_pos.Name = "button_set_light_pos";
            this.button_set_light_pos.Size = new System.Drawing.Size(107, 23);
            this.button_set_light_pos.TabIndex = 13;
            this.button_set_light_pos.Text = "Locate light";
            this.button_set_light_pos.UseVisualStyleBackColor = true;
            this.button_set_light_pos.Click += new System.EventHandler(this.button_set_light_pos_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 812);
            this.Controls.Add(this.button_set_light_pos);
            this.Controls.Add(this.textBox_Z);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_draw);
            this.Controls.Add(this.groupBox_sides);
            this.Controls.Add(this.groupBox_sphere);
            this.Controls.Add(this.groupBox_cube2);
            this.Controls.Add(this.groupBox_cube1);
            this.Controls.Add(this.pb);
            this.Name = "Form1";
            this.Text = "Ray Tracing Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.groupBox_cube1.ResumeLayout(false);
            this.groupBox_cube1.PerformLayout();
            this.groupBox_cube2.ResumeLayout(false);
            this.groupBox_cube2.PerformLayout();
            this.groupBox_sphere.ResumeLayout(false);
            this.groupBox_sphere.PerformLayout();
            this.groupBox_sides.ResumeLayout(false);
            this.groupBox_sides.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.GroupBox groupBox_cube1;
        private System.Windows.Forms.RadioButton rb_cube1_transparent;
        private System.Windows.Forms.RadioButton rb_cube1_mirror;
        private System.Windows.Forms.RadioButton rb_cube1_fill;
        private System.Windows.Forms.GroupBox groupBox_cube2;
        private System.Windows.Forms.RadioButton rb_cube2_transparent;
        private System.Windows.Forms.RadioButton rb_cube2_mirror;
        private System.Windows.Forms.RadioButton rb_cube2_fill;
        private System.Windows.Forms.GroupBox groupBox_sphere;
        private System.Windows.Forms.RadioButton rb_sphere_transparent;
        private System.Windows.Forms.RadioButton rb_sphere_mirror;
        private System.Windows.Forms.RadioButton rb_sphere_fill;
        private System.Windows.Forms.GroupBox groupBox_sides;
        private System.Windows.Forms.CheckBox checkBox_up;
        private System.Windows.Forms.CheckBox checkBox_down;
        private System.Windows.Forms.CheckBox checkBox_back;
        private System.Windows.Forms.CheckBox checkBox_right;
        private System.Windows.Forms.CheckBox checkBox_front;
        private System.Windows.Forms.CheckBox checkBox_left;
        private System.Windows.Forms.Button button_draw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Z;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_set_light_pos;
    }
}

