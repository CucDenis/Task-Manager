import {
  createAsyncThunk,
  createSelector,
  createSlice,
} from "@reduxjs/toolkit";
import { API_ENDPOINTS } from "../../../services/api/config";
import { entityService } from "../../../services/api/entityService";
import { RootState } from "../../../app/redux/store";

type LoginCredentials = {
  email: string;
  password: string;
  userType: string;
};

type User = {
  email: string;
  fullName: string;
};

type UserState = {
  user: User;
  isLoading: boolean;
  error: string | null;
};

export const loginThunk = createAsyncThunk(
  "user/login",
  async (credentials: LoginCredentials) => {
    try {
      const response = await entityService.create<User, LoginCredentials>(
        API_ENDPOINTS.auth + "/login",
        credentials
      );

      return response.data;
    } catch (error) {
      console.error("Login error:", error);
      throw error;
    }
  }
);

export const logoutThunk = createAsyncThunk("user/logout", async () => {
  await entityService.create(API_ENDPOINTS.auth + "/logout", {});
});

const initialState: UserState = {
  user: {
    email: "",
    fullName: "",
  },
  isLoading: false,
  error: null,
};

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    clearState: (state) => {
      state.user = initialState.user;
      state.isLoading = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginThunk.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(loginThunk.fulfilled, (state, action) => {
        state.isLoading = false;
        state.user = action.payload;
      })
      .addCase(loginThunk.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || "Login failed";
      })
      .addCase(logoutThunk.fulfilled, (state) => {
        userSlice.caseReducers.clearState(state);
      });
  },
});

// Selectors
const selectUserState = (state: RootState) => state.user;

export const selectUser = createSelector(
  [selectUserState],
  (user) => user.user
);

export const selectAuthIsLoading = createSelector(
  [selectUserState],
  (user) => user.isLoading
);

export const selectAuthError = createSelector(
  [selectUserState],
  (user) => user.error
);

// Reducer
export const userReducer = userSlice.reducer;
export const { clearState } = userSlice.actions;
