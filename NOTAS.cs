using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class program
{
    static List<Curso> Cursos = new List<Curso>();

    static void Main(string[] args)
    {
        bool showMenu = true;

        while (showMenu)
        {
            showMenu = MainMenu();
        }
    }

    static bool MainMenu()
    {
        Console.Clear();

        Console.WriteLine("==========================================================================");
        Console.WriteLine("COLEGIO DIOS ES BUENO");
        Console.WriteLine("==========================================================================");
        Console.WriteLine("1) Agregar estudiante");
        Console.WriteLine("2) Eliminar estudiante");
        Console.WriteLine("3) Reporte de notas");
        Console.WriteLine("4) Salir");
        Console.Write("\nSeleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                AgregarEstudiante();
                return true;

            case "2":
                EliminarEstudiante();
                return true;

            case "3":
                ReporteNotas();
                return true;

            case "4":
                Salir();
                return false;

            default:
                return true;
        }
    }

    static void AgregarEstudiante()
    {
        Console.Clear();

        Console.WriteLine("======== CURSOS ========");

        if (Cursos.Count > 0)
        {
            for (int i = 0; i < Cursos.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {Cursos[i].Nombre}");
            }
        }

        Console.WriteLine($"{Cursos.Count + 1}) Crear nuevo curso");

        Console.Write("\nSeleccione un curso: ");
        int opcionCurso = Convert.ToInt32(Console.ReadLine());

        Curso curso;

        if (opcionCurso == Cursos.Count + 1)
        {
            Console.Write("Ingrese el nombre del nuevo curso: ");
            string nombreCurso = Console.ReadLine();

            curso = new Curso(nombreCurso);
            Cursos.Add(curso);
        }
        else
        {
            curso = Cursos[opcionCurso - 1];
        }

        Console.WriteLine();
        Console.WriteLine("======== AGREGAR ESTUDIANTE =========");

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Apellido: ");
        string apellido = Console.ReadLine();

        Console.Write("Nota 1: ");
        double nota1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Nota 2: ");
        double nota2 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Nota 3: ");
        double nota3 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Nota 4: ");
        double nota4 = Convert.ToDouble(Console.ReadLine());

        double promedio = (nota1 + nota2 + nota3 + nota4) / 4;

        string literal;

        if (promedio >= 90)
        {
            literal = "A";
        }
        else if (promedio >= 80)
        {
            literal = "B";
        }
        else if (promedio >= 70)
        {
            literal = "C";
        }
        else
        {
            literal = "D";
        }

        Estudiante estudiante = new Estudiante
        {
            Nombre = nombre,
            Apellido = apellido,
            Nota1 = nota1,
            Nota2 = nota2,
            Nota3 = nota3,
            Nota4 = nota4,
            Promedio = promedio,
            Literal = literal
        };

        curso.AgregarEstudiante(estudiante);
        File.AppendAllText("estudiantes.txt",
    $"Curso: {curso.Nombre}\n" +
    $"Nombre: {estudiante.Nombre}\n" +
    $"Apellido: {estudiante.Apellido}\n" +
    $"Nota 1: {estudiante.Nota1}\n" +
    $"Nota 2: {estudiante.Nota2}\n" +
    $"Nota 3: {estudiante.Nota3}\n" +
    $"Nota 4: {estudiante.Nota4}\n" +
    $"Promedio: {estudiante.Promedio:F2}\n" +
    $"Literal: {estudiante.Literal}\n" +
    "====================================\n"
);

        Console.WriteLine();
        Console.WriteLine("Estudiante agregado.");
        Console.WriteLine("Curso: " + curso.Nombre);
        Console.WriteLine("Promedio: " + promedio.ToString("F2"));

        Console.ReadKey();
    }

    static void EliminarEstudiante()
    {
        Console.Clear();

        if (Cursos.Count == 0)
        {
            Console.WriteLine("No hay cursos registrados.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("======== CURSOS ========");

        for (int i = 0; i < Cursos.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {Cursos[i].Nombre}");
        }

        Console.Write("\nSeleccione el curso: ");
        int opcionCurso = Convert.ToInt32(Console.ReadLine());

        Curso curso = Cursos[opcionCurso - 1];

        Console.WriteLine();
        Console.WriteLine("Estudiantes del curso:");

        for (int i = 0; i < curso.Estudiantes.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1}) {curso.Estudiantes[i].Nombre} {curso.Estudiantes[i].Apellido}");
        }

        Console.Write("\nSeleccione el estudiante que desea eliminar: ");
        int opcionEstudiante = Convert.ToInt32(Console.ReadLine());

        Estudiante estudiante = curso.Estudiantes[opcionEstudiante - 1];

        curso.Estudiantes.Remove(estudiante);

        Console.WriteLine();
        Console.WriteLine("Estudiante eliminado.");

        Console.ReadKey();
        
    }

    static void ReporteNotas()
    {
        Console.Clear();

        if (Cursos.Count == 0)
        {
            Console.WriteLine("No hay estudiantes, agregue a alguien primero.");
            Console.ReadKey();
            return;
        }

        string reporte = "";

        Console.WriteLine("COLEGIO DIOS ES BUENO");
        Console.WriteLine("CALIFICACIONES DEL CUATRIMESTRE");
        Console.WriteLine();

        reporte += "COLEGIO DIOS ES BUENO\n";
        reporte += "CALIFICACIONES DEL CUATRIMESTRE\n\n";

        foreach (Curso curso in Cursos.OrderBy(c => c.Nombre))
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("CURSO: " + curso.Nombre);
            Console.WriteLine("==============================================");

            reporte += "==============================================\n";
            reporte += "CURSO: " + curso.Nombre + "\n";
            reporte += "==============================================\n";

            Console.WriteLine(
                "Nombre       Apellido       Nota1  Nota2  Nota3  Nota4  Promedio  Literal");

            Console.WriteLine(
                "==========================================================================");

            reporte +=
                "Nombre       Apellido       Nota1  Nota2  Nota3  Nota4  Promedio  Literal\n";

            reporte +=
                "==========================================================================\n";

            foreach (Estudiante estudiante in curso.Estudiantes.OrderBy(e => e.Apellido))
            {
                string linea =
                    $"{estudiante.Nombre,-12}" +
                    $"{estudiante.Apellido,-15}" +
                    $"{estudiante.Nota1,-7}" +
                    $"{estudiante.Nota2,-7}" +
                    $"{estudiante.Nota3,-7}" +
                    $"{estudiante.Nota4,-7}" +
                    $"{estudiante.Promedio,-10:F2}" +
                    $"{estudiante.Literal}";

                Console.WriteLine(linea);

                reporte += linea + "\n";
            }

            int cantidadA = curso.Estudiantes.Count(e => e.Literal == "A");
            int cantidadB = curso.Estudiantes.Count(e => e.Literal == "B");
            int cantidadC = curso.Estudiantes.Count(e => e.Literal == "C");
            int cantidadD = curso.Estudiantes.Count(e => e.Literal == "D");

            Console.WriteLine();
            Console.WriteLine("TOTALES");
            Console.WriteLine("=================================");
            Console.WriteLine("Estudiantes en A: " + cantidadA);
            Console.WriteLine("Estudiantes en B: " + cantidadB);
            Console.WriteLine("Estudiantes en C: " + cantidadC);
            Console.WriteLine("Estudiantes reprobados: " + cantidadD);
            Console.WriteLine();

            reporte += "\nTOTALES\n";
            reporte += "Estudiantes en A: " + cantidadA + "\n";
            reporte += "Estudiantes en B: " + cantidadB + "\n";
            reporte += "Estudiantes en C: " + cantidadC + "\n";
            reporte += "Estudiantes reprobados: " + cantidadD + "\n\n";
        }

        File.WriteAllText("reporte.txt", reporte);

       

        Console.ReadKey();
    }

    static void Salir()
    {
        Console.Clear();

        Console.WriteLine("Presiona enter para salir.");

        Console.ReadKey();
    }
}


class Curso
{
    public string Nombre { get; set; }

    public List<Estudiante> Estudiantes { get; set; }

    public Curso(string nombre)
    {
        Nombre = nombre;
        Estudiantes = new List<Estudiante>();
    }

    public void AgregarEstudiante(Estudiante estudiante)
    {
        Estudiantes.Add(estudiante);
    }
}


class Estudiante
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }

    public double Nota1 { get; set; }
    public double Nota2 { get; set; }
    public double Nota3 { get; set; }
    public double Nota4 { get; set; }

    public double Promedio { get; set; }

    public string Literal { get; set; }
}
