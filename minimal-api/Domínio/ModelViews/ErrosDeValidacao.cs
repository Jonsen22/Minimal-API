namespace minimal_api.Domínio.ModelViews
{
    public struct ErrosDeValidacao
    {
        public ErrosDeValidacao()
        {
            Mensagens = new List<string>();
        }

        public List<string> Mensagens { get; set; } 
    }
}
