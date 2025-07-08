import { defineStore } from "pinia";
import axios from "axios";

export interface Masina {
  id: number;
  marca: string;
  model: string;
  an: number;
  motor: string;
}

export const useMasiniStore = defineStore("masini", {
  state: () => ({
    masini: [] as Masina[],
    loading: false,
    error: null as string | null,
  }),

  actions: {
    async getAllMasini() {
      this.loading = true;
      this.error = null;
      try {
        const response = await axios.get("http://localhost:5272/api/masini");
        console.log("Fetch response:", response.data);
        // Extract the actual array from the API response structure
        if (response.data.success && Array.isArray(response.data.result)) {
          this.masini = response.data.result;
        } else {
          this.masini = [];
          console.error("Invalid response structure:", response.data);
        }
      } catch (error: any) {
        this.error = error.message || "Eroare la încărcarea mașinilor";
        console.error("Error fetching masini:", error);
        this.masini = [];
      } finally {
        this.loading = false;
      }
    },

    async addMasina(masina: Omit<Masina, "id">) {
      this.loading = true;
      this.error = null;
      try {
        const response = await axios.post(
          "http://localhost:5272/api/masini",
          masina
        );
        console.log("Add response:", response.data);
        // Extract the actual masina object from the API response
        if (response.data.success && response.data.result) {
          const newMasina = response.data.result;
          this.masini.push(newMasina);
          return newMasina;
        } else {
          throw new Error(response.data.message || "Eroare la adăugare");
        }
      } catch (error: any) {
        this.error = error.message || "Eroare la adăugarea mașinii";
        console.error("Error adding masina:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async updateMasina(id: number, masina: Omit<Masina, "id">) {
      this.loading = true;
      this.error = null;
      try {
        const response = await axios.put(
          `http://localhost:5272/api/masini/${id}`,
          { ...masina, id }
        );
        // Extract the actual masina object from the API response
        const updatedMasina = response.data.result;
        const index = this.masini.findIndex((m) => m.id === id);
        if (index !== -1) {
          this.masini[index] = updatedMasina;
        }
        // Refresh the entire list to ensure consistency
        await this.getAllMasini();
        return updatedMasina;
      } catch (error: any) {
        this.error = error.message || "Eroare la actualizarea mașinii";
        console.error("Error updating masina:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async deleteMasina(id: number) {
      this.loading = true;
      this.error = null;
      try {
        await axios.delete(`http://localhost:5272/api/masini/${id}`);
        this.masini = this.masini.filter((m) => m.id !== id);
      } catch (error: any) {
        this.error = error.message || "Eroare la ștergerea mașinii";
        console.error("Error deleting masina:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },
  },
});
