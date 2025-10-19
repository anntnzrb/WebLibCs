using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebLibCs.Core.DTOs;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Web.ViewModels.Home;
using AutoMapper;

namespace WebLibCs.Web.Controllers;

public class HomeController(
    ILibroRepository libroRepository,
    IAutorRepository autorRepository,
    IMapper mapper) : Controller
{
    private readonly ILibroRepository _libroRepository = libroRepository;
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Get latest 6 books
        var latestBooks = await _libroRepository.GetLatestBooksAsync(6);
        var latestBooksDto = _mapper.Map<IEnumerable<LibroDto>>(latestBooks);

        // Get featured authors (all authors for now, could be filtered)
        var featuredAuthors = await _autorRepository.GetAllAsync();
        var featuredAuthorsDto = _mapper.Map<IEnumerable<AutorDto>>(featuredAuthors.Take(6));

        var viewModel = new HomeIndexViewModel
        {
            LatestBooks = latestBooksDto,
            FeaturedAuthors = featuredAuthorsDto,
        };

        return View(viewModel);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}