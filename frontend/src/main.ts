import { createApp } from "vue";
import { createPinia } from "pinia";
import PrimeVue from "primevue/config";
import "./style.css";
import App from "./App.vue";

// PrimeVue Components
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Dialog from "primevue/dialog";
import Toolbar from "primevue/toolbar";
import ConfirmDialog from "primevue/confirmdialog";
import Toast from "primevue/toast";
import ConfirmationService from "primevue/confirmationservice";
import ToastService from "primevue/toastservice";

const app = createApp(App);
const pinia = createPinia();

app.use(pinia);
app.use(PrimeVue);
app.use(ConfirmationService);
app.use(ToastService);

// Register PrimeVue components
app.component("Button", Button);
app.component("InputText", InputText);
app.component("InputNumber", InputNumber);
app.component("DataTable", DataTable);
app.component("Column", Column);
app.component("Dialog", Dialog);
app.component("Toolbar", Toolbar);
app.component("ConfirmDialog", ConfirmDialog);
app.component("Toast", Toast);

app.mount("#app");
