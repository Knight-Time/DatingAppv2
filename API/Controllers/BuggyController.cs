using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exception = System.Exception;

namespace API.Controllers;

public class BuggyController(DataContext context):BaseApiController
{

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secret message";
    }
    
    
    
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = context.Users.Find(-1);
        if (thing == null) return NotFound();
        
        return thing;
    }
    
    
    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        var thing = context.Users.Find(-1) ?? throw new Exception("fuck");
        
        
        return thing;
    }
    
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("bad request");
    }
}