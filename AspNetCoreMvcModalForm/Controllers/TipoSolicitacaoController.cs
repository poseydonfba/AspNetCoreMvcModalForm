using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using AspNetCoreMvcModalForm.Business.Models;
using AspNetCoreMvcModalForm.Data.Context;
using AspNetCoreMvcModalForm.Helpers;
using AspNetCoreMvcModalForm.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AspNetCoreMvcModalForm.Controllers
{
    public class TipoSolicitacaoController : BaseController
    {
        private readonly DataDbContext _context;

        public TipoSolicitacaoController(DataDbContext context)
        {
            _context = context;
        }

        // GET: TipoSolicitacao
        public IActionResult Index()
        {
            return View();
        }

        [NoDirectAccess]
        public IActionResult List()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetDataTable()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var sortColumnDirectionMinus = sortColumnDirection.Equals("desc") ? "-" : "";

            var filter = new TipoSolicitacaoFilter
            {
                Descricao = string.IsNullOrEmpty(searchValue) ? null : searchValue,
                Offset = skip,
                Limit = pageSize,
                Sort = sortColumnDirectionMinus + sortColumn
            };

            var result = _context.TipoSolicitacoes.AsQueryable().Filter(filter);

            recordsTotal = await result.CountAsync();

            var data = await result.Sort(filter).Paginate(filter).ToListAsync();
            var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
            return Ok(jsonData);
        }

        // GET: TipoSolicitacao/Details/5
        [NoDirectAccess]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TipoSolicitacoes == null)
            {
                return NotFound();
            }

            var tipoSolicitacao = await _context.TipoSolicitacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoSolicitacao == null)
            {
                return NotFound();
            }

            return View(tipoSolicitacao);
        }

        // GET: TipoSolicitacao/Create
        [NoDirectAccess]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoSolicitacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Id")] TipoSolicitacao tipoSolicitacao)
        {
            if (ModelState.IsValid)
            {
                tipoSolicitacao.Id = Guid.NewGuid();
                _context.Add(tipoSolicitacao);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Json(new { isValid = true, html = string.Empty, model = tipoSolicitacao });
            }
            //return View(tipoSolicitacao);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, nameof(Create), tipoSolicitacao) });
        }

        // GET: TipoSolicitacao/Edit/5
        [NoDirectAccess]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TipoSolicitacoes == null)
            {
                return NotFound();
            }

            var tipoSolicitacao = await _context.TipoSolicitacoes.FindAsync(id);
            if (tipoSolicitacao == null)
            {
                return NotFound();
            }
            return View(tipoSolicitacao);
        }

        // POST: TipoSolicitacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Descricao,Id")] TipoSolicitacao tipoSolicitacao)
        {
            if (id != tipoSolicitacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoSolicitacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoSolicitacaoExists(tipoSolicitacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return Json(new { isValid = true, html = string.Empty, model = tipoSolicitacao });
            }
            //return View(tipoSolicitacao);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, nameof(Edit), tipoSolicitacao) });
        }

        // GET: TipoSolicitacao/Delete/5
        [NoDirectAccess]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TipoSolicitacoes == null)
            {
                return NotFound();
            }

            var tipoSolicitacao = await _context.TipoSolicitacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoSolicitacao == null)
            {
                return NotFound();
            }

            return View(tipoSolicitacao);
        }

        // POST: TipoSolicitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TipoSolicitacoes == null)
            {
                return Problem("Entity set 'DataDbContext.TipoSolicitacoes'  is null.");
            }
            var tipoSolicitacao = await _context.TipoSolicitacoes.FindAsync(id);
            if (tipoSolicitacao != null)
            {
                _context.TipoSolicitacoes.Remove(tipoSolicitacao);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Json(new { isValid = true, html = string.Empty, model = tipoSolicitacao });
        }

        private bool TipoSolicitacaoExists(Guid id)
        {
          return _context.TipoSolicitacoes.Any(e => e.Id == id);
        }
    }
}
