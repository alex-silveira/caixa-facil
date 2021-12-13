using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    [EnableCors("EnableCors")]
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : Controller
    {
        private readonly Context _context;

        public InventoryController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index(string arrowId)
        {
            Task<List<Inventory>> resultOrder = null;

            switch (arrowId)
            {
                case "arrow-up-barCode":
                    resultOrder = _context.Inventory.OrderBy(c => c.barCode).ToListAsync();
                    break;
                case "arrow-down-barCode":
                    resultOrder = _context.Inventory.OrderByDescending(c => c.barCode).ToListAsync();
                    break;
                case "arrow-up-name":
                    resultOrder = _context.Inventory.OrderBy(c => c.name).ToListAsync();
                    break;
                case "arrow-down-name":
                    resultOrder = _context.Inventory.OrderByDescending(c => c.name).ToListAsync();
                    break;
                case "arrow-down-amount":
                    resultOrder = _context.Inventory.OrderByDescending(c => c.amount).ToListAsync();
                    break;
                case "arrow-up-unitValue":
                    resultOrder = _context.Inventory.OrderBy(c => c.unitValue).ToListAsync();
                    break;
                case "arrow-down-unitValue":
                    resultOrder = _context.Inventory.OrderByDescending(c => c.unitValue).ToListAsync();
                    break;
                case "arrow-up-expirationDate":
                    resultOrder = _context.Inventory.OrderBy(c => c.expirationDate).ToListAsync();
                    break;
                case "arrow-down-expirationDate":
                    resultOrder = _context.Inventory.OrderByDescending(c => c.expirationDate).ToListAsync();
                    break;
                case "arrow-up-registerDate":
                    resultOrder = _context.Inventory.OrderBy(c => c.registerDate).ToListAsync();
                    break;
                case "arrow-down-registerDate":
                    resultOrder = _context.Inventory.OrderByDescending(c => c.registerDate).ToListAsync();
                    break;
                default:
                    resultOrder = _context.Inventory.ToListAsync();
                    break;
            }

            return Json(await resultOrder);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.inventoryId == id);

            if (inventory == null)
            {
                return BadRequest();
            }

            return Json(inventory);
        }

        // POST: Produtos/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("EstoqueId,CodigoBarras,Nome,Quantidade,ValorUnitario,DataValidade,DataRegistro")][FromBody] Inventory inventory)
        {
            var result = from e in _context.Inventory select e;
            var barCode = result.Where(e => e.barCode == inventory.barCode);
            var name = result.Where(e => e.name == inventory.name);

            var msg = "";

            if (barCode.Count() > 0)
            {
                msg = "Código de Barras Existente!";
                return ValidationProblem(msg, null, null, "errorBarCode");
            }
            if (name.Count() > 0)
            {
                msg = "Um produto com este nome já existe!";
                return ValidationProblem(msg, null, null, "errorName");
            }

            if (ModelState.IsValid)
            {
                inventory.registerDate = DateTime.Now;
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                msg = "Produto " + inventory.name + " cadastrado com sucesso!";
                return Ok(msg);
            }
            return Json(inventory);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var inventory = await _context.Inventory.FindAsync(id);

            if (inventory == null)
            {
                return BadRequest();
            }
            return Json(inventory);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int inventoryId, [Bind("inventoryId,barCode,name,amount,unitValue,expirationDate,registerDate")][FromBody] Inventory inventory)
        {
            if (inventoryId != inventory.inventoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    inventory.registerDate = DateTime.Now;
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(inventory.inventoryId))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok("Produto atualizado com sucesso!");
        }

        // POST: Produtos/Delete/5
        [HttpDelete]
        [Route("Excluir")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
            return Ok("Item excluído com sucesso!");
        }
        private bool ProductExists(int id)
        {
            return _context.Inventory.Any(e => e.inventoryId == id);
        }
    }
}