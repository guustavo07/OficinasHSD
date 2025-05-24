let todasOficinas = [];

window.addEventListener("DOMContentLoaded", () => {
  configurarModais();
  carregarOficinas();
  configurarFiltro();
});

// Carrega e renderiza a lista de oficinas
async function carregarOficinas() {
  try {
    todasOficinas = await apiRequest("/Oficina/ObterTodos");
    renderizarOficinas(todasOficinas);
  } catch (err) {
    showToast("Erro ao carregar oficinas: " + err.message, "error");
  }
}

// Detalhes da oficina
async function verDetalhes(id) {
  try {
    const oficina = await apiRequest(`/Oficina/ObterPorId/${id}`);
    const conteudo = `
      <div class="detalhes-container">
        <i class="fas fa-tools icon-large"></i>
        <h2>${oficina.nome}</h2>
        <p><strong>Endereço:</strong> ${oficina.endereco}</p>
        <p><strong>Serviços:</strong> ${oficina.servicos}</p>
      </div>
    `;
    document.getElementById("conteudo-detalhes").innerHTML = conteudo;
    document.getElementById("modal-detalhes").style.display = "flex";
  } catch (err) {
    showToast("Erro ao buscar detalhes da oficina: " + err.message, "error");
  }
}

// Configura eventos de ambos os modais (adicionar e editar)
function configurarModais() {
  // Modal Adicionar
  document
    .getElementById("btn-nova-oficina")
    .addEventListener("click", abrirModalAdicionar);
  document
    .getElementById("fechar-modal-adicionar")
    .addEventListener("click", fecharModalAdicionar);
  document
    .getElementById("modal-adicionar-oficina")
    .addEventListener("click", (e) => {
      if (e.target.id === "modal-adicionar-oficina") fecharModalAdicionar();
    });
  document
    .getElementById("form-adicionar-oficina")
    .addEventListener("submit", async (e) => {
      e.preventDefault();
      const novaOficina = {
        nome: document.getElementById("nome-adicionar").value,
        endereco: document.getElementById("endereco-adicionar").value,
        servicos: document.getElementById("servicos-adicionar").value,
      };
      try {
        await apiRequest("/Oficina/Inserir", {
          method: "POST",
          body: JSON.stringify(novaOficina),
        });
        fecharModalAdicionar();
        e.target.reset();
        carregarOficinas();
        showToast("Oficina cadastrada com sucesso", "success");
      } catch (err) {
        showToast("Falha ao cadastrar oficina: " + err.message, "error");
      }
    });

  // Modal Editar
  document
    .getElementById("fechar-modal-editar")
    .addEventListener("click", fecharModalEdicao);
  document
    .getElementById("modal-editar-oficina")
    .addEventListener("click", (e) => {
      if (e.target.id === "modal-editar-oficina") fecharModalEdicao();
    });
  document
    .getElementById("form-editar-oficina")
    .addEventListener("submit", async (e) => {
      e.preventDefault();
      const oficinaAtualizada = {
        id: document.getElementById("id-editar").value,
        nome: document.getElementById("nome-editar").value,
        endereco: document.getElementById("endereco-editar").value,
        servicos: document.getElementById("servicos-editar").value,
      };
      try {
        await apiRequest(`/Oficina/Atualizar/${oficinaAtualizada.id}`, {
          method: "PUT",
          body: JSON.stringify(oficinaAtualizada),
        });
        fecharModalEdicao();
        carregarOficinas();
        showToast("Oficina atualizada com sucesso", "success");
      } catch (err) {
        showToast("Erro ao atualizar oficina: " + err.message, "error");
      }
    });
}

function abrirModalAdicionar() {
  document.getElementById("modal-adicionar-oficina").style.display = "flex";
}

function fecharModalAdicionar() {
  document.getElementById("modal-adicionar-oficina").style.display = "none";
}

function editarOficina(id) {
  fecharModalDetalhes();
  apiRequest(`/Oficina/ObterPorId/${id}`)
    .then((oficina) => {
      // Preenche campos
      document.getElementById("id-editar").value = oficina.id;
      document.getElementById("nome-editar").value = oficina.nome;
      document.getElementById("endereco-editar").value = oficina.endereco;
      document.getElementById("servicos-editar").value = oficina.servicos;
      abrirModalEdicao();
    })
    .catch((err) =>
      showToast("Erro ao buscar oficina para editar: " + err.message, "error")
    );
}

function abrirModalEdicao() {
  document.getElementById("modal-editar-oficina").style.display = "flex";
}

function fecharModalEdicao() {
  document.getElementById("modal-editar-oficina").style.display = "none";
}

function fecharModalDetalhes() {
  document.getElementById("modal-detalhes").style.display = "none";
}

// Exclusão de oficina
let oficinaIdParaExcluir = null;
function confirmarExclusao(id) {
  oficinaIdParaExcluir = id;
  document.getElementById("modal-confirmar-exclusao").style.display = "flex";
}

document
  .getElementById("btn-cancelar-exclusao")
  .addEventListener("click", () => {
    document.getElementById("modal-confirmar-exclusao").style.display = "none";
    oficinaIdParaExcluir = null;
  });

document
  .getElementById("btn-confirmar-exclusao")
  .addEventListener("click", async () => {
    if (!oficinaIdParaExcluir) return;
    try {
      await apiRequest(`/Oficina/Deletar/${oficinaIdParaExcluir}`, {
        method: "DELETE",
      });
      document.getElementById("modal-confirmar-exclusao").style.display =
        "none";
      carregarOficinas();
      showToast("Oficina excluida com sucesso", "success");
    } catch (err) {
      showToast("Erro ao excluir oficina: " + err.message, "error");
    }
  });

function renderizarOficinas(oficinas) {
  const lista = document.getElementById("shop-list");
  lista.innerHTML = "";

  document.querySelector(".container-vazia")?.remove();
  lista.style.display = oficinas.length ? "grid" : "none";

  if (!oficinas.length) {
    const containerVazio = document.createElement("div");
    containerVazio.className = "container-vazia";
    containerVazio.innerHTML = `
      <p>Não há oficinas cadastradas ainda.</p>
      <button class="botao-adicionar-centralizado" id="btn-adicionar-vazio">
        Adicionar Oficina
      </button>
    `;
    document.querySelector("main.container").appendChild(containerVazio);
    document
      .getElementById("btn-adicionar-vazio")
      .addEventListener("click", abrirModalAdicionar);
    return;
  }

  oficinas.forEach((oficina) => {
    const item = document.createElement("li");
    item.className = "card";
    item.innerHTML = `
      <i class="fas fa-tools icon"></i>
      <h3>${oficina.nome}</h3>
      <div class="card-buttons">
        <button class="btn-detalhes" onclick="verDetalhes('${oficina.id}')">Ver Detalhes</button>
        <button class="btn-editar" onclick="editarOficina('${oficina.id}')">
          <i class="fas fa-edit"></i>
        </button>
        <button class="btn-excluir" onclick="confirmarExclusao('${oficina.id}')">
          <i class="fas fa-trash-alt"></i>
        </button>
      </div>
    `;
    lista.appendChild(item);
  });
}

function configurarFiltro() {
  const inputFiltro = document.getElementById("filtro-nome");
  inputFiltro.addEventListener("input", () => {
    const termo = inputFiltro.value.trim().toLowerCase();
    // filtra por nome contendo o termo
    const filtradas = todasOficinas.filter((of) =>
      of.nome.toLowerCase().includes(termo)
    );
    renderizarOficinas(filtradas);
  });
}
