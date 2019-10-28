using System.Collections.Generic;

namespace INPTPZ1
{
    class Polynome
    {
        public List<ComplexNumber> Coeficients { get; set; }

        public Polynome()
        {
            Coeficients = new List<ComplexNumber>();
        }

        public Polynome Derive()
        {
            Polynome polynome = new Polynome();
            for (int i = 1; i < Coeficients.Count; i++)
            {
                polynome.Coeficients.Add(Coeficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return polynome;
        }

        public ComplexNumber Eval(ComplexNumber evalComplexNumberCopy)
        {
            ComplexNumber zeroComplexNumber = ComplexNumber.Zero;
            for (int i = 0; i < Coeficients.Count; i++)
            {
                ComplexNumber coeficientComplexNumber = Coeficients[i];
                ComplexNumber copyEvalComplexNumber = evalComplexNumberCopy;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        copyEvalComplexNumber  = copyEvalComplexNumber .Multiply(evalComplexNumberCopy);

                    coeficientComplexNumber = coeficientComplexNumber.Multiply(copyEvalComplexNumber );
                }

                zeroComplexNumber = zeroComplexNumber.Add(coeficientComplexNumber);
            }

            return zeroComplexNumber;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Coeficients.Count; i++)
            {
                output += Coeficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        output += "x";
                    }
                }
                output += " + ";
            }
            return output;
        }
    }
}
