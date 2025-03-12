import {
  createSelector,
  createAsyncThunk,
  createSlice,
} from "@reduxjs/toolkit";
import { RootState } from "../../../app/redux/store";
import { entityService } from "../../../services/api/entityService";
import { API_ENDPOINTS } from "../../../services/api/config";

type InterventionState = {
  files: InterventionFile[];
  totalPages: number;
  totalCount: number;
  currentPage: number;
  isLoading: boolean;
  error: string | null;
};

const initialState: InterventionState = {
  files: [],
  totalPages: 0,
  totalCount: 0,
  currentPage: 1,
  isLoading: false,
  error: null,
};

type FetchParams = {
  pageNumber: number;
  pageSize?: number;
  technicianName?: string;
  clientName?: string;
  interventionDate?: string;
};

export const fetchInterventions = createAsyncThunk(
  "intervention/fetchAll",
  async (params: FetchParams) => {
    const queryParams = new URLSearchParams();
    Object.entries(params).forEach(([key, value]) => {
      if (value) queryParams.append(key, value.toString());
    });

    console.log(queryParams.toString());

    const response = await entityService.get<PaginatedInterventionResponse>(
      `${API_ENDPOINTS.interventions}?${queryParams.toString()}`
    );
    return response.data;
  }
);

export const interventionSlice = createSlice({
  name: "intervention",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchInterventions.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchInterventions.fulfilled, (state, action) => {
        state.isLoading = false;
        state.files = action.payload.items;
        state.totalPages = action.payload.totalPages;
        state.totalCount = action.payload.totalCount;
        state.currentPage = action.payload.pageNumber;
        state.error = null;
      })
      .addCase(fetchInterventions.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || "Failed to fetch interventions";
      });
  },
});

// Selectors
const selectInterventionState = (state: RootState) => state.intervention;

export const selectInterventions = createSelector(
  [selectInterventionState],
  (intervention) => intervention.files
);

export const selectInterventionPagination = createSelector(
  [selectInterventionState],
  (intervention) => ({
    currentPage: intervention.currentPage,
    totalPages: intervention.totalPages,
    totalCount: intervention.totalCount,
  })
);

export const selectInterventionLoading = createSelector(
  [selectInterventionState],
  (intervention) => intervention.isLoading
);

export const interventionReducer = interventionSlice.reducer;
