using EcoEnergy.Repositories.Interfaces;
using EcoEnergy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EcoEnergy.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly IProceduresRepository _proceduresRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly ISimulacaoRepository _simulacaoRepository;


        public ProceduresController(
            IProceduresRepository proceduresRepository, 
            IEmpresaRepository empresaRepository,
            IUsuarioRepository usuarioRepository,
            IEnderecoRepository enderecoRepository,
            IOrcamentoRepository orcamentoRepository,
            ISimulacaoRepository simulacaoRepository)
        {
            _proceduresRepository = proceduresRepository;
            _empresaRepository = empresaRepository;
            _usuarioRepository = usuarioRepository;
            _enderecoRepository = enderecoRepository;
            _orcamentoRepository = orcamentoRepository;
            _simulacaoRepository = simulacaoRepository;
        }

        public IActionResult MenuProcedures()
        {
            TempData["SuccessMessage"] = null;
            TempData["ErrorMessage"] = null;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarEmpresas()
        {
            try
            {
                var empresas = await _empresaRepository.GetAll();
                return View(empresas);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao listar empresas!\n" + e.Message;
                return RedirectToAction("EmpresaView");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            try
            {
                // Busca todos os usuários
                var usuarios = await _usuarioRepository.GetAll();

                // Itera sobre os usuários para buscar a empresa associada, se existir
                foreach (var usuario in usuarios)
                {
                    if (usuario.IdEmpresa.HasValue)
                    {
                        try
                        {
                            // Busca a empresa pelo IdEmpresa
                            var empresa = await _empresaRepository.GetById(usuario.IdEmpresa.Value);
                            if (empresa != null)
                            {
                                usuario.Empresa = empresa;
                            }
                        }
                        catch (System.Exception e)
                        {
                            usuario.Empresa = null;
                        }
                    }
                }

                return View(usuarios);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao listar usuários!\n" + e.Message;
                return RedirectToAction("UsuarioView");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarEnderecos()
        {
            try
            {
                // Busca todos os endereços
                var enderecos = await _enderecoRepository.GetAll();

                // Itera sobre os endereços para buscar o usuário e a empresa associados, se existirem
                foreach (var endereco in enderecos)
                {
                    // Buscar usuário associado ao endereço, se houver
                    if (endereco.IdUsuario.HasValue)
                    {
                        try
                        {
                            var usuario = await _usuarioRepository.GetById(endereco.IdUsuario.Value);
                            if (usuario != null)
                            {
                                endereco.Usuario = usuario;
                            }
                        }
                        catch (System.Exception)
                        {
                            endereco.Usuario = null;
                        }
                    }

                    // Buscar empresa associada ao endereço, se houver
                    if (endereco.IdEmpresa.HasValue)
                    {
                        try
                        {
                            var empresa = await _empresaRepository.GetById(endereco.IdEmpresa.Value);
                            if (empresa != null)
                            {
                                endereco.Empresa = empresa;
                            }
                        }
                        catch (System.Exception)
                        {
                            endereco.Empresa = null;
                        }
                    }
                }

                return View(enderecos);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao listar endereços!\n" + e.Message;
                return RedirectToAction("EnderecoView");
            }
        }


        [HttpGet]
        public async Task<IActionResult> ListarSimulacoes()
        {
            try
            {
                // Busca todas as simulações
                var simulacoes = await _simulacaoRepository.GetAll();

                // Itera sobre as simulações para buscar o usuário e o endereço associados, se existirem
                foreach (var simulacao in simulacoes)
                {
                    if (simulacao.IdUsuario > 0)
                    {
                        try
                        {
                            // Busca o usuário pelo IdUsuario
                            var usuario = await _usuarioRepository.GetById(simulacao.IdUsuario);
                            if (usuario != null)
                            {
                                simulacao.Usuario = usuario;
                            }
                        }
                        catch (System.Exception e)
                        {
                            simulacao.Usuario = null;
                        }
                    }

                    if (simulacao.IdEndereco > 0)
                    {
                        try
                        {
                            // Busca o endereço pelo IdEndereco
                            var endereco = await _enderecoRepository.GetById(simulacao.IdEndereco);
                            if (endereco != null)
                            {
                                simulacao.Endereco = endereco;
                            }
                        }
                        catch (System.Exception e)
                        {
                            simulacao.Endereco = null;
                        }
                    }
                }

                return View(simulacoes);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao listar simulações!\n" + e.Message;
                return RedirectToAction("SimulacaoView");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarOrcamentos()
        {
            try
            {
                // Busca todos os orçamentos
                var orcamentos = await _orcamentoRepository.GetAll();

                // Itera sobre os orçamentos para buscar os dados relacionados
                foreach (var orcamento in orcamentos)
                {
                    // Se houver uma simulação associada, buscar o usuário associado à simulação
                    if (orcamento.IdSimulacao > 0)
                    {
                        try
                        {
                            var simulacao = await _simulacaoRepository.GetById(orcamento.IdSimulacao);
                            if (simulacao != null)
                            {
                                orcamento.Simulacao = simulacao;

                                if (simulacao.IdUsuario > 0)
                                {
                                    var usuario = await _usuarioRepository.GetById(simulacao.IdUsuario);
                                    if (usuario != null)
                                    {
                                        simulacao.Usuario = usuario;
                                    }
                                }
                            }
                        }
                        catch (System.Exception)
                        {
                            orcamento.Simulacao = null;
                        }
                    }

                    // Se houver uma empresa associada, buscar a empresa
                    if (orcamento.IdEmpresa > 0)
                    {
                        try
                        {
                            var empresa = await _empresaRepository.GetById(orcamento.IdEmpresa);
                            if (empresa != null)
                            {
                                orcamento.Empresa = empresa;
                            }
                        }
                        catch (System.Exception)
                        {
                            orcamento.Empresa = null;
                        }
                    }
                }

                return View(orcamentos);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao listar orçamentos!\n" + e.Message;
                return RedirectToAction("OrcamentoView");
            }
        }

        [HttpGet]
        public IActionResult EmpresaView()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmpresaView(ProceduresViewModel model)
        {
            try
            {
                await _proceduresRepository.InsertEmpresa(
                    model.NrCnpj,
                    model.NmEmpresa,
                    model.NrTelefone,
                    model.DsEspecialidade
                );
                TempData["SuccessMessage"] = "Empresa inserida com sucesso!";
                return RedirectToAction("EmpresaView");
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao inserir empresa!\n" + e.Message;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UsuarioView()
        {
            try
            {
                // Busca todas as empresas para popular o dropdown
                var empresas = await _empresaRepository.GetAll();

                // Cria o ViewModel e popula as empresas disponíveis
                var model = new ProceduresViewModel
                {                    
                    Empresas = empresas.Select(e => new SelectListItem
                    {
                        Value = e.IdEmpresa.ToString(),
                        Text = $"{e.NmEmpresa} - {e.NrCnpj}"
                    }).ToList()
                };

                return View(model);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao carregar dados para edição!\n" + e.Message;
                return View();
            }            
        }

        [HttpPost]
        public async Task<IActionResult> UsuarioView(ProceduresViewModel model)
        {
            try
            {
                await _proceduresRepository.InsertUsuario(
                    model.NmUsuario,
                    model.DsEmail,
                    model.NrTelefone,
                    model.NrCpf,
                    model.IdEmpresa
                );
                TempData["SuccessMessage"] = "Usuário inserido com sucesso!";
                return RedirectToAction("UsuarioView");
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao inserir usuário!\n" + e.Message;
            }
            return View(model);
        }

        [HttpGet]        
        public async Task<IActionResult> EnderecoView()
        {
            try
            {
                // Busca todas as empresas e usuários para popular os dropdowns
                var empresas = await _empresaRepository.GetAll();
                var usuarios = await _usuarioRepository.GetAll();

                // Cria o ViewModel e popula as listas de empresas e usuários disponíveis
                var model = new ProceduresViewModel
                {
                    Empresas = empresas.Select(e => new SelectListItem
                    {
                        Value = e.IdEmpresa.ToString(),
                        Text = $"{e.NmEmpresa} - {e.NrCnpj}"
                    }).ToList(),
                    Usuarios = usuarios.Select(u => new SelectListItem
                    {
                        Value = u.IdUsuario.ToString(),
                        Text = $"{u.NmUsuario} - {u.DsEmail}"
                    }).ToList()
                };

                return View(model);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao carregar dados para cadastro de endereço!\n" + e.Message;
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> EnderecoView(ProceduresViewModel model)
        {
            try
            {
                await _proceduresRepository.InsertEndereco(
                    model.DsCep,
                    model.DsLogradouro,
                    model.NrLogradouro,
                    model.DsBairro,
                    model.DsCidade,
                    model.DsEstado,
                    model.DsPais,
                    model.IdUsuario,
                    model.IdEmpresa
                );
                TempData["SuccessMessage"] = "Endereço inserido com sucesso!";
                return RedirectToAction("EnderecoView");
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao inserir Endereço!\n" + e.Message;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult OrcamentoView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrcamentoView(ProceduresViewModel model)
        {
            try
            {
                await _proceduresRepository.InsertOrcamento(
                    model.NrValorProposto,
                    model.DsPrazo,
                    model.DtOrcamento,
                    model.DsServicos,
                    model.IdSimulacao.Value,
                    model.IdEmpresa.Value
                );
                TempData["SuccessMessage"] = "Orçamento inserido com sucesso!";
                return RedirectToAction("OrcamentoView");
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao inserir Orçamento!\n" + e.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SimulacaoView()
        {
            try
            {
                // Busca todos os usuários e endereços para popular os dropdowns
                var usuarios = await _usuarioRepository.GetAll();
                var enderecos = await _enderecoRepository.GetAll();

                // Cria o ViewModel e popula as listas de usuários e endereços disponíveis
                var model = new ProceduresViewModel
                {
                    Usuarios = usuarios.Select(u => new SelectListItem
                    {
                        Value = u.IdUsuario.ToString(),
                        Text = $"{u.NmUsuario} - {u.DsEmail}"
                    }).ToList(),
                    Enderecos = enderecos.Select(e => new SelectListItem
                    {
                        Value = e.IdEndereco.ToString(),
                        Text = $"{e.DsLogradouro}, {e.NrLogradouro} - {e.DsCidade}, {e.DsEstado}"
                    }).ToList()
                };

                return View(model);
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao carregar dados para a simulação!\n" + e.Message;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> SimulacaoView(ProceduresViewModel model)
        {
            try
            {
                await _proceduresRepository.InsertSimulacao(
                    model.NrCustoEstimado,
                    model.NrEconomia,
                    model.DtSimulacao,
                    model.NrConsumoMensal,
                    model.NrAreaPlaca,
                    model.NrPotenciaEstimada,
                    model.NrProducaoMensal,
                    model.NrTempoRetornoInvestimento,
                    model.DsOrcamentoSolicitado,
                    model.IdUsuario.Value,
                    model.IdEndereco.Value
                );
                TempData["SuccessMessage"] = "Simulação inserida com sucesso!";
                return RedirectToAction("OrcamentoView");
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = "Erro ao inserir Simulação!\n" + e.Message;
            }
            return View(model);
        }
    }
}
