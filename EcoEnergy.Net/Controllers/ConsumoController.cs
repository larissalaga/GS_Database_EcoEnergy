using Microsoft.AspNetCore.Mvc;
using WebApplicationOdontoPrev.ViewModels;

namespace WebApplicationOdontoPrev.Controllers
{
    public class ConsumoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ConsumoViewModel());
        }

        [HttpPost]
        public IActionResult Index(ConsumoViewModel model)
        {
            if (ModelState.IsValid)
            {
                double totalConsumo = 0;

                // Cálculo do consumo baseado na quantidade de cada aparelho
                totalConsumo += model.QuantidadeGeladeira * 56; // Geladeira (56 kWh/mês por unidade)
                totalConsumo += model.QuantidadeTelevisao * 15; // Televisão (15 kWh/mês por unidade)
                totalConsumo += model.QuantidadeMicroondas * 10; // Microondas (10 kWh/mês por unidade)
                totalConsumo += model.QuantidadeArCondicionado * 75; // Ar-condicionado (75 kWh/mês por unidade)

                model.ConsumoTotal = totalConsumo;
                model.CustoTotal = Math.Round(totalConsumo * model.CustoKWh, 2); // Calcula o custo total usando o valor do kWh do ViewModel

                return View(model);
            }

            return View(model);
        }
    }
}
