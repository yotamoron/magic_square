using Ninject;
using System;
using System.Reflection;

namespace MagicSquare
{
    public class Program
    {
        static void Main(string[] args)
        {
            StandardKernel kernel = GetKernel();            
            MagicSquare magicSquare = kernel.Get<MagicSquare>();

            magicSquare.Play();
        }

        private static StandardKernel GetKernel()
        {
            StandardKernel kernel = new StandardKernel();
            kernel.Load("*.dll");

            return kernel;
        }
    }
}
