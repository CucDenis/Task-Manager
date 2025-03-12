import {
  createAsyncThunk,
  createSlice,
  createSelector,
  PayloadAction,
} from "@reduxjs/toolkit";
import { entityService } from "../../../services/api/entityService";
import { API_ENDPOINTS } from "../../../services/api/config";
import { RootState } from "../../../app/redux/store";
import { logoutThunk } from "./userSlice";

type AuthStatusResponse = {
  isAuthenticated: boolean;
};

type AuthState = {
  isLoading: boolean;
  error: string | null;
  isAuthenticated: boolean;
};

export const checkAuthStatus = createAsyncThunk(
  "auth/checkStatus",
  async () => {
    try {
      const response = await entityService.get<AuthStatusResponse>(
        API_ENDPOINTS.auth + "/check-auth"
      );

      return response.data;
    } catch (error) {
      console.error("Auth check error:", error);
      throw error; // Properly throw the error to trigger rejected case
    }
  }
);

const initialState: AuthState = {
  isLoading: false,
  error: null,
  isAuthenticated: false,
};

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(logoutThunk.fulfilled, (state) => {
        state.isAuthenticated = false;
      })
      .addCase(checkAuthStatus.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(
        checkAuthStatus.fulfilled,
        (state, action: PayloadAction<AuthStatusResponse>) => {
          state.isAuthenticated = action.payload.isAuthenticated ?? false;
          state.isLoading = false;
          state.error = null;
        }
      )
      .addCase(checkAuthStatus.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || "Failed to check authentication";
      });
  },
});

// Selectors
const selectAuthState = (state: RootState) => state.auth;

export const selectAuthIsLoading = createSelector(
  [selectAuthState],
  (auth) => auth.isLoading
);

export const selectAuthError = createSelector(
  [selectAuthState],
  (auth) => auth.error
);

export const selectIsAuthenticated = createSelector(
  [selectAuthState],
  (auth) => auth.isAuthenticated
);

// Reducer
export const authReducer = authSlice.reducer;
