
namespace DesktopTanuki
{
    partial class TanukiMainBody
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TanukiMainBody));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemUranai = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBakushin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.tanukiByeTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tanukiMoveTimer = new System.Windows.Forms.Timer(this.components);
            this.tanukiFukidashiTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemUranai,
            this.toolStripMenuItemBakushin,
            this.toolStripSeparator1,
            this.toolStripMenuItemQuit,
            this.toolStripSeparator2,
            this.toolStripMenuItemVersion});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 104);
            // 
            // toolStripMenuItemUranai
            // 
            this.toolStripMenuItemUranai.Name = "toolStripMenuItemUranai";
            this.toolStripMenuItemUranai.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemUranai.Text = "占い";
            this.toolStripMenuItemUranai.Click += new System.EventHandler(this.toolStripMenuItemUranai_Click);
            // 
            // toolStripMenuItemBakushin
            // 
            this.toolStripMenuItemBakushin.Name = "toolStripMenuItemBakushin";
            this.toolStripMenuItemBakushin.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemBakushin.Text = "驀進";
            this.toolStripMenuItemBakushin.Click += new System.EventHandler(this.toolStripMenuItemBakushin_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // toolStripMenuItemQuit
            // 
            this.toolStripMenuItemQuit.Name = "toolStripMenuItemQuit";
            this.toolStripMenuItemQuit.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemQuit.Text = "バイバイ(^^)/~~~";
            this.toolStripMenuItemQuit.Click += new System.EventHandler(this.toolStripMenuItemQuit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(162, 6);
            // 
            // toolStripMenuItemVersion
            // 
            this.toolStripMenuItemVersion.Name = "toolStripMenuItemVersion";
            this.toolStripMenuItemVersion.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemVersion.Text = "バージョン情報";
            this.toolStripMenuItemVersion.Click += new System.EventHandler(this.toolStripMenuItemVersion_Click);
            // 
            // tanukiByeTimer
            // 
            this.tanukiByeTimer.Tick += new System.EventHandler(this.tanukiByeTimer_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "たぬき召喚中";
            this.notifyIcon1.Visible = true;
            // 
            // tanukiMoveTimer
            // 
            this.tanukiMoveTimer.Interval = 10;
            this.tanukiMoveTimer.Tick += new System.EventHandler(this.tanukiMoveTimer_Tick);
            // 
            // tanukiFukidashiTimer
            // 
            this.tanukiFukidashiTimer.Tick += new System.EventHandler(this.tanukiFukidashiTimer_Tick);
            // 
            // TanukiMainBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TanukiMainBody";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tanuki";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TanukiView_FormClosing);
            this.Load += new System.EventHandler(this.Tanuki_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TanukiMainBody_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TanukiBody_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TanukiBody_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TanukiView_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TanukiBody_MouseDown);
            this.MouseLeave += new System.EventHandler(this.TanukiBody_MouseLeave);
            this.MouseHover += new System.EventHandler(this.TanukiBody_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TanukiBody_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TanukiBody_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TanukiBody_PreviewKeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVersion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemQuit;
        private System.Windows.Forms.Timer tanukiByeTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer tanukiMoveTimer;
        private System.Windows.Forms.Timer tanukiFukidashiTimer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUranai;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBakushin;
    }
}

