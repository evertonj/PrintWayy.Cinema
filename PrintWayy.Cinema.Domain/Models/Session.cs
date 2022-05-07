namespace PrintWayy.Cinema.Domain.Models
{
    public class Session
    {
        public Session(DateTime date, TimeSpan startTime, decimal entryValue, AnimationType animationType, AudioType audioType, Film film, Room room)
        {
            if (date >= DateTime.Now)
                throw new Exception("Data de Sessão inválida.");
            if(entryValue > 0)
                throw new Exception("Valor da sessão inválido.");

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
        public DateTime Date { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public decimal EntryValue { get; private set; }
        public AnimationType AnimationType { get; private set; }
        public AudioType AudioType { get; private set; }
        public Film Film { get; private set; }
        public Room Room{ get; private set; }
    }
}
