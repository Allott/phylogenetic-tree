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

        public string GetStringCoords(Vector4 p)//used for printing i guess
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


        public void SphereToCartisian(Node self)
        {
            Node p = self.parent;

            //calculate cartisian position
            self.position.X = (float)(p.radius * Math.Sin(self.phi) * Math.Cos(self.theta));
            self.position.Y = (float)(p.radius * Math.Sin(self.phi) * Math.Sin(self.theta));
            self.position.Z = (float)(p.radius * Math.Cos(self.phi));

            
            //transform cartisian position with sphere co-ords
            Matrix4x4 m1 = new Matrix4x4(
                (float)Math.Cos(p.theta), (float)(-1 * Math.Sin(p.theta)), 0, 0,
                (float)Math.Sin(p.theta), (float)Math.Cos(p.theta), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);//z

            Matrix4x4 m2 = new Matrix4x4(
                (float)Math.Cos(p.phi), 0, (float)Math.Sin(p.phi), 0,
                0, 1, 0, 0,
                (float)(-1 * Math.Sin(p.phi)), 0, (float)Math.Cos(p.phi), 0,
                0, 0, 0, 1);//y

            Matrix4x4 m3 = Matrix4x4.Multiply(m1, m2);
            self.m = m3;
            self.position = Vector4.Transform(self.position, m3);

            //tranform to parent coords
            self.position = self.position + p.position;

        }


    }
}
