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
        private ILiteDatabase db;
        private ISessionRepository sessionRepository;
        private SessionHandler sessionHandler;
        private CreateSessionRequest createSession = ObjectMother.CreateSessionRequestObject;

        public SessionHandlerTest()
        {
            db = new LiteDatabase(":memory:");
            sessionRepository = new SessionRepository(db);
            sessionHandler = new SessionHandler(sessionRepository);
        }

        [Fact]
        public void DataSessaoInvalida()
        {
            //arrange
            createSession.Date = DateTime.Now.AddDays(-2);
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
            //act
            var result = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().Be(Session.ValorSessaoInvalido);
        }

        [Fact]
        public void SessaoCriadaComSucesso()
        {
            //arrange
            var result = sessionHandler.Handle(createSession, new System.Threading.CancellationToken()).Result;
            //assert
            result.ErrorMessage.Should().BeNull();
            result.Date.Should().Be(createSession.Date);
            result.StartTime.Should().Be(createSession.StartTime);
            result.EndTime.Should().Be(createSession.StartTime.Add(createSession.Film.Duration));
            result.Film.Id.Should().Be(createSession.Film.Id);
            result.EntryValue.Should().Be(createSession.EntryValue);
            result.AudioType.Should().Be(createSession.AudioType);
            result.AnimationType.Should().Be(createSession.AnimationType);
            result.Room.Should().Be(createSession.Room);
        }

        [Fact]
        public void SalaVinculadaSessaoMesmoHorario()
        {
            //arrange
            SessaoCriadaComSucesso();
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
