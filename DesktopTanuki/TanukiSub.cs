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
    public partial class TanukiSubBody : Form
    {
        System.EventHandler m_ehFrameChanged;

        int m_int_uranaiStatus;

        /// <summary>
        /// たぬきサブ
        /// </summary>
        public TanukiSubBody()
        {
            InitializeComponent();

            m_ehFrameChanged = new EventHandler(Image_FrameChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        public void doBakushin()
        {
            DateTime dateTime = DateTime.Now;
            Random random = new Random(dateTime.Millisecond);
            int intBakushinType = random.Next(1, 3);
            switch(intBakushinType)
            {
                case 1:
                    setTanuki("8A");
                    break;
                case 2:
                    setTanuki("8B");
                    break;
                case 3:
                    setTanuki("8C");
                    break;
                default:
                    break;
            }
            timerBakushin.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBakushin_Tick(object sender, EventArgs e)
        {
            Point pos = Location;
            if (pos.X == -1 * Width)
            {
                timerBakushin.Enabled = false;
                setTanuki("");
            }
            else
            {
                pos.X -= 50;
                Location = pos;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void doUranai()
        {
            setTanuki("7");

            DateTime dateTime = DateTime.Now;
            Random random = new Random(dateTime.Millisecond);
            m_int_uranaiStatus = random.Next(1, 100);
            timerUranai.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUranai_Tick(object sender, EventArgs e)
        {
            if (m_int_uranaiStatus > 0)
            {
                if (m_int_uranaiStatus % 2 == 0)
                {
                    setTanuki("7A");
                }
                else
                {
                    setTanuki("7B");
                }
                m_int_uranaiStatus = 0;
            }
            else
            {
                setTanuki("");
                timerUranai.Enabled = false;
            }
        }

        /// <summary>
        /// たぬき降マ
        /// </summary>
        /// <param name="type"></param>
        public void setTanuki(String type)
        {
            Bitmap bmp_skin = null;
            switch (type)
            {
                case "7":
                    bmp_skin = global::DesktopTanuki.Properties.Resources.tanuki_007;
                    break;
                case "7A":
                    bmp_skin = global::DesktopTanuki.Properties.Resources.tanuki_007A;
                    break;
                case "7B":
                    bmp_skin = global::DesktopTanuki.Properties.Resources.tanuki_007B;
                    break;
                case "8A":
                    bmp_skin = global::DesktopTanuki.Properties.Resources.tanuki_008A;
                    break;
                case "8B":
                    bmp_skin = global::DesktopTanuki.Properties.Resources.tanuki_008B;
                    break;
                case "8C":
                    bmp_skin = global::DesktopTanuki.Properties.Resources.tanuki_008C;
                    break;
                default:
                    break;
            }

            if (bmp_skin == null)
            {
                if (BackgroundImage != null)
                {
                    BackgroundImage.Dispose();
                }
                Visible = false;
            }
            else
            {
                TopMost = true;

                FrameDimension frameDimension = new FrameDimension(bmp_skin.FrameDimensionsList[0]);
                int int_frame_count = bmp_skin.GetFrameCount(frameDimension);

                int int_left = -1;
                int int_bottom = -1;
                for (int int_frame_no = 0; int_frame_no < int_frame_count; ++int_frame_no)
                {
                    bmp_skin.SelectActiveFrame(frameDimension, int_frame_no);

                    // 全フレームをチェックしてY座標最大値を取得する。
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

                    // 全フレームをチェックしてX座標最小値を取得する。
                    for (int int_x = 0; int_x < Width; ++int_x)
                    {
                        bool bol_found = false;
                        for (int int_y = 0; int_y < Height; ++int_y)
                        {
                            Color color = bmp_skin.GetPixel(int_x, int_y);
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

                }

                Height = bmp_skin.Height;
                Width = bmp_skin.Width;

                // タスクバーを除くデスクトップ作業領域サイズ
                int screen_width = Screen.PrimaryScreen.WorkingArea.Width;
                int screen_height = Screen.PrimaryScreen.WorkingArea.Height;

                // たぬきサイズ
                int tanuki_width = Width;
                int tanuki_height = Height;

                Point pos;
                switch (type)
                {
                    case "7":
                    case "7A":
                    case "7B":
                        pos = new Point(0, screen_height - int_bottom);
                        break;
                    case "8A":
                    case "8B":
                    case "8C":
                        pos = new Point(screen_width, screen_height - int_bottom);
                        break;
                    default:
                        pos = new Point(0, screen_height - int_bottom);
                        break;
                }
                Location = pos;

                if (BackgroundImage != null)
                {
                    BackgroundImage.Dispose();
                }
                BackgroundImage = bmp_skin;
                ImageAnimator.Animate(BackgroundImage, m_ehFrameChanged);
                Visible = true;
            }
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
        private void TanukiSubBody_Paint(object sender, PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames(BackgroundImage);
        }
    }
}
