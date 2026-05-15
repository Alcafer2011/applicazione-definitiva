const cfg = {
  orchestratorBase: "http://localhost:5102",
  crmBase: "http://localhost:5103",
  twinBase: "http://localhost:5105",
  govBase: "http://localhost:5104"
};

function selectPanel(panelId) {
  document.querySelectorAll(".bos-panel").forEach(p => p.classList.remove("active"));
  document.querySelectorAll(".nav-btn").forEach(b => b.classList.remove("active"));
  const panel = document.getElementById("panel-" + panelId);
  if (panel) panel.classList.add("active");
  const btn = document.querySelector('.nav-btn[data-panel="' + panelId + '"]');
  if (btn) btn.classList.add("active");
}

document.querySelectorAll(".nav-btn").forEach(btn => {
  btn.addEventListener("click", () => {
    const panel = btn.getAttribute("data-panel");
    selectPanel(panel);
  });
});

// === DIGITAL BRAIN ===
const brainTenant = document.getElementById("brain-tenant");
const brainQuestion = document.getElementById("brain-question");
const brainSend = document.getElementById("brain-send");
const brainOutput = document.getElementById("brain-output");

brainSend.addEventListener("click", async () => {
  const tenantId = brainTenant.value || "default";
  const question = brainQuestion.value || "Stato azienda?";

  brainOutput.textContent = "Invio richiesta al Brain...";

  try {
    const res = await fetch(cfg.orchestratorBase + "/api/brain/query", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ tenantId: tenantId, question: question })
    });
    const data = await res.json();
    brainOutput.textContent = JSON.stringify(data, null, 2);
  } catch (err) {
    brainOutput.textContent = "Errore: " + err;
  }
});

// === CRM: COMPANY ===
const crmCompanyName = document.getElementById("crm-company-name");
const crmCompanyVat = document.getElementById("crm-company-vat");
const crmCompanyAddress = document.getElementById("crm-company-address");
const crmCompanyCreate = document.getElementById("crm-company-create");
const crmCompanyLoad = document.getElementById("crm-company-load");
const crmCompanyList = document.getElementById("crm-company-list");

crmCompanyCreate.addEventListener("click", async () => {
  const payload = {
    name: crmCompanyName.value,
    vatNumber: crmCompanyVat.value,
    address: crmCompanyAddress.value
  };
  try {
    const res = await fetch(cfg.crmBase + "/api/company", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });
    const data = await res.json();
    crmCompanyList.textContent = "Creata azienda:\n" + JSON.stringify(data, null, 2);
  } catch (err) {
    crmCompanyList.textContent = "Errore: " + err;
  }
});

crmCompanyLoad.addEventListener("click", async () => {
  try {
    const res = await fetch(cfg.crmBase + "/api/company");
    const data = await res.json();
    crmCompanyList.textContent = JSON.stringify(data, null, 2);
  } catch (err) {
    crmCompanyList.textContent = "Errore: " + err;
  }
});

// === CRM: CUSTOMER ===
const crmCustomerCompanyId = document.getElementById("crm-customer-companyid");
const crmCustomerName = document.getElementById("crm-customer-name");
const crmCustomerEmail = document.getElementById("crm-customer-email");
const crmCustomerPhone = document.getElementById("crm-customer-phone");
const crmCustomerCreate = document.getElementById("crm-customer-create");
const crmCustomerLoad = document.getElementById("crm-customer-load");
const crmCustomerList = document.getElementById("crm-customer-list");

crmCustomerCreate.addEventListener("click", async () => {
  const payload = {
    companyId: crmCustomerCompanyId.value,
    fullName: crmCustomerName.value,
    email: crmCustomerEmail.value,
    phone: crmCustomerPhone.value
  };
  try {
    const res = await fetch(cfg.crmBase + "/api/customer", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });
    const data = await res.json();
    crmCustomerList.textContent = "Creato cliente:\n" + JSON.stringify(data, null, 2);
  } catch (err) {
    crmCustomerList.textContent = "Errore: " + err;
  }
});

crmCustomerLoad.addEventListener("click", async () => {
  try {
    const res = await fetch(cfg.crmBase + "/api/customer");
    const data = await res.json();
    crmCustomerList.textContent = JSON.stringify(data, null, 2);
  } catch (err) {
    crmCustomerList.textContent = "Errore: " + err;
  }
});

// === TWIN: NODES ===
const twinNodeName = document.getElementById("twin-node-name");
const twinNodeType = document.getElementById("twin-node-type");
const twinNodeCreate = document.getElementById("twin-node-create");
const twinNodeLoad = document.getElementById("twin-node-load");
const twinNodeList = document.getElementById("twin-node-list");

twinNodeCreate.addEventListener("click", async () => {
  const payload = {
    name: twinNodeName.value,
    type: twinNodeType.value,
    status: "Healthy"
  };
  try {
    const res = await fetch(cfg.twinBase + "/api/twin", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });
    const data = await res.json();
    twinNodeList.textContent = "Nodo registrato:\n" + JSON.stringify(data, null, 2);
  } catch (err) {
    twinNodeList.textContent = "Errore: " + err;
  }
});

twinNodeLoad.addEventListener("click", async () => {
  try {
    const res = await fetch(cfg.twinBase + "/api/twin");
    const data = await res.json();
    twinNodeList.textContent = JSON.stringify(data, null, 2);
  } catch (err) {
    twinNodeList.textContent = "Errore: " + err;
  }
});

// === TWIN: ALERTS ===
const twinAlertNodeId = document.getElementById("twin-alert-nodeid");
const twinAlertSeverity = document.getElementById("twin-alert-severity");
const twinAlertMessage = document.getElementById("twin-alert-message");
const twinAlertCreate = document.getElementById("twin-alert-create");
const twinAlertLoad = document.getElementById("twin-alert-load");
const twinAlertList = document.getElementById("twin-alert-list");

twinAlertCreate.addEventListener("click", async () => {
  const payload = {
    nodeId: twinAlertNodeId.value,
    severity: twinAlertSeverity.value || "Info",
    message: twinAlertMessage.value
  };
  try {
    const res = await fetch(cfg.twinBase + "/api/alerts", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });
    const data = await res.json();
    twinAlertList.textContent = "Allarme creato:\n" + JSON.stringify(data, null, 2);
  } catch (err) {
    twinAlertList.textContent = "Errore: " + err;
  }
});

twinAlertLoad.addEventListener("click", async () => {
  try {
    const res = await fetch(cfg.twinBase + "/api/alerts");
    const data = await res.json();
    twinAlertList.textContent = JSON.stringify(data, null, 2);
  } catch (err) {
    twinAlertList.textContent = "Errore: " + err;
  }
});

// === GOVERNANCE: USERS ===
const govUserUsername = document.getElementById("gov-user-username");
const govUserDisplay = document.getElementById("gov-user-display");
const govUserRoleId = document.getElementById("gov-user-roleid");
const govUserCreate = document.getElementById("gov-user-create");
const govUserLoad = document.getElementById("gov-user-load");
const govUserList = document.getElementById("gov-user-list");

govUserCreate.addEventListener("click", async () => {
  const payload = {
    username: govUserUsername.value,
    displayName: govUserDisplay.value,
    roleId: govUserRoleId.value
  };
  try {
    const res = await fetch(cfg.govBase + "/api/user", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });
    const data = await res.json();
    govUserList.textContent = "Utente creato:\n" + JSON.stringify(data, null, 2);
  } catch (err) {
    govUserList.textContent = "Errore: " + err;
  }
});

govUserLoad.addEventListener("click", async () => {
  try {
    const res = await fetch(cfg.govBase + "/api/user");
    const data = await res.json();
    govUserList.textContent = JSON.stringify(data, null, 2);
  } catch (err) {
    govUserList.textContent = "Errore: " + err;
  }
});

// === GOVERNANCE: ROLES ===
const govRoleName = document.getElementById("gov-role-name");
const govRolePerms = document.getElementById("gov-role-perms");
const govRoleCreate = document.getElementById("gov-role-create");
const govRoleLoad = document.getElementById("gov-role-load");
const govRoleList = document.getElementById("gov-role-list");

govRoleCreate.addEventListener("click", async () => {
  const perms = (govRolePerms.value || "").split(",").map(p => p.trim()).filter(p => p.length > 0);
  const payload = {
    name: govRoleName.value,
    permissions: perms
  };
  try {
    const res = await fetch(cfg.govBase + "/api/role", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });
    const data = await res.json();
    govRoleList.textContent = "Ruolo creato:\n" + JSON.stringify(data, null, 2);
  } catch (err) {
    govRoleList.textContent = "Errore: " + err;
  }
});

govRoleLoad.addEventListener("click", async () => {
  try {
    const res = await fetch(cfg.govBase + "/api/role");
    const data = await res.json();
    govRoleList.textContent = JSON.stringify(data, null, 2);
  } catch (err) {
    govRoleList.textContent = "Errore: " + err;
  }
});

// pannello di default
selectPanel("brain");



