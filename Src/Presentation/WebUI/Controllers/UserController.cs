using Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
   
    public class UserController : CustomBaseController
    {
        #region Post Apis
        /// <summary>
        /// Sign up  user to be able to use the app with <paramref name="command"/> values
        /// </summary>
        /// <param name="command">Some required information  to sign you up</param>
        /// <returns> if model <paramref name="command"/> values is valid so return  NoContentCode=204 http status code </returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///      "name": "ali",
        ///      "lastName": "mohammadzadeh",
        ///      "email": "ali@example.com",
        ///      "birthDay": "1993-06-24T10:55:56.228Z",
        ///      "password": "123456",
        ///      "confirmPassword": "123456",
        ///      "aboutMe": "I'm a good person",
        ///     }
        /// </remarks>
        /// <response code="200">return succeed message and user Id</response>
        /// <response code="400">return error message for request model</response>
        /// <response code="500">return internal server error </response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SignUp([FromBody]CreateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        #endregion
    }
}
