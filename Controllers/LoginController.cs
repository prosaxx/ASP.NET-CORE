
using Microsoft.AspNetCore.Mvc;


namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("V1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            //Recupera o usuario
            var user = userRepository.Get(model.Username, model.Password);

            // usuario existente
            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalidos" });

            // Gera Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            }
        };
    }
}
