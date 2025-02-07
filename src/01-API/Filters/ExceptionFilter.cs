using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PosTech.Hackathon.Pacientes.Domain.Exceptions;
using PosTech.Hackathon.Pacientes.Domain.Responses;
using System.Net;

namespace PosTech.Hackathon.Pacientes.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string message = "Ocorreu uma falha inesperada no servidor";

        if (context.Exception is DomainException || context.Exception.InnerException is DomainException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            message = context.Exception.InnerException == null ? context.Exception.Message : context.Exception.InnerException.Message;
        }
        else if (context.Exception is PersistencyException) 
        {
            statusCode = (int)HttpStatusCode.Conflict;
            message = context.Exception.Message;
        }

        var response = context.HttpContext.Response;
        response.StatusCode = statusCode;
        response.ContentType = "application/json";
        context.Result = new JsonResult(new DefaultOutput<Exception>(false, message));
    }
}
