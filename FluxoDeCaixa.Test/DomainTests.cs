using AutoMapper;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Test
{
    public class DomainTests
    {
        [Fact(DisplayName = "Test the inclusion of a new credit")]
        public void NewCredit()
        {
            var myProfile = new FluxoDeCaixa.Infra.Data.Mapper.ProfileConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var context = new FluxoDeCaixa.Infra.Data.Context.FluxoCaixaContext();
            var repositoryService = new FluxoDeCaixa.Infra.Data.Repositories.MovimentacaoRepository(mapper, context);
            var service = new FluxoDeCaixa.Domain.Services.MovimentacaoService(repositoryService);


            var mockLancaento = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                DataLancamento = new DateTime(2022, 11, 10),
                IdTipoLancamento = Enumeradores.ETipoLancamento.Credito,
                Valor = (decimal)150.34
            };

            var result = service.inserir(mockLancaento);
            Assert.False(result.HasErrors);
        }
        [Fact(DisplayName = "Test the inclusion of a new Debit")]
        public void NewDebit()
        {
            var myProfile = new FluxoDeCaixa.Infra.Data.Mapper.ProfileConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var context = new FluxoDeCaixa.Infra.Data.Context.FluxoCaixaContext();
            var repositoryService = new FluxoDeCaixa.Infra.Data.Repositories.MovimentacaoRepository(mapper, context);
            var service = new FluxoDeCaixa.Domain.Services.MovimentacaoService(repositoryService);


            var mockLancaento = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                DataLancamento = new DateTime(2022, 11, 10),
                IdTipoLancamento = Enumeradores.ETipoLancamento.Debito,
                Valor = (decimal)320.34
            };

            var result = service.inserir(mockLancaento);

            Assert.False(result.HasErrors);
        }

        [Fact(DisplayName = "Test the inclusion of a new Credit with an invalid amount")]
        public void NewCreditWithInvalidAmount()
        {
            var myProfile = new FluxoDeCaixa.Infra.Data.Mapper.ProfileConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var context = new FluxoDeCaixa.Infra.Data.Context.FluxoCaixaContext();
            var repositoryService = new FluxoDeCaixa.Infra.Data.Repositories.MovimentacaoRepository(mapper, context);
            var service = new FluxoDeCaixa.Domain.Services.MovimentacaoService(repositoryService);


            var mockLancaento = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                DataLancamento = new DateTime(2022, 11, 10),
                IdTipoLancamento = Enumeradores.ETipoLancamento.Credito,
                Valor = 0
            };

            var result = service.inserir(mockLancaento);

            Assert.True(result.HasErrors);
        }

        [Fact(DisplayName = "Test the balance of the day")]
        public void BalancePerDayTest()
        {
            var myProfile = new FluxoDeCaixa.Infra.Data.Mapper.ProfileConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var context = new FluxoDeCaixa.Infra.Data.Context.FluxoCaixaContext();
            var repositoryService = new FluxoDeCaixa.Infra.Data.Repositories.MovimentacaoRepository(mapper, context);
            var service = new FluxoDeCaixa.Domain.Services.MovimentacaoService(repositoryService);
            var consolidadoRepository = new FluxoDeCaixa.Infra.Data.Repositories.ConsolidadoRepository(context, mapper);
            var serviceCalculo = new FluxoDeCaixa.Domain.Services.ConsolidadoService(consolidadoRepository);
            var serviceConsulta = new FluxoDeCaixa.Domain.Services.ConsultaConsolidadoService(consolidadoRepository);

            var dt10 = new DateTime(2022, 11, 10);
            var dt09 = new DateTime(2022, 11, 09);

            var mockLancaentoCreditoDia10 = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                DataLancamento = dt10,
                IdTipoLancamento = Enumeradores.ETipoLancamento.Credito,
                Valor = (decimal)1349
            };

            var mockLancaentoDebitoDia10 = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                DataLancamento = dt10,
                IdTipoLancamento = Enumeradores.ETipoLancamento.Debito,
                Valor = 349
            };

            var mockLancaentoCreditoDia09 = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                DataLancamento = dt09,
                IdTipoLancamento = Enumeradores.ETipoLancamento.Credito,
                Valor = (decimal)784
            };

            var mockLancaentoDebitoDia09 = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                DataLancamento = dt09,
                IdTipoLancamento = Enumeradores.ETipoLancamento.Debito,
                Valor = 80
            };

            service.inserir(mockLancaentoCreditoDia10);
            service.inserir(mockLancaentoDebitoDia10);
            service.inserir(mockLancaentoCreditoDia09);
            service.inserir(mockLancaentoDebitoDia09);

            serviceCalculo.ExecutarConsolidacao().Wait();

            var result = serviceConsulta.GetConsolidado(2022, 11).Response;

            var valConsolidadoDia10 = result.First(x => x.Data.Date == dt10.Date).ValorCosolidado;
            var valConsolidadoDia09 = result.First(x => x.Data.Date == dt09.Date).ValorCosolidado;

            Assert.True(valConsolidadoDia09 == 704 && valConsolidadoDia10 == 1000);
        }
    }
}