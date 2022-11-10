namespace FluxoDeCaixa.Domain.Model
{
    public class ResponseData<T>
    {
        private List<string> ListOfErrors { get; set; }
        public T Response { get; set; }
        public ResponseData()
        {
            ListOfErrors = new List<string>();
        }
        public ResponseData<T> addError(string erro)
        {
            if (!ListOfErrors.Any(x => x.ToLower().Equals(erro.ToLower())))
                ListOfErrors.Add(erro);
            return this;
        }
        public ResponseData<T> addError(List<string> erros)
        {
            foreach (var err in erros)
                addError(err);

            return this;
        }
        public bool HasErrors
        {
            get
            {
                return ListOfErrors != null && ListOfErrors.Count > 0;
            }
        }
        public List<string> AllErrors
        {
            get
            {
                return ListOfErrors;
            }
        }
    }
    public class ResponseData : ResponseData<object>
    {

    }
}
