using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebLibCs.Core.DTOs;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Core.Interfaces.Services;
using WebLibCs.Core.Common;
using WebLibCs.Web.ViewModels.Libro;

namespace WebLibCs.Web.Controllers;

public class LibrosController(
    ILibroRepository libroRepository,
    IAutorRepository autorRepository,
    IImageService imageService,
    IUnitOfWork unitOfWork,
    IMapper mapper) : Controller
{
    private readonly ILibroRepository _libroRepository = libroRepository;
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly IImageService _imageService = imageService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    // GET: Libros
    [HttpGet]
    public async Task<IActionResult> Index(string searchString, int page = 1)
    {
        const int pageSize = Constants.DefaultPageSize;
        var pagedResult = await _libroRepository.GetPagedAsync(page, pageSize, searchString);

        var librosDto = _mapper.Map<IEnumerable<LibroDto>>(pagedResult.Items);

        var viewModel = new LibroIndexViewModel
        {
            Libros = librosDto,
            CurrentPage = pagedResult.CurrentPage,
            TotalPages = pagedResult.TotalPages,
            HasPrevious = pagedResult.HasPrevious,
            HasNext = pagedResult.HasNext,
            SearchTerm = searchString,
        };

        ViewData["CurrentFilter"] = searchString;
        return View(viewModel);
    }

    // GET: Libros/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var libro = await _libroRepository.GetByIdWithAutorAsync(id.Value);
        if (libro == null)
        {
            return NotFound();
        }

        var libroDto = _mapper.Map<LibroDto>(libro);
        var viewModel = _mapper.Map<LibroDetailsViewModel>(libroDto);

        return View(viewModel);
    }

    // GET: Libros/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var autores = await _autorRepository.GetAllAsync();
        var autoresDto = _mapper.Map<IEnumerable<AutorDto>>(autores);

        var viewModel = new CreateLibroViewModel
        {
            Autores = autoresDto,
        };

        return View(viewModel);
    }

    // POST: Libros/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLibroViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var createDto = _mapper.Map<CreateLibroDto>(viewModel);
                var libro = _mapper.Map<Core.Entities.Libro>(createDto);

                // Handle image upload
                string? imagenRuta = null;
                if (viewModel.ImagenArchivo != null)
                {
                    imagenRuta = await _imageService.SaveImageAsync(
                        viewModel.ImagenArchivo,
                        Constants.DefaultFolder);
                }

                // Create new record with image path
                libro = libro with { ImagenRuta = imagenRuta };

                await _libroRepository.AddAsync(libro);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al guardar el libro: {ex.Message}");
            }
        }

        // Reload autores in case of error
        var autores = await _autorRepository.GetAllAsync();
        var autoresDto = _mapper.Map<IEnumerable<AutorDto>>(autores);
        viewModel = viewModel with { Autores = autoresDto };

        return View(viewModel);
    }

    // GET: Libros/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var libro = await _libroRepository.GetByIdWithAutorAsync(id.Value);
        if (libro == null)
        {
            return NotFound();
        }

        var autores = await _autorRepository.GetAllAsync();
        var autoresDto = _mapper.Map<IEnumerable<AutorDto>>(autores);

        var libroDto = _mapper.Map<LibroDto>(libro);
        var viewModel = _mapper.Map<EditLibroViewModel>(libroDto);
        viewModel = viewModel with { Autores = autoresDto };

        return View(viewModel);
    }

    // POST: Libros/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditLibroViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var updateDto = _mapper.Map<UpdateLibroDto>(viewModel);
                var libro = _mapper.Map<Core.Entities.Libro>(updateDto);

                // Handle image update
                var imagenRuta = await _imageService.UpdateImageAsync(
                    viewModel.ImagenArchivo,
                    viewModel.ImagenRutaActual,
                    Constants.DefaultFolder);

                // Create new record with updated image path
                libro = libro with { ImagenRuta = imagenRuta };

                await _libroRepository.UpdateAsync(libro);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al actualizar el libro: {ex.Message}");
            }
        }

        // Reload autores in case of error
        var autores = await _autorRepository.GetAllAsync();
        var autoresDto = _mapper.Map<IEnumerable<AutorDto>>(autores);
        viewModel = viewModel with { Autores = autoresDto };

        return View(viewModel);
    }

    // GET: Libros/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var libro = await _libroRepository.GetByIdWithAutorAsync(id.Value);
        if (libro == null)
        {
            return NotFound();
        }

        var libroDto = _mapper.Map<LibroDto>(libro);
        var viewModel = _mapper.Map<DeleteLibroViewModel>(libroDto);

        return View(viewModel);
    }

    // POST: Libros/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var libro = await _libroRepository.GetByIdAsync(id);
        if (libro != null)
        {
            // Delete associated image
            if (!string.IsNullOrEmpty(libro.ImagenRuta))
            {
                await _imageService.DeleteImageAsync(libro.ImagenRuta);
            }

            await _libroRepository.DeleteAsync(libro);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}