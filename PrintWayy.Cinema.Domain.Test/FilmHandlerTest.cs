using FluentAssertions;
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
        private IFilmRepository filmRepository;
        private ISessionRepository sessionRepository;
        private FilmHandler filmHandler;
        private SessionHandler sessionHandler;
        private CreateSessionRequest createSession = ObjectMother.CreateSessionRequestObject;
        private CreateFilmRequest createFilm = ObjectMother.CreateFilmRequestObject;
        public FilmHandlerTest()
        {
            filmRepository = new FilmRepository(null);
            sessionRepository = new SessionRepository(null);
            filmHandler = new FilmHandler(filmRepository, sessionRepository);
            sessionHandler = new SessionHandler(sessionRepository, filmRepository);
        }

        [Fact]
        public void DeveConterUmCaminhoDeImagemValido()
        {
            //arrange
            var createRequest = createFilm;
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveConterUmaImageNoFormatoStringBase64);
        }

        [Fact]
        public void DeveInformarTitulo()
        {
            //arrange
            var createRequest = createFilm;
            createFilm.Title = string.Empty;
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarTitulo);
        }

        [Fact]
        public void DeveInformarDescricao()
        {
            //arrange
            var createRequest = createFilm;
            createFilm.Description = string.Empty;
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDescricao);
        }

        [Fact]
        public void DeveInformarDuracao()
        {
            //arrange
            var createRequest = createFilm;
            createRequest.Duration = "00:00:00";
            //act
            var result = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDuracao);
        }

        [Fact]
        public void DeveCadastrarComSucesso()
        {
            //act
            var result = filmHandler.Handle(createFilm, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Id.Should().NotBe(Guid.Empty);
            result.ImageBase64.Should().Be(createFilm.ImageBase64);
            result.Title.Should().Be(createFilm.Title);
            result.Description.Should().Be(createFilm.Description);
            result.Duration.Should().Be(createFilm.Duration);
        }

        [Fact]
        public void NaoDeveTerTituloRepetido()
        {
            //arrange
            DeveCadastrarComSucesso();
            var createRequest = createFilm;
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
            createSession.FilmId = filmRepository.Create(ObjectMother.FilmObject).Id;
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
            var createRequest = createFilm;
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var deleteRequest = new DeleteFilmRequest() { Id = createResponse.Id };
            //act
            var result = filmHandler.Handle(deleteRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public void DeveConterUmaImageNoFormatoStringBase64()
        {
            //arrange
            var createRequest = createFilm;
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImageBase64 = new ImageData(), Title = "O jogo da imitação 2", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0).ToString(Film.DURATION_PATTERN) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveConterUmaImageNoFormatoStringBase64);
        }

        [Fact]
        public void UpdateDeveInformarTitulo()
        {
            //arrange
            var createRequest = createFilm;
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImageBase64 = ObjectMother.ImageDataObject, Title = "", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0).ToString(Film.DURATION_PATTERN) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarTitulo);
        }

        [Fact]
        public void UpdateDeveInformarDescricao()
        {
            //arrange
            var createRequest = createFilm;
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImageBase64 = ObjectMother.ImageDataObject, Title = "O jogo da imitação 2", Description = "", Duration = new System.TimeSpan(1, 54, 0).ToString(Film.DURATION_PATTERN) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDescricao);
        }

        [Fact]
        public void UpdateDeveInformarDuracao()
        {
            //arrange
            var createRequest = createFilm; 
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImageBase64 = ObjectMother.ImageDataObject, Title = "O jogo da imitação 2", Description = "Conhecer a história do pai da informática é essencial" };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Film.DeveInformarDuracao);
        }

        [Fact]
        public void UpdateFilmeSucesso()
        {
            //arrange
            var createRequest = createFilm; 
            var createResponse = filmHandler.Handle(createRequest, new System.Threading.CancellationToken()).Result;
            var updateRequest = new UpdateFilmRequest() { Id = createResponse.Id, ImageBase64 = ObjectMother.ImageDataObject, Title = "O jogo da imitação 3", Description = "Conhecer a história do pai da informática é essencial", Duration = new TimeSpan(1, 58, 22).ToString(Film.DURATION_PATTERN) };
            //act
            var result = filmHandler.Handle(updateRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Id.Should().Be(updateRequest.Id);
            result.ImageBase64.Should().Be(updateRequest.ImageBase64);
            result.Title.Should().Be(updateRequest.Title);
            result.Description.Should().Be(updateRequest.Description);
            result.Duration.Should().Be(updateRequest.Duration);
        }
    }
}