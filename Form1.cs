using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracingRoom
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp;
        Point3D camera = new Point3D();
        List<Light> lightList = new List<Light>();
        List<Figure> figureList = new List<Figure>();
        Color[,] colorMatrix;
        Point3D[,] pixelMatrix;
        Cube scene;
        Point3D defaultLightPosition = new Point3D(-4.9f, 4.9f, 4.9f);
        Point3D step_up, step_down, up, down;
        Light additionLight;
        int temp_i;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            pb.Image = bmp;
            g = Graphics.FromImage(bmp);
            colorMatrix = new Color[pb.Width, pb.Height];
            pixelMatrix = new Point3D[pb.Width, pb.Height];
            additionLight = new Light(defaultLightPosition, new FloatColor(1f, 1f, 1f));
        }

        /// <summary>
        /// Инициализация сцены
        /// </summary>
        public void LoadFigures()
        {
            scene = new Cube(10);
            Point3D normal = Side.GetNormal(scene.listSides[0]);                           
            Point3D center = (scene.listSides[0][0]+ scene.listSides[0][1]+ scene.listSides[0][2]+ scene.listSides[0][3]) / 4;   //TODO
            camera = center + normal * 11;

            //Создание стен комнаты с помощью костыльных кубов
            {
                var front = new Cube(10);
                front.Move(0, -10, 0);
                front.SetPen(new Pen(Color.Red));
                front.material = checkBox_front.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
                figureList.Add(front);

                var left = new Cube(10);
                left.Move(10, 0, 0);
                left.SetPen(new Pen(Color.Orange));
                left.material = checkBox_left.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
                figureList.Add(left);

                var right = new Cube(10);
                right.Move(-10, 0, 0);
                right.SetPen(new Pen(Color.Yellow));
                right.material = checkBox_right.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
                figureList.Add(right);

                var back = new Cube(10);
                back.Move(0, 10, 0);
                back.SetPen(new Pen(Color.Blue));
                back.material = checkBox_back.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
                figureList.Add(back);

                var down = new Cube(10);
                down.Move(0, 0, -10);
                down.SetPen(new Pen(Color.Green));
                down.material = checkBox_down.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
                figureList.Add(down);

                var up = new Cube(10);
                up.Move(0, 0, 10);
                up.SetPen(new Pen(Color.White));
                up.material = checkBox_up.Checked ? new Material(0.9f, 0f, 0f, 0.1f, 1f) : new Material(0, 0, 0.05f, 0.7f);
                figureList.Add(up);
            }

            //Создание света
            Light main_light = new Light(new Point3D(0f, 2f, 4.9f), new FloatColor(1f, 1f, 1f));
            lightList.Add(main_light); lightList.Add(additionLight);

            //Создание фигур сцены
            {
                var sphere = new Sphere(new Point3D(2, -2, 1f), 2.5f);
                //var sphere = new Sphere(new Point3D(-2.5f, 2, -3.25f), 2.5f);
                sphere.SetPen(new Pen(Color.White));
                if (rb_sphere_fill.Checked)
                    sphere.material = new Material(0f, 0f, 0.1f, 0.7f, 1.5f);
                if (rb_sphere_transparent.Checked)
                    sphere.material = new Material(0, 0.9f, 0.0f, 0.0f, 1.03f);
                if (rb_sphere_mirror.Checked)
                    sphere.material = new Material(0.9f, 0f, 0f, 0.1f, 1f);

                var cube1 = new Cube(2.8f);
                cube1.Move(-1.5f, -0.5f, -3.9f);
                cube1.RotateAround(-20, "CZ");
                cube1.SetPen(new Pen(Color.Aqua));
                if (rb_cube1_fill.Checked)
                    cube1.material = new Material(0f, 0f, 0.1f, 0.7f, 1.5f);
                if (rb_cube1_transparent.Checked)
                    cube1.material = new Material(0, 0.7f, 0.1f, 0.5f, 1f);
                if (rb_cube1_mirror.Checked)
                    cube1.material = new Material(0.9f, 0f, 0f, 0.1f, 1f);

                var cube2 = new Cube(3f);
                cube2.Move(2f, 0f, -3.6f);
                cube2.RotateAround(20, "CZ");
                cube2.SetPen(new Pen(Color.Green));
                if (rb_cube2_fill.Checked)
                    cube2.material = new Material(0f, 0f, 0.1f, 0.7f, 1.5f);
                if (rb_cube2_transparent.Checked)
                    cube2.material = new Material(0, 0.7f, 0.1f, 0.5f, 1f);
                if (rb_cube2_mirror.Checked)
                    cube2.material = new Material(0.9f, 0f, 0f, 0.1f, 1f);

                figureList.Add(cube1);
                figureList.Add(cube2);
                figureList.Add(sphere);
            }
        }

        /// <summary>
        /// Очистка сцены
        /// </summary>
        public void Clear()
        {
            colorMatrix = new Color[pb.Width, pb.Height];
            pixelMatrix = new Point3D[pb.Width, pb.Height];
            lightList = new List<Light>();
            figureList = new List<Figure>();
        }

        /// <summary>
        /// Отрисовка сцены
        /// </summary>
        public void DrawScene()
        {
            g.Clear(Color.White);
            pixelMatrix = new Point3D[pb.Width, pb.Height];
            colorMatrix = new Color[pb.Width, pb.Height];
            step_up = (scene.listSides[0][1] - scene.listSides[0][0]) / (pb.Width - 1);
            step_down = (scene.listSides[0][2] - scene.listSides[0][3]) / (pb.Height - 1);
            up = new Point3D(scene.listSides[0][0]);
            down = new Point3D(scene.listSides[0][3]);
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
                    FloatColor color = Tracing(r, 7, 1);
                    if (color.R > 1.0f || color.G > 1.0f || color.B > 1.0f)
                    {
                        float z = (float)Math.Sqrt((float)(color.R * color.R + color.G * color.G + color.B * color.B));
                        if (z != 0)
                            color = new FloatColor(color.R / z, color.G / z, color.B / z);
                    }
                    colorMatrix[i, j] = color.ToColor();
                }
        }

        /// <summary>
        /// Отрисовка сцены с помощбю распараллеливания
        /// </summary>
        public void DrawSceneParallel()
        {
            g.Clear(Color.White);
            pixelMatrix = new Point3D[pb.Width, pb.Height];
            colorMatrix = new Color[pb.Width, pb.Height];
            step_up = (scene.listSides[0][1] - scene.listSides[0][0]) / (pb.Width - 1);
            step_down = (scene.listSides[0][2] - scene.listSides[0][3]) / (pb.Height - 1);
            up = new Point3D(scene.listSides[0][0]);
            down = new Point3D(scene.listSides[0][3]);
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

            Parallel.For(0, pb.Width*pb.Height, ParallelTracing);
        }

        /// <summary>
        /// Метод для распаралеливания рэй трейсинга
        /// </summary>
        /// <param name="i"></param>
        private void ParallelTracing(int x)
        {
            (int i, int j) = (x / pb.Width,x % pb.Width);
            Ray r = new Ray(camera, pixelMatrix[i, j]);
            r.start = new Point3D(pixelMatrix[i, j]);
            FloatColor color = Tracing(r, 7, 1);
            if (color.R > 1.0f || color.G > 1.0f || color.B > 1.0f)
            {
                float z = (float)Math.Sqrt((float)(color.R * color.R + color.G * color.G + color.B * color.B));
                if (z != 0)
                    color = new FloatColor(color.R / z, color.G / z, color.B / z);
            }
            colorMatrix[i, j] = color.ToColor();

        }

        private void button_draw_Click(object sender, EventArgs e)
        {
            var stp = new Stopwatch();
            stp.Start();
            Clear();
            LoadFigures();
            DrawScene();
            for (int i = 0; i < pb.Width; ++i)
            {
                for (int j = 0; j < pb.Height; ++j)
                {
                    bmp.SetPixel(i, j, colorMatrix[i, j]);
                }
            }
            pb.Refresh();
            stp.Stop();
            label_Info.Text = $"Ellapsed: {stp.ElapsedMilliseconds} ms";
        }

        private void button_paral_draw_Click(object sender, EventArgs e)
        {
            var stp = new Stopwatch();
            stp.Start();
            Clear();
            LoadFigures();
            DrawSceneParallel();
            for (int i = 0; i < pb.Width; ++i)
            {
                for (int j = 0; j < pb.Height; ++j)
                {
                    bmp.SetPixel(i, j, colorMatrix[i, j]);
                }
            }
            pb.Refresh();
            stp.Stop();
            label_Info.Text = $"Ellapsed: {stp.ElapsedMilliseconds} ms";
        }

        /// <summary>
        /// Достижима ли точка для света из источника
        /// </summary>
        /// <param name="light">Источник света</param>
        /// <param name="intersect">Точка пересечения</param>
        public bool HasLightRay(Point3D light, Point3D intersect)
        {
            float max_t = (light - intersect).Length();
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
        /// <param name="current_ray">Луч</param>
        /// <param name="iterations_last">Оставшиеся итерации рекурсии</param>
        /// <param name="env">Коэфицент преломления среды</param>
        /// <returns></returns>
        public FloatColor Tracing(Ray current_ray, int iterations_last, float env)
        {
            //выход из рекурсии
            if (iterations_last <= 0)
                return new FloatColor();

            Point3D normal = null;
            Material material = new Material();
            FloatColor result = new FloatColor();

            //поиск ближайшего пересечения
            float distance_to_figure = FindNearestIntersection(current_ray, ref normal, ref material);

            //отсечение луча, если нет пересечения с фигурой
            if (distance_to_figure == -1)
                return new FloatColor();

            //точка падения луча
            Point3D hit_point = current_ray.start + current_ray.direction * distance_to_figure;

            //отражение луча, если угол 
            float refract_coef = 1 / material.environment;
            if (Point3D.ScalarMult(current_ray.direction, normal) > 0)
            {
                normal *= -1;
                refract_coef = material.environment;
            }

            //Обработка источников света
            result += HandleLight(hit_point, normal, material);

            //Запуск луча на преломление
            result += HandleRefraction(hit_point, normal, material, refract_coef, current_ray, iterations_last);

            //Запуск луча на отражение
            result += HandleReflection(hit_point, normal, material, current_ray, iterations_last, env);

            return result;
        }

        //Возвращает расстояние до ближайшей фигуры или -1
        private float FindNearestIntersection(Ray current_ray, ref Point3D normal, ref Material material)
        {
            var result = float.MaxValue;
            foreach (Figure fig in figureList)
            {
                if (fig.FigureIntersection(current_ray, out float intersect, out Point3D norm))
                    if (intersect < result)
                    {
                        result = intersect;
                        normal = norm;
                        material = new Material(fig.material);
                    }
            }
            if (result == float.MaxValue)
                return -1;
            return result;
        }

        //Обработка отражения
        private FloatColor HandleReflection(Point3D hit_point, Point3D normal, Material material, Ray cur_ray, int iteration, float env_coef)
        {
            var result = new FloatColor();
            if (material.reflection > 0)
            {
                Ray reflected_ray = cur_ray.Reflect(hit_point, normal);
                result += material.reflection * Tracing(reflected_ray, iteration - 1, env_coef);
            }
            return result;
        }

        //Обработка преломления
        private FloatColor HandleRefraction(Point3D hit_point, Point3D normal, Material material, float refract_coef, Ray cur_ray, int iteration)
        {
            var result = new FloatColor();
            if (material.refraction > 0)
            {
                Ray refracted_ray = cur_ray.Refract(hit_point, normal, refract_coef);
                if (refracted_ray != null)
                    result += material.refraction * Tracing(refracted_ray, iteration - 1, material.environment);
            }
            return result;
        }

        //Обработка падения света
        private FloatColor HandleLight(Point3D hit_point, Point3D normal, Material material)
        {
            var result = new FloatColor();
            foreach (Light light in lightList)
            {
                FloatColor ambient_coef = light.color * material.ambient;
                ambient_coef.R = (ambient_coef.R * material.color.R);
                ambient_coef.G = (ambient_coef.G * material.color.G);
                ambient_coef.B = (ambient_coef.B * material.color.B);
                result += ambient_coef;
                if (HasLightRay(light.position, hit_point))
                    result += light.Shade(hit_point, normal, material.color, material.diffuse);
            }
            return result;
        }

        /// <summary>
        /// Замена позиции дополнительного света
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_set_light_pos_Click(object sender, EventArgs e)
        {
            double x, y, z;
            if (!double.TryParse(textBox_X.Text, out x))
                return;
            if (!double.TryParse(textBox_Y.Text, out y))
                return;
            if (!double.TryParse(textBox_Z.Text, out z))
                return;
            lightList.Remove(additionLight);
            additionLight = new Light(new Point3D((float)x, (float)y, (float)z), new FloatColor(1f, 1f, 1f));
            lightList.Add(additionLight);
        }
    }
}
