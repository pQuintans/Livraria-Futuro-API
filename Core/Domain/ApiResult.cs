﻿using LivrariaFuturo.Authentication.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaFuturo.Authorization.Core.Domain
{
    /// <summary>
    /// Resultado padrão para todas as rotas da API
    /// </summary>
    public class ApiResult : IActionResult
    {
        private readonly Saida _saida;

        public ApiResult(Saida saida)
        {
            _saida = saida;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(_saida)
            {
                StatusCode = _saida.StatusCode
            };

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
