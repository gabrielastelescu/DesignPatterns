using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    // “Clients should not be forced to depend upon interfaces that they do not use.”
    public class InterfaceSegregrationPrinciple
    {
        public class Document
        {
        }

        public interface IMachine
        {
            public void Print(Document d);
            public void Scan(Document d);
            public void Fax(Document d);
        }

        public class MultifunctionPrinter : IMachine
        {
            public void Fax(Document d)
            {
                throw new NotImplementedException();
            }

            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        // Breaking the principle...wrong!! The old printer doesn't know fax and scan, therefore methods not needed
        public class OldFashinedPrinter : IMachine
        {
            void IMachine.Fax(Document d)
            {
            }

            void IMachine.Print(Document d)
            {
            }

            void IMachine.Scan(Document d)
            {
            }
        }

        // Solution
        public interface IFax
        {
            void Fax(Document d);
        }
        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }
        public class Printer : IPrinter
        {
            public void Print(Document d)
            {

            }
        }

        public class PhotoCopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
            }

            public void Scan(Document d)
            {
            }
        }

        public interface IMultifunctionDevice : IScanner, IPrinter
        {
        }

        public class MultiFunctionMachine : IMultifunctionDevice
        {
            private IPrinter printer;
            private IScanner scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                if (printer == null)
                {
                    throw new ArgumentNullException(paramName: nameof(printer));
                }
                if (scanner == null)
                {
                    throw new ArgumentNullException(paramName: nameof(scanner));
                }
                this.printer = printer;
                this.scanner = scanner;

            }

            public void Print(Document d)
            {
                printer.Print(d);
            }

            public void Scan(Document d)
            {
                scanner.Scan(d);
            }
        }
    }
}
