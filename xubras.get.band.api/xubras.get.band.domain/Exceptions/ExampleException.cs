namespace xubras.get.band.domain.Exceptions
{
    using System;

    public class ExampleException : Exception
    {
        public ExampleException()
        {

        }

        public ExampleException(string mensagem)
            : base(mensagem)
        {

        }
    }
}
