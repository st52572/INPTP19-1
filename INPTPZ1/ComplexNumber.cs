namespace INPTPZ1
{
    class ComplexNumber
    {
        
            public double RealPart { get; set; }
            public double ImaginaryPart { get; set; }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                RealPart = 0,
                ImaginaryPart = 0
            };

            public ComplexNumber Multiply(ComplexNumber otherComplexNumber)
            {
                return new ComplexNumber()
                {
                    RealPart = this.RealPart * otherComplexNumber.RealPart - this.ImaginaryPart * otherComplexNumber.ImaginaryPart,
                    ImaginaryPart = this.RealPart * otherComplexNumber.ImaginaryPart + this.ImaginaryPart * otherComplexNumber.RealPart
                };
            }

            public ComplexNumber Add(ComplexNumber otherComplexNumber)
            {
                return new ComplexNumber()
                {
                    RealPart = this.RealPart + otherComplexNumber.RealPart,
                    ImaginaryPart = this.ImaginaryPart + otherComplexNumber.ImaginaryPart
                };
            }
            public ComplexNumber Subtract(ComplexNumber otherComplexNumber)
            {
                return new ComplexNumber()
                {
                    RealPart = this.RealPart - otherComplexNumber.RealPart,
                    ImaginaryPart = this.ImaginaryPart - otherComplexNumber.ImaginaryPart
                };
            }

            public override string ToString()
            {
                return $"({RealPart} + {ImaginaryPart}i)";
            }

            internal ComplexNumber Divide(ComplexNumber otherComplexNumber)
            {
                ComplexNumber dividedComplexNumber = this.Multiply(new ComplexNumber() { RealPart = otherComplexNumber.RealPart, ImaginaryPart = -otherComplexNumber.ImaginaryPart });
                double divisor = otherComplexNumber.RealPart * otherComplexNumber.RealPart + otherComplexNumber.ImaginaryPart * otherComplexNumber.ImaginaryPart;

                return new ComplexNumber()
                {
                    RealPart = dividedComplexNumber.RealPart / divisor,
                    ImaginaryPart = dividedComplexNumber.ImaginaryPart / divisor
                };
            }
        
    }
}
