namespace PrintWayy.Cinema.Domain.Models
{
    public class Film
    {
        public const string DeveConterUmCaminhoDeImagemValido = "Por favor forneça um caminho de imagem valido.";
        public const string DeveInformarTitulo = "O Titulo precisa ser informado.";
        public const string DeveInformarDescricao = "Por favor informe uma descrição.";
        public const string DeveInformarDuracao = "Por favor informe a duração.";
        public const string NaoDeveTerTituloRepetido = "Filmes não podem ter títulos repetidos.";
        public const string FilmeNaoEncontrado = "Filme não encontrado na base de dados.";
        public const string NaoRemoveFilmeVinculadoSessao = "Não é possivel remover o Filme que está vinculado a uma Sessão.";
        public const string DURATION_PATTERN = @"hh\:mm\:ss";

        public Film(string imagePath, string title, string description, TimeSpan duration)
        {
            if (!File.Exists(imagePath))
                throw new ArgumentException(DeveConterUmCaminhoDeImagemValido);
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException(DeveInformarTitulo);
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException(DeveInformarDescricao);
            if (duration.Ticks <= 0)
                throw new ArgumentException(DeveInformarDuracao);
            Id = Guid.NewGuid();
            ImagePath = imagePath;
            Title = title;
            Description = description;
            Duration = duration;
        }

        public Film(Guid id, string imagePath, string title, string description, TimeSpan duration):this(imagePath,title, description, duration)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public string ImagePath { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}