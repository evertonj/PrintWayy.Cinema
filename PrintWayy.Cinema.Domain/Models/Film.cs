namespace PrintWayy.Cinema.Domain.Models
{
    public class Film
    {
        public Film(string imagePath, string title, string description, TimeSpan duration)
        {
            if (!File.Exists(imagePath))
                throw new ArgumentNullException("Por favor forneça um caminho de imagem valido.");
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("O Titulo precisa ser informado.");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("Por favor informe uma descrição.");
            if (duration.Seconds > 0)
                throw new ArgumentException("Por favor informe a duração.");
            Id = Guid.NewGuid();
            ImagePath = imagePath;
            Title = title;
            Description = description;
            Duration = duration;
        }

        public Guid Id { get; private set; }
        public string ImagePath { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}