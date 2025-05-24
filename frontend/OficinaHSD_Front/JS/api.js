const API_BASE = "https://localhost:44322";

function getToken() {
  return localStorage.getItem("jwtToken") || "";
}

function authHeaders() {
  const token = getToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

async function apiRequest(endpoint, options = {}) {
  const headers = options.headers || {};

  const token = getToken();

  if (token) {
    headers["Authorization"] = `Bearer ${token}`;
  }

  const response = await fetch(`${API_BASE}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...headers,
    },
  });

  // Se o token expirou ou for inválido
  if (response.status === 401) {
    showToast("Sua sessão expirou. Faça login novamente.", "error");
    localStorage.removeItem("token");
    setTimeout(() => {
      window.location.href = "login.html";
    }, 1500);
    return; 
  }

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || "Erro na requisição");
  }

  const contentType = response.headers.get("content-type");
  if (
    response.status === 204 ||
    !contentType ||
    !contentType.includes("application/json")
  ) {
    return;
  }

  return await response.json();
}

const toastContainer = document.getElementById("toast-container");

function showToast(message, type = "success", duration = 4000) {
  const toast = document.createElement("div");
  toast.className = `toast toast-${type}`;
  toast.innerHTML = `
    <span class="toast-message">${message}</span>
    <button class="close-btn">&times;</button>
  `;

  // Fecha ao clicar no botão
  toast.querySelector(".close-btn").addEventListener("click", () => {
    hideToast(toast);
  });

  toastContainer.appendChild(toast);

  requestAnimationFrame(() => toast.classList.add("show"));

  // Remove após um tempo
  setTimeout(() => hideToast(toast), duration);
}

function hideToast(toast) {
  toast.classList.remove("show");
  toast.addEventListener("transitionend", () => {
    toast.remove();
  });
}
