<template>
  <div id="app">
    <Toast />
    <ConfirmDialog />

    <div class="card">
      <h1>Gestiunea Mașinilor</h1>

      <Toolbar class="mb-4">
        <template #start>
          <div class="flex gap-2">
            <Button label="Adaugă Mașină" icon="pi pi-plus" @click="openNew" />
          </div>
        </template>
      </Toolbar>

      <DataTable
        :value="masiniStore.masini"
        :loading="masiniStore.loading"
        dataKey="id"
        showGridlines
        class="p-datatable-striped"
      >
        <Column field="id" header="ID" style="width: 80px"></Column>
        <Column field="marca" header="Marca" style="min-width: 120px"></Column>
        <Column field="model" header="Model" style="min-width: 120px"></Column>
        <Column field="an" header="An" style="min-width: 100px"></Column>
        <Column field="motor" header="Motor" style="min-width: 120px"></Column>
        <Column header="Acțiuni" style="width: 150px">
          <template #body="slotProps">
            <div class="flex gap-2">
              <Button
                icon="pi pi-pencil"
                size="small"
                @click="editMasina(slotProps.data)"
                severity="info"
              />
              <Button
                icon="pi pi-trash"
                size="small"
                @click="confirmDeleteMasina(slotProps.data)"
                severity="danger"
              />
            </div>
          </template>
        </Column>
      </DataTable>

      <!-- Dialog pentru adăugare/editare -->
      <Dialog
        v-model:visible="masinaDialog"
        :style="{ width: '500px' }"
        :header="editMode ? 'Editează Mașină' : 'Adaugă Mașină'"
        :modal="true"
        :draggable="false"
        :resizable="false"
        :closable="true"
        :closeOnEscape="true"
        class="masina-dialog"
      >
        <div class="flex flex-col gap-4">
          <div class="flex flex-col gap-2">
            <label for="marca">Marca</label>
            <InputText
              id="marca"
              v-model="masina.marca"
              :class="{ 'p-invalid': submitted && !masina.marca }"
              required
            />
            <small v-if="submitted && !masina.marca" class="p-error"
              >Marca este obligatorie.</small
            >
          </div>

          <div class="flex flex-col gap-2">
            <label for="model">Model</label>
            <InputText
              id="model"
              v-model="masina.model"
              :class="{ 'p-invalid': submitted && !masina.model }"
              required
            />
            <small v-if="submitted && !masina.model" class="p-error"
              >Modelul este obligatoriu.</small
            >
          </div>

          <div class="flex flex-col gap-2">
            <label for="an">An</label>
            <InputNumber
              id="an"
              v-model="masina.an"
              :class="{
                'p-invalid':
                  submitted &&
                  (!masina.an || masina.an < 1900 || masina.an > 2025),
              }"
              :min="1900"
              :max="2025"
              :useGrouping="false"
              :minFractionDigits="0"
              :maxFractionDigits="0"
              mode="decimal"
              required
            />
            <small
              v-if="
                submitted &&
                (!masina.an || masina.an < 1900 || masina.an > 2025)
              "
              class="p-error"
            >
              Anul trebuie să fie între 1900 și 2025.
            </small>
          </div>

          <div class="flex flex-col gap-2">
            <label for="motor">Motor</label>
            <InputText
              id="motor"
              v-model="masina.motor"
              :class="{ 'p-invalid': submitted && !masina.motor }"
              required
            />
            <small v-if="submitted && !masina.motor" class="p-error"
              >Motorul este obligatoriu.</small
            >
          </div>
        </div>

        <template #footer>
          <Button
            label="Anulează"
            icon="pi pi-times"
            @click="hideDialog"
            severity="secondary"
            text
            class="p-button-text"
          />
          <Button
            label="Salvează"
            icon="pi pi-check"
            @click="saveMasina"
            :loading="masiniStore.loading"
            :disabled="masiniStore.loading"
            class="p-button-primary"
          />
        </template>
      </Dialog>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useConfirm } from "primevue/useconfirm";
import { useToast } from "primevue/usetoast";
import { useMasiniStore, type Masina } from "./stores/masiniStore";

const masiniStore = useMasiniStore();
const confirm = useConfirm();
const toast = useToast();

const masinaDialog = ref(false);
const editMode = ref(false);
const submitted = ref(false);

const masina = ref<Omit<Masina, "id"> & { id?: number }>({
  marca: "",
  model: "",
  an: 2024,
  motor: "",
});

const openNew = () => {
  masina.value = {
    marca: "",
    model: "",
    an: 2024,
    motor: "",
  };
  editMode.value = false;
  submitted.value = false;
  masinaDialog.value = true;
};

const editMasina = (masinaData: Masina) => {
  masina.value = { ...masinaData };
  editMode.value = true;
  submitted.value = false;
  masinaDialog.value = true;
};

const hideDialog = () => {
  masinaDialog.value = false;
  submitted.value = false;
};

const saveMasina = async () => {
  submitted.value = true;

  if (
    !masina.value.marca ||
    !masina.value.model ||
    !masina.value.motor ||
    !masina.value.an ||
    masina.value.an < 1900 ||
    masina.value.an > 2025
  ) {
    return;
  }

  try {
    if (editMode.value && masina.value.id) {
      await masiniStore.updateMasina(masina.value.id, {
        marca: masina.value.marca,
        model: masina.value.model,
        an: masina.value.an,
        motor: masina.value.motor,
      });
      toast.add({
        severity: "success",
        summary: "Succes",
        detail: "Mașina a fost actualizată",
        life: 3000,
      });
    } else {
      await masiniStore.addMasina({
        marca: masina.value.marca,
        model: masina.value.model,
        an: masina.value.an,
        motor: masina.value.motor,
      });
      toast.add({
        severity: "success",
        summary: "Succes",
        detail: "Mașina a fost adăugată",
        life: 3000,
      });
    }

    hideDialog();
  } catch (error) {
    toast.add({
      severity: "error",
      summary: "Eroare",
      detail: "A apărut o eroare la salvare",
      life: 3000,
    });
  }
};

const confirmDeleteMasina = (masinaData: Masina) => {
  confirm.require({
    message: `Ești sigur că vrei să ștergi mașina ${masinaData.marca} ${masinaData.model}?`,
    header: "Confirmare Ștergere",
    icon: "pi pi-exclamation-triangle",
    acceptClass: "p-button-danger",
    acceptLabel: "Da",
    rejectLabel: "Nu",
    accept: async () => {
      try {
        await masiniStore.deleteMasina(masinaData.id);
        toast.add({
          severity: "success",
          summary: "Succes",
          detail: "Mașina a fost ștearsă",
          life: 3000,
        });
      } catch (error) {
        toast.add({
          severity: "error",
          summary: "Eroare",
          detail: "A apărut o eroare la ștergere",
          life: 3000,
        });
      }
    },
  });
};

onMounted(() => {
  console.log("App mounted, loading masini...");
  masiniStore.getAllMasini().then(() => {
    console.log("Masini loaded:", masiniStore.masini);
  });
});
</script>

<style scoped>
#app {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.card {
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  padding: 2rem;
}

h1 {
  color: #333;
  margin-bottom: 2rem;
  text-align: center;
}

.flex {
  display: flex;
}

.flex-col {
  flex-direction: column;
}

.gap-2 {
  gap: 0.5rem;
}

.gap-4 {
  gap: 1rem;
}

.mb-4 {
  margin-bottom: 1rem;
}

.p-error {
  color: #e24c4c;
  font-size: 0.875rem;
}

.p-invalid {
  border-color: #e24c4c !important;
}
</style>
