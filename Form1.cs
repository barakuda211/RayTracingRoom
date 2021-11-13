﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracingRoom
{
    public partial class Form1 : Form
    {
        Point3D camera = new Point3D();
        List<Light> lightList = new List<Light>();
        List<Figure> figureList = new List<Figure>();
        Color[,] colorMatrix;
        Point3D[,] pixelMatrix;
        Cube cube1, cube2, room;
        Sphere sphere;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pb.Image = new Bitmap(pb.Width, pb.Height);
            colorMatrix = new Color[pb.Width, pb.Height];
            pixelMatrix = new Point3D[pb.Width, pb.Height];
        }

        public void LoadFigures()
        {
            room = new Cube(10);
            Point3D normal = Side.norm(room.sides[0]);                           
            Point3D center = (room.sides[0][0]+ room.sides[0][1]+ room.sides[0][2]+ room.sides[0][3]) / 4;   //TODO
            camera = center + normal * 11;

            
            room.SetPen(new Pen(Color.Gray));
            room.sides[0].drawing_pen = new Pen(Color.Blue);
            room.sides[0].anotherMaterial = checkBox_back.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
            room.sides[1].drawing_pen = new Pen(Color.Red);
            room.sides[1].anotherMaterial = checkBox_front.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
            room.sides[2].drawing_pen = new Pen(Color.Orange);
            room.sides[2].anotherMaterial = checkBox_right.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
            room.sides[3].drawing_pen = new Pen(Color.YellowGreen);
            room.sides[3].anotherMaterial = checkBox_left.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
            room.sides[4].drawing_pen = new Pen(Color.Gray);
            room.sides[4].anotherMaterial = checkBox_up.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
            room.sides[5].drawing_pen = new Pen(Color.Green);
            room.sides[5].anotherMaterial = checkBox_down.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
            room.fMaterial = new Material(0, 0, 0.05f, 0.7f);

            Light l1 = new Light(new Point3D(0f, 2f, 4.9f), new Point3D(1f, 1f, 1f));
            Light l2 = new Light(new Point3D(-4.9f, 4.9f, 4.9f), new Point3D(1f, 1f, 1f));
            lightList.Add(l1); lightList.Add(l2);

            sphere = new Sphere(new Point3D(2.5f, -2, 1f), 2.5f);
            sphere.SetPen(new Pen(Color.White));
            if (rb_sphere_fill.Checked)
                sphere.fMaterial = new Material(0f, 0f, 0.1f, 0.7f, 1.5f);
            if (rb_sphere_transparent.Checked)
                sphere.fMaterial = new Material(0, 0.7f, 0.1f, 0.5f, 1f);
            if (rb_sphere_mirror.Checked)
                sphere.fMaterial = new Material(0.9f, 0f, 0f, 0.1f, 1f);

            cube1 = new Cube(2.8f);
            cube1.Offset(-1.5f, 1.5f, -3.9f);
            cube1.RotateAround(-20, "CZ");
            cube1.SetPen(new Pen(Color.Aqua));
            if (rb_cube1_fill.Checked)
                cube1.fMaterial = new Material(0f, 0f, 0.1f, 0.7f, 1.5f);
            if (rb_cube1_transparent.Checked)
                cube1.fMaterial = new Material(0, 0.7f, 0.1f, 0.5f, 1f);
            if (rb_cube1_mirror.Checked)
                cube1.fMaterial = new Material(0.9f, 0f, 0f, 0.1f, 1f);

            cube2 = new Cube(3f);
            cube2.Offset(2f, 2f, -3.6f);
            cube2.RotateAround(20, "CZ");
            cube2.SetPen(new Pen(Color.Red));
            if (rb_cube2_fill.Checked)
                cube2.fMaterial = new Material(0f, 0f, 0.1f, 0.7f, 1.5f);
            if (rb_cube2_transparent.Checked)
                cube2.fMaterial = new Material(0, 0.7f, 0.1f, 0.5f, 1f);
            if (rb_cube2_mirror.Checked)
                cube2.fMaterial = new Material(0.9f, 0f, 0f, 0.1f, 1f);

            figureList.Add(room);
            figureList.Add(cube1);
            figureList.Add(cube2);
            figureList.Add(sphere);
        }

        public void Clear()
        {
            colorMatrix = new Color[pb.Width, pb.Height];
            pixelMatrix = new Point3D[pb.Width, pb.Height];
            lightList = new List<Light>();
            figureList = new List<Figure>();
        }

        public void DrawScene()
        {
            pixelMatrix = new Point3D[pb.Width, pb.Height];
            colorMatrix = new Color[pb.Width, pb.Height];
            Point3D step_up = (room.sides[0][1] - room.sides[0][0]) / (pb.Width - 1);
            Point3D step_down = (room.sides[0][2] - room.sides[0][3]) / (pb.Height - 1);
            Point3D up = new Point3D(room.sides[0][0]);
            Point3D down = new Point3D(room.sides[0][3]);
            for (int i = 0; i < pb.Width; ++i)
            {
                Point3D step_y = (up - down) / (pb.Height - 1);
                Point3D d = new Point3D(down);
                for (int j = 0; j < pb.Height; ++j)
                {
                    pixelMatrix[i, j] = d;
                    d += step_y;
                }
                up += step_up;
                down += step_down;
            }

            for (int i = 0; i < pb.Width; ++i)
                for (int j = 0; j < pb.Height; ++j)
                {
                    Ray r = new Ray(camera, pixelMatrix[i, j]);
                    r.start = new Point3D(pixelMatrix[i, j]);
                    Point3D color = Tracing(r, 10, 1);//луч,кол-во итераций,коэфф
                    if (color.x > 1.0f || color.y > 1.0f || color.z > 1.0f)
                        color = Point3D.norm(color);
                    colorMatrix[i, j] = Color.FromArgb((int)(255 * color.x), (int)(255 * color.y), (int)(255 * color.z));
                }
        }

        private void button_draw_Click(object sender, EventArgs e)
        {
            Clear();
            LoadFigures();
            DrawScene();
            for (int i = 0; i < pb.Width; ++i)
            {
                for (int j = 0; j < pb.Height; ++j)
                {
                    (pb.Image as Bitmap).SetPixel(i, j, colorMatrix[i, j]);
                }
            }
            pb.Refresh();
        }

        /// <summary>
        /// Достижима ли точка для света из источника
        /// </summary>
        /// <param name="light">Источник света</param>
        /// <param name="intersect">Точка пересечения</param>
        public bool HasLightRay(Point3D light, Point3D intersect)
        {
            float max_t = (light - intersect).length();
            Ray r = new Ray(intersect, light);
            foreach (Figure fig in figureList)
                if (fig.FigureIntersection(r, out float t, out Point3D n)) 
                    if (t < max_t && t > Figure.eps)
                        return false;
            return true;
        }

        /// <summary>
        /// Рекурсивный поиск пересечения луча с фигурами
        /// </summary>
        /// <param name="r">Луч</param>
        /// <param name="iter">Глубина рекурсии</param>
        /// <param name="env">Коэфицент преломления среды</param>
        /// <returns></returns>
        public Point3D Tracing(Ray r, int iter, float env)
        {
            if (iter <= 0)
                return new Point3D(0, 0, 0);
            float rey_fig_intersect = 0;

            Point3D normal = null;
            Material material = new Material();
            Point3D res_color = new Point3D(0, 0, 0);

            bool need_refract = false;

            //поиск ближайшего пересечения
            foreach (Figure fig in figureList)
            {
                if (fig.FigureIntersection(r, out float intersect, out Point3D norm))
                    if (intersect < rey_fig_intersect || rey_fig_intersect == 0)
                    {
                        rey_fig_intersect = intersect;
                        normal = norm;
                        material = new Material(fig.fMaterial);
                    }
            }

            if (rey_fig_intersect == 0)
                return new Point3D(0, 0, 0);


            if (Point3D.scalar(r.direction, normal) > 0)
            {
                normal *= -1;
                need_refract = true;
            }

            Point3D hit_point = r.start + r.direction * rey_fig_intersect;

            foreach (Light light in lightList)
            {
                //цвет коэффициент принятия фонового освещения
                Point3D ambient_coef = light.color_light * material.ambient;
                ambient_coef.x = (ambient_coef.x * material.color.x);
                ambient_coef.y = (ambient_coef.y * material.color.y);
                ambient_coef.z = (ambient_coef.z * material.color.z);
                res_color += ambient_coef;
                // диффузное освещение
                if (HasLightRay(light.point_light, hit_point))
                    res_color += light.Shade(hit_point, normal, material.color, material.diffuse);
            }


            if (material.reflection > 0)
            {
                Ray reflected_ray = r.Reflect(hit_point, normal);
                res_color += material.reflection * Tracing(reflected_ray, iter - 1, env);
            }


            if (material.refraction > 0)
            {
                float refract_coef;
                if (need_refract)
                    refract_coef = material.environment;
                else
                    refract_coef = 1 / material.environment;

                Ray refracted_ray = r.Refract(hit_point, normal, material.refraction, refract_coef);

                if (refracted_ray != null)
                    res_color += material.refraction * Tracing(refracted_ray, iter - 1, material.environment);
            }
            return res_color;
        }
    }
}