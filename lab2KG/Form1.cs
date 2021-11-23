using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2KG
{
    public partial class Form1 : Form
    {
        //Массив цветов
        Pen[] arrayColor = { Pens.Red,Pens.Green, Pens.Gray, Pens.Blue,  Pens.Orange, Pens.Purple, Pens.Pink, Pens.Violet, Pens.Yellow };

        private int width, height;
        private List<Figure> listFigure;
        private List<Figure> activeFigure;
        private List<List<int>> groupY;
        int Y=0;
        int time = 100;
        //буфер кадра
        private int[] bufCadr;
        Figure activ = new Figure();
        List<int[]> bc;
        //z-буфер
        private float[] bufZ;
        float minZ;
        float minY;
        int numFon;
        private int indFigure;
        public Form1()
        {
            InitializeComponent();

           width = pictureBox1.ClientSize.Width;
            height = pictureBox1.ClientSize.Height;
            
            pictureBox1.Refresh();




           
        }

        private void mainfunc()
        {
            bc = new List<int[]>();
            indFigure = 1;

            //y-группы
            groupY = new List<List<int>>();
            for (int i = 0; i < height; i++)
            {
                groupY.Add(null);
            }

            listFigure = new List<Figure>();

            //добавили фигуры, для каждой нашли информацию
            addFigures();
            listBox1.Items.Clear();
            foreach (Figure fig in listFigure)
                listBox1.Items.Add(fig.printAll());


            //добавим фигуры в соответствующие y группы
            foreach (Figure figur in listFigure)
            {
                if (groupY[figur.maxY] == null && figur.maxY < height)
                {
                    groupY[figur.maxY] = new List<int>();
                }
                groupY[figur.maxY].Add(figur.attribute - 1);
            }

            //минимальное значение зет буфера
           findMinZ();
            // minZ = -10;
            //значение фона
            numFon = 0;
            activeFigure = new List<Figure>();
           
            Y = height - 1;
        }
  
        private void Helpnlast(int[] mass)
        {
          //  bc.Add(new int[width]);
            for (int i = 0; i < width; i++)
                bc[bc.Count - 1][i] = mass[i];
        }
        private void Helpn(int[] mass)
        {
            bc.Add(new int[width]);
            for (int i = 0; i < width; i++)
                bc[bc.Count - 1][i] = mass[i];
        }
        //найти минимальое z
        private void findMinZ()
        {
            minZ = listFigure[0].minZ;
            foreach (Figure figur in listFigure)
            { if (figur.minZ < minZ)
                    minZ = figur.minZ;
            }
            }



        private void findMinY()
        {
            minY = listFigure[0].findMinY();
            foreach (Figure figur in listFigure)
            {
                if (figur.findMinY() < minY)
                    minY = figur.findMinY();
            }
            

        }

            
        private void addFigures()
        {
            listFigure = new List<Figure>();
            //добавляем фигуры


            List<Point3d> pointList = new List<Point3d>();

             pointList.Add(new Point3d(150, 200, 150));
             pointList.Add(new Point3d(300, 150, 50));
             pointList.Add(new Point3d(250, 300, 50));

            listFigure.Add(new Figure(pointList, indFigure++));



            pointList = new List<Point3d>();
            pointList.Add(new Point3d(60, 208, 54));
            pointList.Add(new Point3d(28, 208, 86));
            pointList.Add(new Point3d(28, 240, 118));
            pointList.Add(new Point3d(60, 272, 118));
            pointList.Add(new Point3d(82, 272, 86));
            pointList.Add(new Point3d(82, 240, 54));


            listFigure.Add(new Figure(pointList, indFigure++));

            pointList = new List<Point3d>();
            pointList.Add(new Point3d(250, 300, 100));
            pointList.Add(new Point3d(250, 100, 100));
            pointList.Add(new Point3d(100, 100, 100));
            pointList.Add(new Point3d(100, 300, 100));
        
             

            listFigure.Add(new Figure(pointList, indFigure++));

            pointList = new List<Point3d>();

            pointList.Add(new Point3d(50, 300, 70));
            pointList.Add(new Point3d(134, 150, 250));
            pointList.Add(new Point3d(270, 110, 20));

            listFigure.Add(new Figure(pointList, indFigure++));

           

            pointList = new List<Point3d>();

            pointList.Add(new Point3d(170, 260, 130));
            pointList.Add(new Point3d(130, 320, 30));
            pointList.Add(new Point3d(220, 330, 70));

            listFigure.Add(new Figure(pointList, indFigure++));

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
           
              e.Graphics.TranslateTransform(0, ClientSize.Height-1);

    

            e.Graphics.DrawRectangle(Pens.Black, 0, 0, 1, 1);

            bool flag = false;
              if (bc != null && bc.Count != 0)
              {
                  for (int i = bc.Count-1; i >=0; i--)
                  {
                      for (int j = 0; j < bc[i].Length; j++)
                      {
                        if (bc[i][j] != 0)
                        {
                            e.Graphics.DrawRectangle(arrayColor[bc[i][j] - 1], j, -(391 - i), 1, 1);
                          
                        }
                      }
                  }
              }
            if (activ != null)
            {
                if(activ.activPair!=null && Y>101)
                foreach (PairEdge pair in activ.activPair)
                {
                            e.Graphics.DrawLine(new Pen(Color.Black, 1), pair.left.Item1.X, -pair.left.Item1.Y, pair.left.Item2.X, -pair.left.Item2.Y);
                            e.Graphics.DrawLine(new Pen(Color.Black, 1), pair.right.Item1.X, -pair.right.Item1.Y, pair.right.Item2.X, -pair.right.Item2.Y);
                        
                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainfunc();
            timer1.Start();
           
            //mainfunc(0);
            // pictureBox1.Refresh();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            button1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {

            timer1.Stop();
            button1.Enabled = false;

        }
        /// ////////////////////////////////////////////////////////////////////////////////////////
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
         
            label2.Text = Convert.ToString(e.X);
            if (bufZ != null)
            {
                textBox3.Text = Convert.ToString(bufZ[Convert.ToInt32(e.X)]);
                textBox1.Text = Convert.ToString(bufCadr[Convert.ToInt32(e.X)]);
                label5.Text = Convert.ToString(Y);
            }
            
         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (Y > 0)
            {  //Инициализация строки буфера зет и буфера кадра
                bufCadr = Enumerable.Repeat<int>(0, width).ToArray();
                bufZ = Enumerable.Repeat<float>(minZ, width).ToArray();

                Helpn(bufCadr);

                //проверка на новые многоугольники

                if (groupY[Y] != null)
                {
                    //если в данной строке появляются новые фигуры
                    foreach (int j in groupY[Y])
                    {
                        activeFigure.Add(listFigure[j]);
                    }
                }
                //по фигурам
                for (int f = 0; f < activeFigure.Count; f++)
                {
                    
                    //проверяю, осталась ли фигура активной

                    if (--activeFigure[f].delY <= 0)
                    {
                        activeFigure.RemoveAt(f--);
                        continue;

                    }
                    activeFigure[f].becomeActive(Y);//добавляем активные ребра
                    activeFigure[f].workWithPair(Y);//обновляю пары ребер и перерасчитываю параметры пар

                    activ = activeFigure[f];
                    // /*
                    //прохожу по активным парам фигуры
                    foreach (PairEdge pair in activeFigure[f].activPair)
                    {
                        int x1 = (int)pair.left.X - (1 / 2), x2 = (int)pair.right.X - (1 / 2);
                        float z = pair.Zl;
                        //запускаю цикл по x
                        for (int x = x1; x <= x2; x++)
                        {
                            if (z > bufZ[x])
                            {
                                bufZ[x] = z;
                                bufCadr[x] = activeFigure[f].attribute;

                            }
                            z -= pair.delZx;
                        }
                        Helpnlast(bufCadr);
                        pictureBox1.Refresh();

                        Thread.Sleep(time);
                    }
     
                }

                Y--;
            }
    }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            time = time * 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            time = (int)time /2;
        }

        private void drowFigures(PaintEventArgs e)
        {
            foreach (Figure figur in listFigure)
            {
               
              for(int i=0; i< figur.pointList.Count-1; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Green, 1), figur.pointList[i].X, -figur.pointList[i].Y, figur.pointList[i+1].X, -figur.pointList[i+1].Y);
                }
                e.Graphics.DrawLine(new Pen(Color.Green, 1), figur.pointList[figur.pointList.Count - 1].X, -figur.pointList[figur.pointList.Count - 1].Y, figur.pointList[0].X, -figur.pointList[0].Y);
            }
        }
    }
}
