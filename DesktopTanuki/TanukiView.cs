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
        bool m_bolBye;

        Point m_initPos;

        System.EventHandler m_ehFrameChanged;

        int m_int_tanuki_number;

        /// <summary>
        /// たぬき本体
        /// </summary>
        public TanukiBody()
        {
            InitializeComponent();

            m_bolBye = false;

            m_ehFrameChanged = new EventHandler(Image_FrameChanged);
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

            TopMost = true;

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
            m_initPos = new Point(screen_width - tanuki_width, screen_height - int_bottom);
            Location = m_initPos;

            // たぬき動作許可
            ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);
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
        /// たぬき召喚解除指示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            // たぬきとバイバイする。
            Close();
        }

        /// <summary>
        /// たぬきバイバイ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if  (!m_bolBye)
            {
                // たぬきは召喚解除されるとき、召喚解除直前にバイバイする。

                // 手を振る前の召喚解除命令はキャンセルする。
                e.Cancel = true;

                // たぬきの動作をバイバイに変更する。
                BackgroundImage.Dispose();
                BackgroundImage = global::DesktopTanuki.Properties.Resources.tanuki_002;
                ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);

                // バイバイする時間はタイマーで決める。
                tanukiByeTimer.Interval = 1000;     // バイバイし続ける時間(1000ms)
                tanukiByeTimer.Enabled = true;      // バイバイタイマー起動

                // 手を振る準備が整ったので、バイバイ状態ONにする。
                m_bolBye = true;
            }
            else
            {

            }
        }

        /// <summary>
        /// バイバイタイマータイムアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tanukiByeTimer_Tick(object sender, EventArgs e)
        {
            // たぬきとバイバイする。
            Close();
        }

        /// <summary>
        /// バージョン情報
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemVersion_Click(object sender, EventArgs e)
        {
            System.Diagnostics.FileVersionInfo ver =
                System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            MessageBox.Show( "バージョン情報\n" + ver);
        }

        Point m_mousePoint;

        /// <summary>
        /// たぬき捕獲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                m_mousePoint = new Point(e.X, e.Y);

                BackgroundImage.Dispose();
                BackgroundImage = global::DesktopTanuki.Properties.Resources.tanuki_003;
                ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);
            }
        }

        /// <summary>
        /// たぬき捕獲中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Left += e.X - m_mousePoint.X;
                Top += e.Y - m_mousePoint.Y;
            }
        }


        /// <summary>
        /// たぬき捕獲解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_MouseUp(object sender, MouseEventArgs e)
        {
            BackgroundImage.Dispose();
            BackgroundImage = global::DesktopTanuki.Properties.Resources.tanuki_001;
            ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);

            Top = m_initPos.Y;
        }

        /// <summary>
        /// たぬき指示開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // 左移動指示
            if (e.KeyCode == Keys.Left)
            {
                if (m_int_tanuki_number != 4)
                {
                    BackgroundImage.Dispose();
                    BackgroundImage = global::DesktopTanuki.Properties.Resources.tanuki_004;
                    ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);
                    m_int_tanuki_number = 4;

                    tanukiMoveTimer.Interval = 10;
                    tanukiMoveTimer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// たぬき指示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_KeyDown(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// たぬき指示解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_KeyUp(object sender, KeyEventArgs e)
        {
            // 左移動指示解除
            if (m_int_tanuki_number != 1)
            {
                BackgroundImage.Dispose();
                BackgroundImage = global::DesktopTanuki.Properties.Resources.tanuki_001;
                ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);
                m_int_tanuki_number = 1;

                tanukiMoveTimer.Enabled = false;
            }
        }

        /// <summary>
        /// たぬき移動タイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tanukiMoveTimer_Tick(object sender, EventArgs e)
        {
            if ( Left < -1 * Width)
            {
                Left = Screen.PrimaryScreen.WorkingArea.Width;
            }
            else
            {
                Left -= 10;
            }
        }
    }
}
