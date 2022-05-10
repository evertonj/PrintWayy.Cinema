namespace PrintWayy.Cinema.Domain.Models
{
    public class Session
    {
        public const string DataSessaoInvalida = "Data de Sessão inválida.";
        public const string ValorSessaoInvalido = "Valor da sessão inválido.";
        public const string SalaVinculadaSessaoMesmoHorario = "A Sala esta vinculada a uma sessão neste horário selecionado.";
        public const string SessaoNaoEncontadaNaBaseDeDados = "Sessão não encontrada na base de dados.";
        public const string SesssaoNaoPodeSerRemovidaSeFaltar10DiasOuMenos = "A sessão só pode ser removida se faltar 10 dias ou mais para que ela ocorra.";

        public Session()
        {
        }

        public Session(DateTime date, TimeSpan startTime, decimal entryValue, AnimationType animationType, AudioType audioType, Film film, Room room)
        {
            Id = Guid.NewGuid();
            Date = date;
            StartTime = startTime;
            Film = film;
            EndTime = startTime.Add(film.Duration);
            EntryValue = entryValue;
            AnimationType = animationType;
            AudioType = audioType;
            Room = room;
        }

        public Guid Id { get; private set; }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            private set
            {
                if (value <= DateTime.Now)
                    throw new Exception(DataSessaoInvalida);
                _date = value;
            }
        }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        private decimal _entryValue;
        public decimal EntryValue
        {
            get { return _entryValue; }
            private set
            {
                if (value < 1)
                    throw new Exception(ValorSessaoInvalido);
                _entryValue = value;
            }
        }
        public AnimationType AnimationType { get; private set; }
        public AudioType AudioType { get; private set; }
        public Film Film { get; private set; }
        public Room Room { get; private set; }
    }
}
