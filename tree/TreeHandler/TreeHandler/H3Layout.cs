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
        public Vector3 position;

        public int r = 0;//size (rename)
        public float d; //distance to parent
        public Vector3 normal;

        int f = 0;


        public H3Layout()
        {

        }

        public string GetCartisianPosition()
        {
            return position.X*10 + " " + position.Y*10 + " " + position.Z*10;
        }

        public void MovePosition(H3Layout parent, float a1, float a2)
        {
            f = 1;
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


            normal = Vector3.TransformNormal(parent.normal, m3);
            //normal = Vector3.Transform(normal, m2);

            position = parent.position + Vector3.Multiply(d, normal);

            //to do
            //test this quickly
            //normaltranform?
            //circle layout


        }


    }
}
