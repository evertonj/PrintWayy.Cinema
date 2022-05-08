using FluentAssertions;
using LiteDB;
using PrintWayy.Cinema.Domain.Commands.Requests.Session;
using PrintWayy.Cinema.Domain.Handlers;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Infra.Data;
using System;
using Xunit;

namespace PrintWayy.Cinema.Domain.Test
{
    public class SessionHandlerTest
    {
        private ISessionRepository sessionRepository;
        private IFilmRepository filmRepository;
        private SessionHandler sessionHandler;
        private CreateSessionRequest createSession = ObjectMother.CreateSessionRequestObject;

        public SessionHandlerTest()
        {
            sessionRepository = new SessionRepository(null);
            filmRepository = new FilmRepository(null);
            sessionHandler = new SessionHandler(sessionRepository,filmRepository);
        }

        [Fact]
        public void DataSessaoInvalida()
        {
            //arrange
            createSession.Date = DateTime.Now.AddDays(-2);
            var film = ObjectMother.FilmObject;
            filmRepository.Update(film);
            createSession.FilmId = film.Id;
            //act
            var result = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Session.DataSessaoInvalida);
        }


        [Fact]
        public void ValorSessaoInvalido()
        {
            //arrange
            createSession.EntryValue = new decimal(-1);
            var film = ObjectMother.FilmObject;
            filmRepository.Update(film);
            createSession.FilmId = film.Id;
            //act
            var result = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Session.ValorSessaoInvalido);
        }

        [Fact]
        public void SessaoCriadaComSucesso()
        {
            //arrange
            var film = ObjectMother.FilmObject;
            filmRepository.Update(film);
            createSession.FilmId = film.Id;
            //act
            var result = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            var session = sessionRepository.FindById(result.Id);
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Date.Should().Be(createSession.Date);
            result.StartTime.Should().Be(createSession.StartTime);
            result.EndTime.Should().Be(session.EndTime);
            result.Film.Id.Should().Be(createSession.FilmId);
            result.EntryValue.Should().Be(createSession.EntryValue);
            result.AudioType.Should().Be(createSession.AudioType);
            result.AnimationType.Should().Be(createSession.AnimationType);
            result.Room.Name.Should().Be(createSession.RoomName);
        }

        [Fact]
        public void SalaVinculadaSessaoMesmoHorario()
        {
            //arrange
            SessaoCriadaComSucesso();
            var film = ObjectMother.FilmObject;
            filmRepository.Update(film);
            createSession.FilmId = film.Id;
            //act
            var result = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Session.SalaVinculadaSessaoMesmoHorario);
        }

        [Fact]
        public void SessaoNaoEncontadaNaBaseDeDados()
        {
            //arrange
            var deleteSessionRequest = new DeleteSessionRequest { Id = Guid.NewGuid() };
            //act
            var result = sessionHandler.Handle(deleteSessionRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Session.SessaoNaoEncontadaNaBaseDeDados);
        }

        [Fact]
        public void SesssaoNaoPodeSerRemovidaSeFaltar10DiasOuMenos()
        {
            //arrange
            var film = ObjectMother.FilmObject;
            filmRepository.Update(film);
            createSession.FilmId = film.Id;
            var response = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            var deleteSessionRequest = new DeleteSessionRequest { Id = response.Id };
            //act
            var result = sessionHandler.Handle(deleteSessionRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Session.SesssaoNaoPodeSerRemovidaSeFaltar10DiasOuMenos);
        }

        [Fact]
        public void SesssaoRemovidaComSucesso()
        {
            //arrange
            createSession.Date = DateTime.Now.AddDays(15);
            var film = ObjectMother.FilmObject;
            filmRepository.Update(film);
            createSession.FilmId = film.Id;
            var response = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            var deleteSessionRequest = new DeleteSessionRequest { Id = response.Id };
            //act
            var result = sessionHandler.Handle(deleteSessionRequest, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Success.Should().BeTrue();
        }
    }
}
