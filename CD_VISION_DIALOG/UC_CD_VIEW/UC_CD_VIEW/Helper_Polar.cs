using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP_Troops
{
    public static class HELPER_POLAR
    {
        public static double[] GetArray_Degree()
        {
            double[] array = new double[360];

            Parallel.For(0, 360, nAngle =>
            {
                array[nAngle] = ((nAngle - 90.0) * Math.PI / 180.0);

            });
            return array;
        }
        public static double[] GetArray_COS()
        {
            double[] arrDegree = GetArray_Degree();

            double[] array = new double[360];

            Parallel.For(0, 360, nAngle =>
            {
                array[nAngle] = Math.Cos(arrDegree[nAngle]);
            });
            return array;
        }
        public static double[] GetArray_SIN()
        {
            double[] arrDegree = GetArray_Degree();

            double[] array = new double[360];

            Parallel.For(0, 360, nAngle =>
            {
                array[nAngle] = Math.Sin(arrDegree[nAngle]);
            });
            return array;
        }
    }
}
