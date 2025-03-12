declare type PaginatedInterventionResponse = {
  items: InterventionFile[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
};
