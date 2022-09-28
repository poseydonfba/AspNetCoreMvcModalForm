using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using AspNetCoreMvcModalForm.Business.Models;
using AspNetCoreMvcModalForm.Data.Context;
using AspNetCoreMvcModalForm.Helpers;
using AspNetCoreMvcModalForm.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core; // Install System.Linq.Dynamic.Core
using System.Threading.Tasks;

namespace AspNetCoreMvcModalForm.Controllers
{
    public class SolicitacaoController : BaseController
    {
        private readonly DataDbContext _context;

        public SolicitacaoController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Solicitacao
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

            var isDataDateTime = DateTime.TryParse(searchValue, out DateTime dataSolicitacao);

            if (isDataDateTime)
            {
                var solicitacoes = _context.Solicitacoes.Include(x => x.TipoSolicitacao).AsQueryable();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    solicitacoes = solicitacoes.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                solicitacoes = solicitacoes.Where(x => DateTime.Compare(x.Data.Date, dataSolicitacao.Date) == 0);

                recordsTotal = await solicitacoes.CountAsync();

                var data = solicitacoes.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            else
            {
                var isQuantidadeInt = int.TryParse(searchValue, out int quantidade);
                var isValorDecimal = decimal.TryParse(searchValue, out decimal valor);

                var solicitacaoFilter = new SolicitacaoFilter
                {
                    Descricao = string.IsNullOrEmpty(searchValue) ? null : searchValue,
                    //Data = isDataDateTime ? dataSolicitacao : default(DateTime?),
                    Quantidade = isQuantidadeInt ? quantidade : default(int?),
                    Valor = isValorDecimal ? valor : default(decimal?),
                    Offset = skip,
                    Limit = pageSize,
                    Sort = sortColumnDirection + sortColumn
                };

                var solicitacoes = _context.Solicitacoes.Include(x => x.TipoSolicitacao).AsQueryable().Filter(solicitacaoFilter);

                recordsTotal = await solicitacoes.CountAsync();

                var data = await solicitacoes.Sort(solicitacaoFilter).Paginate(solicitacaoFilter).ToListAsync();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
        }

        // GET: Solicitacao/Details/5
        [NoDirectAccess]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Solicitacoes == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacoes
                .Include(s => s.TipoSolicitacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        // GET: Solicitacao/Create
        [NoDirectAccess]
        public IActionResult Create()
        {
            ViewData["TipoSolicitacaoId"] = new SelectList(_context.TipoSolicitacoes, "Id", "Descricao");
            return View();
        }

        // POST: Solicitacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Data,Quantidade,Valor,TipoSolicitacaoId,Id")] Solicitacao solicitacao)
        {
            ModelState.Remove("TipoSolicitacao");
            if (ModelState.IsValid)
            {
                solicitacao.Id = Guid.NewGuid();
                _context.Add(solicitacao);
                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = string.Empty, model = solicitacao });
            }
            ViewData["TipoSolicitacaoId"] = new SelectList(_context.TipoSolicitacoes, "Id", "Descricao", solicitacao.TipoSolicitacaoId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, nameof(Create), solicitacao) });
        }

        // GET: Solicitacao/Edit/5
        [NoDirectAccess]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Solicitacoes == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return NotFound();
            }
            ViewData["TipoSolicitacaoId"] = new SelectList(_context.TipoSolicitacoes, "Id", "Descricao", solicitacao.TipoSolicitacaoId);
            return View(solicitacao);
        }

        // POST: Solicitacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Descricao,Data,Quantidade,Valor,TipoSolicitacaoId,Id")] Solicitacao solicitacao)
        {
            if (id != solicitacao.Id)
            {
                return NotFound();
            }

            ModelState.Remove("TipoSolicitacao");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitacaoExists(solicitacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return Json(new { isValid = true, html = string.Empty });
            }
            ViewData["TipoSolicitacaoId"] = new SelectList(_context.TipoSolicitacoes, "Id", "Descricao", solicitacao.TipoSolicitacaoId);
            //return View(solicitacao);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, nameof(Edit), solicitacao) });
        }

        // GET: Solicitacao/Delete/5
        [NoDirectAccess]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Solicitacoes == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacoes
                .Include(s => s.TipoSolicitacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        // POST: Solicitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Solicitacoes == null)
            {
                return Problem("Entity set 'DataDbContext.Solicitacoes'  is null.");
            }
            var solicitacao = await _context.Solicitacoes.FindAsync(id);
            if (solicitacao != null)
            {
                _context.Solicitacoes.Remove(solicitacao);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Json(new { isValid = true, html = string.Empty, model = solicitacao });
        }

        private bool SolicitacaoExists(Guid id)
        {
          return _context.Solicitacoes.Any(e => e.Id == id);
        }
    }
}
