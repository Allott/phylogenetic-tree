using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace TreeHandler
{
    class H3Layout
    {

        const double k = 2;

        public H3Layout()
        {

        }

        public string GetCartisianPosition(Vector3 p)//used for printing i guess
        {
            return p.X*1000 + " " + p.Y*1000 + " " + p.Z*1000;
        }

        public double CalcRadius(double i)//check this
        {
            double x = Math.Sqrt(i / (2 * Math.PI * k * k));

            //Inverse Hyperbolic Sine HArcsin(X) = Log(X + Sqr(X * X + 1)) 
            return k * Math.Log(x + Math.Sqrt(x * x + 1));
        }

        public double CalcHArea(double i)
        {
            double beta = 1.00;//?
            return 2 * Math.PI * (Math.Cosh(i / k) - 1.0) * beta;
        }

        public double CalcChangePhi(double rp, double r1)
        {
            return Math.Atan((r1 / k)/Math.Sinh(rp/k));
        }

        public double CalcChangeTheta(double rp, double r1, double phi)
        {
            return Math.Atan(Math.Tanh(r1 / k) /
                (Math.Sinh(rp / k) * Math.Sinh(phi)));
        }


        public void MovePosition(Node self, float a1, float a2)
        {
            Matrix4x4 m1 = new Matrix4x4(
                (float) Math.Cos(a1), (float) -Math.Sin(a1), 0, 0,
                (float) Math.Sin(a1), (float) Math.Cos(a1), 0, 0,
                0,0,1,0,
                0,0,0,1);//z axis rotation

            Matrix4x4 m2 = new Matrix4x4(
                (float)Math.Cos(a2), 0, (float)Math.Sin(a2), 0,
                0, 1, 0, 0,
                (float)-Math.Sin(a2), 0, (float)Math.Cos(a2), 0,
                0, 0, 0, 1);//check this matrix (its probebly wrong)

            Matrix4x4 m3 = m1 * m2;


            self.normal = Vector3.TransformNormal(self.parent.normal, m3);
            //normal = Vector3.Transform(normal, m2);

            self.position = self.parent.position + Vector3.Multiply((float) self.radius, self.normal);
            

        }


    }
}
