using AutoMapper;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Test
{

    public class IntegrationTest
    {
        [Fact(DisplayName = "Test the inclusion of a new credit passing through application layer")]
        public void NewCreditInApplicationLayer()
        {
            var myProfileApp = new FluxoDeCaixa.Application.Mapper.ProfileConfig();
            var myProfileInfra = new FluxoDeCaixa.Infra.Data.Mapper.ProfileConfig();
            var configurationApp = new MapperConfiguration(cfg => cfg.AddProfile(myProfileApp));
            var configurationInfra = new MapperConfiguration(cfg => cfg.AddProfile(myProfileInfra));
            var mapperApp = new Mapper(configurationApp);
            var mapperInfra = new Mapper(configurationInfra);

            var context = new FluxoDeCaixa.Infra.Data.Context.FluxoCaixaContext();
            var repositoryService = new FluxoDeCaixa.Infra.Data.Repositories.MovimentacaoRepository(mapperInfra, context);
            var serviceMovimentacao = new FluxoDeCaixa.Domain.Services.MovimentacaoService(repositoryService);
            var appService = new FluxoDeCaixa.Application.AppServices.MovimentacaoAppService(serviceMovimentacao, mapperApp);

            var mockLancaento = new MovimentacaoViewModel
            {
                DataLancamento = new DateTime(2022, 11, 10),
                IdTipoLancamento = Enumeradores.ETipoLancamento.Credito,
                Valor = (decimal)150.34
            };

            var result = appService.inserir(mockLancaento);
            Assert.False(result.HasErrors);
        }
        [Fact(DisplayName = "Test the inclusion of a new debit passing through application layer")]
        public void NewDebitInApplicationLayer()
        {
            var myProfileApp = new FluxoDeCaixa.Application.Mapper.ProfileConfig();
            var myProfileInfra = new FluxoDeCaixa.Infra.Data.Mapper.ProfileConfig();
            var configurationApp = new MapperConfiguration(cfg => cfg.AddProfile(myProfileApp));
            var configurationInfra = new MapperConfiguration(cfg => cfg.AddProfile(myProfileInfra));
            var mapperApp = new Mapper(configurationApp);
            var mapperInfra = new Mapper(configurationInfra);

            var context = new FluxoDeCaixa.Infra.Data.Context.FluxoCaixaContext();
            var repositoryService = new FluxoDeCaixa.Infra.Data.Repositories.MovimentacaoRepository(mapperInfra, context);
            var serviceMovimentacao = new FluxoDeCaixa.Domain.Services.MovimentacaoService(repositoryService);
            var appService = new FluxoDeCaixa.Application.AppServices.MovimentacaoAppService(serviceMovimentacao, mapperApp);

            var mockLancaento = new MovimentacaoViewModel
            {
                DataLancamento = new DateTime(2022, 11, 10),
                IdTipoLancamento = Enumeradores.ETipoLancamento.Debito,
                Valor = 430
            };

            var result = appService.inserir(mockLancaento);
            Assert.False(result.HasErrors);
        }
    }
}
