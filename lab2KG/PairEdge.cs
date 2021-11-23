using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2KG
{
    class PairEdge
    {
        
        public Edge left;
        public Edge right;
        public float Zl, delZx, delZy;

        public PairEdge()
        { }

        public PairEdge(Edge l, Edge r)
        {
            left = l;
            right = r;

        }
    }
}
