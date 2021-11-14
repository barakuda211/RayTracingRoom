using System;
using System.Drawing;

namespace RayTracingRoom
{
    /// <summary>
    /// Вещественное представление цвета для удобных вычислений
    /// </summary>
    public class FloatColor
    {
        public float R,G,B;

        public FloatColor()
        {
            R = 0; G = 0; B = 0;
        }
        public FloatColor(FloatColor fc)
        {
            R = fc.R; G = fc.G; B = fc.B;
        }
        public FloatColor(float r, float g, float b)
        {
            R = r; G = g; B = b;
        }
        public FloatColor(Color col)
        {
            R = col.R*1.0f/255; G = col.G * 1.0f / 255; B = col.B * 1.0f / 255;
        }

        public static FloatColor operator+(FloatColor p1, FloatColor p2)
        {
            return new FloatColor(p1.R + p2.R, p1.G + p2.G, p1.B + p2.B);
        }
        public static FloatColor operator*(FloatColor p1, FloatColor p2)
        {
            return new FloatColor(p1.G * p2.B - p1.B * p2.G, p1.B * p2.R - p1.R * p2.B, p1.R * p2.G - p1.G * p2.R);
        }
        public static FloatColor operator*(float t, FloatColor p1)
        {
            return new FloatColor(p1.R * t, p1.G * t, p1.B * t);
        }
        public static FloatColor operator*(FloatColor p1, float t)
        {
            return new FloatColor(p1.R * t, p1.G * t, p1.B * t);
        }
        public static FloatColor operator/(FloatColor p1, float t)
        {
            return new FloatColor(p1.R / t, p1.G / t, p1.B / t);
        }

        public Color ToColor() => Color.FromArgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
    }

    public class Ray
    {
        public Point3D start, direction;

        public Ray(Point3D st, Point3D end)
        {
            start = new Point3D(st);
            direction = Point3D.Normilize(end - st);
        }
        public Ray() { }
        public Ray(Ray r)
        {
            start = r.start;
            direction = r.direction;
        }

        /// <summary>
        /// Возвращает отражённый луч
        /// </summary>
        /// <param name="hit_point">Точка пересечения</param>
        /// <param name="normal">Нормаль плоскости пересечения</param>
        /// <returns>Отражённый луч</returns>
        public Ray Reflect(Point3D hit_point, Point3D normal)
        {
            Point3D reflect_dir = direction - 2 * normal * Point3D.ScalarMult(direction, normal);
            return new Ray(hit_point, hit_point + reflect_dir);
        }
        /// <summary>
        /// Возвращает преломлённый луч
        /// </summary>
        /// <param name="hit_point">Точка пересечения</param>
        /// <param name="normal">Нормаль плоскости пересечения</param>
        /// <param name="refract_coef">Коэфицент преломления среды</param>
        /// <returns>Преломлённый луч</returns>
        public Ray Refract(Point3D hit_point, Point3D normal, float refract_coef)
        {
            Ray res_ray = new Ray();
            float sclr = Point3D.ScalarMult(normal, direction);

            float k = 1 - refract_coef * refract_coef * (1 - sclr * sclr);
            if (k >= 0)
            {
                float cos_theta = (float)Math.Sqrt(k);
                res_ray.start = new Point3D(hit_point);
                res_ray.direction = Point3D.Normilize(refract_coef * direction - (cos_theta + refract_coef * sclr) * normal);
                return res_ray;
            }
            else
                return null;
        }
    }

    public class Material
    {
        public float reflection;
        public float refraction;
        public float ambient;
        public float diffuse;
        public float environment;
        public FloatColor color;

        public Material(float refl, float refr, float amb, float dif, float env = 1.0f)
        {
            reflection = refl;
            refraction = refr;
            ambient = amb;
            diffuse = dif;
            environment = env;
        }

        public Material(Material m)
        {
            reflection = m.reflection;
            refraction = m.refraction;
            environment = m.environment;
            ambient = m.ambient;
            diffuse = m.diffuse;
            color = new FloatColor(m.color);
        }

        public Material()
        {

        }
    }

    public class Light : Figure
    {
        public Point3D position;
        public FloatColor color;

        public Light(Point3D p, FloatColor c)
        {
            position = new Point3D(p);
            color = new FloatColor(c);
        }

        /// <summary>
        /// Рассчёт освещения в точке
        /// </summary>
        /// <param name="hit_point"></param>
        /// <param name="normal"></param>
        /// <param name="material_color"></param>
        /// <param name="diffuse_coef"></param>
        /// <returns></returns>
        public FloatColor Shade(Point3D hit_point, Point3D normal, FloatColor material_color, float diffuse_coef)
        {
            Point3D dir = position - hit_point;
            dir = Point3D.Normilize(dir);

            FloatColor diff = diffuse_coef * color * Math.Max(Point3D.ScalarMult(normal, dir), 0);
            return new FloatColor(diff.R * material_color.R, diff.G * material_color.G, diff.B * material_color.B);
        }
    }
}
