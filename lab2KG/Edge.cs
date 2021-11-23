using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2KG
{
   
    class Edge
    {  
        public Point3d Item1, Item2;
        public float X, delX, delY;
        private float k, b;
        public Edge(Point3d i1, Point3d i2)
        {
            Item1 = i1;
            Item2 = i2;
            X = int.MaxValue;
            k = (i1.Y - i2.Y) / (i1.X - i2.X);
            b = i2.Y - k * i2.X;
        }

        public float getXfromY(int y)
        {
            return (Item1.X == Item2.X) ? Item1.X : (y - b) / k;
        
        }
    }
}
