using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoDeCaixa.API.Controllers
{
    public class ConsultaConsolidadoController : CustomBaseController
    {
        private readonly IConsultaConsolidadoAppService _consultaConsolidadoAppService;
        ///<sumary>
        ///Consutla de consolidados
        ///</sumary>
        public ConsultaConsolidadoController(IConsultaConsolidadoAppService consultaConsolidadoAppService)
        {
            _consultaConsolidadoAppService = consultaConsolidadoAppService;
        }
        ///<sumary>
        ///Retorna o consolidado do filtro passado via parâmetro
        ///</sumary>
        ///<param name="ano">Ano no formtado YYYY para realizar a consulta</param>
        ///<param name="mes">Mes para realizar a consulta valores de 1 a 12</param>
        [Authorize(Roles = "Admin,Funcionario")]
        [SwaggerResponse(200, "Consulta realizada com sucesso", typeof(List<ConsolidadoModel>))]
        [SwaggerResponse(401, "Não autorizado")]
        [HttpGet]
        [Route("ConsultarConsolidadoPorAnoMes/{ano}/{mes}")]
        public IActionResult ConsultarConsolidadoPorAnoMes(int ano, int mes) =>
            returnToHttp(_consultaConsolidadoAppService.GetConsolidado(ano, mes));

        ///<sumary>
        ///Retorna o consolidado do filtro passado via parâmetro
        ///</sumary>
        ///<param name="ano">Ano no formato YYYY para realizar a consulta</param>
        [Authorize(Roles = "Admin,Funcionario")]
        [SwaggerResponse(200, "Consulta realizada com sucesso", typeof(List<ConsolidadoModel>))]
        [SwaggerResponse(401, "Não autorizado")]
        [HttpGet]
        [Route("ConsultarConsolidadoPorAno/{ano}")]
        public IActionResult ConsultarConsolidadoPorAno(int ano) =>
            returnToHttp(_consultaConsolidadoAppService.GetConsolidado(ano));

    }
}
