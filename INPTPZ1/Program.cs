using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private static int pictureWidth, pictureHeight;
        private static double xMaximum, xMinimum, yMinimum, yMaximux, xStep, yStep;
       
        private const int BRIGHTNESS = 30;
        private const double TOLERANCE = 0.5;
        private const double MINIMUM = 0.01;

        private static Bitmap bitmapPicture;
        private static List<ComplexNumber> roots;
        private static Polynome polynome, derivatedPolynom;
        private static Color[] colors;


        static void Main(string[] args)
        {
            try
            {

                int inputNumber = ProcessInputNumberArgument(args[0]);
                Initialize(inputNumber, inputNumber);
                MakeImage();
                bitmapPicture.Save("../../../out.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception \n" + ex);
            }
        }

        private static int ProcessInputNumberArgument(string input)
        {
            int inputNumber = int.Parse(input);
            if (inputNumber <= 0)
            {
                throw new ArgumentException("Input argument must be greater than zero.");
            }
            return inputNumber;
        }

        private static void Initialize(int widthParameter, int heightParameter)
        {
            pictureWidth = widthParameter;
            pictureHeight = heightParameter;

            Bitmap bitmap = new Bitmap(pictureWidth, pictureHeight);

            xMaximum = 1.5;
            xMinimum = -1.5;
            yMinimum = -1.5;
            yMaximux = 1.5;
            xStep = (xMaximum - xMinimum) / pictureWidth;
            yStep = (yMaximux - yMinimum) / pictureHeight;

            bitmapPicture = new Bitmap(pictureWidth, pictureHeight);
            roots = new List<ComplexNumber>();
            polynome = CreatePolynome();
            derivatedPolynom = polynome.Derive();

            colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            Console.WriteLine(polynome);
            Console.WriteLine(derivatedPolynom);
        }

        private static void MakeImage()
        {
            for (int xCoordinate = 0; xCoordinate < pictureWidth; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < pictureHeight; yCoordinate++)
                {
                    ComplexNumber complexNumber = new ComplexNumber()
                    {
                        RealPart = xMinimum + yCoordinate * xStep,
                        ImaginaryPart = yMinimum + xCoordinate * yStep
                    };

                    CheckZeroComplexNumber(complexNumber);

                    int iteration = 0;
                    CalculateNewtonIteration(ref iteration, ref complexNumber);

                    int numberOfRoots = 0;
                    FindRoots(complexNumber, ref numberOfRoots);

                    SetPixelInImage(numberOfRoots, iteration, xCoordinate, yCoordinate);
                }
            }
        }

        private static void SetPixelInImage(int numberOfRoots, int iteration, int xCoordinate, int yCoordinate)
        {
            Color color = colors[numberOfRoots % colors.Length];
            int red = Math.Min(Math.Max(0, color.R - iteration * 2), 255);
            int blue = Math.Min(Math.Max(0, color.B - iteration * 2), 255);
            int green = Math.Min(Math.Max(0, color.G - iteration * 2), 255);
            color = Color.FromArgb(red, green, blue);
            bitmapPicture.SetPixel(yCoordinate, xCoordinate, color);
        }


        private static Polynome CreatePolynome()
        {
            Polynome polynome = new Polynome();
            polynome.Coeficients.Add(new ComplexNumber() { RealPart = 1 });
            polynome.Coeficients.Add(ComplexNumber.Zero);
            polynome.Coeficients.Add(ComplexNumber.Zero);
            polynome.Coeficients.Add(new ComplexNumber() { RealPart = 1 });
            return polynome;
        }

        private static void CheckZeroComplexNumber(ComplexNumber complexNumber)
        {
            if (complexNumber.RealPart == 0)
                complexNumber.RealPart = 0.0001;
            if (complexNumber.ImaginaryPart == 0)
                complexNumber.ImaginaryPart = 0.0001;
        }


        private static void FindRoots(ComplexNumber complexNumber, ref int numberOfRoots)
        {
            bool knownRoot = false;
            int exponent = 2;

            for (int i = 0; i < roots.Count; i++)
            {
                double realParth = Math.Pow(complexNumber.RealPart - roots[i].RealPart, exponent);
                double imaginaryParth = Math.Pow(complexNumber.ImaginaryPart - roots[i].ImaginaryPart, exponent);

                if (realParth + imaginaryParth <= MINIMUM)
                {
                    knownRoot = true;
                    numberOfRoots = i;
                }
            }
            if (!knownRoot)
            {
                roots.Add(complexNumber);
                numberOfRoots = roots.Count;
            }
        }

        private static void CalculateNewtonIteration(ref int iteration, ref ComplexNumber complexNumber)
        {

            for (int i = 0; i < BRIGHTNESS; i++)
            {
                var diff = polynome.Eval(complexNumber).Divide(derivatedPolynom.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= TOLERANCE)
                {
                    i--;
                }
                iteration++;
            }
        }
    }
}