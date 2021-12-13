using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Models
{
    [EnableCors("EnableCors")]
    [ApiController]
    [Route("[controller]")]
    public class CashierController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<CashierController> _logger;
        public CashierController(Context context, ILogger<CashierController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] List<Cashier> cashier)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in cashier)
                {
                    var obj = new Cashier
                    {
                        barCode = i.barCode,
                        name = i.name,
                        amount = i.amount,
                        unitValue = i.unitValue,
                        saleDate = DateTime.Now
                    };

                    var result = _context.Inventory.SingleOrDefault(e => e.name == i.name);

                    if (result.amount <= 0)
                    {
                        return ValidationProblem("Reponha o estoque deste item");
                    }

                    _context.Cashier.Add(obj);
                    result.amount -= i.amount;
                }

                await _context.SaveChangesAsync();
            }

            return Ok("Venda concluída com sucesso!");
        }

        [HttpGet]
        [Route("GetInventoryData")]
        public async Task<IActionResult> GetInventoryData(int? barCode)
        {
            var result =
                await _context.Inventory
                    .Where(e => e.barCode == barCode)
                    .Select(x => new { x.name, x.unitValue })
                    .SingleOrDefaultAsync();

            if (result == null)
            {
                return ValidationProblem("Nenhum produto com este código de barras");
            }

            return Json(result);
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index(string arrowId)
        {

            Task<List<Cashier>> resultOrder = null;

            switch (arrowId)
            {
                case "arrow-up-CodigoBarras":
                    resultOrder = _context.Cashier.OrderBy(c => c.barCode).ToListAsync();
                    break;
                case "arrow-down-CodigoBarras":
                    resultOrder = _context.Cashier.OrderByDescending(c => c.barCode).ToListAsync();
                    break;
                case "arrow-up-Nome":
                    resultOrder = _context.Cashier.OrderBy(c => c.name).ToListAsync();
                    break;
                case "arrow-down-Nome":
                    resultOrder = _context.Cashier.OrderByDescending(c => c.name).ToListAsync();
                    break;
                case "arrow-up-Quantidade":
                    resultOrder = _context.Cashier.OrderBy(c => c.amount).ToListAsync();
                    break;
                case "arrow-down-Quantidade":
                    resultOrder = _context.Cashier.OrderByDescending(c => c.amount).ToListAsync();
                    break;
                case "arrow-up-ValorUnitario":
                    resultOrder = _context.Cashier.OrderBy(c => c.unitValue).ToListAsync();
                    break;
                case "arrow-down-ValorUnitario":
                    resultOrder = _context.Cashier.OrderByDescending(c => c.unitValue).ToListAsync();
                    break;
                case "arrow-up-DataVenda":
                    resultOrder = _context.Cashier.OrderBy(c => c.saleDate).ToListAsync();
                    break;
                case "arrow-down-DataVenda":
                    resultOrder = _context.Cashier.OrderByDescending(c => c.saleDate).ToListAsync();
                    break;
                default:
                    resultOrder = _context.Cashier.ToListAsync();
                    break;
            }

            return new JsonResult(await resultOrder);
        }
    }
}