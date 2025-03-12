declare type InterventionFileListContextData = {
  currentPage: number;
  setCurrentPage: (page: number) => void;
  search: string;
  setSearch: (search: string) => void;
  filters: {
    technician: string;
    customer: string;
    date: string;
  };
  setFilters: (filters: {
    technician: string;
    customer: string;
    date: string;
  }) => void;
  interventions: InterventionFile[];
  totalPages: number;
  selectedIntervention: InterventionFile | null;
  setSelectedIntervention: (intervention: InterventionFile | null) => void;
  handleNewIntervention: () => void;
  handleRowClick: (intervention: InterventionFile) => void;
  handleUpdateIntervention: (intervention: InterventionFile) => void;
  isLoading: boolean;
  searchFilters: {
    technicianName: string;
    clientName: string;
    interventionDate: string;
  };
  handleSearch: (
    key: "technicianName" | "clientName" | "interventionDate",
    value: string
  ) => void;
};
