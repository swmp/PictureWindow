using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using System.Timers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ImageWindow
{
    public partial class ImageForm : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        Image m_curImage;        
        Size m_curWindowSize;
        string[] m_aImagefiles;
        int m_nImageCount;
        Random m_imageRd;
        int fMaxBoder = 640;
        //System.Timers.Timer m_imageTimer;
        public ImageForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            //ShowInTaskbar = false;
            
            SetStyle(ControlStyles.Opaque, true);
            m_imageRd = new Random();
            TopMost = true;
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            m_curWindowSize = Size;            
            InitializeFiles();
            updateImage();
            Rectangle rc = Screen.PrimaryScreen.WorkingArea;
            SetWindowPos(this.Handle, -1, rc.Right - m_curWindowSize.Width, rc.Bottom - m_curWindowSize.Height, m_curWindowSize.Width, m_curWindowSize.Height, 0x0040);
        }

        void InitializeFiles()
        {
            if(!File.Exists("info.txt"))
            {
                MessageBox.Show("找不到info.txt", "系统提示");
                Application.Exit();
                return;
            }            
            FileStream fs = new FileStream("info.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            m_nImageCount = 0;
            int nLineIndex = 0;
            string s;               
            while (sr.Peek() >= 0)
            {
                s = sr.ReadLine();
                if(s.Length > 2)
                {
                    switch (nLineIndex)
                    {
                        case 0:
                            {
                                m_aImagefiles = Directory.GetFiles(s, "*.jpg");
                                m_nImageCount = m_aImagefiles.Length;
                            }
                            break;
                        case 1:
                            {
                                int nTime = int.Parse(s);
                                if (nTime > 0)
                                {
                                    picTimer.Interval = nTime;
                                }
                            }
                            break;
                        case 2:
                            {
                                int nWidth = int.Parse(s);
                                if(nWidth > 0)
                                {
                                    fMaxBoder = nWidth;
                                    break;
                                }
                            }
                            break;
                        default:
                            break;
                    } 
                    ++nLineIndex;
                }
            }
            sr.Close();            
        }

        //public void ImageTimeEvent (object source, ElapsedEventArgs e)
        //{
            
        //}

        void updateImage()
        {
            if(m_nImageCount > 0)
            {
                if (null != m_curImage)
                {
                    m_curImage.Dispose();
                    m_curImage = null;
                }
                int oldButtom = this.Bottom;
                int oldRight = this.Right;
                string sFile = m_aImagefiles[m_imageRd.Next(m_nImageCount)];
                Trace.TraceInformation(sFile);
                m_curImage = Image.FromFile(sFile);
                
                if(m_curImage.Size.Width > m_curImage.Size.Height)
                {
                    m_curWindowSize.Width = fMaxBoder;
                    m_curWindowSize.Height = fMaxBoder * m_curImage.Size.Height / m_curImage.Size.Width;
                }
                else
                {
                    m_curWindowSize.Height = fMaxBoder;
                    m_curWindowSize.Width = fMaxBoder * m_curImage.Size.Width / m_curImage.Size.Height;
                }
                Size = m_curWindowSize;
                if (oldRight > 1280)
                {
                    this.Left = oldRight - m_curWindowSize.Width;
                }                
                //底对齐
                if(oldButtom > 720)
                {
                    this.Top = oldButtom - m_curWindowSize.Height;
                }
                Refresh();
            }
        }


        private void ImageForm_MouseDown(object sender, MouseEventArgs e)
        {                    
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void ImageForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        private void ImageForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void ImageForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (null != m_curImage)
            {
                g.DrawImage(m_curImage, 0, 0, m_curWindowSize.Width, m_curWindowSize.Height);
            }            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (leftFlag)
            {
                return;
            }
            updateImage();
        }

    }
}
