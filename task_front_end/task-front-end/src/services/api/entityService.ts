import { api } from "./config";
import { ApiResponse, PaginatedResponse } from "./types";

export interface QueryParams {
  page?: number;
  pageSize?: number;
  search?: string;
  [key: string]: number | string | undefined;
}

class EntityService {
  async get<T>(endpoint: string): Promise<ApiResponse<T>> {
    const response = await api.get(endpoint);
    return response;
  }

  async getPaginated<T>(
    endpoint: string,
    params?: QueryParams
  ): Promise<ApiResponse<PaginatedResponse<T>>> {
    const response = await api.get(endpoint, { params });
    return response;
  }

  async getById<T>(
    endpoint: string,
    id: number | string
  ): Promise<ApiResponse<T>> {
    const response = await api.get(`${endpoint}/${id}`);
    return response.data;
  }

  async create<T, D = Omit<T, "id">>(
    endpoint: string,
    data: D
  ): Promise<ApiResponse<T>> {
    const response = await api.post(endpoint, data);
    return response;
  }

  async update<T>(
    endpoint: string,
    id: number | string,
    data: Partial<T>
  ): Promise<ApiResponse<T>> {
    const response = await api.put(`${endpoint}/${id}`, data);
    return response.data;
  }

  async delete(
    endpoint: string,
    id: number | string
  ): Promise<ApiResponse<void>> {
    const response = await api.delete(`${endpoint}/${id}`);
    return response;
  }
}

export const entityService = new EntityService();
