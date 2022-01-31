using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopTanuki
{
    public partial class TanukiBody : Form
    {
        bool bolBye;

        /// <summary>
        /// たぬき本体
        /// </summary>
        public TanukiBody()
        {
            InitializeComponent();

            bolBye = false;
        }

        /// <summary>
        /// たぬき召喚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tanuki_Load(object sender, EventArgs e)
        {
            //------------------------------
            // 初期表示位置の決定
            //------------------------------
            Bitmap bmp_skin = global::DesktopTanuki.Properties.Resources.tanuki_001;


            FrameDimension frameDimension = new FrameDimension(bmp_skin.FrameDimensionsList[0]);
            int int_frame_count = bmp_skin.GetFrameCount(frameDimension);

            // 全フレームをチェックしてY座標最大値を取得する。
            int int_bottom = -1;
            for (int int_frame_no = 0; int_frame_no < int_frame_count; ++int_frame_no)
            {
                bmp_skin.SelectActiveFrame(frameDimension, int_frame_no);

                for (int int_y = Height - 1; int_y >= 0; --int_y)
                {
                    bool bol_found = false;
                    for (int int_x = 0; int_x < Width; ++int_x)
                    {
                        Color color = bmp_skin.GetPixel(int_x, int_y);
                        if (color.A != 0)
                        {
                            bol_found = true;
                            if (int_bottom < int_y)
                            {
                                int_bottom = int_y;
                            }
                            break;
                        }
                    }
                    if (bol_found)
                    {
                        break;
                    }
               }
            }

            Height = bmp_skin.Height;
            Width = bmp_skin.Width;
            BackgroundImage = bmp_skin;


            // タスクバーを除くデスクトップ作業領域サイズ
            int screen_width = Screen.PrimaryScreen.WorkingArea.Width;
            int screen_height = Screen.PrimaryScreen.WorkingArea.Height;
            // たぬきサイズ
            int tanuki_width = Width;
            int tanuki_height = Height;



            // 初期表示位置設定
            Location = new Point(screen_width - tanuki_width, screen_height - int_bottom);

            // たぬき動作許可
            ImageAnimator.Animate(BackgroundImage, new EventHandler(Image_FrameChanged));
        }

        /// <summary>
        /// たぬき動作
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Image_FrameChanged(object o, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// たぬき動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_Paint(object sender, PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames(BackgroundImage);
        }

        private void TanukiView_MouseClick(object sender, MouseEventArgs e)
        {
           
            if (e.Button == MouseButtons.Left)
            {
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position, ToolStripDropDownDirection.AboveLeft);
            }
        }

        /// <summary>
        /// たぬきバイバイ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tanukiByeTimer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// たぬきバイバイ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if  (!bolBye)
            {
                tanukiByeTimer.Interval = 1000;
                tanukiByeTimer.Enabled = true;

                BackgroundImage.Dispose();
                BackgroundImage = global::DesktopTanuki.Properties.Resources.tanuki_002;
                ImageAnimator.Animate(BackgroundImage, new EventHandler(Image_FrameChanged));

                e.Cancel = true;

                bolBye = true;
            }
        }
    }
}
