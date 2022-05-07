using FluentAssertions;
using LiteDB;
using PrintWayy.Cinema.Domain.Commands.Requests.Film;
using PrintWayy.Cinema.Domain.Commands.Requests.Session;
using PrintWayy.Cinema.Domain.Handlers;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Infra.Data;
using System;
using Xunit;

namespace PrintWayy.Cinema.Domain.Test
{
    public class FilmHandlerTest
    {
        private ILiteDatabase db;
        private IFilmRepository filmRepository;
        private ISessionRepository sessionRepository;
        private FilmHandler filmHandler;
        private SessionHandler sessionHandler;
        private CreateSessionRequest createSession = ObjectMother.CreateSessionRequestObject;
        public FilmHandlerTest()
        {
            db = new LiteDatabase(":memory:");
            filmRepository = new FilmRepository(db);
            sessionRepository = new SessionRepository(db);
            filmHandler = new FilmHandler(filmRepository, sessionRepository);
            sessionHandler = new SessionHandler(sessionRepository);
        }

        [Fact]
        public void DeveConterUmCaminhoDeImagemValido()
        {
            //arrange
            var createRequest = new CreateFilmRequest();
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveConterUmCaminhoDeImagemValido);
        }

        [Fact]
        public void DeveInformarTitulo()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png" };
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarTitulo);
        }

        [Fact]
        public void DeveInformarDescricao()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação" };
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDescricao);
        }

        [Fact]
        public void DeveInformarDuracao()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação", Description = "A História do pai da informática." };
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDuracao);
        }

        [Fact]
        public void DeveCadastrarComSucesso()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Id.Should().NotBe(Guid.Empty);
            result.ImagePath.Should().Be(createRequest.ImagePath);
            result.Title.Should().Be(createRequest.Title);
            result.Description.Should().Be(createRequest.Description);
            result.Duration.Should().Be(createRequest.Duration);
        }

        [Fact]
        public void NaoDeveTerTituloRepetido()
        {
            //arrange
            DeveCadastrarComSucesso();
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.NaoDeveTerTituloRepetido);
        }

        [Fact]
        public void FilmeNaoEncontrado()
        {
            //arrange
            var deleteRequest = new DeleteFilmRequest() { Id = Guid.NewGuid() };
            //act
            var result = filmHandler.Handle(deleteRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.FilmeNaoEncontrado);
        }

        [Fact]
        public void NaoRemoveFilmeVinculadoSessao()
        {
            //arrange
            createSession.Film = filmRepository.Create(createSession.Film);
            var response = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            var deleteRequest = new DeleteFilmRequest() { Id = response.Film.Id };
            //act
            var result = filmHandler.Handle(deleteRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.NaoRemoveFilmeVinculadoSessao);
        }

        [Fact]
        public void FilmeRemovidoSucesso()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var deleteRequest = new DeleteFilmRequest() { Id = createResponse.Id };
            //act
            var result = filmHandler.Handle(deleteRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public void UpdateDeveConterUmCaminhoDeImagemValido()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImagePath = "naoTem.png", Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveConterUmCaminhoDeImagemValido);
        }

        [Fact]
        public void UpdateDeveInformarTitulo()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImagePath = "great-place-to-work-pw.png", Title = "", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarTitulo);
        }

        [Fact]
        public void UpdateDeveInformarDescricao()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "", Duration = new System.TimeSpan(1, 54, 0) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDescricao);
        }

        [Fact]
        public void UpdateDeveInformarDuracao()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "Conhecer a história do pai da informática é essencial" };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDuracao);
        }

        [Fact]
        public void UpdateFilmeSucesso()
        {
            //arrange
            var createRequest = new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0) };
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação 3", Description = "Conhecer a história do pai da informática é essencial", Duration = new TimeSpan(1, 58, 22) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Id.Should().NotBe(updateRequest.Id);
            result.ImagePath.Should().Be(updateRequest.ImagePath);
            result.Title.Should().Be(updateRequest.Title);
            result.Description.Should().Be(updateRequest.Description);
            result.Duration.Should().Be(updateRequest.Duration);
        }
    }
}