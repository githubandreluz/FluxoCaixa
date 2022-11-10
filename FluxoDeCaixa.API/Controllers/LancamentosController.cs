using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoDeCaixa.API.Controllers
{
    public class LancamentosController : CustomBaseController
    {
        private readonly IMovimentacaoAppService _movimentacaoAppService;
        ///<sumary>
        ///Criação de novos lançamentos
        ///</sumary>
        public LancamentosController(IMovimentacaoAppService movimentacaoAppService)
        {
            _movimentacaoAppService = movimentacaoAppService;
        }

        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Cria o novo lançamento",
            OperationId = "CriarLancamento"
        )]
        [SwaggerResponse(200, "Lançamento realizado")]
        [SwaggerResponse(401, "Não autorizado")]
        [HttpPost]
        [Route("CriarLancamento")]
        public IActionResult CriarLancamento(MovimentacaoViewModel movimentacao) =>
            returnToHttp(_movimentacaoAppService.inserir(movimentacao));

    }
}
