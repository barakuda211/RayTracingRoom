using System;
using System.Drawing;

namespace RayTracingRoom
{
    public class FloatColor
    {
        float R,G,B;
        public FloatColor()
        {
            R = 0; G = 0; B = 0;
        }
        public FloatColor(float r, float g, float b)
        {
            R = r; G = g; B = b;
        }
        public FloatColor(Color col)
        {
            R = col.R*1.0f/255; G = col.G * 1.0f / 255; B = col.B * 1.0f / 255;
        }

        public Color ToColor() => Color.FromArgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
    }

    public class Ray
    {
        public Point3D start, direction;

        public Ray(Point3D st, Point3D end)
        {
            start = new Point3D(st);
            direction = Point3D.norm(end - st);
        }

        public Ray() { }

        public Ray(Ray r)
        {
            start = r.start;
            direction = r.direction;
        }

        //отражение
        /*
         Направление отраженного луча определяется по закону:
         отраженный луч = падающий луч -  2* нормаль к точке попадания луча на сторону  на скалярное произведение падающего луча и нормали
         из презентации
             */
        public Ray Reflect(Point3D hit_point, Point3D normal)
        {
            //высчитываем направление отраженного луча
            Point3D reflect_dir = direction - 2 * normal * Point3D.scalar(direction, normal);
            return new Ray(hit_point, hit_point + reflect_dir);
        }

        //преломление
        //все вычисления взяты из презентации
        public Ray Refract(Point3D hit_point, Point3D normal, float refraction, float refract_coef)
        {
            Ray res_ray = new Ray();
            float sclr = Point3D.scalar(normal, direction);
            /*
             Если луч падает,то он проходит прямо,не преломляясь
             */
            float n1n2div = refraction / refract_coef;
            float theta_formula = 1 - n1n2div * n1n2div * (1 - sclr * sclr);
            if (theta_formula >= 0)
            {
                float cos_theta = (float)Math.Sqrt(theta_formula);
                res_ray.start = new Point3D(hit_point);
                res_ray.direction = Point3D.norm(direction * n1n2div - (cos_theta + n1n2div * sclr) * normal);
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
        public Point3D color;

        public Material(float refl, float refr, float amb, float dif, float env = 1)
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
            color = new Point3D(m.color);
        }

        public Material()
        {

        }
    }

    public class Light : Figure           // источник света
    {
        public Point3D point_light;       // точка, где находится источник света
        public Point3D color_light;       // цвет источника света

        public Light(Point3D p, Point3D c)
        {
            point_light = new Point3D(p);
            color_light = new Point3D(c);
        }

        //вычисление локальной модели освещения
        public Point3D Shade(Point3D hit_point, Point3D normal, Point3D material_color, float diffuse_coef)
        {
            Point3D dir = point_light - hit_point;
            dir = Point3D.norm(dir);// направление луча 
            //если угол между нормалью и направлением луча больше 90 градусов,то диффузное  освещение равно 0
            Point3D diff = diffuse_coef * color_light * Math.Max(Point3D.scalar(normal, dir), 0);
            return new Point3D(diff.x * material_color.x, diff.y * material_color.y, diff.z * material_color.z);
        }
    }
}
