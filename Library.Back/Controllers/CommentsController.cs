using Library.Core.Models;
using Library.Repositories.Database.Entities;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("book/{bookId}")]
    public async Task<IActionResult> GetCommentsByBookId(int bookId)
    {
        var comments = await _commentService.GetByBookIdAsync(bookId);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(AddCommentRequest comment)
    {
        await _commentService.AddAsync(comment);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteAsync(id);
        return Ok();
    }
}
