declare type PaginatedResponse<T> = {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
};

declare type ApiResponse<T> = {
  data: T;
  message?: string;
  status: number;
};
