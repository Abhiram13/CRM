using System;
using MongoDB.Driver;
using MongoDB.Bson;
using CRM;
using Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace System {
   /// <summary>
   /// Default status message for HttpResponse
   /// </summary>
	public sealed class StatusMessage {
		public static string Send(int status) {
			switch (status) {
				case 200: return "Success";
				case 201: return "Document successfully inserted";
            case 204: return "DataBase/ Collection is empty";
            case 302: return "Document has been found";
            case 304: return "Document has not been modified";
            case 400: return "Request body is not valid";
            case 401: return "You are Unauthorised to access this route";
            case 403: return "This route is Forbidden";
            case 404: return "Document do not exist";
            case 500: return "Server Error";
            default: return "";
			}
		}
	}

   /// <summary>
   /// Class that decodes, view and edit request body from user input
   /// </summary>
	public static class RequestBody {
		/// <summary>
		/// Returns document object by decoding http request
		/// </summary>
		/// <typeparam name="DocumentType">Type of document that need to be decoded</typeparam>
		/// <param name="request">Http Request</param>
		public static DocumentType Decode<DocumentType>(HttpRequest request) {
			return JSON.httpContextDeseriliser<DocumentType>(request).Result;
		}
	}

   /// <summary>
   /// List of Http Status Codes to define action performed with user request
   /// </summary>
	public sealed class StatusCode {
		public const int OK = 200;
		public const int Inserted = 201;
		public const int NoContent = 204;
		public const int DocumentFound = 302;
		public const int NotModified = 304;
		public const int BadRequest = 400;
		public const int Unauthorised = 401;
		public const int Forbidden = 403;
		public const int NotFound = 404;
		public const int ServerError = 500;
	}
}