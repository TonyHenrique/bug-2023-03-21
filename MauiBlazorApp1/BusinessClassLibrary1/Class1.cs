using Flurl.Http;

namespace BusinessClassLibrary1
{
    public static class Class1
    {
        public enum Ambiente
        {
            Normal,
            Hibrido
        }
        public static Ambiente AmbienteAtual { get; set; }

        public static async Task<Ambiente> ObtemDiretiva()
        {
            return AmbienteAtual;
        }

        //public static BuscaAgendamento()
        //{
        // 
        //return;
        //}

        public static async Task<Mensagem[]> BuscaProdutores()
        {
            return null;
        }
    }


    public class Mensagem
    {
        public int Numero { get; set; }
        public DateTime DataHora { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Lingua { get; set; }
        public int StatusPublicacao { get; set; }
        public Multimidiadamensagem[] MultimidiaDaMensagem { get; set; }
        public string Local { get; set; }
    }

    public class Multimidiadamensagem
    {
        public int Tipo { get; set; }
        public string URL { get; set; }
    }

}