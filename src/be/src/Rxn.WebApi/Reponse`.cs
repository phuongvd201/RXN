namespace Rxn.WebApi
{
    public class Response<T> : Response
    {
        public T Result { get; set; }
    }
}