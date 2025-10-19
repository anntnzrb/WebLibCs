using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebLibCs.Core.DTOs;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Core.Interfaces.Services;
using WebLibCs.Core.Common;
using WebLibCs.Web.ViewModels.Autor;

namespace WebLibCs.Web.Controllers;

public class AutoresController(
    IAutorRepository autorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IImageService imageService) : Controller
{
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IImageService _imageService = imageService;

    // GET: Autores
    [HttpGet]
    public async Task<IActionResult> Index(string searchString)
    {
        var autores = string.IsNullOrEmpty(searchString)
            ? await _autorRepository.GetAllAsync()
            : await _autorRepository.SearchByNameAsync(searchString);

        var autoresDto = _mapper.Map<IEnumerable<AutorDto>>(autores);

        var viewModel = new AutorIndexViewModel
        {
            Autores = autoresDto,
            SearchTerm = searchString,
            TotalCount = autoresDto.Count(),
        };

        ViewData["CurrentFilter"] = searchString;
        return View(viewModel);
    }

    // GET: Autores/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var autor = await _autorRepository.GetWithLibrosAsync(id.Value);
        if (autor == null)
        {
            return NotFound();
        }

        var autorDto = _mapper.Map<AutorDto>(autor);
        var librosDto = _mapper.Map<IEnumerable<LibroDto>>(autor.Libros);

        var viewModel = new DetailsAutorViewModel(
            autorDto.Id,
            autorDto.Nombre,
            autorDto.ImagenRuta,
            librosDto
        );

        return View(viewModel);
    }

    // GET: Autores/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Autores/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAutorViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var createDto = _mapper.Map<CreateAutorDto>(viewModel);
            var autor = _mapper.Map<Core.Entities.Autor>(createDto);

            // Handle image upload
            string? imagenRuta = null;
            if (viewModel.ImagenArchivo != null)
            {
                imagenRuta = await _imageService.SaveImageAsync(viewModel.ImagenArchivo, Constants.DefaultFolder);
            }
            autor = autor with { ImagenRuta = imagenRuta };

            await _autorRepository.AddAsync(autor);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }

    // GET: Autores/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var autor = await _autorRepository.GetByIdAsync(id.Value);
        if (autor == null)
        {
            return NotFound();
        }

        var autorDto = _mapper.Map<AutorDto>(autor);
        var viewModel = _mapper.Map<EditAutorViewModel>(autorDto);

        return View(viewModel);
    }

    // POST: Autores/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditAutorViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Get existing autor to preserve current image if no new image is uploaded
                var existingAutor = await _autorRepository.GetByIdAsync(id);
                if (existingAutor == null)
                {
                    return NotFound();
                }

                var updateDto = _mapper.Map<UpdateAutorDto>(viewModel);
                var autor = _mapper.Map<Core.Entities.Autor>(updateDto);

                // Handle image update
                var imagenRuta = await _imageService.UpdateImageAsync(
                    viewModel.ImagenArchivo,
                    existingAutor.ImagenRuta,
                    Constants.DefaultFolder);

                autor = autor with { ImagenRuta = imagenRuta };

                await _autorRepository.UpdateAsync(autor);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!await _autorRepository.ExistsAsync(viewModel.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }

    // GET: Autores/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var autor = await _autorRepository.GetWithLibrosAsync(id.Value);
        if (autor == null)
        {
            return NotFound();
        }

        var autorDto = _mapper.Map<AutorDto>(autor);
        var viewModel = _mapper.Map<DeleteAutorViewModel>(autorDto);

        return View(viewModel);
    }

    // POST: Autores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var autor = await _autorRepository.GetByIdAsync(id);
        if (autor != null)
        {
            // Delete image if exists
            if (!string.IsNullOrEmpty(autor.ImagenRuta))
            {
                await _imageService.DeleteImageAsync(autor.ImagenRuta);
            }

            await _autorRepository.DeleteAsync(autor);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}