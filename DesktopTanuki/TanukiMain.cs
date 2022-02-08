using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DesktopTanuki
{
    public partial class TanukiMainBody : Form
    {
        TanukiSubBody m_tanukiSubBodyUranai;
        TanukiSubBody m_tanukiSubBodyBakushin;

        Fukidashi m_frm_fukidashi;

        bool m_bolBye;

        Point m_initPos;

        System.EventHandler m_ehFrameChanged;

        string m_str_tanuki_number;

        Rectangle m_rectangle;

        int m_int_hima_counter;
        int m_int_hima_counter_max;

        int m_int_daikichi_counter;
        int m_int_daikyo_counter;

        /// <summary>
        /// たぬき本体
        /// </summary>
        public TanukiMainBody()
        {
            InitializeComponent();

            TopMost = true;

            m_tanukiSubBodyUranai = new TanukiSubBody();
            m_tanukiSubBodyBakushin = new TanukiSubBody();

            m_frm_fukidashi = new Fukidashi();

            m_bolBye = false;

            m_ehFrameChanged = new EventHandler(Image_FrameChanged);

            tanukiFukidashiTimer.Interval = 5000;
            //tanukiFukidashiTimer.Enabled = true;

            m_int_hima_counter = 0;
            m_int_hima_counter_max = 100;

            m_int_daikichi_counter = 0;
            m_int_daikyo_counter = 0;
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
            setTanukiBody("1");

            // タスクバーを除くデスクトップ作業領域サイズ
            int screen_width = Screen.PrimaryScreen.WorkingArea.Width;
            int screen_height = Screen.PrimaryScreen.WorkingArea.Height;

            // 初期表示位置設定
            m_initPos = new Point(screen_width - Width, screen_height - m_rectangle.Bottom);
            Location = m_initPos;
        }


        /// <summary>
        /// たぬき本体切り替え
        /// </summary>
        /// <param name="str_tanuki_number"></param>
        private void setTanukiBody(string str_tanuki_number)
        {
            Bitmap bitmap;
            switch (str_tanuki_number)
            {
                case "1":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_001;
                    break;
                case "1A":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_001A;
                    break;
                case "1B":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_001B;
                    break;
                case "1C":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_001C;
                    break;
                case "2":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_002;
                    break;
                case "3":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_003;
                    break;
                case "4":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_004;
                    break;
                case "5":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_005;
                    break;
                case "5A":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_005A;
                    break;
                case "5B":
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_005B;
                    break;
                default:
                    bitmap = global::DesktopTanuki.Properties.Resources.tanuki_001;
                    break;
            }

            m_rectangle = getTanukiBorder(bitmap);

            Height = bitmap.Height;
            Width = bitmap.Width;

            if (BackgroundImage != null)
            {
                BackgroundImage.Dispose();
            }
            BackgroundImage = bitmap;
            ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);

            Top = Screen.PrimaryScreen.WorkingArea.Height - m_rectangle.Bottom;

            m_str_tanuki_number = str_tanuki_number;
        }

        /// <summary>
        /// たぬき境界値取得
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private Rectangle getTanukiBorder(Bitmap bitmap)
        {
            FrameDimension frameDimension = new FrameDimension(bitmap.FrameDimensionsList[0]);
            int int_frame_count = bitmap.GetFrameCount(frameDimension);

            int int_left = -1;
            int int_top = -1;
            int int_right = -1;
            int int_bottom = -1;

            for (int int_frame_no = 0; int_frame_no < int_frame_count; ++int_frame_no)
            {
                bitmap.SelectActiveFrame(frameDimension, int_frame_no);

                // フレームをチェックしてX座標最小値を取得する。
                for (int int_x = 0; int_x < Width; ++int_x)
                {
                    bool bol_found = false;
                    for (int int_y = 0; int_y < Height; ++int_y)
                    {
                        Color color = bitmap.GetPixel(int_x, int_y);
                        if (color.A != 0)
                        {
                            bol_found = true;
                            if (int_left < int_x)
                            {
                                int_left = int_x;
                            }
                            break;
                        }
                    }
                    if (bol_found)
                    {
                        break;
                    }
                }

                // フレームをチェックしてY座標最小値を取得する。
                for (int int_y = 0; int_y < Height; ++int_y)
                {
                    bool bol_found = false;
                    for (int int_x = 0; int_x < Width; ++int_x)
                    {
                        Color color = bitmap.GetPixel(int_x, int_y);
                        if (color.A != 0)
                        {
                            bol_found = true;
                            if (int_top < int_y)
                            {
                                int_top = int_y;
                            }
                            break;
                        }
                    }
                    if (bol_found)
                    {
                        break;
                    }
                }

                // フレームをチェックしてX座標最大値を取得する。
                for (int int_x = Width - 1; int_x >= 0; --int_x)
                {
                    bool bol_found = false;
                    for (int int_y = 0; int_y < Height; ++int_y)
                    {
                        Color color = bitmap.GetPixel(int_x, int_y);
                        if (color.A != 0)
                        {
                            bol_found = true;
                            if (int_right < int_x)
                            {
                                int_right = int_x;
                            }
                            break;
                        }
                    }
                    if (bol_found)
                    {
                        break;
                    }
                }

                // フレームをチェックしてY座標最大値を取得する。
                for (int int_y = Height - 1; int_y >= 0; --int_y)
                {
                    bool bol_found = false;
                    for (int int_x = 0; int_x < Width; ++int_x)
                    {
                        Color color = bitmap.GetPixel(int_x, int_y);
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

            return new Rectangle(int_left, int_top, int_right - int_left, int_bottom - int_top);
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
        private void TanukiMainBody_Paint(object sender, PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames(BackgroundImage);

            switch(m_str_tanuki_number)
            {
                case "1":
                    if (m_int_hima_counter > m_int_hima_counter_max)
                    {
                        m_int_hima_counter = 0;
                        setTanukiBody("5");
                    }
                    else
                    {
                        ++m_int_hima_counter;
                    }
                    break;
                case "5":
                    if (m_int_hima_counter > m_int_hima_counter_max)
                    {
                        m_int_hima_counter = 0;
                        setTanukiBody("5A");
                    }
                    else
                    {
                        ++m_int_hima_counter;
                    }
                    break;
                case "5A":
                    if (m_int_hima_counter > m_int_hima_counter_max)
                    {
                        m_int_hima_counter = 0;
                        setTanukiBody("5B");
                    }
                    else
                    {
                        ++m_int_hima_counter;
                    }
                    break;
                case "5B":
                    if (m_int_hima_counter > m_int_hima_counter_max)
                    {
                        m_int_hima_counter = 0;
                        setTanukiBody("5");
                    }
                    else
                    {
                        ++m_int_hima_counter;
                    }
                    break;
                default:
                    break;
            }

        }

        private void TanukiView_MouseClick(object sender, MouseEventArgs e)
        {
            m_int_hima_counter = 0;

            if (e.Button == MouseButtons.Left)
            {
                Application_Idle(sender, e);
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
            m_int_hima_counter = 0;

            if (!m_bolBye)
            {
                // たぬきは召喚解除されるとき、召喚解除直前にバイバイする。

                // 手を振る前の召喚解除命令はキャンセルする。
                e.Cancel = true;

                // たぬきの動作をバイバイに変更する。
                setTanukiBody("2");

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
            m_int_hima_counter = 0;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                m_mousePoint = new Point(e.X, e.Y);
                setTanukiBody("3");
            }
        }

        /// <summary>
        /// たぬき捕獲中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_MouseMove(object sender, MouseEventArgs e)
        {
            m_int_hima_counter = 0;

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
            m_int_hima_counter = 0;
            setTanukiBody("1");
        }

        /// <summary>
        /// たぬき指示開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TanukiBody_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            m_int_hima_counter = 0;

            // 左移動指示
            if (e.KeyCode == Keys.Left)
            {
                if (m_str_tanuki_number != "4")
                {
                    setTanukiBody("4");
                    tanukiMoveTimer.Enabled = true;
                }
            }
            else if (e.KeyCode == Keys.B)
            {
                toolStripMenuItemBakushin_Click(sender, e);
                this.Focus();
            }
            else if (e.KeyCode == Keys.U)
            {
                toolStripMenuItemUranai_Click(sender, e);
                this.Focus();
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
            if (m_str_tanuki_number == "4")
            {
                tanukiMoveTimer.Enabled = false;
                setTanukiBody("1");
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

        private void Application_Idle(Object sender, EventArgs e)
        {
        }

        private void tanukiFukidashiTimer_Tick(object sender, EventArgs e)
        {
            Point pos = new Point(Left - m_frm_fukidashi.Width / 2 + m_rectangle.Left, Top);
            m_frm_fukidashi.Location = pos;
            if (m_frm_fukidashi.Visible)
            {
                m_frm_fukidashi.Visible = false;
            }
            else
            {
                m_frm_fukidashi.setText("まだまだテスト中だし・・・");
                m_frm_fukidashi.Show();
            }
        }

        private void TanukiBody_MouseHover(object sender, EventArgs e)
        {
        }

        private void TanukiBody_MouseLeave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 占い
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemUranai_Click(object sender, EventArgs e)
        {
            m_int_hima_counter = 0;

            m_tanukiSubBodyUranai.doUranai(this);
        }

        public void receveUranaiKekka(string str_kekka)
        {
            m_int_hima_counter = 0;

            if ( str_kekka == "daikichi" )
            {
                ++m_int_daikichi_counter;
                m_int_daikyo_counter = 0;

                if (m_int_daikichi_counter == 1)
                {
                    setTanukiBody("1B");
                }
                else
                {
                    setTanukiBody("1C");
                }
            }
            else if (str_kekka == "daikyou")
            {
                ++m_int_daikyo_counter;
                m_int_daikichi_counter = 0;

                if (m_int_daikyo_counter == 1)
                {
                    setTanukiBody("3");
                }
                else
                {
                    setTanukiBody("3");
                }
            }
            else
            {
                setTanukiBody("1");
            }
        }

        /// <summary>
        /// 驀進
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemBakushin_Click(object sender, EventArgs e)
        {
            m_int_hima_counter = 0;

            m_tanukiSubBodyBakushin.doBakushin();
        }
    }
}
