using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2KG
{
    
    class Figure
    {
    //
    //список точек
    //
    public List<Point3d> pointList;

        //самая верхняя сканирующая строка
        public int maxY;
        //число сканирующих строк
        public int delY;
        //число ребер
        public int countEdge;
        //список ребер
        public List<Edge> edgeList;
        //коэффициенты уравнения
        public float A, B, C, D;
        //атрибут видимости или номер многоугольника
        public int attribute;

        public String allInf;

        public String strstr = "";

        //самая верхняя сканирующая строка
        public float minZ;


        //данные для активных фигур

        //все для активны ребер
       // public List<Tuple<Point3d, Point3d>> allEdgeList;
        public List<PairEdge> activPair;
        public List<Edge> edgeActive;

        public Figure()
        { }

            public Figure(List<Point3d> list, int numb)
        {
            if (list.Count < 3)
                return;
            pointList = list;
            attribute = numb;
            delY = findDelY();
            findEquation(list[0], list[1], list[2]);
     
            shiftEdges();
            minZ = findMinZ();

            

        }

       

        //ищем очередные активные ребра
        public void becomeActive(int y)
        {
            // edgeActive.Clear();

            //если фигура новая, то создаем список активных ребер
            if (edgeActive == null)
                edgeActive = new List<Edge>();
                //удаление неактивных ребер
             /*
              else   for (int i = 0; i < edgeActive.Count; i++)
                {

                    if (edgeActive[i].Item2.Y >= y)
                    {
                       
                        edgeActive.RemoveAt(i--); }
                }
                */
            // обновляем в список активных ребер
            for (int i = 0; i < edgeList.Count; i++)
              //  foreach (Edge edge in edgeList)
            {
                if (edgeList[i].Item1.Y >= y && edgeList[i].Item2.Y < y)
                {
                    strstr += (" add " + y);
                    edgeActive.Add(edgeList[i]);
                    edgeList.RemoveAt(i--);
                }
            }

            //сортировка ребер по иксу
            Edge temp;
            for (int i = 0; i < edgeActive.Count; i++)
            {
                for (int j = i + 1; j < edgeActive.Count; j++)
                {
                    if (edgeActive[i].getXfromY(y) > edgeActive[j].getXfromY(y))
                    {
                        temp = edgeActive[i];
                        edgeActive[i] = edgeActive[j];
                        edgeActive[j] = temp;
                    }
                }
            }
        }


        //удаляем неактивные ребра
        public void workWithPair(int y)
        {
            //если список актив ребер еще не создан, это когда фигура только попала в список активных
            if (activPair == null)
            {
                activPair = new List<PairEdge>();

            }

                for (int i = 0; i < edgeActive.Count; i++)
                {
                    edgeActive[i] = findForX(edgeActive[i], y);
                if (edgeActive[i].delY <= 0)
                {
                    edgeActive.RemoveAt(i--);
                    strstr += (" del " + y);
                }
              
            }
            

            activPair.Clear();
            //добавляю
                if (edgeActive.Count % 2 == 0)
                {
                    for (int i = 0; i < edgeActive.Count - 1; i++)
                    {
                        activPair.Add(new PairEdge(edgeActive[i], edgeActive[i + 1]));
                    activPair[activPair.Count-1] = findeForZ(activPair[activPair.Count - 1], y);
                    }
                }
            

}
        //расчет данных для ребра, пересекающего строку (x)
        private Edge findForX(Edge edge, int y)
        {

            //если новое ребро
            if (edge.X == int.MaxValue)
            {
                edge.X = (int)edge.Item1.X;
                edge.delY = (int)Math.Abs(y - edge.Item2.Y);
               // edge.delX = Math.Abs((edge.Item1.X- edge.Item2.X)/ edge.delY);
                  edge.delX = ((edge.Item1.X - edge.Item2.X) / edge.delY) - 1/2;
                strstr += (" delX " + edge.delX);
            }
            //не новое ребро
            else
            {
                edge.X -= edge.delX;
                edge.delY--;
            }

            return edge;
        }

        private PairEdge findeForZ(PairEdge pair, int y)
        {
            // z(х + 1/2, у + 1/2)
            pair.Zl = -(A * (pair.left.X + (1 / 2)) + B * (y + (1 / 2)) + D) / C;
            pair.delZx = (C == 0) ? 0 : A / C;
            pair.delZy = (C == 0) ? 0 : B / C;
            return pair;
        }
        public string printAll()
        {
            allInf = "№" + attribute+": ΔY =" + delY + "; Ребра (" + countEdge + "): ";
            foreach (Edge edge in edgeList)
            {
                allInf += "(" + edge.Item1.X + "; " + edge.Item2.X + ") ";
            }

            allInf += "; A: " + A + ", B: " + B + ", C: " + C + ", D: " + D + ". ";
            return allInf;
        }
        //получаем список всех ребер
        public void shiftEdges()
        {
            //edgeList.Clear();
            edgeList = new List<Edge>();
            int count = pointList.Count();
            for (int i = 0; i < count-1; i++)
            {
                if (pointList[i].Y != pointList[i + 1].Y)
                {
                    if (pointList[i].Y > pointList[i + 1].Y)
                        edgeList.Add(new Edge(pointList[i], pointList[i + 1]));
                    else if (pointList[i].Y < pointList[i + 1].Y) edgeList.Add(new Edge(pointList[i + 1], pointList[i]));
                }
            }

            
                if (pointList[0].Y < pointList[count - 1].Y)
                    edgeList.Add(new Edge(pointList[count - 1], pointList[0]));
            else if (pointList[0].Y > pointList[count - 1].Y)
                edgeList.Add(new Edge(pointList[0], pointList[count - 1]));
            
            countEdge = edgeList.Count();
        }

            //найти уравнение плоскости
            private void findEquation(Point3d p1, Point3d p2, Point3d p3)
        {
          
            A = p1.Y * (p2.Z - p3.Z) + p2.Y * (p3.Z - p1.Z) + p3.Y * (p1.Z - p2.Z);
            B = p1.Z * (p2.X - p3.X) + p2.Z * (p3.X - p1.X) + p3.Z * (p1.X - p2.X);
            C = p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y);
            D = -(p1.X * (p2.Y * p3.Z - p3.Y * p2.Z) + p2.X * (p3.Y * p1.Z - p1.Y * p3.Z) + p3.X * (p1.Y * p2.Z - p2.Y * p1.Z));

        }

            //найти количество пересеченных строк
            private int findDelY()
        {
            float min = pointList[0].Y, max= pointList[0].Y;
            foreach (Point3d point in pointList)
            { if (point.Y > max)
                    max = point.Y;
                if (point.Y < min)
                    min = point.Y;
            }
            maxY = (int)(max - 0.5);
            return (int)(max - min);
        }

        //самая дальняя z
        private float findMinZ()
        {
            float min = pointList[0].Z;
            foreach (Point3d point in pointList)
            {
              
                if (point.Z < min)
                    min = point.Z;
            }
            return min;
        }

        public float findMinY()
        {
            float min = pointList[0].Y;
            foreach (Point3d point in pointList)
            {

                if (point.Y < min)
                    min = point.Y;
            }
            return min;
        }

        public float findXfromY(int y)
    {
        return (-B*y-D)/A;
    }

    }

   
}
