async function loginUser(event) {
  event.preventDefault();
  const form = event.target;
  const body = {
    username: form.username.value,
    passwordHash: form.password.value,
  };
  try {
    const data = await apiRequest("/Usuario/Login", {
      method: "POST",
      body: JSON.stringify(body),
    });
    if (data.status == "500") {
      showToast("Usuário ou senha inválidos", "error");
      return;
    }
    localStorage.setItem("jwtToken", data.token);
    window.location.href = "index.html";
  } catch (err) {
    showToast("Falha no login: " + err.message, "error");
  }
}

async function registerUser(event) {
  event.preventDefault();
  const form = event.target;
  const body = {
    username: form.username.value,
    passwordHash: form.password.value,
  };
  try {
    await apiRequest("/Usuario/Registrar", {
      method: "POST",
      body: JSON.stringify(body),
    });
    showToast("Registro realizado! Agora faça login.", "success");
    setTimeout(() => {
      window.location.href = "login.html";
    }, 1500);
  } catch (err) {
    if (err.status === 409) {
      showToast(err.message, "error");
    } else {
      showToast("Falha no registro: " + err.message, "error");
    }
  }
}
