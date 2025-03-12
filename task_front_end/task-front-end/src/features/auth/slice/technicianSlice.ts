import {
  createAsyncThunk,
  createSlice,
  createSelector,
} from "@reduxjs/toolkit";
import { entityService } from "../../../services/api/entityService";
import { API_ENDPOINTS } from "../../../services/api/config";
import { RootState } from "../../../app/redux/store";

type TechnicianState = {
  technicians: Technician[];
  isLoading: boolean;
  error: string | null;
};

export const getAllTechnicians = createAsyncThunk(
  "technician/getAll",
  async () => {
    try {
      const response = await entityService.get<Technician[]>(
        API_ENDPOINTS.technicians // This will now use /auth/technicians
      );
      console.log("Technicians response:", response.data); // Add debug logging
      return response.data;
    } catch (error) {
      console.error("Failed to fetch technicians:", error);
      throw error;
    }
  }
);

const initialState: TechnicianState = {
  technicians: [],
  isLoading: false,
  error: null,
};

export const technicianSlice = createSlice({
  name: "technician",
  initialState,
  reducers: {
    clearTechnicians: (state) => {
      state.technicians = [];
      state.isLoading = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getAllTechnicians.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(getAllTechnicians.fulfilled, (state, action) => {
        state.isLoading = false;
        state.technicians = action.payload;
      })
      .addCase(getAllTechnicians.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || "Failed to fetch technicians";
      });
  },
});

// Selectors
const selectTechnicianState = (state: RootState) => state.technician;

export const selectTechnicians = createSelector(
  [selectTechnicianState],
  (technician) => technician.technicians
);

export const selectTechnicianIsLoading = createSelector(
  [selectTechnicianState],
  (technician) => technician.isLoading
);

export const selectTechnicianError = createSelector(
  [selectTechnicianState],
  (technician) => technician.error
);

export const { clearTechnicians } = technicianSlice.actions;
export const technicianReducer = technicianSlice.reducer;
