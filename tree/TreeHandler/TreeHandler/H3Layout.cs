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

        public string GetStringCoords(Vector4 p, double r)//used for printing 
        {
            return p.X*(100/r) + " " + p.Y*(100/r) + " " + (p.Z-(r/2))*(100/r);//1000 so node are drawn with sufficient distance between
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

            //calculate positon on parent hemisphere
            self.position.X = (float)(p.radius * Math.Sin(self.phi) * Math.Cos(self.theta));
            self.position.Y = (float)(p.radius * Math.Sin(self.phi) * Math.Sin(self.theta));
            self.position.Z = (float)(p.radius * Math.Cos(self.phi));


            /*
            //rotate to parent angle
            Node c = self;


            for (int i = 0; i < self.depth; i++)//multi parent rotation bug fix, needs changing for efficiency
            {
                c = c.parent;

                Matrix4x4 yr = new Matrix4x4(
                (float)Math.Cos(-c.phi), 0, (float)Math.Sin(-c.phi), 0,
                0, 1, 0, 0,
                (float)(-1 * Math.Sin(-c.phi)), 0, (float)Math.Cos(-c.phi), 0,
                0, 0, 0, 1
                );

                Matrix4x4 zr = new Matrix4x4(
                    (float)Math.Cos(-c.theta), (float)(-1 * Math.Sin(-c.theta)), 0, 0,
                    (float)Math.Sin(-c.theta), (float)Math.Cos(-c.theta), 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                    );
                Matrix4x4 r3 = yr * zr;

                self.position = Vector4.Transform(self.position, r3);

            }
            */

            //rotate to parent angle fixed version
            self.position = Vector4.Transform(self.position, self.parent.cumulativeRotation);


            //set cumulative angle for child rotations

            Matrix4x4 yr = new Matrix4x4(
                (float)Math.Cos(-self.phi), 0, (float)Math.Sin(-self.phi), 0,
                0, 1, 0, 0,
                (float)(-1 * Math.Sin(-self.phi)), 0, (float)Math.Cos(-self.phi), 0,
                0, 0, 0, 1);

            Matrix4x4 zr = new Matrix4x4(
                (float)Math.Cos(-self.theta), (float)(-1 * Math.Sin(-self.theta)), 0, 0,
                (float)Math.Sin(-self.theta), (float)Math.Cos(-self.theta), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

            Matrix4x4 r3 = yr * zr;
            self.cumulativeRotation = r3 * self.parent.cumulativeRotation;

            //move to parent position
            self.position = self.position + p.position;

        }


    }
}
