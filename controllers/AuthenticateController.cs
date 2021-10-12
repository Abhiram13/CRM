using System;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Services.Authentication;
using MongoDB.Driver;
using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Net.Http;

namespace Controllers {
   namespace Authentication {
      [Route("")]
      public class AuthenticationController : Controller {

         [HttpGet]
         public ResponseModel Home() {
				return new ResponseModel(System.StatusCode.Inserted, "");
			}

         [HttpPost]
         [Route("Login")]
         public ResponseModel login() {
				LoginRequest login = RequestBody.Decode<LoginRequest>(Request);
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {
					Collection = Table.employee,
					filter = Builders<Employee>.Filter.Eq("empid", login.empid),
				};
				ResponseModel response = new Login(document, login).Authenticate();
				CookieOptions options = new CookieOptions() {
				   SameSite = SameSiteMode.None,
				   Domain = "localhost",
				   Secure = true,               
				};
				Response.Headers.Add("Access-Control-Allow-Credentials", "true");
				Response.Cookies.Append("auth", response.Response, options);		

				return response;
			}

         [HttpGet]
         [Route("soc")]
			public async Task Sockets() {
				if (HttpContext.WebSockets.IsWebSocketRequest) {
					using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
					await Echo(HttpContext, webSocket);
				} else {
					HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				}
         }

			private async Task Echo(HttpContext context, WebSocket webSocket) {
				var buffer = new byte[1024 * 4];
				WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				while (!result.CloseStatus.HasValue) {
					await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
					result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				}
				await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
			}
      }
		
		[Route("sock")]
		public class WebSocketsController : ControllerBase {
			private readonly ILogger<WebSocketsController> _logger;

			public WebSocketsController(ILogger<WebSocketsController> logger) {
				_logger = logger;
			}

			[HttpGet("ws")]
			public async Task Get() {
				Console.WriteLine(HttpContext.WebSockets.IsWebSocketRequest);
				if (HttpContext.WebSockets.IsWebSocketRequest) {					
					using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
					_logger.Log(LogLevel.Information, "WebSocket connection established");
					await Echo(webSocket);
				} else {
					HttpContext.Response.StatusCode = 400;
				}
			}

			private async Task Echo(WebSocket webSocket) {
				var buffer = new byte[1024 * 4];
				var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				_logger.Log(LogLevel.Information, "Message received from Client");

				while (!result.CloseStatus.HasValue) {
					var serverMsg = Encoding.UTF8.GetBytes($"Server: Hello. You said: {Encoding.UTF8.GetString(buffer)}");
					await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
					_logger.Log(LogLevel.Information, "Message sent to Client");

					result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
					_logger.Log(LogLevel.Information, "Message received from Client");
				}
            
				await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
				_logger.Log(LogLevel.Information, "WebSocket connection closed");
			}
		}
   }
}