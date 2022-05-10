namespace PrintWayy.Cinema.Domain.Models
{
    public class Film
    {
        public const string DeveConterUmaImageNoFormatoStringBase64 = "Por favor forneça uma imagem no formato base64.";
        public const string DeveInformarTitulo = "O Título precisa ser informado.";
        public const string DeveInformarDescricao = "Por favor informe uma descrição.";
        public const string DeveInformarDuracao = "Por favor informe a duração.";
        public const string NaoDeveTerTituloRepetido = "Filmes não podem ter títulos repetidos.";
        public const string FilmeNaoEncontrado = "Filme não encontrado na base de dados.";
        public const string NaoRemoveFilmeVinculadoSessao = "Não é possivel remover o Filme que está vinculado a uma Sessão.";
        public const string DURATION_PATTERN = @"hh\:mm\:ss";

        public Film() { }
        public Film(ImageData imageBase64, string title, string description, TimeSpan duration)
        {
            Id = Guid.NewGuid();
            ImageBase64 = imageBase64;
            Title = title;
            Description = description;
            Duration = duration;
        }

        public Film(Guid id, ImageData imageBase64, string title, string description, TimeSpan duration) : this(imageBase64, title, description, duration)
        {
            Id = id;
        }

        
        public Guid Id { get; private set; }

        private ImageData _imageData;
        public ImageData ImageBase64
        {
            get { return _imageData; }
            private set
            {
                if (Convert.FromBase64String(value?.ImageBase64) == null)
                    throw new ArgumentException(DeveConterUmaImageNoFormatoStringBase64);
                _imageData = value;
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(DeveInformarTitulo);
                _title = value;
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(DeveInformarDescricao);
                _description = value;
            }
        }
        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get { return _duration; }
            private set
            {
                if (value.Ticks <= 0)
                    throw new ArgumentException(DeveInformarDuracao);
                _duration = value;
            }
        }
    }
}