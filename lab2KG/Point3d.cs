using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2KG
{
    class Point3d
    {
        private float x;
        private float y;
        private float z;
        public float X
        {
            get => x;
            set => x = value;
        }
        public float Y
        {
            get => y;
            set => y = value;
        }
        public float Z
        {
            get => z;
            set => z = value;
        }

        public Point3d(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Point3d(Point3d point)
        {
            this.x = point.X;
            this.y = point.Y;
            this.z = point.Z;
        }
    }
}
