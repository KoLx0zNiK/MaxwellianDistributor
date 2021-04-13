using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace raspredMaksvell
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int N;
        double R;
        double Vmax;
        double Xmax; 
        double Ymax;
        double dt;
        double K;//задание переменных
        double[] X;
        double[] Y;
        double[] Vx;
        double[] Vy;//задание массива
        Random rnd = new Random();
        Bitmap Bmp1, Bmp2;//создание битмапа(области для рисования)
        Graphics Gr1, Gr2;//создание графика(тут будем рисовать)
        Pen pen1 = new Pen(Color.Red, 2);//создание ручки
        Pen pen2 = new Pen(Color.Green, 2);//создание ручки
        SolidBrush Br = new SolidBrush(Color.Yellow);//создание кисточки

        private void button1_Click(object sender, EventArgs e)
        {
            N = int.Parse(textBox1.Text);
            R = double.Parse(textBox2.Text);
            Vmax = double.Parse(textBox3.Text);
            Xmax = double.Parse(textBox4.Text)/2;
            Ymax = double.Parse(textBox5.Text)/2;
            dt = double.Parse(textBox6.Text);
            K = double.Parse(textBox7.Text);//забираем переменные из меню
            X = new double[N];
            Y = new double[N];
            Vx = new double[N];
            Vy = new double[N];//ввод массива с N переменными
            Gr1.Clear(pictureBox1.BackColor);//чистит поле
            Gr1.DrawRectangle(pen2, (int)((Bmp1.Width / 2) - K * Xmax), (int)((Bmp1.Height / 2) - K * Ymax), (int)(2 * K * Xmax), (int)(2 * K * Ymax));//рисуется область
            Gr2.DrawImage(Bmp1, 0, 0);
            for (int i = 0; i < N; i++)
            {
                X[i] = (2 * rnd.NextDouble() - 1) * (Xmax - R);
                Y[i] = (2 * rnd.NextDouble() - 1) * (Ymax - R);
                Vx[i] = (2 * rnd.NextDouble() - 1) * Vmax;
                Vy[i] = (2 * rnd.NextDouble() - 1) * Vmax;
            }//задается случайное значение скорости и начальное положение
            for (int i = 0; i < N; i++)
            {
                Gr2.DrawEllipse(pen1, (int)((Bmp1.Width / 2) + K * (X[i] - R)), (int)((Bmp1.Height / 2) - K * (Y[i] + R)),(int)(2*K*R),(int)(2*K*R));
                Gr2.FillEllipse(Br, (int)((Bmp1.Width / 2) + K * (X[i] - R)), (int)((Bmp1.Height / 2) - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));
            }// рисуются молекулы
            pictureBox1.Image = Bmp2;
            button1.Enabled = false;//нельзя нажать первую кнопку
            button2.Enabled = true;//можно нажать вторую кнопку
            timer1.Enabled = true;//таймер включается
            //Xscr = (int)((Bmp1.Width / 2) + K*X);Yscr = (int)((Bmp1.Width / 2) + K*Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;//включается первая кнопка
            button2.Enabled = false;//выключается вторая кнопка
            timer1.Enabled = false;//выключается таймер
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();// по нажатию 3 кнопки прога закрывается
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //движение частиц
            for (int i = 0; i < N; i++)
            {
                X[i] += Vx[i] * dt;
                Y[i] += Vy[i] * dt;
                if ((X[i] >= Xmax - R) && (Vx[i] > 0)) Vx[i] = -Vx[i];
                if ((X[i] <= -Xmax + R)&& (Vx[i] < 0)) Vx[i] = -Vx[i];
                if ((Y[i] >= Ymax - R) && (Vy[i]> 0)) Vy[i] = -Vy[i];
                if ((Y[i] <= -Ymax + R)&& (Vy[i] < 0)) Vy[i] = -Vy[i];
            }
            Gr2.DrawImage(Bmp1, 0, 0);
            for (int i = 0; i < N; i++)
            {
                Gr2.DrawEllipse(pen1, (int)((Bmp1.Width / 2) + K * (X[i] - R)), (int)((Bmp1.Height / 2) - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));
                Gr2.FillEllipse(Br, (int)((Bmp1.Width / 2) + K * (X[i] - R)), (int)((Bmp1.Height / 2) - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));
            }
            pictureBox1.Image = Bmp2;


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Gr1 = Graphics.FromImage(Bmp1);
            Bmp2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Gr2 = Graphics.FromImage(Bmp2);
        }
    }
}
