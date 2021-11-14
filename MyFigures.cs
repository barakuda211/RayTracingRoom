using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace RayTracingRoom
{
    /// <summary>
    /// Базовый класс фигуры
    /// </summary>
    public class Figure
    {
        public static float eps = 0.0001f;
        public List<Point3D> listPoints = new List<Point3D>();
        public List<Side> listSides = new List<Side>();
        public Material material;
        public Figure() { }

        public Figure(Figure f)
        {
            foreach (Point3D p in f.listPoints)
                listPoints.Add(new Point3D(p));

            foreach (Side s in f.listSides)
            {
                listSides.Add(new Side(s));
                listSides.Last().host = this;
            }
        }

        public bool RayIntersectsTriangle(Ray r, Point3D p0, Point3D p1, Point3D p2, out float intersect)
        {
            intersect = -1;
            Point3D edge1 = p1 - p0;
            Point3D edge2 = p2 - p0;
            Point3D h = r.direction * edge2;
            float a = Point3D.ScalarMult(edge1, h);
            if (a > -eps && a < eps)
                return false;       
            float f = 1.0f / a;
            Point3D s = r.start - p0;
            float u = f * Point3D.ScalarMult(s, h);
            if (u < 0 || u > 1)
                return false;
            Point3D q = s * edge1;
            float v = f * Point3D.ScalarMult(r.direction, q);
            if (v < 0 || u + v > 1)
                return false;
            float t = f * Point3D.ScalarMult(edge2, q);
            if (t > eps)
            {
                intersect = t;
                return true;
            }
            else      
                return false;
        }

        public virtual bool FigureIntersection(Ray r, out float intersect, out Point3D normal)
        {
            intersect = 0;
            normal = null;
            Side side = null;
            foreach (Side figure_side in listSides)
            {
                if (figure_side.listPoints.Count == 4)
                {
                    if (RayIntersectsTriangle(r, figure_side[0], figure_side[1], figure_side[3], out float t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                    else if (RayIntersectsTriangle(r, figure_side[1], figure_side[2], figure_side[3], out t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                }
            }
            if (intersect != 0)
            {
                normal = Side.GetNormal(side);
                material.color = new FloatColor(side.pen.Color);
                return true;
            }
            return false;
        }

        public float[,] GetMatrix()
        {
            var res = new float[listPoints.Count, 4];
            for (int i = 0; i < listPoints.Count; i++)
            {
                res[i, 0] = listPoints[i].x;
                res[i, 1] = listPoints[i].y;
                res[i, 2] = listPoints[i].z;
                res[i, 3] = 1;
            }
            return res;
        }

        public void ApplyMatrix(float[,] matrix)
        {
            for (int i = 0; i < listPoints.Count; i++)
            {
                listPoints[i].x = matrix[i, 0] / matrix[i, 3];
                listPoints[i].y = matrix[i, 1] / matrix[i, 3];
                listPoints[i].z = matrix[i, 2] / matrix[i, 3];
            }
        }

        private Point3D GetCenter()
        {
            Point3D res = new Point3D(0, 0, 0);
            foreach (Point3D p in listPoints)
            {
                res.x += p.x;
                res.y += p.y;
                res.z += p.z;

            }
            res.x /= listPoints.Count();
            res.y /= listPoints.Count();
            res.z /= listPoints.Count();
            return res;
        }

        public void RotateArondRad(float rangle, string type)
        {
            float[,] mt = GetMatrix();
            Point3D center = GetCenter();
            switch (type)
            {
                case "CX":
                    mt = ApplyOffset(mt, -center.x, -center.y, -center.z);
                    mt = ApplyRotation_X(mt, rangle);
                    mt = ApplyOffset(mt, center.x, center.y, center.z);
                    break;
                case "CY":
                    mt = ApplyOffset(mt, -center.x, -center.y, -center.z);
                    mt = ApplyRotation_Y(mt, rangle);
                    mt = ApplyOffset(mt, center.x, center.y, center.z);
                    break;
                case "CZ":
                    mt = ApplyOffset(mt, -center.x, -center.y, -center.z);
                    mt = ApplyRotation_Z(mt, rangle);
                    mt = ApplyOffset(mt, center.x, center.y, center.z);
                    break;
                case "X":
                    mt = ApplyRotation_X(mt, rangle);
                    break;
                case "Y":
                    mt = ApplyRotation_Y(mt, rangle);
                    break;
                case "Z":
                    mt = ApplyRotation_Z(mt, rangle);
                    break;
                default:
                    break;
            }
            ApplyMatrix(mt);
        }

        public void RotateAround(float angle, string type)
        {
            RotateArondRad(angle * (float)Math.PI / 180, type);
        }

        public void Move(float xs, float ys, float zs)
        {
            ApplyMatrix(ApplyOffset(GetMatrix(), xs, ys, zs));
        }

        public virtual void SetPen(Pen dw)
        {
            foreach (Side s in listSides)
                s.pen = dw;
        }

        private static float[,] MultiplyMatrix(float[,] m1, float[,] m2)
        {
            float[,] res = new float[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m2.GetLength(1); j++)
                {
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        res[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return res;
        }

        private static float[,] ApplyOffset(float[,] transform_matrix, float offset_x, float offset_y, float offset_z)
        {
            float[,] translationMatrix = new float[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { offset_x, offset_y, offset_z, 1 } };
            return MultiplyMatrix(transform_matrix, translationMatrix);
        }

        private static float[,] ApplyRotation_X(float[,] transform_matrix, float angle)
        {
            float[,] rotationMatrix = new float[,] { { 1, 0, 0, 0 }, { 0, (float)Math.Cos(angle), (float)Math.Sin(angle), 0 },
                { 0, -(float)Math.Sin(angle), (float)Math.Cos(angle), 0}, { 0, 0, 0, 1} };
            return MultiplyMatrix(transform_matrix, rotationMatrix);
        }

        private static float[,] ApplyRotation_Y(float[,] transform_matrix, float angle)
        {
            float[,] rotationMatrix = new float[,] { { (float)Math.Cos(angle), 0, -(float)Math.Sin(angle), 0 }, { 0, 1, 0, 0 },
                { (float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0}, { 0, 0, 0, 1} };
            return MultiplyMatrix(transform_matrix, rotationMatrix);
        }

        private static float[,] ApplyRotation_Z(float[,] transform_matrix, float angle)
        {
            float[,] rotationMatrix = new float[,] { { (float)Math.Cos(angle), (float)Math.Sin(angle), 0, 0 }, { -(float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0 },
                { 0, 0, 1, 0 }, { 0, 0, 0, 1} };
            return MultiplyMatrix(transform_matrix, rotationMatrix);
        }
    }
    /// <summary>
    /// Куб
    /// </summary>
    public class Cube : Figure 
    { 
        public Cube(float sz) : base()
        {
            listPoints.Add(new Point3D(sz / 2, sz / 2, sz / 2));
            listPoints.Add(new Point3D(-sz / 2, sz / 2, sz / 2));
            listPoints.Add(new Point3D(-sz / 2, sz / 2, -sz / 2));
            listPoints.Add(new Point3D(sz / 2, sz / 2, -sz / 2));
            listPoints.Add(new Point3D(sz / 2, -sz / 2, sz / 2));
            listPoints.Add(new Point3D(-sz / 2, -sz / 2, sz / 2));
            listPoints.Add(new Point3D(-sz / 2, -sz / 2, -sz / 2));
            listPoints.Add(new Point3D(sz / 2, -sz / 2, -sz / 2));

            Side s = new Side(this);
            s.listPoints.AddRange(new int[] { 3, 2, 1, 0 });
            listSides.Add(s);

            s = new Side(this);
            s.listPoints.AddRange(new int[] { 4, 5, 6, 7 });
            listSides.Add(s);

            s = new Side(this);
            s.listPoints.AddRange(new int[] { 2, 6, 5, 1 });
            listSides.Add(s);

            s = new Side(this);
            s.listPoints.AddRange(new int[] { 0, 4, 7, 3 });
            listSides.Add(s);

            s = new Side(this);
            s.listPoints.AddRange(new int[] { 1, 5, 4, 0 });
            listSides.Add(s);

            s = new Side(this);
            s.listPoints.AddRange(new int[] { 2, 3, 7, 6 });
            listSides.Add(s);
        }
    }
    /// <summary>
    /// Вспомогательный класс стороны для вычислений
    /// </summary>
    public class Side
    {
        public Figure host = null;
        public List<int> listPoints = new List<int>();
        public Pen pen = new Pen(Color.Black);
        public Point3D Normal;

        public Side(Figure h = null)
        {
            host = h;
        }

        public Side(Side s)
        {
            listPoints = new List<int>(s.listPoints);
            host = s.host;
            pen = s.pen.Clone() as Pen;
            Normal = new Point3D(s.Normal);
        }

        public Point3D this[int index] { get { return host != null ? host.listPoints[listPoints[index]] : null; } }
            
        public static Point3D GetNormal(Side S)
        {
            if (S.listPoints.Count() < 3)
                return new Point3D(0, 0, 0);
            Point3D U = S[1] - S[0];
            Point3D V = S[S.listPoints.Count - 1] - S[0];
            Point3D normal = U * V;
            return Point3D.Normilize(normal);
        }
    }
    /// <summary>
    /// Сфера
    /// </summary>
    public class Sphere : Figure
    {
        float radius;
        Pen pen;

        public Sphere(Point3D p, float r)
        {
            listPoints.Add(p);
            radius = r;
        }

        public override void SetPen(Pen p) => pen = p;

        public static bool RaySphereIntersection(Ray r, Point3D sphere_pos, float sphere_rad, out float t)
        {
            Point3D k = r.start - sphere_pos;
            float b = Point3D.ScalarMult(k, r.direction);
            float c = Point3D.ScalarMult(k, k) - sphere_rad * sphere_rad;
            float d = b * b - c;
            t = 0;
            if (d >= 0)
            {
                float sqrtd = (float)Math.Sqrt(d);
                float t1 = -b + sqrtd;
                float t2 = -b - sqrtd;

                float min_t = Math.Min(t1, t2);
                float max_t = Math.Max(t1, t2);

                t = (min_t > eps) ? min_t : max_t;
                return t > eps;
            }
            return false;
        }

        public override bool FigureIntersection(Ray r, out float t, out Point3D normal)
        {
            t = 0;
            normal = null;
            if (RaySphereIntersection(r, listPoints[0], radius, out t) && (t > eps))
            {
                normal = (r.start + r.direction * t) - listPoints[0];
                normal = Point3D.Normilize(normal);
                material.color = new FloatColor(pen.Color);
                return true;
            }
            return false;
        }
    }
}
