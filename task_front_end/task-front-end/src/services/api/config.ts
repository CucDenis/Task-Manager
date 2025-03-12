import axios from "axios";
import { store } from "../../app/redux/store";
import { logoutThunk } from "../../features/auth/slice/userSlice";

export const API_URL = import.meta.env.VITE_API_URL;
export const API_TIMEOUT = Number(import.meta.env.VITE_API_TIMEOUT);

export const API_ENDPOINTS = {
  auth: "/auth",
  interventions: "/interventions",
  technicians: "/technicians",
} as const;

export const api = axios.create({
  baseURL: API_URL,
  timeout: API_TIMEOUT,
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      await store.dispatch(logoutThunk());
    }
    return Promise.reject(error);
  }
);
