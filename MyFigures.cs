﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracingRoom
{
    public class Figure
    {
        public static float eps = 0.0001f;
        public List<Point3D> points = new List<Point3D>();
        public List<Side> sides = new List<Side>();
        public Material fMaterial;
        public Figure() { }

        public Figure(Figure f)
        {
            foreach (Point3D p in f.points)
                points.Add(new Point3D(p));

            foreach (Side s in f.sides)
            {
                sides.Add(new Side(s));
                sides.Last().host = this;
            }
        }

        public bool RayIntersectsTriangle(Ray r, Point3D p0, Point3D p1, Point3D p2, out float intersect)
        {
            intersect = -1;
            Point3D edge1 = p1 - p0;
            Point3D edge2 = p2 - p0;
            Point3D h = r.direction * edge2;
            float a = Point3D.scalar(edge1, h);
            if (a > -eps && a < eps)
                return false;       
            float f = 1.0f / a;
            Point3D s = r.start - p0;
            float u = f * Point3D.scalar(s, h);
            if (u < 0 || u > 1)
                return false;
            Point3D q = s * edge1;
            float v = f * Point3D.scalar(r.direction, q);
            if (v < 0 || u + v > 1)
                return false;
            float t = f * Point3D.scalar(edge2, q);
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
            foreach (Side figure_side in sides)
            {

                if (figure_side.points.Count == 3)
                {
                    if (RayIntersectsTriangle(r, figure_side.getPoint(0), figure_side.getPoint(1), figure_side.getPoint(2), out float t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                }
                else if (figure_side.points.Count == 4)
                {
                    if (RayIntersectsTriangle(r, figure_side.getPoint(0), figure_side.getPoint(1), figure_side.getPoint(3), out float t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                    else if (RayIntersectsTriangle(r, figure_side.getPoint(1), figure_side.getPoint(2), figure_side.getPoint(3), out t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                }
            }
            if (intersect != 0)
            {
                normal = Side.norm(side);
                fMaterial.color = new Point3D(side.drawing_pen.Color.R / 255f, side.drawing_pen.Color.G / 255f, side.drawing_pen.Color.B / 255f);
                return true;
            }
            return false;
        }

        public float[,] GetMatrix()
        {
            var res = new float[points.Count, 4];
            for (int i = 0; i < points.Count; i++)
            {
                res[i, 0] = points[i].x;
                res[i, 1] = points[i].y;
                res[i, 2] = points[i].z;
                res[i, 3] = 1;
            }
            return res;
        }

        public void ApplyMatrix(float[,] matrix)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].x = matrix[i, 0] / matrix[i, 3];
                points[i].y = matrix[i, 1] / matrix[i, 3];
                points[i].z = matrix[i, 2] / matrix[i, 3];
            }
        }

        private Point3D GetCenter()
        {
            Point3D res = new Point3D(0, 0, 0);
            foreach (Point3D p in points)
            {
                res.x += p.x;
                res.y += p.y;
                res.z += p.z;

            }
            res.x /= points.Count();
            res.y /= points.Count();
            res.z /= points.Count();
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

        public void Offset(float xs, float ys, float zs)
        {
            ApplyMatrix(ApplyOffset(GetMatrix(), xs, ys, zs));
        }

        public void SetPen(Pen dw)
        {
            foreach (Side s in sides)
                s.drawing_pen = dw;
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

    public class Cube : Figure 
    { 
        public Cube(float sz) : base()
        {
            points.Add(new Point3D(sz / 2, sz / 2, sz / 2)); // 0 
            points.Add(new Point3D(-sz / 2, sz / 2, sz / 2)); // 1
            points.Add(new Point3D(-sz / 2, sz / 2, -sz / 2)); // 2
            points.Add(new Point3D(sz / 2, sz / 2, -sz / 2)); //3
            points.Add(new Point3D(sz / 2, -sz / 2, sz / 2)); // 4
            points.Add(new Point3D(-sz / 2, -sz / 2, sz / 2)); //5
            points.Add(new Point3D(-sz / 2, -sz / 2, -sz / 2)); // 6
            points.Add(new Point3D(sz / 2, -sz / 2, -sz / 2)); // 7

            Side s = new Side(this);
            s.points.AddRange(new int[] { 3, 2, 1, 0 });
            sides.Add(s);

            s = new Side(this);
            s.points.AddRange(new int[] { 4, 5, 6, 7 });
            sides.Add(s);

            s = new Side(this);
            s.points.AddRange(new int[] { 2, 6, 5, 1 });
            sides.Add(s);

            s = new Side(this);
            s.points.AddRange(new int[] { 0, 4, 7, 3 });
            sides.Add(s);

            s = new Side(this);
            s.points.AddRange(new int[] { 1, 5, 4, 0 });
            sides.Add(s);

            s = new Side(this);
            s.points.AddRange(new int[] { 2, 3, 7, 6 });
            sides.Add(s);
        }
    }

    public class Side
    {
        public Figure host = null;
        public List<int> points = new List<int>();
        public Pen drawing_pen = new Pen(Color.Black);
        public Point3D Normal;
        public Material anotherMaterial = new Material();

        public Side(Figure h = null)
        {
            host = h;
        }

        public Side(Side s)
        {
            points = new List<int>(s.points);
            host = s.host;
            drawing_pen = s.drawing_pen.Clone() as Pen;
            Normal = new Point3D(s.Normal);
        }

        public Point3D getPoint(int index)
        {
            if (host != null)
                return host.points[points[index]];
            return null;
        }
        public Point3D this[int index] { get { return host != null ? host.points[points[index]] : null; } }
            

        public static Point3D norm(Side S)
        {
            if (S.points.Count() < 3)
                return new Point3D(0, 0, 0);
            Point3D U = S[1] - S[0];
            Point3D V = S[S.points.Count - 1] - S[0];
            Point3D normal = U * V;
            return Point3D.norm(normal);
        }

        public void CalculateSideNormal()
        {
            Normal = norm(this);
        }
    }

    public class Sphere : Figure
    {
        float radius;

        public Sphere(Point3D p, float r)
        {
            points.Add(p);
            radius = r;
        }

        public static bool RaySphereIntersection(Ray r, Point3D sphere_pos, float sphere_rad, out float t)
        {
            Point3D k = r.start - sphere_pos;
            float b = Point3D.scalar(k, r.direction);
            float c = Point3D.scalar(k, k) - sphere_rad * sphere_rad;
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
            if (RaySphereIntersection(r, points[0], radius, out t) && (t > eps))
            {
                normal = (r.start + r.direction * t) - points[0];
                normal = Point3D.norm(normal);
                return true;
            }
            return false;
        }
    }
}
